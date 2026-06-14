using MySql.EntityFrameworkCore.Extensions;
using SnapGuard.Hub.Components;
using SnapGuard.Hub.Configurations;
using SnapGuard.Hub.Services;
using SnapGuard.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo { Title = "SnapGuard API", Version = "v1" });
});

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

/* // Add SignalR and Response Compression Middleware
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
}); */

builder.Services.Configure<MqttSettings>(
    builder.Configuration.GetSection("Mqtt"));
builder.Services.Configure<PictureServerSettings>(
    builder.Configuration.GetSection("PictureServer"));

builder.Services.AddSingleton<IdGenerationService>();
builder.Services.AddSingleton<RequestCorrelationService>();
builder.Services.AddSingleton<CorrelationService>();
builder.Services.AddSingleton<IMqttClientService, MqttClientService>();
builder.Services.AddHostedService<MqttBackgroundService>();
builder.Services.AddMySQLServer<SnapGuardContext>(
    builder.Configuration.GetConnectionString("MySQL")!
);

var app = builder.Build();
//app.UseResponseCompression();
//app.MapHub<DeviceHub>("/deviceHub");

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SnapGuardContext>();
        await context.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<SnapGuardContext>>();
        logger.LogError(ex, "An error occurred creating the database");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnapGuard v1");
    });
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();
app.Run();
