using Api.UserEndpoints;
using ApplicationCore;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Minimal;

public static class UserApi
{
    public static RouteGroupBuilder MapUserApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users").RequireAuthorization();

        group.MapGet("/", GetUsers);
        group.MapGet("/{id:int}", GetUser);
        group.MapPost("/", CreateUser);
        group.MapPut("/", UpdateUser);
        group.MapDelete("/{id:int}", DeleteUser);

        return group;
    }

    private static async Task<IResult> GetUsers([AsParameters] UserListRequest request, AppDbContext db, IMapper mapper)
    {
        if (request.PerPage == 0) request.PerPage = 10;
        if (request.Page == 0) request.Page = 1;

        var query = db.Users.AsQueryable();
        var items = await query.Skip(request.PerPage * (request.Page - 1))
                               .Take(request.PerPage)
                               .ToListAsync();
        var result = items.Select(i => mapper.Map<UserListResult>(i));
        return Results.Ok(result);
    }

    private static async Task<IResult> GetUser(int id, AppDbContext db, IMapper mapper)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null) return Results.NotFound();
        var result = mapper.Map<UserResult>(user);
        return Results.Ok(result);
    }

    private static async Task<IResult> CreateUser(CreateUserCommand request, AppDbContext db, IMapper mapper)
    {
        var user = mapper.Map<User>(request);
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        var result = mapper.Map<CreateUserResult>(user);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateUser(UpdateUserCommand request, AppDbContext db, IMapper mapper)
    {
        var user = await db.Users.FindAsync(request.Id);
        if (user is null) return Results.NotFound();
        mapper.Map(request, user);
        db.Users.Update(user);
        await db.SaveChangesAsync();
        var result = mapper.Map<UpdatedUserResult>(user);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteUser(int id, AppDbContext db)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return Results.NotFound();
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
}
