using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoWrapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UniversitySystemSummer.Core.Modules;
using UniversitySystemSummer.Data;
using UniversitySystemSummer.Data.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<RepositoryModule>();
    container.RegisterModule<ServiceModule>();
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiResponseAndExceptionWrapper();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();