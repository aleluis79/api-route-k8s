var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("API2", httpClient => {
    httpClient.BaseAddress = new Uri("http://api2-route:5000/api2/Home/");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(option =>
{
    option.RouteTemplate = "api/{documentName}/swagger.json";
});
app.UseSwaggerUI(option =>
{
    option.RoutePrefix = "api";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
