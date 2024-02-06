using AutoDoorLocking;
using Clay.Application.Services;
using Clay.Domain.Repositories;
using Clay.Domain.Services;
using Clay.Infrastructure.Data;
using Clay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ILockService, LockService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
