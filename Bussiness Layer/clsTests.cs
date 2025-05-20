using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bussiness_Layer.clsApplications;

namespace Bussiness_Layer
{
    public class clsTests
    {
        enum _enMode {AddNew = 1, Update = 2};
        _enMode _Mode = _enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public bool Result { get; set; }
        public string Notes { get; set; }

        public clsTests() 
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.CreatedByUserID = -1; 
            this.Result = false;
            this.Notes = string.Empty;

            _Mode = _enMode.AddNew;
        }
        public clsTests(int testID, int testAppointmentID, int createdByUserID, bool result, string notes)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(testAppointmentID);
            this.CreatedByUserID = createdByUserID;
            this.Result = result;
            this.Notes = notes;

            _Mode = _enMode.Update;
        }
        private bool _AddNewTest()
        {
            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID, this.Result, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }
        
        public bool Save()
        {
            switch (_Mode)
            {
                case _enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case _enMode.Update:

                   // return _UpdateTest();
            }
            return false;

        }
        public static clsTests Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;  bool TestResult = false;
            string Notes = "";
            if (clsTestsData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTests(TestID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
                return null;
        }
    }
}
