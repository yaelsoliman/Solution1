using Application.Exceptions;
using Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                var response=context.Response;
                response.ContentType = "application/json";
                Error error = new();
                switch (ex)
                {
                    case CustomValidationException validationEx:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        error.FriendlyMessage = validationEx.FriendlyErrorMessage;
                        error.ErrorMessages = validationEx.ErrorMessages;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.FriendlyMessage=ex.Message;
                        break;
                }
                var result=JsonSerializer.Serialize(error);
                await response.WriteAsync(result);
            }
        }
    }
}
