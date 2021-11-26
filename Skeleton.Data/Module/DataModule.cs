using Skeleton.Core.Infrastructure;
using Skeleton.Data.Context;
using Skeleton.Data.Repositories;
using Skeleton.Data.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Skeleton.Data.Module
{
    public class DataModule : IDrawModule
    {
        private readonly IConfiguration Configuration;

        public DataModule(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void Load(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DrawDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Skeleton.Data");
                });
            });
        }
    }
}
