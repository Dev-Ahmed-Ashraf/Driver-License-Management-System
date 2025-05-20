using Bussiness_Layer;
using Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication.Controls;
using Driver_License_Management_Project.Properties;
using Driver_License_Management_Project.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication
{
    public partial class Vision_Test_Appointments : Form
    {
        
        int _LocalDrivingLicenseID; 
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        
        public Vision_Test_Appointments(int localDrivingLicenseID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = localDrivingLicenseID;
            _TestType = TestType;
        }
        private void _FillLblTitle_Image()
        {
            switch (_TestType)
            {
                    case clsTestTypes.enTestType.VisionTest:
                    {
                        this.Text = "Vision Test Appointments";
                        lblTitle.Text = this.Text;
                        pbTitle.Image = Resources.Vision_512;
                        break;
                    }
                    case clsTestTypes.enTestType.WrittenTest:
                    {
                        this.Text = "Written Test Appointments"; 
                        lblTitle.Text = this.Text;
                        pbTitle.Image = Resources.Written_Test_512;
                        break;
                    }
                    case clsTestTypes.enTestType.StreetTest:
                    {
                        this.Text = "Street Test Appointments";
                        lblTitle.Text = this.Text;
                        pbTitle.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }
        private void _RefreshDataInDGV()
        {
            DGVAppointments.DataSource = clsTestAppointment.GetAllTestAppointments(_LocalDrivingLicenseID, (byte)_TestType);
            lblRecords.Text = DGVAppointments.RowCount.ToString();
        }
        private void Vision_Test_Appointments_Load(object sender, EventArgs e)
        {                     
           drivingLicenseAppInfo.FillLabelsInControl(_LocalDrivingLicenseID);
            _FillLblTitle_Image();
            _RefreshDataInDGV();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            clsTestAppointment clsTestAppointment = clsTestAppointment.GetLastTestAppointment(_LocalDrivingLicenseID, (byte)_TestType);
            if (clsTestAppointment != null)
            {
                clsTests clsTest = clsTests.Find(clsTestAppointment.TestID);
                bool IsLocked = clsTestAppointment.IsTestAppoIsLocked(clsTestAppointment.TestAppointmentID);
                if (IsLocked == false)
                {
                    MessageBox.Show("You have an active appointment now, please Take The appointment first", "Ërror", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }
                bool Result = clsTest.Result;
                
                if (IsLocked == true && Result == true)
                {
                    MessageBox.Show("You Already Passed This Test, So you cannot take a new appointment, As TestID = " + clsTestAppointment.TestID.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (IsLocked == true && Result == false)
                {
                    Shedule_Test shedule_Test = new Shedule_Test(_LocalDrivingLicenseID, _TestType, -1, false, true);
                    shedule_Test.ShowDialog();
                    _RefreshDataInDGV();
                }
            }
            else           
            {
                Shedule_Test shedule_Test = new Shedule_Test(_LocalDrivingLicenseID, _TestType);
                shedule_Test.ShowDialog();
                _RefreshDataInDGV();
            }
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsLocked = clsTestAppointment.IsTestAppoIsLocked((int)DGVAppointments.CurrentRow.Cells[0].Value);
            Shedule_Test shedule_Test = new Shedule_Test(_LocalDrivingLicenseID, _TestType, (int)DGVAppointments.CurrentRow.Cells[0].Value, IsLocked);
            shedule_Test.ShowDialog();
            _RefreshDataInDGV();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsLocked = clsTestAppointment.IsTestAppoIsLocked((int)DGVAppointments.CurrentRow.Cells[0].Value);
            Take_Test take_Test = new Take_Test(_LocalDrivingLicenseID, (int)DGVAppointments.CurrentRow.Cells[0].Value , _TestType, IsLocked);
            take_Test.ShowDialog();
            _RefreshDataInDGV();
        }
    }
}
