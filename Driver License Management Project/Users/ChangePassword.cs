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
using System.Text.RegularExpressions;

namespace Driver_License_Management_Project.Users
{
    public partial class ChangePassword : Form
    {
        int _UserID = -1;
        clsUser _User;

        public ChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

            // Enable validation on form
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            
            // Add validation events
            txtCurrent.Validating += txtCurrent_Validating;
            txtNew.Validating += txtNew_Validating;
            txtConfirm.Validating += txtConfirm_Validating;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            userInfo1.LoadUserInfo(_UserID);
        }
        private void _ResetDefualtValues()
        {
            txtCurrent.Text = "";
            txtNew.Text = "";
            txtConfirm.Text = "";
            //txtCurrent.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all controls on the form
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the validation errors before changing password.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to change your password?", "Change Password", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (clsUser.ChangePassword(_UserID, clsUtil.HashData(txtNew.Text.Trim())))
            {
                MessageBox.Show("Password changed successfully", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to change password", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCurrent_Validating(object sender, CancelEventArgs e)
        {
            TextBox currentPasswordBox = (TextBox)sender;
            string currentPassword = currentPasswordBox.Text.Trim();

            // Check if empty
            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(currentPasswordBox, "Current password is required");
                return;
            }

            // Verify current password matches
            if (clsUtil.HashData(currentPassword) != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(currentPasswordBox, "Current password is incorrect");
                return;
            }

            errorProvider1.SetError(currentPasswordBox, null);
        }

        private void txtNew_Validating(object sender, CancelEventArgs e)
        {
            TextBox newPasswordBox = (TextBox)sender;
            string newPassword = newPasswordBox.Text.Trim();
            string currentPassword = txtCurrent.Text.Trim();

            // Check if empty
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(newPasswordBox, "New password is required");
                return;
            }

            // Check minimum length
            if (newPassword.Length < 8)
            {
                e.Cancel = true;
                errorProvider1.SetError(newPasswordBox, "Password must be at least 8 characters long");
                return;
            }

            // Check password complexity
            bool hasUpperCase = newPassword.Any(char.IsUpper);
            bool hasLowerCase = newPassword.Any(char.IsLower);
            bool hasDigit = newPassword.Any(char.IsDigit);
            bool hasSpecialChar = newPassword.Any(c => !char.IsLetterOrDigit(c));

            if (!(hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar))
            {
                e.Cancel = true;
                errorProvider1.SetError(newPasswordBox, 
                    "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
                return;
            }

            // Check if new password is same as current password
            if (newPassword == currentPassword)
            {
                e.Cancel = true;
                errorProvider1.SetError(newPasswordBox, "New password must be different from current password");
                return;
            }

            errorProvider1.SetError(newPasswordBox, null);
        }

        private void txtConfirm_Validating(object sender, CancelEventArgs e)
        {
            TextBox confirmPasswordBox = (TextBox)sender;
            string confirmPassword = confirmPasswordBox.Text.Trim();
            string newPassword = txtNew.Text.Trim();

            // Check if empty
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(confirmPasswordBox, "Please confirm your new password");
                return;
            }

            // Check if passwords match
            if (confirmPassword != newPassword)
            {
                e.Cancel = true;
                errorProvider1.SetError(confirmPasswordBox, "Passwords do not match");
                return;
            }

            errorProvider1.SetError(confirmPasswordBox, null);
        }
    }
}
