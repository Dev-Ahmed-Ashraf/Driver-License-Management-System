using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using Driver_License_Management_Project.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Driver_License_Management_Project
{
    public partial class AddEditPerson : Form
    {
        public delegate void Manage_People_Click(object sender, int PersonID);
        public event Manage_People_Click DataBack;
        public enum enMode { AddNewMode = 0, UpdateMode = 1 };
        private enMode Mode;

        private int _PersonID = -1;
        private clsPerson _Person;

        public AddEditPerson()
        {
            InitializeComponent();
            Mode = enMode.AddNewMode;
            
            // Enable validation on form
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            
            // Add validation events
            tbFirstName.Validating += ValidateEmptyTextBox;
            tbLastName.Validating += ValidateEmptyTextBox;
            tbNationalNum.Validating += tbNationalNum_Validating;
            tbEmail.Validating += tbEmail_Validating;
            tbPhone.Validating += ValidatePhoneNumber;
            txtAddress.Validating += ValidateEmptyTextBox;
            dateTimePicker1.Validating += ValidateDateOfBirth;
        }
        public AddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;

            Mode = enMode.UpdateMode;
        }

        private void SetupComboBoxWithData()
        {
            DataTable CountriesDataTable = clsCountry.GetCountries();
            comboBoxCountries.DataSource = CountriesDataTable;
            comboBoxCountries.DisplayMember = "CountryName";
            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString("Egypt");
        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            SetupComboBoxWithData();

            if (Mode == enMode.AddNewMode)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person Info";
            }

            //set default image for the person.
            if (rbMale.Checked)
                PbPersonImage.Image = Resources.Male;
            else
                PbPersonImage.Image = Resources.Female;

            //hide/show the remove linke incase there is no image for the person.
            linkLabelRemoveImage.Visible = (PbPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;

            //should not allow adding age more than 100 years
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to Egypt
            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString("Egypt");

            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbThirdName.Text = "";
            tbLastName.Text = "";
            tbNationalNum.Text = "";
            rbMale.Checked = true;
            tbPhone.Text = "";
            tbEmail.Text = "";
            txtAddress.Text = "";
        }
        private void _LoadPersonImage()
        {
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                PbPersonImage.ImageLocation = ImagePath;
            else
            {
                if (rbMale.Checked == true)
                    PbPersonImage.Image = Resources.Male;
                if (rbFemale.Checked == true)
                    PbPersonImage.Image = Resources.Female;
            }
        }
        private void _LoadPersonInfo()
        {
            _Person = clsPerson.Find(_PersonID);
            lblPersonID.Text = _PersonID.ToString();
            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            tbNationalNum.Text = _Person.NationalNum;
            dateTimePicker1.Text = _Person.DateOfBirth.ToShortDateString();
            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString(_Person.CountryInfo.CountryName);
            if (_Person.Gender == "Male")
            {
                rbMale.Checked = true;
            }
            if (_Person.Gender == "Female")
            {
                rbFemale.Checked = true;
            }
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            _LoadPersonImage();
        }
        private void AddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (Mode == enMode.UpdateMode)
            {
                this.Text = "Edit Person Info"; 
                _LoadPersonInfo();
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            PbPersonImage.Image = Resources.Male;
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            PbPersonImage.Image = Resources.Female;
        }

        private void linkLabelSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to allow only image files
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedImagePath = openFileDialog.FileName;

                // Load the image and display it
                PbPersonImage.ImageLocation = selectedImagePath;
                linkLabelRemoveImage.Visible = true;
            }
        }
        private void linkLabelRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PbPersonImage.Image = null;
            PbPersonImage.ImageLocation = null;
            if (rbMale.Checked == true)
                PbPersonImage.Image = Resources.Male;
            if (rbFemale.Checked == true)
                PbPersonImage.Image = Resources.Female;

            linkLabelRemoveImage.Visible = false;
        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != PbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (PbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = PbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        PbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all controls on the form
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the validation errors before saving.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
                return;

            if (MessageBox.Show("Do you Sure that you want to Save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            
                _Person.FirstName = tbFirstName.Text.Trim();
                _Person.SecondName = tbSecondName.Text.Trim();
                _Person.ThirdName = tbThirdName.Text.Trim();
                _Person.LastName = tbLastName.Text.Trim();
                _Person.Email = tbEmail.Text.Trim();
                _Person.Phone = tbPhone.Text.Trim();
                _Person.NationalNum = tbNationalNum.Text.Trim();
                _Person.Address = txtAddress.Text.Trim();
                _Person.DateOfBirth = dateTimePicker1.Value;
                _Person.NationalityID = clsCountry.Find(comboBoxCountries.Text).ID;
            if (PbPersonImage.ImageLocation != null)
                _Person.ImagePath = PbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
            if (rbMale.Checked == true)
                    _Person.Gender = "Male";
            if (rbFemale.Checked == true)
                    _Person.Gender = "Female";


                if (_Person.Save())
                {
                    lblPersonID.Text = _Person.ID.ToString();
                    Mode = enMode.UpdateMode;
                    lblTitle.Text = "Update Person Info";

                    MessageBox.Show("Person Saved Succesfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataBack?.Invoke(this, _Person.ID);
                }
                else
                    MessageBox.Show(" Data NOT Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            
            // Skip validation for optional fields
            if (textBox == tbSecondName || textBox == tbThirdName || textBox == tbEmail)
            {
                errorProvider1.SetError(textBox, null);
                return;
            }

            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(textBox, null);
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (tbEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(tbEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(tbEmail, null);
            };

        }

        private void tbNationalNum_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNationalNum.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNum, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(tbNationalNum, null);
            }

            //Make sure the national number is not used by another person
            if (tbNationalNum.Text.Trim() != _Person.NationalNum && clsPerson.IsExist(tbNationalNum.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNum, "National Number is used for another person!");
            }
            else
            {
                errorProvider1.SetError(tbNationalNum, null);
            }
        }

        private void ValidatePhoneNumber(object sender, CancelEventArgs e)
        {
            TextBox phoneTextBox = (TextBox)sender;
            
            // Allow empty phone number
            if (string.IsNullOrEmpty(phoneTextBox.Text.Trim()))
            {
                errorProvider1.SetError(phoneTextBox, null);
                return;
            }

            // Validate phone number format (allows international format)
            string phonePattern = @"^\+?[0-9]{8,15}$";
            if (!Regex.IsMatch(phoneTextBox.Text.Trim(), phonePattern))
            {
                e.Cancel = true;
                errorProvider1.SetError(phoneTextBox, "Please enter a valid phone number (8-15 digits, optionally starting with +)");
            }
            else
            {
                errorProvider1.SetError(phoneTextBox, null);
            }
        }

        private void ValidateDateOfBirth(object sender, CancelEventArgs e)
        {
            DateTimePicker datePicker = (DateTimePicker)sender;
            
            if (datePicker.Value > DateTime.Now.AddYears(-18))
            {
                e.Cancel = true;
                errorProvider1.SetError(datePicker, "Person must be at least 18 years old");
            }
            else if (datePicker.Value < DateTime.Now.AddYears(-100))
            {
                e.Cancel = true;
                errorProvider1.SetError(datePicker, "Person cannot be older than 100 years");
            }
            else
            {
                errorProvider1.SetError(datePicker, null);
            }
        }
    }
}
