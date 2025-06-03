using Api.AuthEndpoints;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Minimal;

public static class AuthApi
{
    public static void MapAuthApi(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/authenticate", AuthenticateUser).AllowAnonymous();
    }

    private static async Task<IResult> AuthenticateUser(AuthenticateRequest request, AppDbContext db)
    {
        var response = new AuthenticateResponse();
        var user = await db.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
        if (user is null || user.Password != request.Password)
        {
            return Results.Ok(response);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("userLogin", user.Login),
                new Claim("userId", user.Id.ToString())
            })
        };
        var jwt = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(jwt);

        response.Result = true;
        response.Login = user.Login;
        response.FullName = $"{user.FirstName} {user.LastName}";
        response.Token = token;

        return Results.Ok(response);
    }
}
