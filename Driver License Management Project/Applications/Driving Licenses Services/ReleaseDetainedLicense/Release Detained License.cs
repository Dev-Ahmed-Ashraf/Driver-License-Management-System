using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Licenses;
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

namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services.ReleaseDetainedLicense
{
    public partial class Release_Detained_License : Form
    {
        int _DetainedLicenseID;
        clsLicense License;
        public Release_Detained_License(int DetainedLicenseID = -1)
        {
            InitializeComponent();
            _DetainedLicenseID = DetainedLicenseID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadData()
        {
            License = clsLicense.Find(_DetainedLicenseID);
            clsDetainedLicense DetainedLicense = clsDetainedLicense.FindByLicenseID(_DetainedLicenseID);

            ctrlShowDriverLicenseInfoWithFilter1.LoadLicenseInfo(_DetainedLicenseID);

            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToString();
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblAppFees.Text = clsAppTypes.Find((int)clsApplications.enAppTypeID.ReleaseDetainedDrivingLicense).AppFees.ToString();
            lblTotalFees.Text = (Convert.ToInt32(lblFineFees.Text) + Convert.ToInt32(lblAppFees.Text)).ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            btnReleaseLicense.Enabled = true;
            llShowLicenseInfo.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseDetails licenseDetails = new LicenseDetails(ctrlShowDriverLicenseInfoWithFilter1.LicenseID);
            licenseDetails.ShowDialog();
        }
 
        private void Release_Detained_License_Load(object sender, EventArgs e)
        {
            if (_DetainedLicenseID != -1)
            {
                ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
                _LoadData();
                return;
            }
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblAppFees.Text = clsAppTypes.Find((int)clsApplications.enAppTypeID.ReleaseDetainedDrivingLicense).AppFees.ToString();
        }

        private bool _ValidateReleaseOperation()
        {
            if (ctrlShowDriverLicenseInfoWithFilter1.LicenseID <= 0)
            {
                MessageBox.Show("Please select a valid license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(ctrlShowDriverLicenseInfoWithFilter1.LicenseID);
            if (detainedLicense == null)
            {
                MessageBox.Show("No detained license found for the selected license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (detainedLicense.IsReleased)
            {
                MessageBox.Show("This license has already been released", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate fees
            decimal fineFees = Convert.ToDecimal(lblFineFees.Text);
            decimal appFees = Convert.ToDecimal(lblAppFees.Text);
            decimal totalFees = fineFees + appFees;

            if (fineFees <= 0 || appFees <= 0 || totalFees <= 0)
            {
                MessageBox.Show("Invalid fee amounts detected", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            if (!_ValidateReleaseOperation())
                return;

            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                int ApplicationID = -1;
                bool IsReleased = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

                if (!IsReleased)
                {
                    MessageBox.Show("Failed to release the detained license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Detained license released successfully", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAppID.Text = ApplicationID.ToString();
                btnReleaseLicense.Enabled = false;
                ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while releasing the license: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlShowDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            clsDetainedLicense DetainedLicense = clsDetainedLicense.FindByLicenseID(SelectedLicenseID);

            lblLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseInfo.Enabled = (SelectedLicenseID != -1);
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (DetainedLicense == null)
            {
                MessageBox.Show("No detained license found for the selected license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DetainedLicense.IsReleased)
            {
                MessageBox.Show("This license has already been released", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                lblDetainID.Text = DetainedLicense.DetainID.ToString();
                lblLicenseID.Text = DetainedLicense.LicenseID.ToString();
                lblDetainDate.Text = DetainedLicense.DetainDate.ToString();
                lblFineFees.Text = DetainedLicense.FineFees.ToString();
                lblTotalFees.Text = (Convert.ToDecimal(lblFineFees.Text) + Convert.ToDecimal(lblAppFees.Text)).ToString();
                btnReleaseLicense.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading license details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Release_Detained_License_Activated(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
        }
    }
}
