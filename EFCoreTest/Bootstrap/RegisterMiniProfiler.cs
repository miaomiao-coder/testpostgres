namespace EFCoreTest.Bootstrap
{
    public static class RegisterMiniProfiler
    {
        public static IServiceCollection RegisterProfiler(this IServiceCollection services)
        {
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                options.TrackConnectionOpenClose = true;

                // (Optional) Use something other than the "light" color scheme.
                // (defaults to "light")
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;

                // The below are newer options, available in .NET Core 3.0 and above:

                // (Optional) You can disable MVC filter profiling
                // (defaults to true, and filters are profiled)
                options.EnableMvcFilterProfiling = true;
                // ...or only save filters that take over a certain millisecond duration (including their children)
                // (defaults to null, and all filters are profiled)
                // options.MvcFilterMinimumSaveMs = 1.0m;

                // (Optional) You can disable MVC view profiling
                // (defaults to true, and views are profiled)
                options.EnableMvcViewProfiling = true;
                // ...or only save views that take over a certain millisecond duration (including their children)
                // (defaults to null, and all views are profiled)
                // options.MvcViewMinimumSaveMs = 1.0m;

            });
            return services;
        }


        public static WebApplication UseProfiler(this WebApplication app)
        {
            app.UseMiniProfiler();
            return app;
        }
    }
}
