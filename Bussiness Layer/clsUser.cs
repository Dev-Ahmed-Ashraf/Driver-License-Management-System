using Data_Access_Layer;
using Data_Access_Layer.Users;
using System;
using System.Data;

namespace Bussiness_Layer
{
    public class clsUser
    {
        private enum _enMode { AddNewMode = 0, UpdateMode = 1 }
        _enMode Mode = _enMode.UpdateMode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo;

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = true;

            Mode = _enMode.AddNewMode;
        }
        public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = new clsPerson();

            Mode = _enMode.UpdateMode;
        }
        public static clsUser Find(int ID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByID(ID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;

        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdatePerson()
        {
            return (clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive));
        }
        public static bool DeleteUser(int UserID)
        {
            return (clsUserData.DeleteUser(UserID));
        }
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.AddNewMode:
                    if (_AddNewUser())
                    {
                        Mode = _enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case _enMode.UpdateMode:
                    return _UpdatePerson();
            }
            return false;
        }
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        public static bool IsPersonUser(int PersonID)
        {
            if (clsUserData.IsPersonUser(PersonID))
                return true;

            return false;
        }
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return (clsUserData.ChangePassword(UserID, NewPassword));
        }
        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword
                                (UserName, Password, ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
    }
}
