using BimaTech.Parking.Data;
using Microsoft.EntityFrameworkCore;

namespace BimaTech.Parking.Auth;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(string email, string password);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(ApplicationDbContext dbContext, IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse?> LoginAsync(string email, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            return null;
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return null;
        }

        var (token, expiresAt) = _jwtTokenService.GenerateToken(user);

        return new LoginResponse(token, user.Email, expiresAt);
    }
}
