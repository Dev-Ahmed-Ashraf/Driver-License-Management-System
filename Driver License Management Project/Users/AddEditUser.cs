using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Users.Controls;
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
    public partial class AddEditUser : Form
    {
        public delegate void Manage_Users_Click(object sender, int PersonID);
        public event Manage_Users_Click DataBack;
        public enum _enMode { AddNewMode = 0, UpdateMode = 1 }
        _enMode Mode = _enMode.AddNewMode;

        int _UserID = -1;
        clsUser _User;

        public AddEditUser()
        {
            InitializeComponent();
            Mode = _enMode.AddNewMode;
            
            // Enable validation on form
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            
            // Add validation events
            txtUserName.Validating += txtUserName_Validating;
            txtPassword.Validating += txtPassword_Validating;
            txtConfirm.Validating += txtConfirm_Validating;
        }
        public AddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

            Mode = _enMode.UpdateMode;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateMode()
        {
            this.Text = "Edit User Info";
            lblTitle.Text = "Edit User Info"; 

            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            personInfoWithSearch1.LoadPersonInfo(_User.PersonID);
            personInfoWithSearch1.FilterEnabled = false;
            tabLoginInfo.Enabled = true;
            btnSave.Enabled = true;
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirm.Text = _User.Password;
            checkBoxIsActive.Checked = _User.IsActive;
        }
        private void AddEditUser_Load(object sender, EventArgs e)
        {
            if (Mode == _enMode.UpdateMode)
            { 
                UpdateMode();
                return;     
            }

            _User = new clsUser();
            personInfoWithSearch1.FilterFocus();
            tabLoginInfo.Enabled = false;
            btnSave.Enabled = false;
                     
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == _enMode.UpdateMode)
            {
                btnSave.Enabled = true;
                tabLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tabLoginInfo"];
                return;
            }

            // Validate person selection
            if (personInfoWithSearch1.PersonID == -1)
            {
                MessageBox.Show("Please select a person", "Select a Person", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                personInfoWithSearch1.FilterFocus();
                return;
            }

            // Check if person is already a user
            if (clsUser.IsPersonUser(personInfoWithSearch1.PersonID))
            {
                MessageBox.Show("This person is already a user", "Duplicate User", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnSave.Enabled = true;
            tabLoginInfo.Enabled = true;
            tabControl1.SelectedTab = tabControl1.TabPages["tabLoginInfo"];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all controls on the form
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the validation errors before saving.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure that you want to save?", "Save", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            
                _User.PersonID = personInfoWithSearch1.PersonID;
                _User.UserName = txtUserName.Text.Trim();
                _User.Password = clsUtil.HashData(txtPassword.Text.Trim());               
                _User.IsActive = checkBoxIsActive.Checked;

                if (_User.Save())
                {
                    MessageBox.Show("User Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this, _UserID);
                    if (Mode == _enMode.AddNewMode)
                    {
                        lblUserID.Text = _User.UserID.ToString();
                        Mode = _enMode.UpdateMode;
                        this.Text = "Edit User Info";
                        lblTitle.Text = "Edit User Info";
                    }
                }
                else
                {
                    MessageBox.Show("User Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            TextBox userNameTextBox = (TextBox)sender;
            string userName = userNameTextBox.Text.Trim();

            // Check if empty
            if (string.IsNullOrWhiteSpace(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(userNameTextBox, "Username is required");
                return;
            }

            // Validate username format (alphanumeric and underscore only, 3-20 characters)
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]{3,20}$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(userNameTextBox, "Username must be 3-20 characters and contain only letters, numbers, and underscores");
                return;
            }

            // Check for existing username
            if (Mode == _enMode.AddNewMode)
            {
                if (clsUser.IsUserExist(userName))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(userNameTextBox, "Username is already taken");
                    return;
                }
            }
            else
            {
                // In update mode, only check if username changed and is taken
                if (_User.UserName != userName && clsUser.IsUserExist(userName))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(userNameTextBox, "Username is already taken");
                    return;
                }
            }

            errorProvider1.SetError(userNameTextBox, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox passwordTextBox = (TextBox)sender;
            string password = passwordTextBox.Text.Trim();

            // Skip validation in update mode if password is empty (not changing password)
            if (Mode == _enMode.UpdateMode && string.IsNullOrEmpty(password))
            {
                errorProvider1.SetError(passwordTextBox, null);
                return;
            }

            // Check if empty
            if (string.IsNullOrWhiteSpace(password))
            {
                e.Cancel = true;
                errorProvider1.SetError(passwordTextBox, "Password is required");
                return;
            }

            // Validate password strength
            if (password.Length < 8)
            {
                e.Cancel = true;
                errorProvider1.SetError(passwordTextBox, "Password must be at least 8 characters long");
                return;
            }

            // Check for password complexity
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            if (!(hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar))
            {
                e.Cancel = true;
                errorProvider1.SetError(passwordTextBox, 
                    "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
                return;
            }

            errorProvider1.SetError(passwordTextBox, null);
        }

        private void txtConfirm_Validating(object sender, CancelEventArgs e)
        {
            TextBox confirmTextBox = (TextBox)sender;
            string confirmPassword = confirmTextBox.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Skip validation in update mode if both password fields are empty (not changing password)
            if (Mode == _enMode.UpdateMode && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(confirmPassword))
            {
                errorProvider1.SetError(confirmTextBox, null);
                return;
            }

            // Check if empty
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(confirmTextBox, "Please confirm your password");
                return;
            }

            // Check if passwords match
            if (confirmPassword != password)
            {
                e.Cancel = true;
                errorProvider1.SetError(confirmTextBox, "Passwords do not match");
                return;
            }

            errorProvider1.SetError(confirmTextBox, null);
        }
    }
}
