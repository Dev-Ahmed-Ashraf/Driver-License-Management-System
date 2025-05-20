using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
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

namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services
{
    public partial class Renew_Driving_License : Form
    {
        int _DriverID, _NewLicenseID;
        public Renew_Driving_License()
        {
            InitializeComponent();
        }
        private void Renew_Driving_License_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "[???]";
            lblAppFees.Text = clsAppTypes.Find((int)clsApplications.enAppTypeID.RenewDrivingLicense).AppFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseDetails licenseDetails = new LicenseDetails(_NewLicenseID);
            licenseDetails.ShowDialog();
        }

        private void Renew_Driving_License_Activated(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private bool _ValidateRenewOperation()
        {
            if (ctrlShowDriverLicenseInfoWithFilter1.LicenseID <= 0)
            {
                MessageBox.Show("Please select a valid license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            clsLicense selectedLicense = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
            if (selectedLicense == null)
            {
                MessageBox.Show("Selected license not found", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate license is active
            if (!selectedLicense.IsActive)
            {
                MessageBox.Show("Cannot renew an inactive license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate license is expired
            if (selectedLicense.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show($"Cannot renew a license that is not yet expired. Current license expires on: {selectedLicense.ExpirationDate.ToShortDateString()}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate license class exists and has valid fees
            if (selectedLicense.LicenseClassInfo == null)
            {
                MessageBox.Show("License class information not found", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (selectedLicense.LicenseClassInfo.ClassFees <= 0)
            {
                MessageBox.Show("Invalid license class fees", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate application fees
            decimal appFees = Convert.ToDecimal(lblAppFees.Text);
            if (appFees <= 0)
            {
                MessageBox.Show("Invalid application fees", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (!_ValidateRenewOperation())
                return;

            if (MessageBox.Show("Are you sure you want to renew this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                clsLicense NewLicense = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

                if (NewLicense == null)
                {
                    MessageBox.Show("Failed to renew the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblRenewApplicationID.Text = NewLicense.ApplicationID.ToString();
                _NewLicenseID = NewLicense.LicenseID;
                lblRenewLicenseID.Text = _NewLicenseID.ToString();
                MessageBox.Show($"License renewed successfully with ID={_NewLicenseID}", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnRenewLicense.Enabled = false;
                ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowLicenseInfo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while renewing the license: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlShowDriverLicenseInfoWithFilter1_OnLicenseSelected_1(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
                return;

            try
            {
                clsLicense selectedLicense = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
                if (selectedLicense == null)
                {
                    MessageBox.Show("Selected license not found", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate license is active
                if (!selectedLicense.IsActive)
                {
                    MessageBox.Show("Cannot renew an inactive license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate license is expired
                if (selectedLicense.ExpirationDate > DateTime.Now)
                {
                    MessageBox.Show($"Cannot renew a license that is not yet expired. Current license expires on: {selectedLicense.ExpirationDate.ToShortDateString()}", 
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int DefaultValidityLength = selectedLicense.LicenseClassInfo.ValidityLength;
                if (DefaultValidityLength <= 0)
                {
                    MessageBox.Show("Invalid license validity length", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToShortDateString();
                lblLicenseFees.Text = selectedLicense.LicenseClassInfo.ClassFees.ToString();
                lblTotalFees.Text = (Convert.ToDecimal(lblAppFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();

                btnRenewLicense.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading license details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
