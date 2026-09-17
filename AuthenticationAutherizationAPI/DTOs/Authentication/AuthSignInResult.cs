namespace AuthenticationAutherizationAPI.DTOs.Authentication;

public abstract record AuthSignInResult
{
    public sealed record Success(AuthenticationResponse Tokens) : AuthSignInResult;
    public sealed record RequiresMfa(string PendingMfaToken) : AuthSignInResult;
    public sealed record Failed(string Reason) : AuthSignInResult;
    public sealed record LockedOut(string Reason) : AuthSignInResult;
}