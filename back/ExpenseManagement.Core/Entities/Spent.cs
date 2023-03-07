namespace ExpenseManagement.Core.Entities
{
    public  class Spent: EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        /// <param name="postedAt"></param>
        /// <param name="category"></param>
        public Spent(long codeUser, string description, double value, DateTime postedAt, string category)
        {
            CodeUser = codeUser;
            Description = description;
            Value = value;
            PostedAt = postedAt;
            Category = category;
        }

        public long CodeUser { get; private set; }
        public string? Description { get; private set; }
        public double Value { get; private set; }
        public DateTime PostedAt { get; private set; }
        public string? Category { get; set; }
    }
}
