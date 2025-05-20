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

namespace Driver_License_Management_Project.Licenses.Detain_License
{
    
    public partial class Detain_License : Form
    {
        int _LicenseID;
        public Detain_License()
        {
            InitializeComponent();
        }

        private void Detain_License_Load(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
            lblDetainDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            txtFineFees.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void Detain_License_Activated(object sender, EventArgs e)
        {
            if (ctrlShowDriverLicenseInfoWithFilter1.LicenseID <= 0)
            {
                ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
            }
        }
        private void ctrlShowDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseInfo.Enabled = (SelectedLicenseID != -1);
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                txtFineFees.Enabled = false;
                btnDetainLicense.Enabled = false;
                return;
            }

            if (ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This Local License is NOT Active", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFineFees.Enabled = false;
                btnDetainLicense.Enabled = false;
                return;
            }

            if (clsDetainedLicense.IsLicenseDetained(SelectedLicenseID))
            {
                MessageBox.Show("This License is already Detained, select another License ", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                llShowLicenseInfo.Enabled = true;
                txtFineFees.Enabled = false;
                btnDetainLicense.Enabled = false;
                return;
            }

            txtFineFees.Enabled = true;
            txtFineFees.Focus();
            btnDetainLicense.Enabled = true;
        }

        private bool _ValidateDetainOperation()
        {
            if (ctrlShowDriverLicenseInfoWithFilter1.LicenseID <= 0)
            {
                MessageBox.Show("Please select a valid license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate license exists and is active
            if (!ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Cannot detain an inactive license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate license is not already detained
            if (clsDetainedLicense.IsLicenseDetained(ctrlShowDriverLicenseInfoWithFilter1.LicenseID))
            {
                MessageBox.Show("This license is already detained", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate fine fees
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Fine fees amount is required!");
                return false;
            }

            if (!decimal.TryParse(txtFineFees.Text, out decimal fineFees) || fineFees <= 0)
            {
                errorProvider1.SetError(txtFineFees, "Please enter a valid fine fees amount!");
                return false;
            }

            errorProvider1.Clear();
            return true;
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            if (!_ValidateDetainOperation())
                return;

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                clsDetainedLicense DetainedLicense = new clsDetainedLicense
                {
                    LicenseID = ctrlShowDriverLicenseInfoWithFilter1.LicenseID,
                    CreatedByUserID = clsGlobal.CurrentUser.UserID,
                    DetainDate = DateTime.Now,
                    FineFees = Convert.ToSingle(txtFineFees.Text.Trim())
                };

                if (!DetainedLicense.Save())
                {
                    MessageBox.Show("Failed to detain license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"License detained successfully with ID={DetainedLicense.DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblDetainID.Text = DetainedLicense.DetainID.ToString();
                btnDetainLicense.Enabled = false;
                ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while detaining the license: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Fine fees amount is required!");
                e.Cancel = true;
                return;
            }

            if (!decimal.TryParse(txtFineFees.Text, out decimal fineFees) || fineFees <= 0)
            {
                errorProvider1.SetError(txtFineFees, "Please enter a valid fine fees amount!");
                e.Cancel = true;
                return;
            }

            errorProvider1.Clear();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, decimal point, and control characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
                return;
            }
        }
    }
}
