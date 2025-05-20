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

namespace Driver_License_Management_Project.Drivers
{
    public partial class Manage_Drivers : Form
    {
        private DataTable _dtAllDrivers;
        private DataTable _dtFilteredDrivers;

        public Manage_Drivers()
        {
            InitializeComponent();
            _InitializeFilterControls();
        }

        private void _InitializeFilterControls()
        {
            // Initialize the combo box with specific column names
            if (DGVdrivers != null && DGVdrivers.Columns.Count > 0)
            {
                combofilter.Items.Clear();
                combofilter.Items.Add("All Fields");
                combofilter.Items.Add("Driver ID");
                combofilter.Items.Add("Full Name");
                combofilter.Items.Add("National ID Number");
                combofilter.Items.Add("Number of Active Licenses");
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
            if (_dtAllDrivers == null) return;

            string filterText = tbfilter.Text.Trim().ToLower();
            string selectedField = combofilter.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(filterText))
            {
                DGVdrivers.DataSource = _dtAllDrivers;
                lblRecords.Text = DGVdrivers.RowCount.ToString();
                return;
            }

            _dtFilteredDrivers = _dtAllDrivers.Clone();

            foreach (DataRow row in _dtAllDrivers.Rows)
            {
                bool matchFound = false;

                if (selectedField == "All Fields")
                {
                    // Search in all specific columns
                    if (_ContainsText(row, "Driver_ID", filterText) ||
                        _ContainsText(row, "Full Name", filterText) ||
                        _ContainsText(row, "NationalNo", filterText) ||
                        _ContainsText(row, "NumberOfActiveLicenses", filterText))
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
                        matchFound = _ContainsText(row, columnName, filterText);
                    }
                }

                if (matchFound)
                {
                    _dtFilteredDrivers.ImportRow(row);
                }
            }

            DGVdrivers.DataSource = _dtFilteredDrivers;
            lblRecords.Text = DGVdrivers.RowCount.ToString();
        }

        private string _GetColumnName(string displayName)
        {
            switch (displayName)
            {
                case "Driver ID":
                    return "Driver_ID";
                case "Full Name":
                    return "Full Name";
                case "National ID Number":
                    return "NationalNo";
                case "Number of Active Licenses":
                    return "NumberOfActiveLicenses";
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

        private void _RefreshDGVDrivers()
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();
            _dtFilteredDrivers = _dtAllDrivers.Copy();
            DGVdrivers.DataSource = _dtFilteredDrivers;
            lblRecords.Text = DGVdrivers.RowCount.ToString();
            
            // Refresh combo box items if needed
            if (combofilter.Items.Count == 0)
            {
                _InitializeFilterControls();
            }
        }

        private void Manage_Drivers_Load(object sender, EventArgs e)
        {
            _RefreshDGVDrivers();
        }
    }
}
