using User.Core.Entities;
using User.Core.Repositories;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public AppDbContext _context { get; }
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<Users> IUserRepository.GetUser(string id)
        {
            var res = await _context.Users.Where(e=> e.Id.ToString() == id).FirstOrDefaultAsync();
            return res ?? new Users();
        }

        async Task<Users> IUserRepository.CreateUser(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
