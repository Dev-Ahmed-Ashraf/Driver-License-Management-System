using Bussiness_Layer;
using Driver_License_Management_Project.Applications.ManageApplications.International_Driving_License_Applicaion;
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

namespace Driver_License_Management_Project.Licenses.controls
{
    public partial class Driver_License_History : UserControl
    {
        public Driver_License_History()
        {
            InitializeComponent();
        }

        public void FilllocalDGV(int DriverID)
        {
            DGVLocal.DataSource = clsLicense.GetAllLicenses(DriverID);
            lblLocalRecords.Text = DGVLocal.RowCount.ToString();
        }
        public void FillInternationalDGV(int DriverID)
        {
            DGVInternational.DataSource = clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
            lblInternationalRecords.Text = DGVInternational.RowCount.ToString();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseDetails licenseDetails = new LicenseDetails((int)DGVLocal.CurrentRow.Cells[0].Value);
            licenseDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show_International_License_Info show_International_License_Info = new Show_International_License_Info((int)DGVInternational.CurrentRow.Cells[0].Value);
            show_International_License_Info.ShowDialog();
        }
    }
}
