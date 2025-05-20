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

namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services.Replacement
{
    public partial class Replacement_For_Damaged_OR_Lost : Form
    {
        int _NewLicenseID;
        clsLicense.enIssueReason IssueReason;
        public Replacement_For_Damaged_OR_Lost()
        {
            InitializeComponent();
        }
        private void ctrlShowDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
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
                    MessageBox.Show("Cannot replace an inactive license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnIssueReplacement.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading license details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Replacement_For_Damaged_OR_Lost_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            rbLost.Checked = true;
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                float appFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.ReplacementLostDrivingLicense).AppFees;
                if (appFees <= 0)
                {
                    MessageBox.Show("Invalid application fees for lost license replacement", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblAppFees.Text = appFees.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating fees: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                float appFees = clsAppTypes.Find((int)clsApplications.enAppTypeID.ReplacementDamagedDrivingLicense).AppFees;
                if (appFees <= 0)
                {
                    MessageBox.Show("Invalid application fees for damaged license replacement", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblAppFees.Text = appFees.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating fees: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void Replacement_For_Damaged_OR_Lost_Activated(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
        }
        private bool _ValidateReplacementOperation()
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
                MessageBox.Show("Cannot replace an inactive license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate replacement reason is selected
            if (!rbDamaged.Checked && !rbLost.Checked)
            {
                MessageBox.Show("Please select a replacement reason (Damaged or Lost)", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnReplacementLicense_Click(object sender, EventArgs e)
        {
            if (!_ValidateReplacementOperation())
                return;

            if (MessageBox.Show("Are you sure you want to replace this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                clsLicense.enIssueReason issueReason = rbDamaged.Checked ? 
                    clsLicense.enIssueReason.DamagedReplacement : 
                    clsLicense.enIssueReason.LostReplacement;

                clsLicense NewLicense = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReplacementLicense(issueReason, clsGlobal.CurrentUser.UserID);

                if (NewLicense == null)
                {
                    MessageBox.Show("Failed to replace the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblReplacementApplicationID.Text = NewLicense.ApplicationID.ToString();
                _NewLicenseID = NewLicense.LicenseID;
                lblReplacementLicenseID.Text = _NewLicenseID.ToString();
                MessageBox.Show($"License replaced successfully with ID={_NewLicenseID}", "License Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnIssueReplacement.Enabled = false;
                ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
                groupBox3.Enabled = false;
                llShowLicenseInfo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while replacing the license: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
