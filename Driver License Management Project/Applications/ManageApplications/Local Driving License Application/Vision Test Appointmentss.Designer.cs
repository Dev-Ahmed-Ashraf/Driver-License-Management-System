namespace Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication
{
    partial class Vision_Test_Appointments
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vision_Test_Appointments));
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DGVAppointments = new Zuby.ADGV.AdvancedDataGridView();
            this.CMSAppointments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAppointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddNewAppo = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.drivingLicenseAppInfo = new Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication.Controls.DrivingLicenseAppInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAppointments)).BeginInit();
            this.CMSAppointments.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbTitle
            // 
            this.pbTitle.Image = global::Driver_License_Management_Project.Properties.Resources.Vision_512;
            this.pbTitle.Location = new System.Drawing.Point(219, 1);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(446, 213);
            this.pbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTitle.TabIndex = 0;
            this.pbTitle.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Stencil", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Location = new System.Drawing.Point(73, 183);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(736, 57);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Vision Test Appointments";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 655);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Appointments : ";
            // 
            // DGVAppointments
            // 
            this.DGVAppointments.AllowUserToAddRows = false;
            this.DGVAppointments.AllowUserToDeleteRows = false;
            this.DGVAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGVAppointments.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.DGVAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAppointments.ContextMenuStrip = this.CMSAppointments;
            this.DGVAppointments.FilterAndSortEnabled = true;
            this.DGVAppointments.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVAppointments.Location = new System.Drawing.Point(24, 689);
            this.DGVAppointments.Name = "DGVAppointments";
            this.DGVAppointments.ReadOnly = true;
            this.DGVAppointments.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DGVAppointments.Size = new System.Drawing.Size(844, 159);
            this.DGVAppointments.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.DGVAppointments.TabIndex = 4;
            // 
            // CMSAppointments
            // 
            this.CMSAppointments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.takeTestToolStripMenuItem,
            this.editAppointmentToolStripMenuItem});
            this.CMSAppointments.Name = "CMSAppointments";
            this.CMSAppointments.Size = new System.Drawing.Size(169, 48);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // editAppointmentToolStripMenuItem
            // 
            this.editAppointmentToolStripMenuItem.Name = "editAppointmentToolStripMenuItem";
            this.editAppointmentToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.editAppointmentToolStripMenuItem.Text = "Edit Appointment";
            this.editAppointmentToolStripMenuItem.Click += new System.EventHandler(this.editAppointmentToolStripMenuItem_Click);
            // 
            // btnAddNewAppo
            // 
            this.btnAddNewAppo.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewAppo.Image")));
            this.btnAddNewAppo.Location = new System.Drawing.Point(825, 643);
            this.btnAddNewAppo.Name = "btnAddNewAppo";
            this.btnAddNewAppo.Size = new System.Drawing.Size(44, 44);
            this.btnAddNewAppo.TabIndex = 5;
            this.btnAddNewAppo.UseVisualStyleBackColor = true;
            this.btnAddNewAppo.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightCoral;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(765, 857);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 32);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 865);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "# Records : ";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(123, 866);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(39, 20);
            this.lblRecords.TabIndex = 8;
            this.lblRecords.Text = "      ";
            // 
            // drivingLicenseAppInfo
            // 
            this.drivingLicenseAppInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.drivingLicenseAppInfo.Location = new System.Drawing.Point(10, 244);
            this.drivingLicenseAppInfo.Name = "drivingLicenseAppInfo";
            this.drivingLicenseAppInfo.Size = new System.Drawing.Size(870, 407);
            this.drivingLicenseAppInfo.TabIndex = 2;
            // 
            // Vision_Test_Appointments
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(892, 901);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewAppo);
            this.Controls.Add(this.DGVAppointments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.drivingLicenseAppInfo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbTitle);
            this.KeyPreview = true;
            this.Name = "Vision_Test_Appointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.Vision_Test_Appointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAppointments)).EndInit();
            this.CMSAppointments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lblTitle;
        private Controls.DrivingLicenseAppInfo drivingLicenseAppInfo;
        private System.Windows.Forms.Label label4;
        private Zuby.ADGV.AdvancedDataGridView DGVAppointments;
        private System.Windows.Forms.Button btnAddNewAppo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.ContextMenuStrip CMSAppointments;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editAppointmentToolStripMenuItem;
    }
}