using PORTIMAGES.Common.Helpers;

namespace PORTIMAGES.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebLayer(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "UserScheme";
                options.DefaultChallengeScheme = "UserScheme";
            })
            .AddCookie("UserScheme", options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict; 
                options.Cookie.SecurePolicy = env.IsDevelopment()? CookieSecurePolicy.SameAsRequest: CookieSecurePolicy.Always;

            })
            .AddCookie("StaffScheme", options =>
            {
                options.LoginPath = "/StaffLogin";
                options.LogoutPath = "/StaffLogout";
                options.AccessDeniedPath = "/StaffLogout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax; 
                options.Cookie.SecurePolicy = env.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
            });
            services.AddSingleton<FileHelper>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                return new FileHelper(env.WebRootPath);
            });
            return services;
        }
    }
}
