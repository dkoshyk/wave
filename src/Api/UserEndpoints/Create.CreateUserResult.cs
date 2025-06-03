namespace Api.UserEndpoints;

public record CreateUserResult(int Id, string Login, string Password, string? Email, string? PhoneNumber, string? FirstName, string? LastName);
