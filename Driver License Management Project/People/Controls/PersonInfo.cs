using Bussiness_Layer;
using Driver_License_Management_Project.Properties;
using System.IO;
using System.Windows.Forms;

namespace Driver_License_Management_Project
{
    public partial class PersonInfo : UserControl
    {
        private clsPerson _Person;
#pragma warning disable CS0414 // The field 'PersonInfo._PersonID' is assigned but its value is never used
        private int _PersonID = -1;
#pragma warning restore CS0414 // The field 'PersonInfo._PersonID' is assigned but its value is never used
        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public PersonInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender.ToLower() == "female")
                pictureBox1.Image = Resources.Female;
            else
                pictureBox1.Image = Resources.Male;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FillLabels()
        {
            _PersonID = _Person.ID;
            lblPersonID2.Text = _Person.ID.ToString();
            lblName2.Text = _Person.FullName;
            lblNationalNum2.Text = _Person.NationalNum;
            lblDateOfBirth2.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry2.Text = _Person.CountryInfo.CountryName;
            lblGender2.Text = _Person.Gender;
            lblPhone2.Text = _Person.Phone;
            lblEmail2.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            _LoadPersonImage();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person Whit Person ID = " + PersonID + " NOT Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillLabels();
        }
        public void LoadPersonInfo(string NationalNumber)
        {
            _Person = clsPerson.Find(NationalNumber);
            if (_Person == null)
            {
                MessageBox.Show("Person Whit National Number = " + NationalNumber + " NOT Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillLabels();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(_Person.ID);
            addEditPerson.ShowDialog();
            LoadPersonInfo(_Person.ID);
        }   

        public void ResetLabels()
        {
            lblPersonID2.Text = "[????]";
            lblName2.Text = "[????]";
            lblGender2.Text = "[????]";
            lblEmail2.Text = "[????]";
            lblDateOfBirth2.Text = "[????]";
            lblCountry2.Text = "[????]";
            txtAddress.Text = "[????]";
            lblNationalNum2.Text = "[????]";
            lblPhone2.Text = "[????]";
            pictureBox1.Image = null;
        }
    }
}
