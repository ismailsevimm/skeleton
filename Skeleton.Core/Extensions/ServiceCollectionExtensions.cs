using Skeleton.Core.Infrastructure;
using Skeleton.Core.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Skeleton.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var response = Result<NoContentResult>.Fail(errorDto, 400);

                    return new OkObjectResult(response);
                };
            });
        }

        public static IServiceCollection AddDependecyResolvers(this IServiceCollection services, IDrawModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }

            return ServiceTool.Create(services);
        }
    }
}
