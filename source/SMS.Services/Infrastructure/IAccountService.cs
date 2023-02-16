using SMS.DTOs.DTOs;
using DTOUserInfo = SMS.DTOs.DTOs.UserInfo;


namespace SMS.Services.Infrastructure
{
    public interface IAccountService
    {
        UserInfo GetUserInfo(string userName);
        void UpdateUserInfo(DTOUserInfo userInfo);
    }
}
