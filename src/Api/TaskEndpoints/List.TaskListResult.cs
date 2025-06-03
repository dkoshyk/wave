namespace Api.TaskEndpoints;

public record TaskListResult
{
    public List<TaskItemDto> Items { get; set; } = new();
    public int Count { get; set; }
}
