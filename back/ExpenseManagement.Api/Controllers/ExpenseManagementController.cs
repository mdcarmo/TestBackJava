using ExpenseManagement.Api.Application.Dto;
using ExpenseManagement.Api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ExpenseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/expense-management")]
    public class ExpenseManagementController : ControllerBase
    {
        private readonly IExpenseManagementService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service">service</param>
        public ExpenseManagementController(IExpenseManagementService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel">viewModel</param>
        /// <returns>código da despesa cadastrada</returns>
        [HttpPost]
        [Authorize(Roles = "system")]
        public async Task<IActionResult> Post(SpentDto dto)
        {
            var codeSpent = await _service.AddAsync(dto);
            return Ok(codeSpent);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Update([FromRoute] string id, SpentDto dto)
        {
            var updateOK = await _service.UpdateAsync(id, dto);

            if(updateOK)
                return NoContent();

            return NotFound();
        }

        /// <summary>
        /// Retorna os gastos efetuados no cartão de crédito dado o código do cliente.
        /// </summary>
        /// <param name="usercode">Código do cliente</param>
        /// <returns>Listagem de gastos efetuados no cartão do cliente.</returns>
        [HttpGet("{usercode}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetByCodeAsync(long usercode)
        {
            /*
            Funcionalidade: Listagem de gastos*
            Dado que acesso como um cliente autenticado que pode visualizar os gastos do cartão
            Quando acesso a interface de listagem de gastos
            Então gostaria de ver meus gastos mais atuais.


            * Para esta funcionalidade é esperado 2.000 acessos por segundo.
            * O cliente espera ver gastos realizados a 5 segundos atrás.
            */

            var spents = await _service.GetByCodeAsync(usercode);

            if (spents == null)
                return NotFound();

            return Ok(spents);
        }

        /// <summary>
        /// Retorna os gastos efetuados no cartão de crédito dado o código do cliente.
        /// </summary>
        /// <param name="usercode">Código do cliente</param>
        /// <returns>Listagem de gastos efetuados no cartão do cliente.</returns>
        [HttpGet("getById/{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var spent = await _service.GetByIdAsync(id);

            if (spent == null)
                return NotFound();

            return Ok(spent);
        }

        /// <summary>
        /// Retorna os gastos efetuados no cartão de crédito dado o código do cliente.
        /// </summary>
        /// <param name="usercode">Código do cliente</param>
        /// <returns>Listagem de gastos efetuados no cartão do cliente.</returns>
        [HttpGet("filterByDate/{usercode}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetByCodeAndDateAsync(long usercode, string dateFind)
        {
            /*
            Funcionalidade: Filtro de gastos
            Dado que acesso como um cliente autenticado
            E acessei a interface de listagem de gastos
            E configure o filtro de data igual a 27/03/1992
            Então gostaria de ver meus gastos apenas deste dia.
            */

            var spents = await _service.GetByCodeAndDateAsync(usercode, Convert.ToDateTime(dateFind, CultureInfo.InvariantCulture));

            if (spents == null)
                return NotFound();

            return Ok(spents);
        }
    }
}
