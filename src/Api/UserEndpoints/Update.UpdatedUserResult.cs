namespace Api.UserEndpoints;

public record UpdatedUserResult(int Id, string? Login, string? Password, string? Email, string? PhoneNumber, string? FirstName, string? LastName);
