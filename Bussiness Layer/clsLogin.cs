using Data_Access_Layer.Users;

namespace Bussiness_Layer
{
    public class clsLogin
    {
        public static int IsExist(string UserName, string Password)
        {
            return (clsUserData.IsUserExist(UserName, Password));
        }
    }
}
