using Bussiness_Layer;
using Driver_License_Management_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Users.Controls
{
    public partial class UserInfo : UserControl
    {
        clsUser _User;
        public UserInfo()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.Find(UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            personInfo1.LoadPersonInfo(_User.PersonID);
            _FillLoginLabels();
        }
        private void _FillLoginLabels()
        {
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            if (_User.IsActive == false)
                lblActive.Text = "No";
            else 
                lblActive.Text = "Yes";
        }      
    }
}
