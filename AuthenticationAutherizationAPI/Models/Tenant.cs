namespace AuthenticationAutherizationAPI.Models;

public class Tenant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TenantKey { get; set; } = string.Empty;

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}