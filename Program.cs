using Microsoft.EntityFrameworkCore;
using MemoryPalaceApp.Data;
using MemoryPalaceApp.Services;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Configure data protection for reverse proxy scenarios
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data/keys"))
    .SetApplicationName("MemoryPalaceApp");

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.CallbackPath = "/callback";
});


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();

builder.Services.AddDbContext<MemoryPalaceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<MemoryPalaceService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<TokenProvider>();

var app = builder.Build();

// Configure forwarded headers for reverse proxy (MUST be before authentication)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost,
    ForwardLimit = 1,
    RequireHeaderSymmetry = false,
    KnownProxies = { },
    KnownNetworks = { }
});

// Force HTTPS for Auth0 callback URL generation
app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next();
});

// Configure cookies for HTTPS behind reverse proxy
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.SameAsRequest
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // HSTS and HTTPS redirection handled by NGINX reverse proxy
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MemoryPalaceContext>();
    context.Database.EnsureCreated();
}

app.Run();