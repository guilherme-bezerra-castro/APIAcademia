using APIAcademia.Models.Auth;
using APIAcademia.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAcademia.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request.Email != "admin@academia.com" ||
                request.Senha != "senha123")
            {
                return Unauthorized(new { mensagem = "Credenciais inválidas." });
            }

            var (token, expiracao) = _tokenService.GerarToken(request.Email);
            return Ok(new LoginResponse { Token = token, Expiracao = expiracao });
        }
    }
}