namespace Api.TaskEndpoints;

public record CreateTaskResult(int Id, string Title, string? Description, string? Type, string? Status, DateTime? DeadlineOn);
