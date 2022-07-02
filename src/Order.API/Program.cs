using MassTransit;
using Rabbit.MQ.Core.MessageContract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busRegConfig =>
{
    busRegConfig.AddRequestClient(typeof(CheckOrderStatus), TimeSpan.FromSeconds(10));
    busRegConfig.UsingRabbitMq((ctx, config) =>
        {
            config.Host(builder.Configuration.GetConnectionString("RabbitMq"));

            config.ConfigureEndpoints(ctx);
        });
});

builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure(options => options.WaitUntilStarted = true);

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
