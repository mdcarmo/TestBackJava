using ExpenseManagement.Api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">service</param>
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna os gastos efetuados no cartão de crédito dado o código do cliente.
        /// </summary>
        /// <param name="usercode">Código do cliente</param>
        /// <returns>Listagem de gastos efetuados no cartão do cliente.</returns>
        [HttpGet("getall")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAll();

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        /// <summary>
        /// Retorna os gastos efetuados no cartão de crédito dado o código do cliente.
        /// </summary>
        /// <param name="usercode">Código do cliente</param>
        /// <returns>Listagem de gastos efetuados no cartão do cliente.</returns>
        [HttpGet("getbyfilter/{search}")]
        [Authorize(Roles = "client")]
        public IActionResult GetByFilter(string search)
        {
            var categories = _service.GetByFilter(search);

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }
    }
}
