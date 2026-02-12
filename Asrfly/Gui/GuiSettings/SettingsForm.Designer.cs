
namespace Asrfly.Gui.GuiSettings
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            groupBox1 = new System.Windows.Forms.GroupBox();
            buttonSaveGeneralSettings = new System.Windows.Forms.Button();
            linkLabelImportImage = new System.Windows.Forms.LinkLabel();
            pictureBoxLogo = new System.Windows.Forms.PictureBox();
            numericUpDownDataRow = new System.Windows.Forms.NumericUpDown();
            numericUpDownNotification = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBoxCompany = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            buttonSaveConString = new System.Windows.Forms.Button();
            radioButtonNetworkCon = new System.Windows.Forms.RadioButton();
            radioButtonLocalCon = new System.Windows.Forms.RadioButton();
            label7 = new System.Windows.Forms.Label();
            textBoxPassword = new System.Windows.Forms.TextBox();
            textBoxUserName = new System.Windows.Forms.TextBox();
            textBoxDataBase = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            textBoxServer = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            buttonRestore = new System.Windows.Forms.Button();
            buttonBackUp = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDataRow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNotification).BeginInit();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimeout).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonSaveGeneralSettings);
            groupBox1.Controls.Add(linkLabelImportImage);
            groupBox1.Controls.Add(pictureBoxLogo);
            groupBox1.Controls.Add(numericUpDownDataRow);
            groupBox1.Controls.Add(numericUpDownNotification);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxCompany);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(494, 625);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "الاعدادات العامة";
            // 
            // buttonSaveGeneralSettings
            // 
            buttonSaveGeneralSettings.Image = Properties.Resources.icons8_save_32px_1;
            buttonSaveGeneralSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSaveGeneralSettings.Location = new System.Drawing.Point(17, 553);
            buttonSaveGeneralSettings.Margin = new System.Windows.Forms.Padding(5);
            buttonSaveGeneralSettings.Name = "buttonSaveGeneralSettings";
            buttonSaveGeneralSettings.Size = new System.Drawing.Size(471, 55);
            buttonSaveGeneralSettings.TabIndex = 8;
            buttonSaveGeneralSettings.Text = "حفظ ";
            buttonSaveGeneralSettings.UseVisualStyleBackColor = true;
            buttonSaveGeneralSettings.Click += buttonSaveGeneralSettings_Click;
            // 
            // linkLabelImportImage
            // 
            linkLabelImportImage.AutoSize = true;
            linkLabelImportImage.Location = new System.Drawing.Point(198, 450);
            linkLabelImportImage.Name = "linkLabelImportImage";
            linkLabelImportImage.Size = new System.Drawing.Size(51, 25);
            linkLabelImportImage.TabIndex = 4;
            linkLabelImportImage.TabStop = true;
            linkLabelImportImage.Text = "تحميل";
            linkLabelImportImage.LinkClicked += linkLabelImportImage_LinkClicked;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = (System.Drawing.Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new System.Drawing.Point(123, 301);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(219, 146);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 3;
            pictureBoxLogo.TabStop = false;
            // 
            // numericUpDownDataRow
            // 
            numericUpDownDataRow.Location = new System.Drawing.Point(17, 193);
            numericUpDownDataRow.Name = "numericUpDownDataRow";
            numericUpDownDataRow.Size = new System.Drawing.Size(150, 30);
            numericUpDownDataRow.TabIndex = 2;
            numericUpDownDataRow.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // numericUpDownNotification
            // 
            numericUpDownNotification.Location = new System.Drawing.Point(17, 115);
            numericUpDownNotification.Name = "numericUpDownNotification";
            numericUpDownNotification.Size = new System.Drawing.Size(150, 30);
            numericUpDownNotification.TabIndex = 2;
            numericUpDownNotification.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label4.Location = new System.Drawing.Point(176, 266);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(88, 20);
            label4.TabIndex = 0;
            label4.Text = "شعار المؤسسة";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label3.Location = new System.Drawing.Point(322, 199);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(137, 20);
            label3.TabIndex = 0;
            label3.Text = "عدد البيانات المعروضة";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label2.Location = new System.Drawing.Point(278, 121);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(185, 20);
            label2.TabIndex = 0;
            label2.Text = "فترة عرض الاشعارات (ثواني) ";
            // 
            // textBoxCompany
            // 
            textBoxCompany.Location = new System.Drawing.Point(17, 44);
            textBoxCompany.Name = "textBoxCompany";
            textBoxCompany.Size = new System.Drawing.Size(294, 30);
            textBoxCompany.TabIndex = 1;
            textBoxCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label1.Location = new System.Drawing.Point(372, 51);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 20);
            label1.TabIndex = 0;
            label1.Text = "اسم المؤسسة";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            groupBox2.Location = new System.Drawing.Point(528, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(583, 629);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "قواعد البيانات";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(buttonSaveConString);
            groupBox4.Controls.Add(radioButtonNetworkCon);
            groupBox4.Controls.Add(radioButtonLocalCon);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(textBoxPassword);
            groupBox4.Controls.Add(textBoxUserName);
            groupBox4.Controls.Add(textBoxDataBase);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(numericUpDownTimeout);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(textBoxServer);
            groupBox4.Controls.Add(label5);
            groupBox4.Location = new System.Drawing.Point(7, 39);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(548, 470);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "نص الاتصال";
            // 
            // buttonSaveConString
            // 
            buttonSaveConString.Image = Properties.Resources.icons8_save_32px_1;
            buttonSaveConString.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSaveConString.Location = new System.Drawing.Point(8, 404);
            buttonSaveConString.Margin = new System.Windows.Forms.Padding(5);
            buttonSaveConString.Name = "buttonSaveConString";
            buttonSaveConString.Size = new System.Drawing.Size(167, 55);
            buttonSaveConString.TabIndex = 8;
            buttonSaveConString.Text = "حفظ ";
            buttonSaveConString.UseVisualStyleBackColor = true;
            buttonSaveConString.Click += buttonSaveConString_Click;
            // 
            // radioButtonNetworkCon
            // 
            radioButtonNetworkCon.AutoSize = true;
            radioButtonNetworkCon.Location = new System.Drawing.Point(6, 28);
            radioButtonNetworkCon.Name = "radioButtonNetworkCon";
            radioButtonNetworkCon.Size = new System.Drawing.Size(60, 24);
            radioButtonNetworkCon.TabIndex = 0;
            radioButtonNetworkCon.Text = "شبكي";
            radioButtonNetworkCon.UseVisualStyleBackColor = true;
            radioButtonNetworkCon.CheckedChanged += radioButtonNetworkCon_CheckedChanged;
            // 
            // radioButtonLocalCon
            // 
            radioButtonLocalCon.AutoSize = true;
            radioButtonLocalCon.Checked = true;
            radioButtonLocalCon.Location = new System.Drawing.Point(122, 28);
            radioButtonLocalCon.Name = "radioButtonLocalCon";
            radioButtonLocalCon.Size = new System.Drawing.Size(60, 24);
            radioButtonLocalCon.TabIndex = 0;
            radioButtonLocalCon.TabStop = true;
            radioButtonLocalCon.Text = "محلي";
            radioButtonLocalCon.UseVisualStyleBackColor = true;
            radioButtonLocalCon.CheckedChanged += radioButtonLocalCon_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label7.Location = new System.Drawing.Point(402, 202);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(119, 20);
            label7.TabIndex = 0;
            label7.Text = "فترة الاتصال (ثانية)";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Enabled = false;
            textBoxPassword.Location = new System.Drawing.Point(24, 323);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new System.Drawing.Size(355, 26);
            textBoxPassword.TabIndex = 1;
            textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxUserName
            // 
            textBoxUserName.Enabled = false;
            textBoxUserName.Location = new System.Drawing.Point(24, 261);
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.Size = new System.Drawing.Size(355, 26);
            textBoxUserName.TabIndex = 1;
            textBoxUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDataBase
            // 
            textBoxDataBase.Location = new System.Drawing.Point(24, 137);
            textBoxDataBase.Name = "textBoxDataBase";
            textBoxDataBase.Size = new System.Drawing.Size(355, 26);
            textBoxDataBase.TabIndex = 1;
            textBoxDataBase.Text = "AsrflyDataBase";
            textBoxDataBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label9.Location = new System.Drawing.Point(454, 326);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(63, 20);
            label9.TabIndex = 0;
            label9.Text = "كلمة السر";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label8.Location = new System.Drawing.Point(423, 264);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(82, 20);
            label8.TabIndex = 0;
            label8.Text = "اسم المستخدم";
            // 
            // numericUpDownTimeout
            // 
            numericUpDownTimeout.Enabled = false;
            numericUpDownTimeout.Location = new System.Drawing.Point(24, 199);
            numericUpDownTimeout.Name = "numericUpDownTimeout";
            numericUpDownTimeout.Size = new System.Drawing.Size(355, 26);
            numericUpDownTimeout.TabIndex = 2;
            numericUpDownTimeout.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label6.Location = new System.Drawing.Point(434, 140);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(83, 20);
            label6.TabIndex = 0;
            label6.Text = "قاعدة البيانات";
            // 
            // textBoxServer
            // 
            textBoxServer.Location = new System.Drawing.Point(24, 75);
            textBoxServer.Name = "textBoxServer";
            textBoxServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            textBoxServer.Size = new System.Drawing.Size(355, 26);
            textBoxServer.TabIndex = 1;
            textBoxServer.Text = ".\\SQLEXPRESS";
            textBoxServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label5.Location = new System.Drawing.Point(473, 78);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(51, 20);
            label5.TabIndex = 0;
            label5.Text = "السيرفر";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(buttonRestore);
            groupBox3.Controls.Add(buttonBackUp);
            groupBox3.ForeColor = System.Drawing.Color.FromArgb(255, 128, 128);
            groupBox3.Location = new System.Drawing.Point(6, 515);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(549, 108);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "النسخ الاحتياطي والتعيين";
            // 
            // buttonRestore
            // 
            buttonRestore.ForeColor = System.Drawing.Color.Black;
            buttonRestore.Image = Properties.Resources.icons8_Database_Restore_32px_1;
            buttonRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonRestore.Location = new System.Drawing.Point(25, 40);
            buttonRestore.Margin = new System.Windows.Forms.Padding(5);
            buttonRestore.Name = "buttonRestore";
            buttonRestore.Size = new System.Drawing.Size(263, 55);
            buttonRestore.TabIndex = 8;
            buttonRestore.Text = "استعادة النسخة الاحتياطية";
            buttonRestore.UseVisualStyleBackColor = true;
            buttonRestore.Click += buttonRestore_Click;
            // 
            // buttonBackUp
            // 
            buttonBackUp.ForeColor = System.Drawing.Color.Black;
            buttonBackUp.Image = Properties.Resources.icons8_data_backup_32px;
            buttonBackUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonBackUp.Location = new System.Drawing.Point(320, 40);
            buttonBackUp.Margin = new System.Windows.Forms.Padding(5);
            buttonBackUp.Name = "buttonBackUp";
            buttonBackUp.Size = new System.Drawing.Size(221, 55);
            buttonBackUp.TabIndex = 8;
            buttonBackUp.Text = "النسخ الاحتياطي";
            buttonBackUp.UseVisualStyleBackColor = true;
            buttonBackUp.Click += buttonBackUp_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1123, 649);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "اعدادات النظام";
            Activated += SettingsForm_Activated;
            FormClosing += SettingsForm_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDataRow).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNotification).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimeout).EndInit();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelImportImage;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.NumericUpDown numericUpDownDataRow;
        private System.Windows.Forms.NumericUpDown numericUpDownNotification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSaveGeneralSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonBackUp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSaveConString;
        private System.Windows.Forms.RadioButton radioButtonNetworkCon;
        private System.Windows.Forms.RadioButton radioButtonLocalCon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxDataBase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label5;
    }
}