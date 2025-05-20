using Driver_License_Management_Project.Applications.ManageApplications.International_Driving_License_Applicaion.Controls;
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
    public partial class Show_International_License_Info : Form
    {
        int _InternationalLicenseID;
        public Show_International_License_Info(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void Show_International_License_Info_Load(object sender, EventArgs e)
        {
            internationalLicenseInfo1.LoadData(_InternationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
