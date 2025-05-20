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

namespace Driver_License_Management_Project.Applications.ManageAppTypes
{
    public partial class Manage_Application_Types : Form
    {
        public Manage_Application_Types()
        {
            InitializeComponent();
        }

        private void Manage_Application_Types_Load(object sender, EventArgs e)
        {
            DGVappTypes.DataSource = clsAppTypes.GetAllAppTypes();
            lblRecords.Text = DGVappTypes.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Application_Types edit_Application_Types = new Edit_Application_Types((int)DGVappTypes.CurrentRow.Cells[0].Value);
            edit_Application_Types.ShowDialog();
            Manage_Application_Types_Load(null, null);
        }
    }
}
