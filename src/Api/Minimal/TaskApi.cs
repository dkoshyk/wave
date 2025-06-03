using Api.TaskEndpoints;
using ApplicationCore;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.Minimal;

public static class TaskApi
{
    public static RouteGroupBuilder MapTaskApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/tasks").RequireAuthorization();

        group.MapGet("/", GetTasks);
        group.MapGet("/allowanonymous", GetTasksAllowAnonymous).AllowAnonymous();
        group.MapGet("/{id:int}", GetTask);
        group.MapPost("/", CreateTask);
        group.MapPut("/", UpdateTask);
        group.MapDelete("/{id:int}", DeleteTask);

        return group;
    }

    private static async Task<IResult> GetTasks([AsParameters] TaskListRequest request, HttpContext ctx, AppDbContext db, IMapper mapper)
    {
        int.TryParse(ctx.User.FindFirst("userId")?.Value, out var userId);
        if (request.PerPage == 0) request.PerPage = 10;
        if (request.Page == 0) request.Page = 1;

        IQueryable<TaskItem> query = db.Tasks;
        if (userId > 0)
            query = query.Where(x => x.OwnerId == userId);

        if (!string.IsNullOrEmpty(request.ContainsTitle))
            query = query.Where(x => x.Title.Contains(request.ContainsTitle));
        if (!string.IsNullOrEmpty(request.EqualType))
            query = query.Where(x => x.Type.Equals(request.EqualType, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(request.EqualStatus))
            query = query.Where(x => x.Type.Equals(request.EqualStatus, StringComparison.OrdinalIgnoreCase));
        if (request.EqualPriority.HasValue)
            query = query.Where(x => x.Priority == request.EqualPriority);
        if (request.FromDeadlineOn.HasValue)
            query = query.Where(x => x.DeadlineOn >= request.FromDeadlineOn);
        if (request.ToDeadlineOn.HasValue)
            query = query.Where(x => x.DeadlineOn <= request.ToDeadlineOn);

        var count = await query.CountAsync();
        var items = (await query.Skip(request.PerPage * (request.Page - 1))
                                .Take(request.PerPage)
                                .OrderByDescending(x => x.Id)
                                .ToListAsync())
                    .Select(x => mapper.Map<TaskItemDto>(x));

        var result = new TaskListResult { Count = count };
        result.Items.AddRange(items);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetTasksAllowAnonymous([AsParameters] TaskListRequest request, AppDbContext db, IMapper mapper)
    {
        if (request.PerPage == 0) request.PerPage = 10;
        if (request.Page == 0) request.Page = 1;

        IQueryable<TaskItem> query = db.Tasks;

        if (!string.IsNullOrEmpty(request.ContainsTitle))
            query = query.Where(x => x.Title.Contains(request.ContainsTitle));
        if (!string.IsNullOrEmpty(request.EqualType))
            query = query.Where(x => x.Type.Equals(request.EqualType, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(request.EqualStatus))
            query = query.Where(x => x.Type.Equals(request.EqualStatus, StringComparison.OrdinalIgnoreCase));
        if (request.EqualPriority.HasValue)
            query = query.Where(x => x.Priority == request.EqualPriority);
        if (request.FromDeadlineOn.HasValue)
            query = query.Where(x => x.DeadlineOn >= request.FromDeadlineOn);
        if (request.ToDeadlineOn.HasValue)
            query = query.Where(x => x.DeadlineOn <= request.ToDeadlineOn);

        var count = await query.CountAsync();
        var items = (await query.Skip(request.PerPage * (request.Page - 1))
                                .Take(request.PerPage)
                                .OrderByDescending(x => x.Id)
                                .ToListAsync())
                    .Select(x => mapper.Map<TaskItemDto>(x));

        var result = new TaskListResult { Count = count };
        result.Items.AddRange(items);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetTask(int id, AppDbContext db, IMapper mapper)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (task is null) return Results.NotFound();
        var result = mapper.Map<TaskResult>(task);
        return Results.Ok(result);
    }

    private static async Task<IResult> CreateTask(CreateTaskCommand request, HttpContext ctx, AppDbContext db, IMapper mapper)
    {
        int.TryParse(ctx.User.FindFirst("userId")?.Value, out var userId);
        var task = mapper.Map<TaskItem>(request);
        task.OwnerId = userId;
        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
        var result = mapper.Map<CreateTaskResult>(task);
        return Results.Created($"/api/tasks/{task.Id}", result);
    }

    private static async Task<IResult> UpdateTask(UpdateTaskCommand request, AppDbContext db, IMapper mapper)
    {
        var task = await db.Tasks.FindAsync(request.Id);
        if (task is null) return Results.NotFound();
        mapper.Map(request, task);
        db.Tasks.Update(task);
        await db.SaveChangesAsync();
        var result = mapper.Map<UpdatedTaskResult>(task);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteTask(int id, AppDbContext db)
    {
        var task = await db.Tasks.FindAsync(id);
        if (task is null) return Results.NotFound();
        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
}
