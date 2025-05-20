using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.Applications.ManageTestTypes
{
    public partial class Edit_Test_Type : Form
    {
        int _TestID = -1;
        clsTestTypes _TestTypes;
        public Edit_Test_Type(int ID)
        {
            InitializeComponent();
            _TestID = ID;
        }

        private void Edit_Test_Type_Load(object sender, EventArgs e)
        {
            _TestTypes = clsTestTypes.Find(_TestID);
            lblID.Text = _TestID.ToString();
            txtAppTitle.Text = _TestTypes.Title;
            rtxtDescription.Text = _TestTypes.Description;
            txtAppFees.Text = _TestTypes.Fees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Do you Sure that you want to Save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _TestTypes.Title = txtAppTitle.Text.Trim();
                _TestTypes.Description = rtxtDescription.Text.Trim();
                _TestTypes.Fees = Convert.ToSingle(txtAppFees.Text.Trim());

                if (_TestTypes.UpdateTestType())
                {
                    MessageBox.Show("Test Type Updated Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void rtxtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtAppTitle, null);
            };
        }

        private void txtAppFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtAppFees, null);
            };
           

            if (!clsValidation.IsNumber(txtAppFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtAppFees, null);
            };
        }

        private void txtAppTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtAppTitle, null);
            };
        }

        private void txtAppFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.';

            // Allowing only one decimal point
            e.Handled = (e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1);
        }
    }
}
