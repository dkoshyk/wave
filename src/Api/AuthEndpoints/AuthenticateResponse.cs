namespace Api.AuthEndpoints;

public record AuthenticateResponse
{
    public bool Result { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
