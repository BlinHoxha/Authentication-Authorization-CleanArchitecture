namespace DDD.Contracts.DTO.Authenticate;

public class AuthResult
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public List<string> Errors { get; set; }
}