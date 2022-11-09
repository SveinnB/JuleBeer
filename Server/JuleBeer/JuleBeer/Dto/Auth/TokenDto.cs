namespace JuleBeer.Dto.Auth;

public class TokenDto
{
    public string Token { get; set; }
    public bool IsTokenValid { get; set; }
    public DateTime Expires { get; set; }
}
