using Driver_License_Management_Project.Applications.Driving_Licenses_Services;
using Driver_License_Management_Project.Applications.Driving_Licenses_Services.ReleaseDetainedLicense;
using Driver_License_Management_Project.Applications.Driving_Licenses_Services.Replacement;
using Driver_License_Management_Project.Applications.ManageApplications;
using Driver_License_Management_Project.Applications.ManageApplications.International_Driving_License_Applicaion;
using Driver_License_Management_Project.Applications.ManageAppTypes;
using Driver_License_Management_Project.Applications.ManageTestTypes;
using Driver_License_Management_Project.Drivers;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Licenses.Detain_License;
using Driver_License_Management_Project.People.Controls;
using Driver_License_Management_Project.Users;
using System;
using System.Windows.Forms;

namespace Driver_License_Management_Project
{
    public partial class Main : Form
    {
        frmLogin _frmlogin;
        public Main(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmlogin = frmLogin;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_People manage_People = new Manage_People();
            manage_People.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Users manage_Users = new Manage_Users();
            manage_Users.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Details user_Details = new User_Details(clsGlobal.CurrentUser.UserID);
            user_Details.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ChangePassword changePassword = new ChangePassword(clsGlobal.CurrentUser.UserID);
           changePassword.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmlogin.Show();
            this.Close();
        }
        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Application_Types manage_Application_Type = new Manage_Application_Types();
            manage_Application_Type.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Test_Types manageTestTypes = new Manage_Test_Types();
            manageTestTypes.Show();
        }

        private void AddNewlocalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_New_Driving_Licenses add_New_Driving_Licenses = new Add_New_Driving_Licenses();
            add_New_Driving_Licenses.Show();
        }

        private void localDrivingLicenseAppsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Local_Driving_License_Apllications local_Driving_License_Apllications = new Local_Driving_License_Apllications();
            local_Driving_License_Apllications.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Drivers manage_Drivers = new Manage_Drivers();
            manage_Drivers.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_New_International_License add_New_International_License = new Add_New_International_License();
            add_New_International_License.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            International_Driving_License_Apllications international_Driving_License_Apllications = new International_Driving_License_Apllications();
            international_Driving_License_Apllications.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Renew_Driving_License renew_Driving_License = new Renew_Driving_License();
            renew_Driving_License.ShowDialog();
        }

        private void replacementForDamagedOrLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replacement_For_Damaged_OR_Lost replacement_For_Damaged_OR_Lost = new Replacement_For_Damaged_OR_Lost();
            replacement_For_Damaged_OR_Lost.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detain_License detain_License = new Detain_License();
            detain_License.ShowDialog();
        }

        private void realToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Release_Detained_License release_Detained_License = new Release_Detained_License();
            release_Detained_License.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Release_Detained_License release_Detained_License = new Release_Detained_License();
            release_Detained_License.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Detained_Licenses manage_Detained_Licenses = new Manage_Detained_Licenses();
            manage_Detained_Licenses.ShowDialog();
        }

    }
}
