namespace Api.UserEndpoints;

public record CreateUserCommand(string Login, string Password, string? Email, string? PhoneNumber, string? FirstName, string? LastName);
