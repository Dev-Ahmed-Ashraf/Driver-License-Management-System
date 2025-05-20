using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer
{
    public class clsApplications
    {
        public enum enMode {AddNew = 1, Update = 2};
        public enMode Mode = enMode.AddNew;
        public enum enAppTypeID {NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplacementLostDrivingLicense = 3, ReplacementDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7};
        public clsAppTypes AppTypeInfo;
        public enum enAppStatus {New = 1, Canceled = 2, Completed = 3};
        public enAppStatus AppStatus {  get; set; }
        public int Application_ID { get; set; }
        public int PersonID { get; set; }
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(PersonID).FullName;
            }
        }
        public clsPerson PersonInfo;
        public int AppType_ID { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }

        public clsApplications()
        {
            this.Application_ID = -1;
            this.PersonID = -1;
            this.AppStatus = enAppStatus.New;
            this.AppType_ID = -1;
            this.CreatedByUserID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;

            Mode = enMode.AddNew;
        }
        public clsApplications(int AppID, int AppTypeID, int PersonID, int UserID, DateTime AppDate, DateTime LastDate, enAppStatus AppStatus, float PaidFees)
        {
            this.Application_ID = AppID;
            this.CreatedByUserID = UserID;
            this.CreatedByUserInfo = clsUser.Find(UserID);
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.AppType_ID = AppTypeID;
            this.AppTypeInfo = clsAppTypes.Find(AppTypeID);
            this.ApplicationDate = AppDate;
            this.LastStatusDate = LastDate;
            this.AppStatus = AppStatus;
            this.PaidFees = PaidFees;

            Mode = enMode.Update;

        }
        public bool _AddNewApplication()
        {
            this.Application_ID = clsApplicationsData.AddNewApplication(this.PersonID, this.ApplicationDate, this.AppType_ID,
                (byte)this.AppStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.Application_ID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.Application_ID, this.PersonID, this.ApplicationDate,
                this.AppType_ID, (byte)this.AppStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplication();

            }
            return false;
        }
        public static clsApplications FindAppInfoByID(int ID)
        {
            int ApplicationTypeID = -1, PersonID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;  float PaidFees = 0;
            if (clsApplicationsData.GetApplicationInfoByID(ID, ref  ApplicationTypeID, ref PersonID, ref CreatedByUserID, ref ApplicationDate, ref LastStatusDate, ref ApplicationStatus, ref PaidFees))
            {
                return new clsApplications(ID, ApplicationTypeID, PersonID, CreatedByUserID, ApplicationDate, LastStatusDate, (enAppStatus)ApplicationStatus, PaidFees);
            }
            else
            {
                return null;
            }
        }
        public bool DeleteApplication()
        {
            return (clsApplicationsData.DeleteApplication(this.Application_ID));
        }
        public bool CancleApplication()
        {
            return (clsApplicationsData.UpdateStatus(Application_ID, 2));
        }
        public bool SetComplete()
        {
            return (clsApplicationsData.UpdateStatus(Application_ID, 3));
        }
    }
}
