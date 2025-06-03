namespace Api.UserEndpoints;

public record UserListResult(int Id, string? Email, string? PhoneNumber, string? FirstName, string? LastName, byte[]? ImageStored, DateTime? LastActiveOn);
