using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class CustomJWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public CustomJWTMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        
        Console.WriteLine("Token extracted from Authorization header: " + token);

        if (token != null)
        {
            AttachUserToContext(context, token);
        }
        else
        {
            Console.WriteLine("No token found in the request.");
        }

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            
            var key = Encoding.ASCII.GetBytes("your_secret_key_here");
            Console.WriteLine("Secret key loaded for validation.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero 
            };

            Console.WriteLine("Validating token...");
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            
            Console.WriteLine("Token is valid. Attaching claims to HttpContext.");
            context.User = principal;
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error occurred during token validation: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
