using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer
{
    public class clsLocalLicenseApps : clsApplications
    {
        enum enMode {AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApp_ID { get; set; }
        public int LicenseClass_ID { get; set; }
        public clsLicenseClasses LicenseClassInfo;

        public string PersonFullName
        {
        get 
            { 
                return clsPerson.Find(PersonID).FullName; 
            }
        }
        public clsLocalLicenseApps()
        {
            this.LocalDrivingLicenseApp_ID = -1;
            this.LicenseClass_ID = -1;

            Mode = enMode.AddNew;
        }
        public clsLocalLicenseApps(int LocalDrivingLicenseApplicationID, int ApplicationID, int PersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enAppStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApp_ID= LocalDrivingLicenseApplicationID;
            this.Application_ID = ApplicationID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.AppType_ID = (int)ApplicationTypeID;
            this.AppStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClass_ID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClassInfoByID(LicenseClassID);

            Mode = enMode.Update;
        }


        public static DataTable GetAllLocalLicenseApps()
        {
            return clsLocalDrivingLicenseAppsData.GetAllLocalLicenseApps();
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApp_ID = clsLocalDrivingLicenseAppsData.AddNewLocalDrivingLicenseApplication(this.Application_ID, this.LicenseClass_ID);
            return (this.LocalDrivingLicenseApp_ID != -1);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseAppsData.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingLicenseApp_ID, this.Application_ID, this.LicenseClass_ID);

        }
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseAppsData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApp_ID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.DeleteApplication();
            return IsBaseApplicationDeleted;

        }
        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

            }
            return false;
        }
        public static bool IfPersonHasThisLicense(int PersonID, String ClassName)
        {
            return (clsLocalDrivingLicenseAppsData.IfPersonHasThisLicense(PersonID, ClassName));
        }

        public static clsLocalLicenseApps FindLocalLicenseInfoByID(int LocalLicenseID)
        {
            int AppID = -1, LicenseClassID = -1;
            bool IsFound = clsLocalDrivingLicenseAppsData.GetLocalLicenseInfoByID(LocalLicenseID, ref AppID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplications Application = clsApplications.FindAppInfoByID(AppID);

                return new clsLocalLicenseApps(LocalLicenseID, AppID, Application.PersonID, Application.ApplicationDate, Application.AppType_ID,
                    Application.AppStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;
        }
        public byte GetPassedTestCount()
        {
            return (clsTestsData.GetPassedTestCount(this.LocalDrivingLicenseApp_ID));
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApp)
        {
            return (clsTestsData.GetPassedTestCount(LocalDrivingLicenseApp));
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.PersonID, this.LicenseClass_ID);
        }
        public bool IsLicenseIssue()
        {
            return (GetActiveLicenseID() != -1);
        }
        public bool DoesPassedThisTest(clsTestTypes.enTestType TestType)
        {
            return (clsLocalDrivingLicenseAppsData.DoesPassTestType(this.LocalDrivingLicenseApp_ID, (int)TestType));
        }

        public int IssueLicenseforFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.PersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();

                Driver.PersonID = this.PersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver.Save())
                    DriverID = Driver.DriverID;
                else
                    return -1;
            }
            else
                DriverID = Driver.DriverID;

            clsLicense license = new clsLicense();

            license.ApplicationID = this.Application_ID;
            license.DriverID = DriverID;
            license.LicenseClassID = this.LicenseClass_ID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = license.IssueDate.AddYears(this.LicenseClassInfo.ValidityLength);
            license.IsActive = true;
            license.Notes = Notes;
            license.PaidFees = this.LicenseClassInfo.ClassFees;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.CreatedByUserID = CreatedByUserID;

            if (license.Save())
            {
                this.SetComplete();
                return license.LicenseID;
            }
            else 
                return -1;
        }
        
    }
}
