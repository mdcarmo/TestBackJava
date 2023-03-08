using ExpenseManagement.Api.Application.Dto;
using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;

namespace ExpenseManagement.Api.Application.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ExpenseManagementService : IExpenseManagementService
    {
        private readonly IExpenseManagementRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">repositório</param>
        public ExpenseManagementService(IExpenseManagementRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<string> AddAsync(SpentDto dto)
        {
            Spent spent = SpentDto.FromDto(dto);

            if (string.IsNullOrEmpty(spent.Category))
                spent.Category = await FindCategoryContentDescription(spent.CodeUser, spent.Description);
          
            await _repository.AddAsync(spent);

            return spent.Id.ToString();
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(string id, SpentDto spent)
        {
            var spentFromDb = await _repository.GetByIdAsync(id);
            spentFromDb.Category = spent.Category;

            return await _repository.UpdateAsync(id, spentFromDb);
        }

        /// <inheritdoc />
        public async Task<List<SpentDto>> GetByCodeAsync(long codeUser)
        {
            var listSpending = await _repository.GetByCodeAsync(codeUser);
            return MappingListEntitieToListDto(listSpending);
        }

        /// <inheritdoc />
        public async Task<SpentDto> GetByIdAsync(string id)
        {
            var spent = await _repository.GetByIdAsync(id);
            return SpentDto.FromEntity(spent);
        }

        /// <inheritdoc />
        public async Task<List<SpentDto>> GetByCodeAndDateAsync(long codeUser, DateTime dateFind)
        {
            var listSpending = await _repository.GetByCodeAndDateAsync(codeUser, dateFind);

            return MappingListEntitieToListDto(listSpending);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private async Task<string> FindCategoryContentDescription(long codeUser, string description)
        {
            //Funcionalidade: Categorização automatica de gasto
            //No processo de integração de gastos, a categoria deve ser incluida automaticamente
            //caso a descrição de um gasto seja igual a descrição de qualquer outro gasto já categorizado pelo cliente
            //o mesmo deve receber esta categoria no momento da inclusão do mesmo

            var spent = await _repository.GetCategoryContentDescriptionAsync(codeUser, description);

            if (spent != null)
                return spent.Category;

            return "Desconhecida";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listSpending"></param>
        /// <returns></returns>
        private static List<SpentDto> MappingListEntitieToListDto(List<Spent> listSpending)
        {
            List<SpentDto> listSpentDto = new List<SpentDto>();

            foreach (Spent spent in listSpending)
                listSpentDto.Add(SpentDto.FromEntity(spent));

            return listSpentDto;
        }
    }
}
