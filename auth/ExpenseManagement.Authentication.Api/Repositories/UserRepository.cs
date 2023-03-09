using ExpenseManagement.Authentication.Api.Models;

namespace ExpenseManagement.Authentication.Api.Repositories
{
    public class UserRepository
    {
        public static User Get(string email, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Email= "empresa-teste@gmail.com", Username = "Empresa Teste", Password = "123", Role = "system" });
            users.Add(new User { Id = 1234, Email="john.doe@gmail.com", Username = "Marcelo", Password = "123", Role = "client" });
            return users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
