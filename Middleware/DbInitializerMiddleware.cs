using Lab4.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Lab4.Middleware
{
    public class DbInitializerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public Task Invoke(HttpContext context, Db8011Context dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DBInitializer.Initialize(dbContext);
                context.Session.SetString("starting", "Yes");
            }

            // Call the next delegate/middleware in the pipeline
            return _next.Invoke(context);
        }
    }
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }

    }
}
