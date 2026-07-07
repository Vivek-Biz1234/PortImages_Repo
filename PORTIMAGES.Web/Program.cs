using PORTIMAGES.Infrastructure;
using PORTIMAGES.Application;
using PORTIMAGES.Web;
using System.Text.Json; 

var builder = WebApplication.CreateBuilder(args);

#region Added Manually

// Add logging to console + file
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile("Logs/myapp-{Date}.txt"); // Logs folder + daily rolling


 

// Add Application Layer's dependencies (MediatR, FluentValidation, etc.)
builder.Services.AddApplication();

// Add Infrastructure Layer's dependencies (Dapper, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Add Web Layer's dependencies
builder.Services.AddWebLayer(builder.Environment);
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", context =>
{
    //context.Response.Redirect("/Login");
    context.Response.Redirect("/StaffLogin");
    return Task.CompletedTask;
});

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AuthUser}/{action=Login}/{id?}");

app.Run();
