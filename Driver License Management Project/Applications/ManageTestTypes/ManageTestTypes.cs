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

namespace Driver_License_Management_Project.Applications.ManageTestTypes
{
    public partial class Manage_Test_Types : Form
    {
        public Manage_Test_Types()
        {
            InitializeComponent();
        }
        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            DGVtestTypes.DataSource = clsTestTypes.GetAllTestTypes();
            lblRecords.Text = DGVtestTypes.RowCount.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Test_Type edit_Test_Type = new Edit_Test_Type((int)DGVtestTypes.CurrentRow.Cells[0].Value);
            edit_Test_Type.ShowDialog();
            ManageTestTypes_Load(null, null);
        }
    }
}
