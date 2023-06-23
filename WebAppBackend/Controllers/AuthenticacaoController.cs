using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppBackend.Model;
using WebAppBackend.Services.Interfaces;

namespace WebAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticacaoController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthenticacaoController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login is null)
                return Unauthorized(); ;

            var usuario = _loginService.Login(login.NomeLogin, login.Password);

            if (usuario.Id > 0)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var credenciais = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(10), signingCredentials: credenciais);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new JWTTokenResponse { Token = tokenString });
            }
            else
            {
                return Ok(usuario);
            }
            
        }
    }
}
