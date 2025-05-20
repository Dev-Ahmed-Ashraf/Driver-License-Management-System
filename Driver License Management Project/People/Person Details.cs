using System;
using System.Windows.Forms;

namespace Driver_License_Management_Project
{
    public partial class Person_Details_Form : Form
    {
        int _PersonID;
        public Person_Details_Form(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        private void Person_Details_Form_Load(object sender, EventArgs e)
        {
            personInfo1.LoadPersonInfo(_PersonID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
