global using Serilog;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeSharper.APIPortal.AuthenticationService.Implements;
using WeSharper.APIPortal.AuthenticationService.Interfaces;
using WeSharper.APIPortal.AuthenticationService.Middlewares;
using WeSharper.APIPortal.Middleware;
using WeSharper.BusinessesManagement.Implements;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Implements;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/server.txt").CreateLogger();

// Add services to the container.
var key = builder.Configuration["Token:Key"];
var connectionString = builder.Configuration.GetConnectionString("Reference2DB");

builder.Services.AddSingleton<IAccessTokenManager, AccessTokenManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();

//Identity Role
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<WeSharperContext>();

//Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<WeSharperContext>(options =>
        options.UseSqlServer(connectionString));
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//         .AddEntityFrameworkStores<WeSharperContext>();

builder.Services.AddScoped<IProfileManagementDL, ProfileManagementDL>();
builder.Services.AddScoped<IHobbyManagementDL, HobbyManagementDL>();
builder.Services.AddScoped<IUserPostManagementDL, UserPostManagementDL>();
builder.Services.AddScoped<IFriendManagementDL, FriendManagementDL>();
builder.Services.AddScoped<IGroupManagementDL, GroupManagementDL>();

builder.Services.AddScoped<IProfileManagementBL, ProfileManagementBL>();
builder.Services.AddScoped<IHobbyManagementBL, HobbyManagementBL>();
builder.Services.AddScoped<IUserPostManagementBL, UserPostManagementBL>();
builder.Services.AddScoped<IFriendManagementBL, FriendManagementBL>();
builder.Services.AddScoped<IGroupManagementBL, GroupManagementBL>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
// builder.Services.AddControllers().AddNewtonsoftJson(options =>
//     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
// );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Apply middlewares
app.UseTokenManagerMiddleware();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
