using simple_blog_api_dot_net.Data;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Models;
using simple_blog_api_dot_net.Services.Contracts;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace simple_blog_api_dot_net.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public UserService(AppDbContext dbContext, IOptions<JwtSettings> jwtOptions)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<User> RegisterAsync(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                throw new Exception("O e-mail já está em uso.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<LoginResponse> LoginAsync(UserLoginRequest request)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
                throw new Exception("Credenciais inválidas.");

            bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!valid)
            {
                throw new Exception("Credenciais inválidas.");
            }
                
            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
            };
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}