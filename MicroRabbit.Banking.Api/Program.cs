using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BankingDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnection"));
});

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Banking Microservice", Version = "v1"});
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
  opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking Microservice V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
