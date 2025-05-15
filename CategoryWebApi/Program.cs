using CategoryWebApi.Data;
using CategoryWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddGrpc();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CategoryConnection")));

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
