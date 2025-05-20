using Data_Access_Layer;
using System;
using System.Data;

namespace Bussiness_Layer
{
    public class clsPerson
    {
        private enum _enMode { AddNewMode = 0, UpdateMode = 1 }
        _enMode Mode = _enMode.UpdateMode;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public string NationalNum { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int NationalityID { get; set; }

        public clsCountry CountryInfo;


        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public clsPerson()
        {
            this.ID = -1;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.NationalNum = string.Empty;
            this.Gender = string.Empty;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityID = -1;
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = string.Empty;

            Mode = _enMode.AddNewMode;
        }
        public clsPerson(int ID, string NationalNum,
            string FirstName, string SecondName, string ThirdName, string LastName,
            string Email, string Phone, string Address, int NationalityID,
            DateTime DateOfBirth, string Gender, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNum = NationalNum;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityID = NationalityID;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityID);

            Mode = _enMode.UpdateMode;
        }


        private bool _AddPerson()
        {
            this.ID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNum,
                this.Gender, this.Email, this.Phone, this.Address, this.DateOfBirth, this.NationalityID, this.ImagePath);

            return (this.ID != -1);
        }
        private bool _UpdatePerson()
        {
            return (clsPersonData.UpdatePerson(this.ID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNum,
                this.Gender, this.Email, this.Phone, this.Address, this.DateOfBirth, this.NationalityID, this.ImagePath));
        }
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.AddNewMode:
                    if (_AddPerson())
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


        public static clsPerson Find(int ID)
        {
            string NationalNum = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Gender = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonData.GetPersonInfoByID(ID, ref NationalNum, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref Email, ref Phone,
                ref Address, ref NationalityID, ref DateOfBirth, ref Gender, ref ImagePath))
            {
                return new clsPerson(ID, NationalNum, FirstName, SecondName, ThirdName, LastName, Email, Phone, Address, NationalityID, DateOfBirth, Gender, ImagePath);
            }

            else
                return null;
        }

        public static clsPerson Find(string NationalNumber)
        {
            int ID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Gender = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonData.GetPersonInfoByNationalNum(NationalNumber, ref ID, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref Email, ref Phone,
                ref Address, ref NationalityID, ref DateOfBirth, ref Gender, ref ImagePath))
            {
                return new clsPerson(ID, NationalNumber, FirstName, SecondName, ThirdName, LastName, Email, Phone, Address, NationalityID, DateOfBirth, Gender, ImagePath);
            }

            else
                return null;
        }

        public static bool IsExist(int PersonID)
        {
            return (clsPersonData.IsPersonExist(PersonID));
        }
        public static bool IsExist(string NationalNum)
        {
            return (clsPersonData.IsPersonExist(NationalNum));
        }

        public static bool DeletePerson(int PersonID)
        {
            return (clsPersonData.DeletePerson(PersonID));
        }

    }
}
