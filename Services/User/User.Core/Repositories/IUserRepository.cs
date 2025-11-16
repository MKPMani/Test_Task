using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUser(Guid id);        
        Task<Users> CreateUser(Users product);       
    }
}
