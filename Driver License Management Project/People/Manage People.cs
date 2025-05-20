using Bussiness_Layer;
using Driver_License_Management_Project.GlobalClasses;
using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Linq;

namespace Driver_License_Management_Project
{
    public partial class Manage_People : Form
    {
        private DataTable _dtAllPeople;
        private DataTable _dtFilteredPeople;

        public Manage_People()
        {
            InitializeComponent();
            _InitializeFilterControls();
        }

        private void _InitializeFilterControls()
        {
            // Initialize the combo box with column names
            if (DGVpeople != null && DGVpeople.Columns.Count > 0)
            {
                combofilter.Items.Clear();
                combofilter.Items.Add("All Fields");
                foreach (DataGridViewColumn column in DGVpeople.Columns)
                {
                    combofilter.Items.Add(column.HeaderText);
                }
                combofilter.SelectedIndex = 0;
            }

            // Add event handlers
            combofilter.SelectedIndexChanged += combofilter_SelectedIndexChanged;
            tbfilter.TextChanged += tbfilter_TextChanged;
        }

        private void combofilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void tbfilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void _ApplyFilter()
        {
            if (_dtAllPeople == null) return;

            string filterText = tbfilter.Text.Trim().ToLower();
            string selectedField = combofilter.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(filterText))
            {
                DGVpeople.DataSource = _dtAllPeople;
                lblNumOfRecords.Text = DGVpeople.RowCount.ToString();
                return;
            }

            _dtFilteredPeople = _dtAllPeople.Clone();

            foreach (DataRow row in _dtAllPeople.Rows)
            {
                bool matchFound = false;

                if (selectedField == "All Fields")
                {
                    // Search in all columns
                    foreach (DataColumn col in _dtAllPeople.Columns)
                    {
                        if (row[col].ToString().ToLower().Contains(filterText))
                        {
                            matchFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    // Search in selected column
                    DataGridViewColumn selectedColumn = DGVpeople.Columns.Cast<DataGridViewColumn>()
                        .FirstOrDefault(col => col.HeaderText == selectedField);

                    if (selectedColumn != null)
                    {
                        string columnName = selectedColumn.DataPropertyName;
                        if (row[columnName].ToString().ToLower().Contains(filterText))
                        {
                            matchFound = true;
                        }
                    }
                }

                if (matchFound)
                {
                    _dtFilteredPeople.ImportRow(row);
                }
            }

            DGVpeople.DataSource = _dtFilteredPeople;
            lblNumOfRecords.Text = DGVpeople.RowCount.ToString();
        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtFilteredPeople = _dtAllPeople.Copy();
            DGVpeople.DataSource = _dtFilteredPeople;
            lblNumOfRecords.Text = DGVpeople.RowCount.ToString();
            
            // Refresh combo box items if needed
            if (combofilter.Items.Count == 0)
            {
                _InitializeFilterControls();
            }
        }

        public void Manage_People_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Person_Details_Form person_Details_Form = new Person_Details_Form((int)DGVpeople.CurrentRow.Cells[0].Value);
            person_Details_Form.ShowDialog();
            _RefreshPeopleList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();
            addEditPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();
            addEditPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson((int)DGVpeople.CurrentRow.Cells[0].Value);
            addEditPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            clsPerson _Person = clsPerson.Find((int)DGVpeople.CurrentRow.Cells[0].Value);
            if (clsPerson.DeletePerson(_Person.ID))
            {
                clsUtil.DeleteOldImageFromDisk(_Person.ImagePath);
                MessageBox.Show("Person Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshPeopleList();
            }
            else
                MessageBox.Show("Person NOT Deleted Because it is linked to applications and licenses", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }
    }     
}
