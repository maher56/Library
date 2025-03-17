using Library.Application.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace Library.Api.Extensions
{
    public static class ApiServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token **_only_**",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.CustomSchemaIds(type =>
                {
                    if (type.Name is "Response" or "Request")
                    {
                        var path = type.FullName!.Replace("+", ".");
                        return path.Replace("Query", path.Split(".").Last()).Replace("Command", path.Split(".").Last()).Split(".")[path.Split(".").Length - 2];
                    }

                    if (type.IsGenericType)
                    {
                        var schema = type.GetGenericTypeDefinition().Name.Split('`').First();
                        var genericArgument = type.GetGenericArguments().Last().FullName!.Replace("+", ".").Replace("Query", "");
                        return $"{schema}.{genericArgument.Split(".").ElementAt(genericArgument.Split(".").Length - 2)}.{genericArgument.Split(".").Last()}";
                    }
                    if (type.Name.Contains("Dto"))
                    {
                        return type.FullName!.Split(".").Last().Replace("+", ".").Replace("Query", "").Replace("Command", "");
                    }
                    return type.Name;
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            return services;
        }
        public static IServiceCollection AddJwtService(this IServiceCollection services, string jwtKey)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateActor = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
            return services;
        }
    }
}
