using System;
using System.Net;
using System.Threading.Tasks;
using Ardalis.SmartEnum;
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

                var apiError = ConvertToError(exception);

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = apiError.Status;

                await response.WriteAsJsonAsync(apiError);
            }
        }

        private ApiError ConvertToError(Exception exception)
        {
            return exception switch
            {
                EntityNotFoundException e => new ApiError { Message = e.Message, Status = (int) HttpStatusCode.NotFound },
                EntityNotValidException e => new ApiError { Message = e.Message, Status = (int) HttpStatusCode.BadRequest },
                DomainException e => new ApiError { Message = e.Message, Status = (int) HttpStatusCode.BadRequest },
                SmartEnumNotFoundException e => new ApiError { Message = e.Message, Status = (int) HttpStatusCode.BadRequest },
                _ => new ApiError { Message = "Internal server error", Status = (int) HttpStatusCode.InternalServerError }
            };
        }
    }
}