using Microsoft.Extensions.DependencyInjection;

namespace Skeleton.Core.Infrastructure
{
    public interface IDrawModule
    {
        void Load(IServiceCollection services);
    }
}
