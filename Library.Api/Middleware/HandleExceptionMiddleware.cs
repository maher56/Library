using Library.Domain.Base;
using Library.Domain.Enum;
using Microsoft.Extensions.Localization;
using Resturant.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Library.Api.Middleware
{
    public class HandleExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.NotFound, e.Message);
            }

            catch (InternalServerException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, e.Message);
            }
            catch (BadRequestException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, e.Message);
            }

            catch (UnAuthorizedException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.Unauthorized, e.Message);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, ErrorKey.InternalServerError.ToString());
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int httpStatusCode, string message)
        {
            if (!context.Response.HasStarted)
            {
                var stringLocalizer = context.RequestServices.GetService<IStringLocalizer<ErrorResource>>();
                var result = JsonSerializer.Serialize(new
                {
                    errorMessage = stringLocalizer[message].Value,
                });
                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }
}