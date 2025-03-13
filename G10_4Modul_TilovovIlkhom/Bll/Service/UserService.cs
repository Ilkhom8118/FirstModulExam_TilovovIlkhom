using Bll.Service;
using Dal;
using Dal.Entites;

namespace BII.Services;

public class UserService : IUserService
{
    private readonly MainContext _mainContext;
    public UserService(MainContext mainContext)
    {
        _mainContext = mainContext;
    }
    public async Task<long> AddUser(User user)
    {
        _mainContext.Users.Add(user);
        _mainContext.SaveChanges();
        return user.BotUserId;
    }

    public async Task DeleteUser(long Id)
    {
        var user = await GetUserByID(Id);
        _mainContext.Users.Remove(user);
        _mainContext.SaveChanges();
    }

    public async Task<List<User>> GetAllUser()
    {
        return _mainContext.Users.ToList();
    }

    public async Task<User> GetUserByID(long ID)
    {
        var user = _mainContext.Users.FirstOrDefault(u => u.TelegramUserId == ID);
        if (user == null)
        {
            throw new Exception("User Not Found");
        }
        return user;
    }

    public async Task UpdateUser(User user)
    {
        var userByID = await GetUserByID(user.TelegramUserId);
        userByID.BotUserId = user.BotUserId;
        userByID.UserInfo = user.UserInfo;
        userByID.TelegramUserId = user.TelegramUserId;
        userByID.IsBlocked = user.IsBlocked;
        userByID.CreatedAt = user.CreatedAt;
        userByID.UpdatedAt = user.UpdatedAt;
        _mainContext.SaveChanges();
    }
}
