using Microsoft.AspNetCore.Builder;

namespace GSCBase.Application.Exceptions.Logs
{
    public static class AppBuilderConfiguracoesExtension
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}