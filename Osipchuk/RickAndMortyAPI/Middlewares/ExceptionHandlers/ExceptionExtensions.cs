using RickAndMortyAPI.Middlewares;

namespace RickAndMortyAPI
{
    public static class ExceptionExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
