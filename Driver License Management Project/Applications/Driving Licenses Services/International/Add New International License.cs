using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Licenses;
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
    public partial class Add_New_International_License : Form
    {
        int _InternationalLicenseID;
        clsLicense License;
        public Add_New_International_License()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_New_International_License_Load(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = (DateTime.Now.AddYears(1)).ToShortDateString();
            lblFees.Text = clsAppTypes.Find((int)clsApplications.enAppTypeID.NewInternationalLicense).AppFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_International_License_Info show_International_License_Info = new Show_International_License_Info(_InternationalLicenseID);
            show_International_License_Info.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1, InternationalLicenseID = -1;
            bool IsIssued = ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IssueInternationalLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID, ref InternationalLicenseID);

            if (IsIssued == false)
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicenseID.ToString();
            _InternationalLicenseID = InternationalLicenseID;
            MessageBox.Show("International License Issued Successfully with ID = " + InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlShowDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void Add_New_International_License_Activated(object sender, EventArgs e)
        {
            ctrlShowDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void ctrlShowDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = ctrlShowDriverLicenseInfoWithFilter1.LicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }
            if (ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This Local License is NOT Active", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("This Local License is not [ Class 3 - Ordinary driving license ]", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            int InternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlShowDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            if (InternationalLicenseID != -1)
            {
                MessageBox.Show("This Driver already has an Active International license with ID = " + InternationalLicenseID, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                _InternationalLicenseID = InternationalLicenseID;
                llShowLicenseInfo.Enabled = true;
                return;
            }

            btnIssue.Enabled = true;
        }
    }
}
