using GabionCalculator.API.Middleware;
using GabionCalculator.BAL;
using GabionCalculator.BAL.Services.JwtFeatures;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

string MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
builder.Services.AddDbContext(connection);
builder.Services.AddIdentity<User, IdentityRole>(options => options.MakeOptionsIdentity()).AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddAuthenticationJWT(jwtSettings);
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
    
//app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/StaticFiles"
}); ;

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();   

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();

