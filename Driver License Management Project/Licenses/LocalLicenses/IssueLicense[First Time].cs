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

namespace Driver_License_Management_Project.Licenses.LocalLicenses
{
    public partial class IssueLicense_First_Time : Form
    {
        int _LocalDrivingLicenseAppID;
        clsLocalLicenseApps _LocalDrivingLicenseApplication;
        public IssueLicense_First_Time(int LocalDrivingLicenseApp)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseApp;
        }

        private void IssueLicense_First_Time__Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalLicenseApps.FindLocalLicenseInfoByID(_LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseAppID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (_LocalDrivingLicenseApplication.GetPassedTestCount() != 3)
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }
            drivingLicenseAppInfo1.FillLabelsInControl(_LocalDrivingLicenseAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure You want to issue this License for First Time?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseforFirstTime(txtNotes.Text, clsGlobal.CurrentUser.UserID);

            if(LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                                "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
