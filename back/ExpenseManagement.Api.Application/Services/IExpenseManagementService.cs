using ExpenseManagement.Api.Application.Dto;
using ExpenseManagement.Core.Entities;

namespace ExpenseManagement.Api.Application.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IExpenseManagementService
    {
        /// <summary>
        /// Adiciona uma despesa.
        /// </summary>
        /// <param name="viewModel">viewModel SpentDto</param>
        /// <returns>código da despesa</returns>
        Task<string> AddAsync(SpentDto dto);

        /// <summary>
        /// Recupera todas as despesas de um usuário dado seu código.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <returns>List SpentDto</returns>
        Task<List<SpentDto>> GetByCodeAsync(long codeUser);

        /// <summary>
        /// Recupera todos os gastos de um usuário baseado em um dia especifico.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <param name="dateFind">Data</param>
        /// <returns>List SpentDto</returns>
        Task<List<SpentDto>> GetByCodeAndDateAsync(long codeUser, DateTime dateFind);
    }
}
