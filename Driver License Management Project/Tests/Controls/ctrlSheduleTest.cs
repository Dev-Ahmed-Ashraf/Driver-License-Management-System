using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bussiness_Layer.clsTestTypes;

namespace Driver_License_Management_Project.Tests.Controls
{
    public partial class ctrlShedule_Test : UserControl
    {
        enum enMode {AddNew = 1, Update = 2};
        enMode Mode = enMode.AddNew;

        clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        clsLocalLicenseApps _LocalLicenseApp;
        clsApplications _Application;
        clsTestTypes _TestType;
        clsTestAppointment _TestAppointment;
        bool _IsRetake;
        public clsTestTypes.enTestType TestType
        {
            get 
            { 
                return _TestTypeID;
            }

            set 
            { 
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case enTestType.VisionTest:
                        {
                            lblTitle.Text = "Shedule Vision Test";
                            gbVisionTest.Text = "Vision Test";
                            pbTitle.Image = Resources.Vision_512;
                            break;
                        }
                    case enTestType.WrittenTest:
                        {
                            lblTitle.Text = "Shedule Written Test";
                            gbVisionTest.Text = "Written Test";
                            pbTitle.Image = Resources.Written_Test_512;
                            break;
                        }
                    case enTestType.StreetTest:
                        {
                            lblTitle.Text = "Shedule Street Test";
                            gbVisionTest.Text = "Street Test";
                            pbTitle.Image = Resources.driving_test_512;
                            break;
                        }
                }

            }
        }
        public ctrlShedule_Test()
        {
            InitializeComponent();           
        }
        public void FillLabels(int LocalDLicenseID, clsTestTypes.enTestType _enTestType)
        {
            _LocalLicenseApp = clsLocalLicenseApps.FindLocalLicenseInfoByID(LocalDLicenseID);
            _Application = clsApplications.FindAppInfoByID(_LocalLicenseApp.Application_ID);
            _TestType = clsTestTypes.Find((byte)_enTestType);
            lblLocalAppID.Text = _LocalLicenseApp.LocalDrivingLicenseApp_ID.ToString();
            lblClass.Text = _LocalLicenseApp.LicenseClassInfo.ClassName;
            lblName.Text = _LocalLicenseApp.PersonFullName;
            lblFees.Text = _TestType.Fees.ToString();
            lblTrial.Text = clsTestAppointment.GetTrials(_LocalLicenseApp.LocalDrivingLicenseApp_ID, _TestType.Title).ToString();
            //dateTimePicker1.MinDate = DateTime.Now;
        }
        public void LoadDataTimePicker(int AppointmentID)
        {
            Mode = enMode.Update;
            _TestAppointment = clsTestAppointment.Find(AppointmentID);
            dateTimePicker1.Value = _TestAppointment.AppointmentDate;
            _TestAppointment.TestAppointmentID = AppointmentID;
            
        }
        public void IfTestAppoIsLocked()
        {
            dateTimePicker1.Enabled = false;
            btnSave.Enabled = false;
        }
        public void IsRetakeTestAfterTest(bool IsRetake)
        {
            _IsRetake = IsRetake;
            gbRetakeTest.Enabled = true;
            dateTimePicker1.MinDate = DateTime.Now;
            clsAppTypes AppType = clsAppTypes.Find((int)clsApplications.enAppTypeID.RetakeTest);
            lblRtestFees.Text = AppType.AppFees.ToString(); 
            lblTotalFees.Text = (Convert.ToInt16(lblFees.Text.ToString()) + Convert.ToInt16(lblRtestFees.Text.ToString())).ToString();
        }
        private bool _HandleRetakeApplication()
        {
            
            if (Mode == enMode.AddNew && _IsRetake)
            {
                
                clsApplications Application = new clsApplications();

                Application.PersonID = _LocalLicenseApp.PersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.AppType_ID = (int)clsApplications.enAppTypeID.RetakeTest;
                Application.AppStatus = clsApplications.enAppStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.RetakeTest).AppFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!(Application.Save()))
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.Application_ID;
                lblRtestID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want Save Appointment?", "Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }

            if (Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
            
                     if (!_HandleRetakeApplication())           
                         return;
            }                                      
                
                    _TestAppointment.TestTypeID = _TestType.TestTypeID;
                    _TestAppointment.LocalDrivingLicenseAppID = _LocalLicenseApp.LocalDrivingLicenseApp_ID;
                    _TestAppointment.CreatedByUserID = _Application.CreatedByUserID;
                    _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text.Trim());
                    _TestAppointment.AppointmentDate = dateTimePicker1.Value;
                    
                if (_TestAppointment.Save())
                {
                    MessageBox.Show("Test Appointment Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTitle.Text = "Edit Test Appointment";
                    Mode = enMode.Update;
                }
            
        }
        public void LoadTitles(clsTestTypes.enTestType _enTestType)
        {
            switch (_enTestType)
            {
                case enTestType.VisionTest:
                    {
                        lblTitle.Text = "Shedule Vision Test";
                        gbVisionTest.Text = "Vision Test";
                        pbTitle.Image = Resources.Vision_512;
                        break;
                    }
                case enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Shedule Written Test";
                        gbVisionTest.Text = "Written Test";
                        pbTitle.Image = Resources.Written_Test_512;
                        break;
                    }
                case enTestType.StreetTest:
                    {
                        lblTitle.Text = "Shedule Street Test";
                        gbVisionTest.Text = "Street Test";
                        pbTitle.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }
    }
}
