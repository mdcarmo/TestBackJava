using ExpenseManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.Api.Application.Dto
{
    public class SpentDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        /// <param name="postedAt"></param>
        /// <param name="category"></param>
        public SpentDto(long codeUser, string description, double value, DateTime postedAt, string category)
        {
            CodeUser = codeUser;
            Description = description;
            Value = value;
            PostedAt = postedAt;
            Category = category;
        }

        //public Guid Id { get; set; }
        public string Id { get; set; }
        public long CodeUser { get; private set; }

        [Required(ErrorMessage = "Descricao é obrigatoria.")]
        public string? Description { get; private set; }
        public double Value { get; private set; }
        public DateTime PostedAt { get; private set; }
        public string Category { get; private set; }


        /// <summary>
        /// Transforma uma entidade em dto
        /// </summary>
        /// <param name="entity">entidade</param>
        /// <returns>SpentDto</returns>
        public static SpentDto FromEntity(Spent entity)
        {
            SpentDto dto =  new SpentDto(
                entity.CodeUser,
                entity.Description,
                entity.Value,
                entity.PostedAt,
                entity.Category
             );

            dto.Id = entity.Id;
            return dto;
        }

        /// <summary>
        /// Transforma um dto em entidade
        /// </summary>
        /// <param name="cardSpending">entidade</param>
        /// <returns>Spent</returns>
        public static Spent FromDto(SpentDto dto)
        {
            return new Spent(
                dto.CodeUser,
                dto.Description,
                dto.Value,
                dto.PostedAt,
                dto.Category
             );
        }
    }
}
