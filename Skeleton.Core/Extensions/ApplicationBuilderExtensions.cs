using Skeleton.Core.Result;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Skeleton.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;

                        ErrorDto errorDto = null;

                        if (ex is Exception)
                        {
                            errorDto = new ErrorDto(ex.Message, true);
                        }
                        else
                        {
                            errorDto = new ErrorDto(ex.Message, false);
                        }

                        var response = Result<NoDataDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
