using MassTransit;
using Order.API.Constants;
using Order.API.Consumer;
using Order.API.MessageContract;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    //x.SetKebabCaseEndpointNameFormatter();
    //x.SetInMemorySagaRepositoryProvider();

    //var entryAssembly = Assembly.GetEntryAssembly();
    //x.AddConsumers(entryAssembly);
    //x.AddSagaStateMachines(entryAssembly);
    //x.AddSagas(entryAssembly);
    //x.AddActivities(entryAssembly);

    //x.AddRequestClient<CheckOrderStatus>();
    x.AddConsumer<CheckOrderStatusConsumer>()
        .Endpoint(e =>
        {
            e.Name = ConsumersConstants.CheckOrderStatus;
            //e.InstanceId = Guid.NewGuid().ToString();
            e.ConfigureConsumeTopology = true;
        });

    x.UsingRabbitMq((ctx, config) =>
        {
            //config.Host(builder.Configuration.GetConnectionString("RabbitMq"));
            config.Host("localhost", "/", hostConfig =>
                {
                    hostConfig.Username("guest");
                    hostConfig.Password("guest");
                });

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
