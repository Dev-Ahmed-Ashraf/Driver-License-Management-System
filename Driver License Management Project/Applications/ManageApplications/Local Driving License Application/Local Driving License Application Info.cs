using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services.Local
{
    public partial class Local_Driving_License_Info : Form
    {
        int _LocalDrivingLicenseID;
        public Local_Driving_License_Info(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseAppID;  
        }

        private void Local_Driving_License_Application_Info_Load(object sender, EventArgs e)
        {
            drivingLicenseAppInfo1.FillLabelsInControl(_LocalDrivingLicenseID);
        }
    }
}
