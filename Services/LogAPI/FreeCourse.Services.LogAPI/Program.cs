using FreeCourse.Services.LogAPI;
using FreeCourse.Services.LogAPI.Consumers;
using FreeCourse.Services.LogAPI.Extensions;
using FreeCourse.Shared.CrossCuttingConcerns;
using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using FreeCourse.Shared.CrossCuttingConcerns.Serilog.Loggers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"];
        options.Audience = "resource_api";
        options.RequireHttpsMetadata = false;
    });
#endregion

#region MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LogEventConsumer>();
    x.AddConsumer<ExceptionLogEventConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMqUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");

            config.ReceiveEndpoint("log-service", cfg =>
            {
                cfg.ConfigureConsumer<LogEventConsumer>(context);
            });
            config.ReceiveEndpoint("exception-log-service", cfg =>
            {
                cfg.ConfigureConsumer<ExceptionLogEventConsumer>(context);
            });

        });
    });


});
#endregion

#region LoggerServiceBase

builder.Services.AddLogger(cfg =>
{
    cfg.Logger = new FileLogger(builder.Configuration);
});

#region Add Logger Type
//builder.Services.AddLogger(() => typeof(FileLogger));
#endregion

#region Add Logger Instance
//builder.Services.AddLogger(() => new FileLogger(builder.Configuration));
#endregion
//builder.Services.AddScoped<LoggerServiceBase, FileLogger>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
