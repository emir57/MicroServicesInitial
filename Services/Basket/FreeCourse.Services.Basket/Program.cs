using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.Audience = "resource_basket";
        options.RequireHttpsMetadata = false;
    });
#endregion

#region Services
builder.Services.AddScoped<IBasketService, BasketService>();
#endregion

#region HttpContextAccessor
builder.Services.AddHttpContextAccessor();
#endregion

#region SharedIdentityService
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
#endregion

#region Redis Settings
builder.Services.AddSingleton<RedisSettings>(x =>
    builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>());
#endregion

#region RedisService
builder.Services.AddSingleton<RedisService>(sp =>
{
    RedisService redisService = new(sp.GetRequiredService<RedisSettings>());
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
