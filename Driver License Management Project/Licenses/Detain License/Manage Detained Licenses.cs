using Bussiness_Layer;
using Driver_License_Management_Project.Applications.Driving_Licenses_Services.ReleaseDetainedLicense;
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
    public partial class Manage_Detained_Licenses : Form
    {
        public Manage_Detained_Licenses()
        {
            InitializeComponent();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            Detain_License detain_License = new Detain_License();
            detain_License.ShowDialog();
            _RefreshDGV();
        }
        private void _RefreshDGV()
        {
            DGVdetainedLicenses.DataSource = clsDetainedLicense.GetAllDetainedLicenses();
            lblRecords.Text = DGVdetainedLicenses.RowCount.ToString();
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            Release_Detained_License release_Detained_License = new Release_Detained_License();
            release_Detained_License.ShowDialog();
            _RefreshDGV();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CMSdetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            releaseThisLicenseToolStripMenuItem.Enabled = !(bool)DGVdetainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.Find((int)DGVdetainedLicenses.CurrentRow.Cells[1].Value);
            Person_Details_Form person_Details_Form = new Person_Details_Form(License.DriverInfo.PersonID);
            person_Details_Form.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.Find((int)DGVdetainedLicenses.CurrentRow.Cells[1].Value);
            LicenseDetails licenseDetails = new LicenseDetails(License.LicenseID);
            licenseDetails.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.Find((int)DGVdetainedLicenses.CurrentRow.Cells[1].Value);
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(License.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void releaseThisLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.Find((int)DGVdetainedLicenses.CurrentRow.Cells[1].Value);
            Release_Detained_License release_Detained_License = new Release_Detained_License(License.LicenseID);
            release_Detained_License.ShowDialog();
            _RefreshDGV();
        }
        private void Manage_Detained_Licenses_Activated(object sender, EventArgs e)
        {
            _RefreshDGV();
        }
    }
}
