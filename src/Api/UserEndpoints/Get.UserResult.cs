namespace Api.UserEndpoints;

public record UserResult(int Id, string Login, string Password, string? Email, string? PhoneNumber, string? FirstName, string? LastName, byte[]? ImageStored, DateTime? LastActiveOn);
