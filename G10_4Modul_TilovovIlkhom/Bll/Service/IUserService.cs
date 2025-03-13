using Dal.Entites;

namespace Bll.Service
{
    public interface IUserService
    {
        Task<long> AddUser(User user);
        Task DeleteUser(long Id);
        Task<User> GetUserByID(long ID);
        Task UpdateUser(User user);
        Task<List<User>> GetAllUser();
    }
}