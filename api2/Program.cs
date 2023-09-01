var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(option =>
{
    option.RouteTemplate = "api2/{documentName}/swagger.json";
});
app.UseSwaggerUI(option =>
{
    option.RoutePrefix = "api2";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
