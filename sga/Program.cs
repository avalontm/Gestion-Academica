using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using sga;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    // maximum message size of 2MB
    options.MaximumReceiveMessageSize = 2000000;
});


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddBlazoredToast();
builder.Services.AddHttpClient();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


//DabaBase Manager
DBManager.Connect(app.Configuration);

//Iniciamos la aplicacion
app.Run();

public partial class Program
{
    public static IConfiguration? Configuration { private set; get; }
}