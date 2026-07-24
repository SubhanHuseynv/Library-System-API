using LibrarySystem.Application.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace LibrarySystem.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
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
               await ExceptionHandler(context, ex);
            }
        }

        private static Task ExceptionHandler(HttpContext context,Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = ex switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,
                NotFoundException => (int)HttpStatusCode.NotFound,
                ConflictException => (int)HttpStatusCode.Conflict,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                ForbiddenException => (int)HttpStatusCode.Forbidden,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCodes= context.Response.StatusCode,
                Messages = ex.Message,
                Header = "Error recieved"
            }));
        }
    }
}
