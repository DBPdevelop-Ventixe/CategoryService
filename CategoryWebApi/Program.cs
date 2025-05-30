using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using CategoryWebApi.Data;
using CategoryWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// KeyVault configuration
var keyVaultUri = new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/");

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        keyVaultUri,
        new DefaultAzureCredential(),
        new AzureKeyVaultConfigurationOptions()
        {
            Manager = new CustomSecretManager("Ventixe"),
            ReloadInterval = TimeSpan.FromMinutes(5)
        }
    );
}

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddGrpc();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection")));

var app = builder.Build();

app.MapOpenApi();
app.UseCors(x =>
{
    x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<CategoryService>();
app.MapGet("/", () => "gRPC Category Service is up and running :)");
app.Run();
