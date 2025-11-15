using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUser(string id);        
        Task<Users> CreateUser(Users product);       
    }
}
