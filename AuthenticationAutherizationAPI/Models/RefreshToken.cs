namespace AuthenticationAutherizationAPI.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public string TokenHash { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public virtual ApplicationUser User { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    public string? ReplacedByTokenHash { get; set; }

    public bool IsRevoked => RevokedAtUtc.HasValue;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    public bool IsActive => !IsRevoked && !IsExpired;
}