using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseStrings"));
builder.Services.AddSingleton<DatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddSingleton<IDatabaseSettings>(x =>
    builder.Configuration.GetSection("DatabaseStrings").Get<DatabaseSettings>());
#endregion

#region Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
#endregion

#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.Audience = "resource_catalog";
        options.RequireHttpsMetadata = false;
    });
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(CategoryDto));
#endregion

#region MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});
#endregion

var app = builder.Build();

#region Insert Categories
using (var scope = builder.Services.BuildServiceProvider())
{
    var categoryService = scope.GetRequiredService<ICategoryService>();
    if (categoryService.GetAllAsync().GetAwaiter().GetResult().Data.Any() == false)
    {
        categoryService.CreateAsync(new CategoryDto { Name = "Asp.Net" }).GetAwaiter().GetResult();
        categoryService.CreateAsync(new CategoryDto { Name = "Java" }).GetAwaiter().GetResult();
    }
}
#endregion

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
