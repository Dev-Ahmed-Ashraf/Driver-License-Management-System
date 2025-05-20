using Bussiness_Layer;
using Data_Access_Layer;
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

namespace Driver_License_Management_Project.Applications.ManageApplications.International_Driving_License_Applicaion
{
    public partial class International_Driving_License_Apllications : Form
    {
        public International_Driving_License_Apllications()
        {
            InitializeComponent();
        }
        private void _RefreshDGV()
        {
            DGVInternationalApps.DataSource = clsInternationalLicense.GetAllInternationalLicenses();
            lblRecords.Text = DGVInternationalApps.RowCount.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void International_Driving_License_Apllications_Load(object sender, EventArgs e)
        {
            _RefreshDGV();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find((int)DGVInternationalApps.CurrentRow.Cells[0].Value);
            Person_Details_Form person_Details_Form = new Person_Details_Form((InternationalLicense.DriverInfo.PersonID));
            person_Details_Form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find((int)DGVInternationalApps.CurrentRow.Cells[0].Value);
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(InternationalLicense.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_International_License_Info show_International_License_Info = new Show_International_License_Info((int)DGVInternationalApps.CurrentRow.Cells[0].Value);
            show_International_License_Info.ShowDialog();
        }
    }
}
