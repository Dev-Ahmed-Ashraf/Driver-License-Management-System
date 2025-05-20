using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Tests
{
    public partial class Shedule_Test : Form
    {
        enum enMode {AddNew = 1, Update = 2};
        enMode _Mode;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        int _LocalDrivingLicenseID;
        int _AppointmentID;
        bool _IsLocked;
        bool _RetakeAfterFail;
        public Shedule_Test(int localDrivingLicenseID, clsTestTypes.enTestType TestType, int AppointmentID = -1, bool IsLocked = false, bool RetakeAfterFail = false)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = localDrivingLicenseID;
            _TestType = TestType;
            _AppointmentID = AppointmentID;
            _IsLocked = IsLocked;
            _RetakeAfterFail = RetakeAfterFail;

            if (AppointmentID == -1)
               _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;        
        }

        private void Shedule_Test_Load(object sender, EventArgs e)
        {
            frmShedule_Test.LoadTitles(_TestType);
            frmShedule_Test.FillLabels(_LocalDrivingLicenseID, _TestType);
            if(_Mode == enMode.Update) 
                {
                frmShedule_Test.LoadDataTimePicker(_AppointmentID);
                if(_IsLocked == true)
                { 
                    frmShedule_Test.IfTestAppoIsLocked(); }
                }
            if(_RetakeAfterFail == true)
            {
                frmShedule_Test.IsRetakeTestAfterTest(true);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
