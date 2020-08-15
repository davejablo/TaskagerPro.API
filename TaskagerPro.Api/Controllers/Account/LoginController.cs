using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Account;
using TaskagerPro.Core.Models;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepository _accountService;
        private readonly JwtSettingsModel _jwtSettings;

        public LoginController(IAccountRepository accountService, JwtSettingsModel jwtSettings)
        {
            _accountService = accountService ?? 
                throw new ArgumentNullException(nameof(accountService));

            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            try
            {
                var tokenModel = await _accountService.LoginAsync(model);
                var token = GenerateJwtToken(tokenModel);

                return Ok(new { token });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        private string GenerateJwtToken(TokenModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim("username", model.Username),
                new Claim("account_type", model.AccountType),
                new Claim("account_type_id", model.AccountTypeId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
