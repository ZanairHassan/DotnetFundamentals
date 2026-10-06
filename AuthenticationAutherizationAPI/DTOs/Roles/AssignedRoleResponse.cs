namespace AuthenticationAutherizationAPI.DTOs.Roles;

public class AssignedRoleResponse
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int AssignedUsersCount { get; set; }
}
