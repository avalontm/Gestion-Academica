using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using sga;
using sga.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    // maximum message size of 2MB
    options.MaximumReceiveMessageSize = 2000000;
});


builder.Services.AddControllers();
builder.Services.AddBlazoredToast();
builder.Services.AddHttpClient();

builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddScoped<HubConnection>(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri("/notificaciones"))
      .WithAutomaticReconnect()
      .Build();
});


var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["JwtKey"]));
int SesionExpire = int.Parse(builder.Configuration["SesionExpire"]);

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()

    .AddAuthentication(auth =>
    {
        auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,

        };

    }).AddCookie();

builder.Services.ConfigureApplicationCookie(ops =>
{
    ops.ExpireTimeSpan = TimeSpan.FromDays(SesionExpire);
    ops.SlidingExpiration = true;
});

var app = builder.Build();

Program.Configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.MapBlazorHub();
app.MapHub<NotificacionesHub>("/notificaciones");
app.MapFallbackToPage("/_Host");

//DabaBase Manager
DBManager.Connect(app.Configuration);

//Iniciamos la aplicacion
app.Run();

public partial class Program
{
    public static IConfiguration? Configuration { private set; get; }
}