using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region HttpContextAccessor
builder.Services.AddHttpContextAccessor();
#endregion

#region SharedIdentityService
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
#endregion

#region Redis Settings
builder.Services.AddSingleton(typeof(RedisSettings),
    x => builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>());
#endregion

#region RedisService
builder.Services.AddSingleton<RedisService>(sp =>
{
    RedisService redisService = sp.GetService<RedisService>();
    redisService.Connect();
    return redisService;
});
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
