using Bussiness_Layer;
using Driver_License_Management_Project.Applications.Driving_Licenses_Services;
using Driver_License_Management_Project.Applications.Driving_Licenses_Services.Local;
using Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication;
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

namespace Driver_License_Management_Project.Applications.ManageApplications
{
    public partial class Local_Driving_License_Apllications : Form
    {
        private DataTable _dtAllApplications;
        private DataTable _dtFilteredApplications;

        public Local_Driving_License_Apllications()
        {
            InitializeComponent();
            _InitializeFilterControls();
        }

        private void _InitializeFilterControls()
        {
            // Initialize the combo box with column names
            if (DGVlocalApps != null && DGVlocalApps.Columns.Count > 0)
            {
                combofilter.Items.Clear();
                combofilter.Items.Add("All Fields");
                foreach (DataGridViewColumn column in DGVlocalApps.Columns)
                {
                    combofilter.Items.Add(column.HeaderText);
                }
                combofilter.SelectedIndex = 0;
            }

            // Add event handlers
            combofilter.SelectedIndexChanged += combofilter_SelectedIndexChanged;
            tbfilter.TextChanged += tbfilter_TextChanged;
        }

        private void combofilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void tbfilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void _ApplyFilter()
        {
            if (_dtAllApplications == null) return;

            string filterText = tbfilter.Text.Trim().ToLower();
            string selectedField = combofilter.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(filterText))
            {
                DGVlocalApps.DataSource = _dtAllApplications;
                lblRecords.Text = DGVlocalApps.RowCount.ToString();
                return;
            }

            _dtFilteredApplications = _dtAllApplications.Clone();

            foreach (DataRow row in _dtAllApplications.Rows)
            {
                bool matchFound = false;

                if (selectedField == "All Fields")
                {
                    // Search in all columns
                    foreach (DataColumn col in _dtAllApplications.Columns)
                    {
                        if (row[col].ToString().ToLower().Contains(filterText))
                        {
                            matchFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    // Search in selected column
                    DataGridViewColumn selectedColumn = DGVlocalApps.Columns.Cast<DataGridViewColumn>()
                        .FirstOrDefault(col => col.HeaderText == selectedField);

                    if (selectedColumn != null)
                    {
                        string columnName = selectedColumn.DataPropertyName;
                        if (row[columnName].ToString().ToLower().Contains(filterText))
                        {
                            matchFound = true;
                        }
                    }
                }

                if (matchFound)
                {
                    _dtFilteredApplications.ImportRow(row);
                }
            }

            DGVlocalApps.DataSource = _dtFilteredApplications;
            lblRecords.Text = DGVlocalApps.RowCount.ToString();
        }

        private void _RefreshUsersList()
        {
            _dtAllApplications = clsLocalLicenseApps.GetAllLocalLicenseApps();
            _dtFilteredApplications = _dtAllApplications.Copy();
            DGVlocalApps.DataSource = _dtFilteredApplications;
            lblRecords.Text = DGVlocalApps.RowCount.ToString();
            
            // Refresh combo box items if needed
            if (combofilter.Items.Count == 0)
            {
                _InitializeFilterControls();
            }
        }

        private void Local_Driving_License_Apllications_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Add_New_Driving_Licenses add_New_Driving_Licenses = new Add_New_Driving_Licenses();
            add_New_Driving_Licenses.ShowDialog();
            Local_Driving_License_Apllications_Load(null, null); 
        }  
        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (_ValidateTestScheduling(applicationID, clsTestTypes.enTestType.VisionTest))
            {
                Vision_Test_Appointments vision_Test_Appointments = new Vision_Test_Appointments(applicationID, clsTestTypes.enTestType.VisionTest);
                vision_Test_Appointments.ShowDialog();
                _RefreshUsersList();
            }
        }

        private void sechduleWrittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (_ValidateTestScheduling(applicationID, clsTestTypes.enTestType.WrittenTest))
            {
                Vision_Test_Appointments vision_Test_Appointments = new Vision_Test_Appointments(applicationID, clsTestTypes.enTestType.WrittenTest);
                vision_Test_Appointments.ShowDialog();
                _RefreshUsersList();
            }
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (_ValidateTestScheduling(applicationID, clsTestTypes.enTestType.StreetTest))
            {
                Vision_Test_Appointments vision_Test_Appointments = new Vision_Test_Appointments(applicationID, clsTestTypes.enTestType.StreetTest);
                vision_Test_Appointments.ShowDialog();
                _RefreshUsersList();
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (!_ValidateApplicationOperation(applicationID, "issuing license"))
                return;

            clsLocalLicenseApps application = clsLocalLicenseApps.FindLocalLicenseInfoByID(applicationID);

            // Validate all tests are passed
            if (application.GetPassedTestCount() != 3)
            {
                MessageBox.Show("All tests (Vision, Written, and Street) must be passed before issuing a license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate application status
            if (application.AppStatus != clsApplications.enAppStatus.New)
            {
                MessageBox.Show("Can only issue license for applications with 'New' status", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate no active license exists
            if (application.IsLicenseIssue())
            {
                MessageBox.Show("This person already has an active license", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IssueLicense_First_Time issueLicense_First_Time = new IssueLicense_First_Time(applicationID);
            issueLicense_First_Time.ShowDialog();
            _RefreshUsersList();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Local_Driving_License_Info local_Driving_License_Info = new Local_Driving_License_Info((int)DGVlocalApps.CurrentRow.Cells[0].Value);
            local_Driving_License_Info.ShowDialog();

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_New_Driving_Licenses add_New_Driving_Licenses = new Add_New_Driving_Licenses((int)DGVlocalApps.CurrentRow.Cells[0].Value);
            add_New_Driving_Licenses.ShowDialog();
            _RefreshUsersList();
        }

        private void deletecApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (!_ValidateApplicationOperation(applicationID, "deleting application"))
                return;

            clsLocalLicenseApps application = clsLocalLicenseApps.FindLocalLicenseInfoByID(applicationID);

            // Validate application status
            if (application.AppStatus != clsApplications.enAppStatus.New)
            {
                MessageBox.Show("Can only delete applications with 'New' status", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate no tests have been taken
            if (application.GetPassedTestCount() > 0)
            {
                MessageBox.Show("Cannot delete application that has completed tests", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (application.Delete())
            {
                MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshUsersList();
            }
            else
            {
                MessageBox.Show("Could not delete application, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancleApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVlocalApps.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int applicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            if (!_ValidateApplicationOperation(applicationID, "cancelling application"))
                return;

            clsLocalLicenseApps application = clsLocalLicenseApps.FindLocalLicenseInfoByID(applicationID);

            // Validate application status
            if (application.AppStatus != clsApplications.enAppStatus.New)
            {
                MessageBox.Show("Can only cancel applications with 'New' status", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (application.CancleApplication())
            {
                MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshUsersList();
            }
            else
            {
                MessageBox.Show("Could not cancel application, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CMSLocalApps_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;
            clsLocalLicenseApps LocalLicenseApp = clsLocalLicenseApps.FindLocalLicenseInfoByID(LocalDrivingLicenseID);
            int NumOfPassedTests = (int)DGVlocalApps.CurrentRow.Cells[6].Value;
            bool LicenseExists = LocalLicenseApp.IsLicenseIssue();

            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New);
            editApplicationToolStripMenuItem.Enabled = !(LocalLicenseApp.GetPassedTestCount() > 0) && (LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New);
            //We only allow delete incase the application status is new not complete or Cancelled.
            deletecApplicationToolStripMenuItem.Enabled = LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New;
            deletecApplicationToolStripMenuItem.Enabled = !(LocalLicenseApp.GetPassedTestCount() > 0) && (LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New);
            //We only canel the applications with status=new.
            cancleApplicationToolStripMenuItem.Enabled = LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New;

            sceToolStripMenuItem.Enabled = !LicenseExists;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (NumOfPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;

            bool PassedVisionTest = LocalLicenseApp.DoesPassedThisTest(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = LocalLicenseApp.DoesPassedThisTest(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalLicenseApp.DoesPassedThisTest(clsTestTypes.enTestType.StreetTest);

            sceToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalLicenseApp.AppStatus == clsApplications.enAppStatus.New);


            if (sceToolStripMenuItem.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                sechduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                sechduleWrittToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                sechduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }
        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalLicenseApps localLicenseApps = clsLocalLicenseApps.FindLocalLicenseInfoByID((int)DGVlocalApps.CurrentRow.Cells[0].Value);
            LicenseDetails licenseDetails = new LicenseDetails(localLicenseApps.GetActiveLicenseID());
            licenseDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)DGVlocalApps.CurrentRow.Cells[0].Value;

            clsLocalLicenseApps LocalDrivingLicenseApplication =
                clsLocalLicenseApps.FindLocalLicenseInfoByID(LocalDrivingLicenseApplicationID);

            clsDriver Driver = clsDriver.FindByPersonID(LocalDrivingLicenseApplication.PersonID);
            if (Driver == null)
            {
                MessageBox.Show("This Person has not any Licenses [Local / International]", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(Driver.DriverID);
                showPersonLicenseHistory.ShowDialog();
            }
        }

        private bool _ValidateApplicationOperation(int applicationID, string operation)
        {
            if (applicationID <= 0)
            {
                MessageBox.Show($"Invalid Application ID for {operation}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            clsLocalLicenseApps application = clsLocalLicenseApps.FindLocalLicenseInfoByID(applicationID);
            if (application == null)
            {
                MessageBox.Show($"Application not found for {operation}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool _ValidateTestScheduling(int applicationID, clsTestTypes.enTestType testType)
        {
            if (!_ValidateApplicationOperation(applicationID, "test scheduling"))
                return false;

            clsLocalLicenseApps application = clsLocalLicenseApps.FindLocalLicenseInfoByID(applicationID);

            // Validate application status
            if (application.AppStatus != clsApplications.enAppStatus.New)
            {
                MessageBox.Show("Can only schedule tests for applications with 'New' status", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate test sequence
            switch (testType)
            {
                case clsTestTypes.enTestType.WrittenTest:
                    if (!application.DoesPassedThisTest(clsTestTypes.enTestType.VisionTest))
                    {
                        MessageBox.Show("Must pass Vision Test before scheduling Written Test", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    if (!application.DoesPassedThisTest(clsTestTypes.enTestType.VisionTest) || 
                        !application.DoesPassedThisTest(clsTestTypes.enTestType.WrittenTest))
                    {
                        MessageBox.Show("Must pass both Vision and Written Tests before scheduling Street Test", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
