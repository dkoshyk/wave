namespace ApplicationCore;

public record class User : BaseEntity
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public byte[]? ImageStored { get; set; }
    public DateTime? LastActiveOn { get; set; }

    public ICollection<TaskItem>? Tasks { get; set; }
}
