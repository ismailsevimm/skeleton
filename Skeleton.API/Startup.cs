using Skeleton.Core.Extensions;
using Skeleton.Core.Infrastructure;
using Skeleton.Core.Module;
using Skeleton.Data.Module;
using Skeleton.Services.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Skeleton.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddDependecyResolvers(new IDrawModule[]
            {
                new CoreModule(),
                new DataModule(Configuration),
                new ServiceModule(Configuration),

            });

            services.AddControllers();

            services.UseCustomValidationResponse();

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

                // These will be replaced by DictionaryTKeyEnumTValueSchemaFilter, but are needed to avoid an error.
                // You will need one for every kind of Dictionary<,> you have.
                //c.MapType<Dictionary<Currency, List<string>>>(() => new OpenApiSchema());
                //c.SchemaFilter<DictionaryTKeyEnumTValueSchemaFilter>();

                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Skeleton.API", Version = "v1", Description = "Access Token Authorization Gerektiren Endpoint'lerde Http Header'a Authorization Key'i ile " + "\'Bearer access_token\'" + " þeklinde gönderilir." });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomException();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skeleton.API v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
