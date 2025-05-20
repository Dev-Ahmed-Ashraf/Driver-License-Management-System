namespace Driver_License_Management_Project.Licenses.controls
{
    partial class Driver_License_History
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Driver_License_History));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabLocal = new System.Windows.Forms.TabPage();
            this.lblLocalRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DGVLocal = new Zuby.ADGV.AdvancedDataGridView();
            this.CMSLocalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabInternational = new System.Windows.Forms.TabPage();
            this.lblInternationalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DGVInternational = new Zuby.ADGV.AdvancedDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.CMSInternationalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseDetailsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.TabLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLocal)).BeginInit();
            this.CMSLocalLicenseHistory.SuspendLayout();
            this.tabInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInternational)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.CMSInternationalLicenseHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabLocal);
            this.tabControl1.Controls.Add(this.tabInternational);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(841, 254);
            this.tabControl1.TabIndex = 0;
            // 
            // TabLocal
            // 
            this.TabLocal.BackColor = System.Drawing.Color.Snow;
            this.TabLocal.ContextMenuStrip = this.CMSLocalLicenseHistory;
            this.TabLocal.Controls.Add(this.lblLocalRecords);
            this.TabLocal.Controls.Add(this.label1);
            this.TabLocal.Controls.Add(this.DGVLocal);
            this.TabLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabLocal.Location = new System.Drawing.Point(4, 25);
            this.TabLocal.Name = "TabLocal";
            this.TabLocal.Padding = new System.Windows.Forms.Padding(3);
            this.TabLocal.Size = new System.Drawing.Size(833, 225);
            this.TabLocal.TabIndex = 0;
            this.TabLocal.Text = "Local";
            // 
            // lblLocalRecords
            // 
            this.lblLocalRecords.AutoSize = true;
            this.lblLocalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalRecords.Location = new System.Drawing.Point(94, 199);
            this.lblLocalRecords.Name = "lblLocalRecords";
            this.lblLocalRecords.Size = new System.Drawing.Size(39, 16);
            this.lblLocalRecords.TabIndex = 4;
            this.lblLocalRecords.Text = "        ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "# Records : ";
            // 
            // DGVLocal
            // 
            this.DGVLocal.AllowUserToAddRows = false;
            this.DGVLocal.AllowUserToDeleteRows = false;
            this.DGVLocal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVLocal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGVLocal.BackgroundColor = System.Drawing.Color.Snow;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVLocal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVLocal.ContextMenuStrip = this.CMSLocalLicenseHistory;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVLocal.DefaultCellStyle = dataGridViewCellStyle5;
            this.DGVLocal.FilterAndSortEnabled = true;
            this.DGVLocal.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVLocal.Location = new System.Drawing.Point(6, 6);
            this.DGVLocal.Name = "DGVLocal";
            this.DGVLocal.ReadOnly = true;
            this.DGVLocal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVLocal.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVLocal.Size = new System.Drawing.Size(821, 185);
            this.DGVLocal.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVLocal.TabIndex = 0;
            // 
            // CMSLocalLicenseHistory
            // 
            this.CMSLocalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseDetailsToolStripMenuItem});
            this.CMSLocalLicenseHistory.Name = "CMSLicenseHistory";
            this.CMSLocalLicenseHistory.Size = new System.Drawing.Size(249, 42);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe MDL2 Assets", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showLicenseDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showLicenseDetailsToolStripMenuItem.Image")));
            this.showLicenseDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(248, 38);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // tabInternational
            // 
            this.tabInternational.ContextMenuStrip = this.CMSInternationalLicenseHistory;
            this.tabInternational.Controls.Add(this.lblInternationalRecords);
            this.tabInternational.Controls.Add(this.label3);
            this.tabInternational.Controls.Add(this.DGVInternational);
            this.tabInternational.Location = new System.Drawing.Point(4, 25);
            this.tabInternational.Name = "tabInternational";
            this.tabInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tabInternational.Size = new System.Drawing.Size(833, 225);
            this.tabInternational.TabIndex = 1;
            this.tabInternational.Text = "International";
            this.tabInternational.UseVisualStyleBackColor = true;
            // 
            // lblInternationalRecords
            // 
            this.lblInternationalRecords.AutoSize = true;
            this.lblInternationalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalRecords.Location = new System.Drawing.Point(94, 199);
            this.lblInternationalRecords.Name = "lblInternationalRecords";
            this.lblInternationalRecords.Size = new System.Drawing.Size(39, 16);
            this.lblInternationalRecords.TabIndex = 6;
            this.lblInternationalRecords.Text = "        ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "# Records : ";
            // 
            // DGVInternational
            // 
            this.DGVInternational.AllowUserToAddRows = false;
            this.DGVInternational.AllowUserToDeleteRows = false;
            this.DGVInternational.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVInternational.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGVInternational.BackgroundColor = System.Drawing.Color.PapayaWhip;
            this.DGVInternational.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInternational.FilterAndSortEnabled = true;
            this.DGVInternational.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVInternational.Location = new System.Drawing.Point(6, 6);
            this.DGVInternational.Name = "DGVInternational";
            this.DGVInternational.ReadOnly = true;
            this.DGVInternational.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DGVInternational.Size = new System.Drawing.Size(821, 185);
            this.DGVInternational.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVInternational.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(853, 273);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver License History";
            // 
            // CMSInternationalLicenseHistory
            // 
            this.CMSInternationalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseDetailsToolStripMenuItem1});
            this.CMSInternationalLicenseHistory.Name = "CMSInternationalLicenseHistory";
            this.CMSInternationalLicenseHistory.Size = new System.Drawing.Size(265, 64);
            // 
            // showLicenseDetailsToolStripMenuItem1
            // 
            this.showLicenseDetailsToolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showLicenseDetailsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("showLicenseDetailsToolStripMenuItem1.Image")));
            this.showLicenseDetailsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseDetailsToolStripMenuItem1.Name = "showLicenseDetailsToolStripMenuItem1";
            this.showLicenseDetailsToolStripMenuItem1.Size = new System.Drawing.Size(264, 38);
            this.showLicenseDetailsToolStripMenuItem1.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem1.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem1_Click);
            // 
            // Driver_License_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "Driver_License_History";
            this.Size = new System.Drawing.Size(854, 276);
            this.tabControl1.ResumeLayout(false);
            this.TabLocal.ResumeLayout(false);
            this.TabLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLocal)).EndInit();
            this.CMSLocalLicenseHistory.ResumeLayout(false);
            this.tabInternational.ResumeLayout(false);
            this.tabInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInternational)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.CMSInternationalLicenseHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabLocal;
        private System.Windows.Forms.TabPage tabInternational;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Zuby.ADGV.AdvancedDataGridView DGVLocal;
        private Zuby.ADGV.AdvancedDataGridView DGVInternational;
        private System.Windows.Forms.Label lblLocalRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInternationalRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip CMSLocalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip CMSInternationalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem1;
    }
}
