
namespace Asrfly
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonHome = new System.Windows.Forms.Button();
            buttonCategory = new System.Windows.Forms.Button();
            buttonCustomers = new System.Windows.Forms.Button();
            buttonSuppliers = new System.Windows.Forms.Button();
            buttonProjects = new System.Windows.Forms.Button();
            buttonUsers = new System.Windows.Forms.Button();
            buttonSettings = new System.Windows.Forms.Button();
            buttonLogout = new System.Windows.Forms.Button();
            buttonSystemRecords = new System.Windows.Forms.Button();
            panelContainer = new System.Windows.Forms.Panel();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(buttonHome);
            flowLayoutPanel1.Controls.Add(buttonCategory);
            flowLayoutPanel1.Controls.Add(buttonCustomers);
            flowLayoutPanel1.Controls.Add(buttonSuppliers);
            flowLayoutPanel1.Controls.Add(buttonProjects);
            flowLayoutPanel1.Controls.Add(buttonUsers);
            flowLayoutPanel1.Controls.Add(buttonSettings);
            flowLayoutPanel1.Controls.Add(buttonLogout);
            flowLayoutPanel1.Controls.Add(buttonSystemRecords);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 600);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            flowLayoutPanel1.Size = new System.Drawing.Size(1262, 73);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonHome
            // 
            buttonHome.Image = Properties.Resources.icons8_home_32px_1;
            buttonHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonHome.Location = new System.Drawing.Point(1061, 10);
            buttonHome.Margin = new System.Windows.Forms.Padding(5);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new System.Drawing.Size(165, 55);
            buttonHome.TabIndex = 0;
            buttonHome.Text = "الرئيسية";
            buttonHome.UseVisualStyleBackColor = true;
            buttonHome.Click += buttonHome_Click;
            // 
            // buttonCategory
            // 
            buttonCategory.Image = Properties.Resources.icons8_categorize_32px;
            buttonCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCategory.Location = new System.Drawing.Point(886, 10);
            buttonCategory.Margin = new System.Windows.Forms.Padding(5);
            buttonCategory.Name = "buttonCategory";
            buttonCategory.Size = new System.Drawing.Size(165, 55);
            buttonCategory.TabIndex = 1;
            buttonCategory.Text = "الاصناف";
            buttonCategory.UseVisualStyleBackColor = true;
            buttonCategory.Click += buttonCategory_Click;
            // 
            // buttonCustomers
            // 
            buttonCustomers.Image = Properties.Resources.icons8_people_32px;
            buttonCustomers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCustomers.Location = new System.Drawing.Point(711, 10);
            buttonCustomers.Margin = new System.Windows.Forms.Padding(5);
            buttonCustomers.Name = "buttonCustomers";
            buttonCustomers.Size = new System.Drawing.Size(165, 55);
            buttonCustomers.TabIndex = 2;
            buttonCustomers.Text = "العملاء";
            buttonCustomers.UseVisualStyleBackColor = true;
            buttonCustomers.Click += buttonCustomers_Click;
            // 
            // buttonSuppliers
            // 
            buttonSuppliers.Image = Properties.Resources.icons8_conference_32px;
            buttonSuppliers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSuppliers.Location = new System.Drawing.Point(536, 10);
            buttonSuppliers.Margin = new System.Windows.Forms.Padding(5);
            buttonSuppliers.Name = "buttonSuppliers";
            buttonSuppliers.Size = new System.Drawing.Size(165, 55);
            buttonSuppliers.TabIndex = 3;
            buttonSuppliers.Text = "الموردين";
            buttonSuppliers.UseVisualStyleBackColor = true;
            buttonSuppliers.Click += buttonSuppliers_Click;
            // 
            // buttonProjects
            // 
            buttonProjects.Image = Properties.Resources.icons8_microsoft_project_32px;
            buttonProjects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonProjects.Location = new System.Drawing.Point(361, 10);
            buttonProjects.Margin = new System.Windows.Forms.Padding(5);
            buttonProjects.Name = "buttonProjects";
            buttonProjects.Size = new System.Drawing.Size(165, 55);
            buttonProjects.TabIndex = 4;
            buttonProjects.Text = "المشاريع";
            buttonProjects.UseVisualStyleBackColor = true;
            buttonProjects.Click += buttonProjects_Click;
            // 
            // buttonUsers
            // 
            buttonUsers.Image = Properties.Resources.icons8_users_32px;
            buttonUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonUsers.Location = new System.Drawing.Point(186, 10);
            buttonUsers.Margin = new System.Windows.Forms.Padding(5);
            buttonUsers.Name = "buttonUsers";
            buttonUsers.Size = new System.Drawing.Size(165, 55);
            buttonUsers.TabIndex = 5;
            buttonUsers.Text = "     المستخدمين";
            buttonUsers.UseVisualStyleBackColor = true;
            buttonUsers.Click += buttonUsers_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Image = Properties.Resources.icons8_settings_32px;
            buttonSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSettings.Location = new System.Drawing.Point(11, 10);
            buttonSettings.Margin = new System.Windows.Forms.Padding(5);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new System.Drawing.Size(165, 55);
            buttonSettings.TabIndex = 6;
            buttonSettings.Text = "الاعدادات";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // buttonLogout
            // 
            buttonLogout.Image = Properties.Resources.icons8_Logout_32px;
            buttonLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonLogout.Location = new System.Drawing.Point(1061, 75);
            buttonLogout.Margin = new System.Windows.Forms.Padding(5);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new System.Drawing.Size(165, 55);
            buttonLogout.TabIndex = 7;
            buttonLogout.Text = "خروج";
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // buttonSystemRecords
            // 
            buttonSystemRecords.Image = Properties.Resources.icons8_moleskine_32px;
            buttonSystemRecords.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSystemRecords.Location = new System.Drawing.Point(886, 75);
            buttonSystemRecords.Margin = new System.Windows.Forms.Padding(5);
            buttonSystemRecords.Name = "buttonSystemRecords";
            buttonSystemRecords.Size = new System.Drawing.Size(165, 55);
            buttonSystemRecords.TabIndex = 10;
            buttonSystemRecords.Text = "سجل نظام";
            buttonSystemRecords.UseVisualStyleBackColor = true;
            buttonSystemRecords.Click += buttonSystemRecords_Click;
            // 
            // panelContainer
            // 
            panelContainer.BackColor = System.Drawing.Color.White;
            panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContainer.Location = new System.Drawing.Point(0, 0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new System.Drawing.Size(1262, 600);
            panelContainer.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1262, 673);
            Controls.Add(panelContainer);
            Controls.Add(flowLayoutPanel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "Main";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Asrfly";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosed += Main_FormClosed;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonCategory;
        private System.Windows.Forms.Button buttonCustomers;
        private System.Windows.Forms.Button buttonSuppliers;
        private System.Windows.Forms.Button buttonProjects;
        private System.Windows.Forms.Button buttonUsers;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonLogout;
        public System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button buttonSystemRecords;
    }
}

