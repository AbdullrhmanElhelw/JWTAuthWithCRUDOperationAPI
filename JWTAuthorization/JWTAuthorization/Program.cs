
using JWTAuthorization.BL;
using JWTAuthorization.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace JWTAuthorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Swagger Auth
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
            });
            #endregion


            #region Authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Roles.User), policy =>
                policy.RequireClaim(ClaimTypes.Role, nameof(Roles.User))); 
                
                options.AddPolicy(nameof(Roles.Admin), policy =>
                policy.RequireClaim(ClaimTypes.Role, nameof(Roles.Admin)));


            });

            #endregion

            #region DBContext and Identity

            builder.Services.AddDbContext<JwtDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("JwtDb"))
                );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
                .AddEntityFrameworkStores<JwtDbContext>();

            #endregion

            #region Repos

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            #region Managers
            builder.Services.AddScoped<ICategoryManager, CategoryManager>();
            builder.Services.AddScoped<IProductManager, ProductManager>();
            #endregion

            #region BearerDefaultAuthentecation

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Default";
                options.DefaultChallengeScheme = "Default";
            }
            )
                .AddJwtBearer("Default", options =>
                {
                    var Getkey = builder.Configuration.GetValue<string>("Key");
                    var secretKeyInBytes = Encoding.ASCII.GetBytes(Getkey);
                    var key = new SymmetricSecurityKey(secretKeyInBytes);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key
                    };
                });

            #endregion

            #region Swagger
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}