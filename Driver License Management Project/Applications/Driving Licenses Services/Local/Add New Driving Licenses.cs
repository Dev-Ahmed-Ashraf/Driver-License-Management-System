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
using static Driver_License_Management_Project.Users.AddEditUser;

namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services
{
    public partial class Add_New_Driving_Licenses : Form
    {
        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;

        int _LocalDrivingLicenseAppID;
        clsAppTypes AppType;
        clsLocalLicenseApps LocalLicenseApp;
        public Add_New_Driving_Licenses()
        {
            InitializeComponent();
            LocalLicenseApp = new clsLocalLicenseApps();

            Mode = enMode.AddNew;
        }
        public Add_New_Driving_Licenses(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();    
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;

            Mode = enMode.Update;
        }
        private void _SetupComboBoxWithLicenseClasses()
        {
            DataTable LicenseClasses = clsLicenseClasses.GetAllLicenseClasses();
            cbLicenseClass.DataSource = LicenseClasses;
            cbLicenseClass.DisplayMember = "ClassName";
        }
        private void _FillLabels()
        {
            AppType = clsAppTypes.Find((int)clsApplications.enAppTypeID.NewLocalDrivingLicense);
            lblApplicationDate.Text = DateTime.Now.ToShortDateString().ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblFees.Text = AppType.AppFees.ToString();
            _SetupComboBoxWithLicenseClasses();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                TapApplication.Enabled = true;
                TapControl.SelectedTab = TapControl.TabPages["TapApplication"];
                return;
            }

            if (personInfoWithSearch1.PersonID != -1)
            {               
                btnSave.Enabled = true;
                TapApplication.Enabled = true;
                TapControl.SelectedTab = TapControl.TabPages["TapApplication"];
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personInfoWithSearch1.FilterFocus();
            }
        }

        private void Add_New_Driving_Licenses_Load(object sender, EventArgs e)
        {            
            if(Mode == enMode.Update)              
            {
                _LoadData();
                return;
            }
                         
            personInfoWithSearch1.FilterFocus();
            TapApplication.Enabled = false;
            _FillLabels();
            btnSave.Enabled = false;
        }
        private void _LoadData()
        {
            LocalLicenseApp = clsLocalLicenseApps.FindLocalLicenseInfoByID(_LocalDrivingLicenseAppID);

            if( LocalLicenseApp == null )
            {
                MessageBox.Show("No Local Licenss with ID = " + _LocalDrivingLicenseAppID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            lblTitle.Text = "Update Local Driving License Application";
            this.Text = lblTitle.Text;
            personInfoWithSearch1.LoadPersonInfo(LocalLicenseApp.PersonID);
            personInfoWithSearch1.FilterEnabled = false;
            btnSave.Enabled = true;
            lblLocalDrivingLicebseApplicationID.Text = LocalLicenseApp.LocalDrivingLicenseApp_ID.ToString();
            lblApplicationDate.Text = LocalLicenseApp.ApplicationDate.ToString();
            _SetupComboBoxWithLicenseClasses();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClasses.FindLicenseClassInfoByID(LocalLicenseApp.LicenseClass_ID).ClassName);
            lblFees.Text = LocalLicenseApp.PaidFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            DateTime PersonBirth = clsPerson.Find(personInfoWithSearch1.PersonID).DateOfBirth;
            int MinimumAllowedAge = clsLicenseClasses.FindLicenseClassInfoByID(cbLicenseClass.SelectedIndex + 1).MinimumAllowedAge;
            if (PersonBirth.AddYears(MinimumAllowedAge) > DateTime.Now)
            {
                MessageBox.Show("This Age doesn`t meet the conditions", "You Are Baby :-)", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (clsLocalLicenseApps.IfPersonHasThisLicense(personInfoWithSearch1.PersonID, cbLicenseClass.Text))
            {
                MessageBox.Show("This Person Has Selected License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            
            if (MessageBox.Show("Are you Sure that you want to Save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            LocalLicenseApp.PersonID = personInfoWithSearch1.PersonID;
            LocalLicenseApp.LicenseClass_ID = cbLicenseClass.SelectedIndex + 1;
            LocalLicenseApp.PaidFees = Convert.ToSingle(lblFees.Text);
            LocalLicenseApp.AppStatus = clsApplications.enAppStatus.New;
            LocalLicenseApp.ApplicationDate = DateTime.Now;
            LocalLicenseApp.AppType_ID = 1;
            LocalLicenseApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            LocalLicenseApp.LastStatusDate = DateTime.Now;
            if (LocalLicenseApp.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = LocalLicenseApp.LocalDrivingLicenseApp_ID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                this.Text  = lblTitle.Text;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
