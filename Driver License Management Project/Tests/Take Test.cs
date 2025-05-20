using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
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
    public partial class Take_Test : Form
    {
        int _LocalDrivingLicenseID;
        int _AppointmentID;
        bool _IsLocked;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        clsTests _Test;
        public Take_Test(int LocalDrivingLicenseID, int AppiontmentID, clsTestTypes.enTestType TestType, bool IsLocked)
        {
            InitializeComponent();
            _TestType = TestType;
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            _AppointmentID = AppiontmentID;
            _IsLocked = IsLocked;
        }

        private void Take_Test_Load(object sender, EventArgs e)
        {
            sheduled_Test1.FillLabels(_LocalDrivingLicenseID, _AppointmentID, _TestType);
            sheduled_Test1.LoadTitles(_TestType);
            if(_IsLocked)
                _IfTestAppoIsLocked();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!(rbPass.Checked || rbFail.Checked))
            {
                MessageBox.Show("Please, Select Result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            _Test = new clsTests();
            _Test.TestAppointmentID = _AppointmentID;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.Result = rbPass.Checked;
            _Test.Notes = rtbNotes.Text.Trim();

            if(_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                sheduled_Test1.FillTestID();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
         private void _IfTestAppoIsLocked()
        {
            clsTestAppointment _TestAppo = clsTestAppointment.Find(_AppointmentID);

            _Test = clsTests.Find(_TestAppo.TestID);
            if (_Test.Result)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;  
            rtbNotes.Text = _Test.Notes;

            lblCannot.Visible = true;
            rbFail.Enabled = false;
            rbPass.Enabled = false;
            rtbNotes.Enabled = false;
            btnSave.Enabled = false;         
        }

        }
    }
