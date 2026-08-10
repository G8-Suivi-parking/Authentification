namespace BimaTech.Parking.Auth;

public record LoginRequest(string Email, string Password);

public record LoginResponse(string Token, string Email, DateTime ExpiresAt);

public record ErrorResponse(string Message);
