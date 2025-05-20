using Bussiness_Layer;
using Driver_License_Management_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project
{
    public partial class Manage_Users : Form
    {
        private DataTable _dtAllUsers;
        private DataTable _dtFilteredUsers;

        public Manage_Users()
        {
            InitializeComponent();
            _InitializeFilterControls();
        }

        private void _InitializeFilterControls()
        {
            // Initialize the combo box with specific column names
            if (DGVUsers != null && DGVUsers.Columns.Count > 0)
            {
                combofilter.Items.Clear();
                combofilter.Items.Add("All Fields");
                combofilter.Items.Add("User ID");
                combofilter.Items.Add("Username");
                combofilter.Items.Add("Full Name");
                combofilter.Items.Add("Is Active");
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
            if (_dtAllUsers == null) return;

            string filterText = tbfilter.Text.Trim().ToLower();
            string selectedField = combofilter.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(filterText))
            {
                DGVUsers.DataSource = _dtAllUsers;
                lblNumOfRecords.Text = DGVUsers.RowCount.ToString();
                return;
            }

            _dtFilteredUsers = _dtAllUsers.Clone();

            foreach (DataRow row in _dtAllUsers.Rows)
            {
                bool matchFound = false;

                if (selectedField == "All Fields")
                {
                    // Search in all specific columns
                    if (_ContainsText(row, "UserID", filterText) ||
                        _ContainsText(row, "Username", filterText) ||
                        _ContainsText(row, "FullName", filterText) ||
                        _ContainsText(row, "IsActive", filterText))
                    {
                        matchFound = true;
                    }
                }
                else
                {
                    // Search in the selected specific column
                    string columnName = _GetColumnName(selectedField);
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        if (columnName == "IsActive")
                        {
                            // Special handling for IsActive field
                            bool isActive;
                            if (bool.TryParse(row[columnName].ToString(), out isActive))
                            {
                                if (filterText == "true" || filterText == "yes" || filterText == "1")
                                {
                                    matchFound = isActive;
                                }
                                else if (filterText == "false" || filterText == "no" || filterText == "0")
                                {
                                    matchFound = !isActive;
                                }
                            }
                        }
                        else
                        {
                            matchFound = _ContainsText(row, columnName, filterText);
                        }
                    }
                }

                if (matchFound)
                {
                    _dtFilteredUsers.ImportRow(row);
                }
            }

            DGVUsers.DataSource = _dtFilteredUsers;
            lblNumOfRecords.Text = DGVUsers.RowCount.ToString();
        }

        private string _GetColumnName(string displayName)
        {
            switch (displayName)
            {
                case "User ID":
                    return "UserID";
                case "Username":
                    return "Username";
                case "Full Name":
                    return "FullName";
                case "Is Active":
                    return "IsActive";
                default:
                    return string.Empty;
            }
        }

        private bool _ContainsText(DataRow row, string columnName, string searchText)
        {
            if (!row.Table.Columns.Contains(columnName)) return false;
            
            object value = row[columnName];
            if (value == null) return false;

            return value.ToString().ToLower().Contains(searchText);
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            _dtFilteredUsers = _dtAllUsers.Copy();
            DGVUsers.DataSource = _dtFilteredUsers;
            lblNumOfRecords.Text = DGVUsers.RowCount.ToString();
            
            // Refresh combo box items if needed
            if (combofilter.Items.Count == 0)
            {
                _InitializeFilterControls();
            }
        }

        private void Manage_Users_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Details user_Details = new User_Details((int)DGVUsers.CurrentRow.Cells[0].Value);
            user_Details.ShowDialog();
            _RefreshUsersList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser(((int)DGVUsers.CurrentRow.Cells[0].Value));
            addEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this User?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser((int)DGVUsers.CurrentRow.Cells[0].Value))
                {
                    _RefreshUsersList();
                    MessageBox.Show("Person Deleted Successfully");
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword((int)DGVUsers.CurrentRow.Cells[0].Value);
            changePassword.ShowDialog();
        }
    }
}
