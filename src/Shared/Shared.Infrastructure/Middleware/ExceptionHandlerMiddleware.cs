using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Core.Domain;
using Shared.DTO;

namespace Shared.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                var apiError = new ApiError();
                switch (exception)
                {
                    case EntityNotFoundException e:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        apiError.Message = e.Message;
                        apiError.Errors = e.Errors;
                        break;
                    case DomainException e:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        apiError.Message = e.Message;
                        apiError.Errors = e.Errors;
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        apiError.Message = "Internal server error";
                        break;
                }

                apiError.Status = response.StatusCode;
                await response.WriteAsJsonAsync(apiError);
            }
        }
    }
}