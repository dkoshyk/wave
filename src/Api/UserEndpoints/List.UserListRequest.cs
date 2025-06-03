namespace Api.UserEndpoints;

public record UserListRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}
