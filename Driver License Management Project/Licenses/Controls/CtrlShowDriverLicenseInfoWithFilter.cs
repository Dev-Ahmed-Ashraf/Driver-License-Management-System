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

namespace Driver_License_Management_Project.Licenses.Controls
{
    public partial class CtrlShowDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return show_License1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return show_License1.LicenseInfo; }
        }
        public CtrlShowDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            txtSearch.Text = LicenseID.ToString();
            show_License1.LoadData(LicenseID);
            _LicenseID = show_License1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }
        public void FilterFocus()
        {
            txtSearch.Focus();
        }
        private void txtSearch_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSearch, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtSearch, null);
            }
        }

        private void txtSearch_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            //this will allow only digits 
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
                return;

            }
            _LicenseID = int.Parse(txtSearch.Text);
            LoadLicenseInfo(_LicenseID);
        }
    }
}
