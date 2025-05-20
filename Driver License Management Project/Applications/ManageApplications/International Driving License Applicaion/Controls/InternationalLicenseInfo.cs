using Bussiness_Layer;
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
using System.IO;

namespace Driver_License_Management_Project.Applications.ManageApplications.International_Driving_License_Applicaion.Controls
{
    public partial class InternationalLicenseInfo : UserControl
    {
        int _InternationalLicenseID;
        clsInternationalLicense _InternationalLicense;
        public InternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadData(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find International License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblInternationalID.Text = _InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense.Application_ID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNum;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender;
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive.ToString();
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToString();
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender.ToLower() == "female")
                pbGendor.Image = Resources.Woman_32;
            else
                pbGendor.Image = Resources.Man_32;

            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender.ToLower() == "female")
                pbPersonImage.Image = Resources.Female;
            else
                pbPersonImage.Image = Resources.Male;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
