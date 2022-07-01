using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq((ctx, config) =>
    {
        //config.Host(builder.Configuration.GetConnectionString("RabbitMq"));
        config.Host(
            "localhost",
            "/",
            hostConfig =>
        {
            hostConfig.Username("guest");
            hostConfig.Password("guest");
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
