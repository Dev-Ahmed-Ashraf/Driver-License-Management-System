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

namespace Driver_License_Management_Project.People.Controls
{
    public partial class PersonInfoWithSearch : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
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
                gbSearch.Enabled = _FilterEnabled;
            }
        }

        public PersonInfoWithSearch()
        {
            InitializeComponent();
        }

        private int _PersonID = -1;
        public int PersonID
        {
            get { return personInfo1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return personInfo1.SelectedPersonInfo; }
        }


        private void FindNow()
        {
            switch (cbSearchBy.Text)
            {
                case "Person ID":
                    personInfo1.LoadPersonInfo(int.Parse(txtSearch.Text));

                    break;

                case "National Number":
                    personInfo1.LoadPersonInfo(txtSearch.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(personInfo1.PersonID);
        }
        public void LoadPersonInfo(int PersonID)
        {

            cbSearchBy.SelectedIndex = 0;
            txtSearch.Text = PersonID.ToString();
            FindNow();

        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren()) 
                return;

            FindNow();
        }

        private void PersonInfoWithSearch_Load(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            txtSearch.Focus();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbSearchBy.SelectedIndex = 0;
            txtSearch.Text = PersonID.ToString();
            personInfo1.LoadPersonInfo(PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();
            addEditPerson.DataBack += DataBackEvent;
            addEditPerson.ShowDialog();
        }

        public void FilterFocus()
        {
            txtSearch.Focus();
        }

        private void txtSearch_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text.Trim()))
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
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbSearchBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
