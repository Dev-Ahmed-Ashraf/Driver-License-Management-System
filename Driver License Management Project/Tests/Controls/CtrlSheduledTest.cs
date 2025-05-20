using Bussiness_Layer;
using Driver_License_Management_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bussiness_Layer.clsTestTypes;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_License_Management_Project.Tests.Controls
{
    public partial class CtrlSheduled_Test : UserControl
    {
        clsLocalLicenseApps _LocalLicenseApp;
        clsApplications _Application;
        clsTestTypes _TestType;
        clsTestAppointment _TestAppointment;
        public CtrlSheduled_Test()
        {
            InitializeComponent();
        }

        public void FillLabels(int LocalDLicenseID, int AppointmentID, clsTestTypes.enTestType _enTestType)
        {
            _LocalLicenseApp = clsLocalLicenseApps.FindLocalLicenseInfoByID(LocalDLicenseID);
            _Application = clsApplications.FindAppInfoByID(_LocalLicenseApp.Application_ID);
            _TestType = clsTestTypes.Find((byte)_enTestType);
            _TestAppointment = clsTestAppointment.Find(AppointmentID);
            lblLocalAppID.Text = _LocalLicenseApp.LocalDrivingLicenseApp_ID.ToString();
            lblClass.Text = _LocalLicenseApp.LicenseClassInfo.ClassName;
            lblName.Text = _LocalLicenseApp.PersonFullName;
            lblFees.Text = _TestType.Fees.ToString() + " $";
            lblTrial.Text = clsTestAppointment.GetTrials(_LocalLicenseApp.LocalDrivingLicenseApp_ID, _TestType.Title).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }
        public void FillTestID()
        {
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();
        }
        public void LoadTitles(clsTestTypes.enTestType _enTestType)
        {
            switch (_enTestType)
            {
                case enTestType.VisionTest:
                    {
                        lblTitle.Text = "Take Vision Test";
                        pbTitle.Image = Resources.Vision_512;
                        break;
                    }
                case enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Take Written Test";
                        pbTitle.Image = Resources.Written_Test_512;
                        break;
                    }
                case enTestType.StreetTest:
                    {
                        lblTitle.Text = "Take Street Test";
                        pbTitle.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }
    }
}
