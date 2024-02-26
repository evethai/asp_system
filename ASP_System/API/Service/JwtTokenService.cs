using Domain.Entities;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Service
{
    public interface IJwtTokenService
    {
        //string CreateToken(UserSignInDTO user, List<Claim> roles);
        string CreateToken(ApplicationUser user, IList<String> roles);
        string CreateRefeshToken();
    }
    public class JwtTokenService : IJwtTokenService
    {
        private IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateRefeshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string CreateToken(ApplicationUser user, IList<String> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
