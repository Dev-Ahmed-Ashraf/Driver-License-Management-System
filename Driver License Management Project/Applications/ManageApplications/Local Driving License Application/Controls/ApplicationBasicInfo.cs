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

namespace Driver_License_Management_Project.Applications.controls
{
    public partial class ApplicationBasicInfo : UserControl
    {
        clsApplications _Application;
        public ApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void FillLabelsInAppCtrl(int AppID)
        {
            _Application = clsApplications.FindAppInfoByID(AppID);
            lblAppID.Text = _Application.Application_ID.ToString();
            lblStatus.Text = _Application.AppStatus.ToString();
            lblAppType.Text = _Application.AppTypeInfo.AppTitle;
            lblPerson.Text = _Application.PersonInfo.FullName;
            lblFees.Text = _Application.PaidFees.ToString();
            lblAppDate.Text = _Application.ApplicationDate.ToString();
            lblLastDate.Text = _Application.LastStatusDate.ToString();
            lblUser.Text = _Application.CreatedByUserInfo.UserName;
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person_Details_Form person_Details_Form = new Person_Details_Form(_Application.PersonID);
            person_Details_Form.ShowDialog();
        }
    }
}
