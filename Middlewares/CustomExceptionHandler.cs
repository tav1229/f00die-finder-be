using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace f00die_finder_be.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var response = context.Response;
                    response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    var message = exception.Message;

                    response.StatusCode = exception switch
                    {
                        CustomException => (exception as CustomException).StatusCode,
                        _ => (int)HttpStatusCode.InternalServerError,
                    };

                    var isDevelopment = (app as WebApplication).Environment.IsDevelopment();

                    var responseMessage = new CustomResponse<object>
                    {
                        Error = new List<Error>
                        {
                            new Error
                            {
                                Message = message,
                                Detail = isDevelopment ? exception?.StackTrace : null
                            }
                        }
                    };

                    var result = JsonSerializer.Serialize(responseMessage);

                    await response.WriteAsync(result);


                });
            });
        }

    }
}