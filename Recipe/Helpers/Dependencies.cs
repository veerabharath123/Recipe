using Microsoft.Extensions.Configuration;

namespace Recipe.Helpers
{
    public static class Dependencies
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRequest, Request>();
            services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            //services.AddSingleton<IAppSettings, AppSettings>(e => Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>());
        }
    }
}
