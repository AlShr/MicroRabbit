using MicroRabbit.Domain.Bus;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TransferDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("TransferDbConnection"));
});

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Transfer Microservice", Version = "v1" });
});

builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

DependencyContainer.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(opt =>
{
  opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice V1");
});

app.UseAuthorization();

app.MapControllers();

static void ConfigureEventBus(IApplicationBuilder app)
{
  var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
  eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
}

ConfigureEventBus(app);

app.Run();