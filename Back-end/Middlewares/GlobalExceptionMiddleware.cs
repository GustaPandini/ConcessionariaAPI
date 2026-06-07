using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcessionariaAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;

            switch (exception)
            {
                case ArgumentException e:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = new { erro = e.Message };
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = new { erro = "Ocorreu um erro interno no servidor. Tente novamente mais tarde." };
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}