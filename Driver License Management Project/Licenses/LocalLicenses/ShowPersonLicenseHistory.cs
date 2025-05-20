using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Licenses.LocalLicenses
{
    public partial class ShowPersonLicenseHistory : Form
    {
        int _DriverID;
        int _PersonID;
        private clsDriver _DriverInfo;
        public ShowPersonLicenseHistory(int driverID)
        {
            InitializeComponent();
            _DriverID = driverID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            _DriverInfo = clsDriver.FindByDriverID(_DriverID);
            _PersonID = _DriverInfo.PersonID;
            personInfo1.LoadPersonInfo(_PersonID);
            driver_License_History1.FilllocalDGV(_DriverID);
            driver_License_History1.FillInternationalDGV(_DriverID);
        }
    }
}
