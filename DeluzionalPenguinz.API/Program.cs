using DeluzionalPenguinz.API.Services;
using DeluzionalPenguinz.API.Services.IServices;
using DeluzionalPenguinz.DataAccess;
using DeluzionalPenguinz.DataAccess.Models;
using DeluzionalPenguinz.DataAccess.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<AnouncementsRepository>();
builder.Services.AddScoped<AnouncementsDataService>();

builder.Services.AddScoped<CourseDataService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
 {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddScoped<ApplicationDbContext, ApplicationDbContext>();

builder.Services.AddIdentity<Human, IdentityRole>(e =>
{
    e.Password.RequireUppercase = false;
    e.Password.RequiredLength = 1;
    e.Password.RequiredUniqueChars = 0;
    e.Password.RequireDigit = false;
    e.Password.RequireNonAlphanumeric = false;
    e.Password.RequireLowercase = false;


})
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    //options.Lockout.MaxFailedAccessAttempts = 4;
});

builder.Services.AddControllers();


//builder.Services.AddScoped<IDBSeeder, DBSeeder>();

builder.Services.AddCors(
              options => options.AddPolicy("DeluzionalPenguinz", builder =>
              {
                  //builder.SetIsOriginAllowed(e => e == "https://deluzionalpenguinz.com").AllowAnyMethod().AllowAnyHeader();
                  //builder.WithOrigins("deluzionalpenguinz.com", "https://deluzionalpenguinz.com", "https://deluzionalpenguinz.com/").AllowAnyMethod().AllowAnyHeader();
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              }));




builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
     .AddJwtBearer(opt =>
     {
         opt.RequireHttpsMetadata = true;
         opt.SaveToken = true;
         opt.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings")["Token"])),
             ValidateAudience = false,
             ValidateIssuer = false,
             ClockSkew = TimeSpan.Zero

         };
     });


var app = builder.Build();



// Configure the HTTP request pipeline.




app.UseHttpsRedirection();



app.UseRouting();

app.UseCors("DeluzionalPenguinz");

app.UseAuthentication();

app.UseAuthorization();

//dBSeeder.SeedDatabase();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


// Run the server

app.Run();
