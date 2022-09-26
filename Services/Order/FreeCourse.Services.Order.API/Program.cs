using FreeCourse.Services.Order.Application.Features.Commands.CreateOrder;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MediatR
builder.Services.AddMediatR(typeof(CreateOrderCommand));
#endregion

#region ContextAccessor and SharedIdentity
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddHttpContextAccessor();
#endregion

#region AddDbContext
builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), configure =>
{
    configure.MigrationsAssembly("FreeCourse.Services.Order.Infrastructure");
}));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
