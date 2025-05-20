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

namespace Driver_License_Management_Project.Licenses
{
    public partial class Show_License : UserControl
    {
        int _LicenseID;
        clsLicense _License;
        public Show_License()
        {
            InitializeComponent();
        }      
        public int LicenseID
        { 
            get { return _LicenseID; }
        }
        public clsLicense LicenseInfo
        {
            get { return _License; }
        }
        public void LoadData(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Could not find this License",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = LicenseID.ToString();
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNum;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gender;
            lblIssueDate.Text = _License.IssueDate.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblNotes.Text  = _License.Notes.ToString();
            lblIsActive.Text = _License.IsActive.ToString();
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();
            if (clsDetainedLicense.IsLicenseDetained(_LicenseID)) 
            { lblIsDetained.Text = "Yes"; }
            else
            { lblIsDetained.Text = "No"; }

            if (_License.DriverInfo.PersonInfo.Gender.ToLower() == "female")
                pbGendor.Image = Resources.Woman_32;
            else
                pbGendor.Image = Resources.Man_32;

            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            
            if (_License.DriverInfo.PersonInfo.Gender.ToLower() == "female")
                pbPersonImage.Image = Resources.Female;
            else
                pbPersonImage.Image = Resources.Male;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        
    }
}
