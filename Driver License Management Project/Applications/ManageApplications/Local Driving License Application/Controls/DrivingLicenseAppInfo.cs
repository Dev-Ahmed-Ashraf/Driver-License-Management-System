using Bussiness_Layer;
using Driver_License_Management_Project.Applications.controls;
using Driver_License_Management_Project.Licenses.LocalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication.Controls
{
    public partial class DrivingLicenseAppInfo : UserControl
    {
        int _LocalDrivingLicenseID, _LicenseID;
        clsLocalLicenseApps _LocalLicenseApps;
        
        public DrivingLicenseAppInfo()
        {
            InitializeComponent();
        }

        public void FillLabelsInControl(int LocalDrivingLicenseID)
        {
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            _LocalLicenseApps =  clsLocalLicenseApps.FindLocalLicenseInfoByID(LocalDrivingLicenseID);
            lblLocalAppID.Text = _LocalLicenseApps.LocalDrivingLicenseApp_ID.ToString();
            lblLicenseClass.Text = _LocalLicenseApps.LicenseClassInfo.ClassName;
            lblPassedtest.Text = (_LocalLicenseApps.GetPassedTestCount() + "/3").ToString();     
            applicationBasicInfo1.FillLabelsInAppCtrl(_LocalLicenseApps.Application_ID);
            _LicenseID = _LocalLicenseApps.GetActiveLicenseID();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            if (_LicenseID == -1)
            {
                MessageBox.Show("Could not find this License",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LicenseDetails licenseDetails = new LicenseDetails(_LicenseID);
            licenseDetails.ShowDialog();
        }
    }
}
