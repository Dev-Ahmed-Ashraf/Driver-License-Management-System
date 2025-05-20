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
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public enIssueReason IssueReason { set; get; }

        public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClasses LicenseClassInfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }       
        public int CreatedByUserID { set; get; }
        public clsDetainedLicense DetainedInfo { set; get; }

        public clsLicense()

        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            
            Mode = enMode.AddNew;

        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClassInfoByID(this.LicenseClassID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);


            return (this.LicenseID != -1);
        }
        //private bool _UpdateLicense()
        //{
        //    //call DataAccess Layer 

        //    return clsLicensesData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClass,
        //       this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
        //       this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        //}

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicensesData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return (clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID));
        }
        private bool _DeactiveLicense(int LicenseID)
        {
            return (clsLicensesData.DeactivateLicense(LicenseID));
        }
        public static DataTable GetAllLicenses(int DriverID)
        {
            return clsLicensesData.GetAllLicensesforThisPerson(DriverID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

               case enMode.Update:

                   return false;

            }

            return false;
        }
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplications Application = new clsApplications();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.AppStatus = clsApplications.enAppStatus.Completed;
            Application.AppType_ID = (int)clsApplications.enAppTypeID.RenewDrivingLicense;
            Application.CreatedByUserID = CreatedByUserID;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.RenewDrivingLicense).AppFees;
            
            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.Application_ID;
            NewLicense.Notes = Notes;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.ValidityLength);
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReason.Renew;
            
            if (!NewLicense.Save())
            {
                return null;
            }
            _DeactiveLicense(this.LicenseID);
            return NewLicense;
        }
        public clsLicense ReplacementLicense(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplications Application = new clsApplications();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.AppStatus = clsApplications.enAppStatus.Completed;
            Application.AppType_ID = (IssueReason == enIssueReason.DamagedReplacement) ? (int)clsApplications.enAppTypeID.ReplacementDamagedDrivingLicense : (int)clsApplications.enAppTypeID.ReplacementLostDrivingLicense;
            Application.CreatedByUserID = CreatedByUserID;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsAppTypes.Find(Application.AppType_ID).AppFees;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.Application_ID;
            NewLicense.Notes = this.Notes;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.PaidFees = 0;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;

            if (!NewLicense.Save())
            {
                return null;
            }

            _DeactiveLicense(this.LicenseID);

            return NewLicense;
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.AppType_ID = (int)clsApplications.enAppTypeID.ReleaseDetainedDrivingLicense;
            Application.AppStatus = clsApplications.enAppStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.ReleaseDetainedDrivingLicense).AppFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.Application_ID;


            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.Application_ID);

        }
        public bool IssueInternationalLicense(int CreatedByUserID, ref int ApplicationID, ref int InternationalLicenseID)
        {
            clsApplications Application = new clsApplications();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.AppType_ID = (int)clsApplications.enAppTypeID.NewInternationalLicense;
            Application.AppStatus = clsApplications.enAppStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.NewInternationalLicense).AppFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.Application_ID;

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.Application_ID = ApplicationID;
            InternationalLicense.DriverID = this.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = this.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.CreatedByUserID = CreatedByUserID;
            InternationalLicense.AppType_ID = (int)clsApplications.enAppTypeID.NewInternationalLicense;
            InternationalLicense.IsActive = true;
            if (!InternationalLicense.Save())
            {
                InternationalLicenseID = -1;
                return false;
            }
            InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            return true;
        }
    }
   
}
