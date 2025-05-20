namespace Driver_License_Management_Project.Tests
{
    partial class Shedule_Test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shedule_Test));
            this.btnclose = new System.Windows.Forms.Button();
            this.frmShedule_Test = new Driver_License_Management_Project.Tests.Controls.ctrlShedule_Test();
            this.SuspendLayout();
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.LightGray;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatAppearance.BorderColor = System.Drawing.Color.LightCoral;
            this.btnclose.FlatAppearance.BorderSize = 3;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnclose.Location = new System.Drawing.Point(204, 706);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(103, 32);
            this.btnclose.TabIndex = 31;
            this.btnclose.Text = "Close";
            this.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // frmShedule_Test
            // 
            this.frmShedule_Test.Location = new System.Drawing.Point(12, 12);
            this.frmShedule_Test.Name = "frmShedule_Test";
            this.frmShedule_Test.Size = new System.Drawing.Size(502, 695);
            this.frmShedule_Test.TabIndex = 32;
            this.frmShedule_Test.TestType = Bussiness_Layer.clsTestTypes.enTestType.WrittenTest;
            // 
            // Shedule_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(524, 750);
            this.Controls.Add(this.frmShedule_Test);
            this.Controls.Add(this.btnclose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Shedule_Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shedule Test";
            this.Load += new System.EventHandler(this.Shedule_Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnclose;
        private Controls.ctrlShedule_Test frmShedule_Test;
    }
}