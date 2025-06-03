namespace Api.TaskEndpoints;

public record UpdatedTaskResult(int Id, string Title, string? Description, string? Type, string? Status, int Priority, DateTime? DeadlineOn, DateTime? ClosedOn);
