namespace AuthenticationAutherizationAPI.DTOs.Authentication;

public class VerifyMfaRequest
{
    public string PendingMfaToken { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
} 