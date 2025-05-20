using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bussiness_Layer
{
    public class clsTestAppointment
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public int CreatedByUserID { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; } 
        public DateTime AppointmentDate { get; set; }
        public clsApplications RetakeTestAppInfo;
        public int TestID { 
            get { return _GetTestID(); }
        }


        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseAppID = -1;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            this.PaidFees = 0;
            this.IsLocked = false;
            this.AppointmentDate = DateTime.Now;

            Mode = enMode.AddNew;
        }
        public clsTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseAppID, int createdByUserID, int retakeTestApplicationID, float paidFees, bool isLocked, DateTime appointmentDate)
        {
           
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseAppID = localDrivingLicenseAppID;
            this.CreatedByUserID = createdByUserID;
            this.RetakeTestApplicationID = retakeTestApplicationID;
            this.PaidFees = paidFees;
            this.IsLocked = isLocked;
            this.AppointmentDate = appointmentDate;
            this.RetakeTestAppInfo = clsApplications.FindAppInfoByID(retakeTestApplicationID);

            Mode = enMode.Update;
        }
        public static DataTable GetAllTestAppointments(int LocalDrivingLicenseID, byte TestTypeID)
        {
            return (clsTestAppointmentsData.GetAllTestAppointments(LocalDrivingLicenseID, TestTypeID));
        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment(this.TestTypeID, this.AppointmentDate, this.LocalDrivingLicenseAppID, this.RetakeTestApplicationID
                , this.PaidFees, this.CreatedByUserID);
            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            if (clsTestAppointmentsData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.AppointmentDate, this.LocalDrivingLicenseAppID, this.IsLocked, this.RetakeTestApplicationID
                , this.PaidFees, this.CreatedByUserID))
                return true;
            else 
                return false;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTestAppointment())

                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else 
                            return false;
                    }
                case enMode.Update:
                    return (_UpdateTestAppointment());            
                    
            }
            return false;
        }
        public static int GetTrials(int LocalDrivingLicenseID, string TestTypeTitle)        
        {
            return (clsTestAppointmentsData.GetTrialsOfTestAppo(LocalDrivingLicenseID, TestTypeTitle));
        }
        public static clsTestAppointment Find(int ID)
        {
            int TestTypeID = -1, LocalDrivingLicenseID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1; float PaidFees = 0.0f;
            DateTime AppointmentDate = DateTime.Now; bool IsLocked = false;

            if (clsTestAppointmentsData.GetTestAppoInfoByID(ID, ref TestTypeID, ref LocalDrivingLicenseID, ref AppointmentDate, ref IsLocked, ref CreatedByUserID, ref PaidFees, ref RetakeTestApplicationID))
                return new clsTestAppointment(ID, TestTypeID, LocalDrivingLicenseID, CreatedByUserID, RetakeTestApplicationID, PaidFees, IsLocked, AppointmentDate);
            else
                return null;
        }
        private int _GetTestID()
        {
            return clsTestAppointmentsData.GetTestID(this.TestAppointmentID);
        }
        public static bool IsTestAppoIsLocked(int TestAppointmentID)
        {
            return (clsTestAppointmentsData.IsTestAppoIsLocked(TestAppointmentID));
        }
        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0.0f; bool IsLocked = false;
            if (clsTestAppointmentsData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, TestTypeID, ref TestAppointmentID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, CreatedByUserID, RetakeTestApplicationID, PaidFees, IsLocked, AppointmentDate);
            else
                return null;
        }
    }
}
