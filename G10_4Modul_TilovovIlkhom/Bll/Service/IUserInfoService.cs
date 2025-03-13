using Dal.Entites;

namespace Bll.Service
{
    public interface IUserInfoService
    {
        Task<long> AddUserInfo(UserInfo userInfo);
        Task DeleteUserInfo(long Id);
        Task<UserInfo> GetUserInfByID(long ID);
        Task UpdateUserInfo(UserInfo userInfo);
        Task<List<UserInfo>> GetAllUserInfos();
    }
}