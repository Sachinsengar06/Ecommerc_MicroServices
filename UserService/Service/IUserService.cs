using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        UserModel GetUserById(string id);

    }
}
