using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bussiness_Layer;

namespace Driver_License_Management_Project.People
{
    public partial class Person_Add_Edit_Info : UserControl
    {
        private clsPerson _Person;
        public Person_Add_Edit_Info()
        {
            InitializeComponent();
        }

        private void tbNationalNum_TextChanged(object sender, EventArgs e)
        {
            _Person = clsPerson.Find(tbNationalNum.Text);
            if (_Person != null)
            {
                errorProvider.ContainerControl.Visible = true;
                errorProvider.SetError(ActiveControl, "National Number already Exist");
                errorProvider.GetError(tbNationalNum);
            }
        }
    }
}
