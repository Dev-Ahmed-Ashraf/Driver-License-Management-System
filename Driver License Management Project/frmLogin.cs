using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using System;
using System.Windows.Forms;

namespace Driver_License_Management_Project
{
    public partial class frmLogin : Form
    {
        private int _LogInTrials = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), clsUtil.HashData(txtPassword.Text.Trim()));

            if (User != null)
            {
                if (chbRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPasswordInRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPasswordInRegistry("", "");
                }

                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.SaveInEventLog("Logged In Program Successfully", System.Diagnostics.EventLogEntryType.Information);
                clsGlobal.CurrentUser = User;
                //this.Hide();
                Main main = new Main(this);
                main.Show();
            }
            else
            {
                _LogInTrials++;
                Console.WriteLine("\a");
                txtUserName.Focus();
                MessageBox.Show("Invalid UserName / Password!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                clsGlobal.SaveInEventLog("Invalid UserName / Password!", System.Diagnostics.EventLogEntryType.Error);
                if (_LogInTrials == 3)
                {
                    clsGlobal.SaveInEventLog("Login was wrong three times", System.Diagnostics.EventLogEntryType.Error);
                    Application.Exit();
                }
                return;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentialFromRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chbRememberMe.Checked = true;
            }
            else
                chbRememberMe.Checked = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
