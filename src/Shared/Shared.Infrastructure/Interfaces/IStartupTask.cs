using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Interfaces
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}