using MassTransit;
using Other.API.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busRegConfig =>
{
    busRegConfig.SetKebabCaseEndpointNameFormatter(); // consumer-name
    // busRegConfig.SetSnakeCaseEndpointNameFormatter(); // consumer_name

    busRegConfig.AddConsumer<CheckOrderStatusConsumer, CheckOrderStatusConsumerDefinition>();

    busRegConfig.UsingRabbitMq((ctx, config) =>
    {
        // config.ReceiveEndpoint(ConsumersConstants.CheckOrderStatus, e =>
        //     {
        //         e.ConfigureConsumer<CheckOrderStatusConsumer>(ctx);
        //     });

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