using System.ComponentModel.DataAnnotations;

namespace Api.TaskEndpoints;

public record CreateTaskCommand(
    [property: Required, MaxLength(30)] string Title,
    string? Description,
    string? Type,
    string? Status,
    DateTime? DeadlineOn);
