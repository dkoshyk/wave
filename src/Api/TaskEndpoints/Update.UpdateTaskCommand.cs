using System.ComponentModel.DataAnnotations;

namespace Api.TaskEndpoints;

public record UpdateTaskCommand
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public int Priority { get; set; }
    public DateTime? DeadlineOn { get; set; }
    public DateTime? ClosedOn { get; set; }
}
