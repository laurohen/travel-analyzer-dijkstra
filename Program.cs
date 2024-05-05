using Agenda.Infrastructure.DataAccess;
using travel_analyzer_dijkstra.Infra.Repository;
using travel_analyzer_dijkstra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                           throw new InvalidOperationException("The connection string is null");
    return new SqlConnectionFactory(connectionString);
});

builder.Services.AddSingleton<IRotaRepository, RotaRepository>();
builder.Services.AddSingleton<IRotaService, RotaService>();
builder.Services.AddSingleton<IGrafoService, GrafoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
