namespace Driver_License_Management_Project.Applications.Driving_Licenses_Services.Local
{
    partial class Local_Driving_License_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Local_Driving_License_Info));
            this.drivingLicenseAppInfo1 = new Driver_License_Management_Project.Applications.ManageApplications.Local_Driving_License_Apllication.Controls.DrivingLicenseAppInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drivingLicenseAppInfo1
            // 
            this.drivingLicenseAppInfo1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.drivingLicenseAppInfo1.Location = new System.Drawing.Point(1, 12);
            this.drivingLicenseAppInfo1.Name = "drivingLicenseAppInfo1";
            this.drivingLicenseAppInfo1.Size = new System.Drawing.Size(870, 407);
            this.drivingLicenseAppInfo1.TabIndex = 0;
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
            this.btnClose.Location = new System.Drawing.Point(758, 418);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 32);
            this.btnClose.TabIndex = 182;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // Local_Driving_License_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(873, 461);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.drivingLicenseAppInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Local_Driving_License_Info";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Driving License Application Info";
            this.Load += new System.EventHandler(this.Local_Driving_License_Application_Info_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ManageApplications.Local_Driving_License_Apllication.Controls.DrivingLicenseAppInfo drivingLicenseAppInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}