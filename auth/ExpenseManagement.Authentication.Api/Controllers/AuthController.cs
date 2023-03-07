using ExpenseManagement.Authentication.Api.Dto;
using ExpenseManagement.Authentication.Api.Models;
using ExpenseManagement.Authentication.Api.Repositories;
using ExpenseManagement.Authentication.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Authentication.Api.Controllers
{
    [ApiController]
    [Route("api/expense-management-authentication")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDto dto)
        {
            // Recupera o usuário
            var user = UserRepository.Get(dto.Username, dto.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
