using project_service.Interfaces;
using project_service.Utils.Request;

namespace project_service.Services
{
    public class Authentication
    {
        private readonly IUserService _userService;
        public Authentication(IUserService userService)
        {
            _userService = userService;
        }

        public string RegisterUser(RegisterUser user) {
            return "kiwkiw";
        }
    }
}
