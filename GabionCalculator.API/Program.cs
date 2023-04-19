using GabionCalculator.API.Middleware;
using GabionCalculator.BAL;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>(options => options.MakeOptionsIdentity()).AddEntityFrameworkStores<ApplicationContext>(); ;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{ 
    var services = scope.ServiceProvider;
    try
    {
        await services.InitializeRoleAsync();
    }
    catch (Exception ex) 
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
