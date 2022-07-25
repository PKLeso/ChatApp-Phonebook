using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PhoneBook.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuth(this IServiceCollection services, WebApplicationBuilder config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(opt =>
             {
                 opt.TokenValidationParameters = new TokenValidationParameters
                 {
                     //Token is going to be valid if:
                     ValidateIssuer = true, // - the Issuer is the actual server, that created the token.
                     ValidateAudience = true, // - the receiver of the token is the valid recipient.
                     ValidateLifetime = true, // - if the token has not expired.
                     ValidateIssuerSigningKey = true, // the signing key is valid and is trusted by the server.

                     ValidIssuer = config.Configuration["JwtOptions:Issuer"],
                     ValidAudience = config.Configuration["JwtOptions:Audiance"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Configuration["JwtOptions:key"])) // For testing I will have the key in the configurations, but ideally it has to be stored in the environment variables
                 };
             });
        }
    }
}
