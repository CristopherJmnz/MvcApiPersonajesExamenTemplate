using Amazon.S3;
using MvcApiPersonajesExamenTemplate.Helpers;
using MvcApiPersonajesExamenTemplate.Models;
using MvcApiPersonajesExamenTemplate.Services;
using Newtonsoft.Json;


async Task<string> GetSecretAsync()
{
    return await HelperSecretManager.GetSecretAsync();
}

var builder = WebApplication.CreateBuilder(args);
string secret = GetSecretAsync().GetAwaiter().GetResult();

KeysModel keys = JsonConvert.DeserializeObject<KeysModel>(secret);
string connectionString = keys.ConnectionString;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<PersonajesService>();
builder.Services.AddSingleton<KeysModel>(x => keys);
builder.Services.AddTransient<ServiceStorageS3>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Personajes}/{action=Index}/{id?}");

app.Run();
