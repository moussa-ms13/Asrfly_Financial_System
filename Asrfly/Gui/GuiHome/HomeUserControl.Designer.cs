
namespace Asrfly.Gui.GuiHome
{
    partial class HomeUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeUserControl));
            panel1 = new System.Windows.Forms.Panel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            buttonAddInput = new System.Windows.Forms.Button();
            buttonAddOutput = new System.Windows.Forms.Button();
            buttonAddUser = new System.Windows.Forms.Button();
            buttonAddProject = new System.Windows.Forms.Button();
            buttonAddSupplier = new System.Windows.Forms.Button();
            buttonAddCustomer = new System.Windows.Forms.Button();
            buttonAddCategory = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            pictureBoxLogo = new System.Windows.Forms.PictureBox();
            labelCompanyName = new System.Windows.Forms.Label();
            labelWellcome = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            comboBoxProject = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Aquamarine;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 380);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1262, 220);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            pictureBox1.Image = Properties.Resources.icons8_smart_96px;
            pictureBox1.Location = new System.Drawing.Point(481, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(72, 62);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            groupBox1.Controls.Add(buttonAddInput);
            groupBox1.Controls.Add(buttonAddOutput);
            groupBox1.Controls.Add(buttonAddUser);
            groupBox1.Controls.Add(buttonAddProject);
            groupBox1.Controls.Add(buttonAddSupplier);
            groupBox1.Controls.Add(buttonAddCustomer);
            groupBox1.Controls.Add(buttonAddCategory);
            groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(78, 58);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(1139, 145);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "اضافة";
            // 
            // buttonAddInput
            // 
            buttonAddInput.Image = Properties.Resources.icons8_input_32px;
            buttonAddInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddInput.Location = new System.Drawing.Point(48, 52);
            buttonAddInput.Margin = new System.Windows.Forms.Padding(5);
            buttonAddInput.Name = "buttonAddInput";
            buttonAddInput.Size = new System.Drawing.Size(142, 68);
            buttonAddInput.TabIndex = 1;
            buttonAddInput.Text = "قبض";
            buttonAddInput.UseVisualStyleBackColor = true;
            buttonAddInput.Click += buttonAddInput_Click;
            // 
            // buttonAddOutput
            // 
            buttonAddOutput.Image = Properties.Resources.icons8_output_32px;
            buttonAddOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddOutput.Location = new System.Drawing.Point(200, 52);
            buttonAddOutput.Margin = new System.Windows.Forms.Padding(5);
            buttonAddOutput.Name = "buttonAddOutput";
            buttonAddOutput.Size = new System.Drawing.Size(142, 68);
            buttonAddOutput.TabIndex = 1;
            buttonAddOutput.Text = "صرف";
            buttonAddOutput.UseVisualStyleBackColor = true;
            buttonAddOutput.Click += buttonAddOutput_Click;
            // 
            // buttonAddUser
            // 
            buttonAddUser.Image = Properties.Resources.icons8_users_32px;
            buttonAddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddUser.Location = new System.Drawing.Point(352, 52);
            buttonAddUser.Margin = new System.Windows.Forms.Padding(5);
            buttonAddUser.Name = "buttonAddUser";
            buttonAddUser.Size = new System.Drawing.Size(142, 68);
            buttonAddUser.TabIndex = 1;
            buttonAddUser.Text = "   مستخدم";
            buttonAddUser.UseVisualStyleBackColor = true;
            buttonAddUser.Click += buttonAddUser_Click;
            // 
            // buttonAddProject
            // 
            buttonAddProject.Image = Properties.Resources.icons8_microsoft_project_32px;
            buttonAddProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddProject.Location = new System.Drawing.Point(504, 52);
            buttonAddProject.Margin = new System.Windows.Forms.Padding(5);
            buttonAddProject.Name = "buttonAddProject";
            buttonAddProject.Size = new System.Drawing.Size(142, 68);
            buttonAddProject.TabIndex = 1;
            buttonAddProject.Text = "مشروع";
            buttonAddProject.UseVisualStyleBackColor = true;
            buttonAddProject.Click += buttonAddProject_Click;
            // 
            // buttonAddSupplier
            // 
            buttonAddSupplier.Image = Properties.Resources.icons8_conference_32px;
            buttonAddSupplier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddSupplier.Location = new System.Drawing.Point(656, 52);
            buttonAddSupplier.Margin = new System.Windows.Forms.Padding(5);
            buttonAddSupplier.Name = "buttonAddSupplier";
            buttonAddSupplier.Size = new System.Drawing.Size(142, 68);
            buttonAddSupplier.TabIndex = 1;
            buttonAddSupplier.Text = "مورد";
            buttonAddSupplier.UseVisualStyleBackColor = true;
            buttonAddSupplier.Click += buttonAddSupplier_Click;
            // 
            // buttonAddCustomer
            // 
            buttonAddCustomer.Image = Properties.Resources.icons8_people_32px;
            buttonAddCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddCustomer.Location = new System.Drawing.Point(808, 52);
            buttonAddCustomer.Margin = new System.Windows.Forms.Padding(5);
            buttonAddCustomer.Name = "buttonAddCustomer";
            buttonAddCustomer.Size = new System.Drawing.Size(142, 68);
            buttonAddCustomer.TabIndex = 1;
            buttonAddCustomer.Text = "عميل";
            buttonAddCustomer.UseVisualStyleBackColor = true;
            buttonAddCustomer.Click += buttonAddCustomer_Click;
            // 
            // buttonAddCategory
            // 
            buttonAddCategory.Image = Properties.Resources.icons8_categorize_32px;
            buttonAddCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAddCategory.Location = new System.Drawing.Point(960, 52);
            buttonAddCategory.Margin = new System.Windows.Forms.Padding(5);
            buttonAddCategory.Name = "buttonAddCategory";
            buttonAddCategory.Size = new System.Drawing.Size(142, 68);
            buttonAddCategory.TabIndex = 1;
            buttonAddCategory.Text = "صنف";
            buttonAddCategory.UseVisualStyleBackColor = true;
            buttonAddCategory.Click += buttonAddCategory_Click;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            label1.Location = new System.Drawing.Point(559, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(165, 36);
            label1.TabIndex = 0;
            label1.Text = "الوصول السريع";
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            panel2.Controls.Add(pictureBoxLogo);
            panel2.Controls.Add(labelCompanyName);
            panel2.Location = new System.Drawing.Point(643, 33);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(574, 140);
            panel2.TabIndex = 1;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Right;
            pictureBoxLogo.Image = (System.Drawing.Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new System.Drawing.Point(448, 0);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new System.Drawing.Size(126, 140);
            pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 3;
            pictureBoxLogo.TabStop = false;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            labelCompanyName.Location = new System.Drawing.Point(3, 5);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(445, 128);
            labelCompanyName.TabIndex = 0;
            labelCompanyName.Text = "moussams707@gmail.com";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWellcome
            // 
            labelWellcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            labelWellcome.Location = new System.Drawing.Point(24, 40);
            labelWellcome.Name = "labelWellcome";
            labelWellcome.Size = new System.Drawing.Size(341, 128);
            labelWellcome.TabIndex = 0;
            labelWellcome.Text = "مرحبا بك مجددا موسى\r\n";
            labelWellcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            panel3.Controls.Add(comboBoxProject);
            panel3.Controls.Add(label2);
            panel3.Location = new System.Drawing.Point(358, 252);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(546, 125);
            panel3.TabIndex = 2;
            // 
            // comboBoxProject
            // 
            comboBoxProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            comboBoxProject.FormattingEnabled = true;
            comboBoxProject.Location = new System.Drawing.Point(12, 77);
            comboBoxProject.Name = "comboBoxProject";
            comboBoxProject.Size = new System.Drawing.Size(531, 33);
            comboBoxProject.TabIndex = 1;
            comboBoxProject.SelectedIndexChanged += comboBoxProject_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(546, 49);
            label2.TabIndex = 0;
            label2.Text = "المشاريع";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HomeUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(labelWellcome);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "HomeUserControl";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Size = new System.Drawing.Size(1262, 600);
            Load += HomeUserControl_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddInput;
        private System.Windows.Forms.Button buttonAddOutput;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonAddProject;
        private System.Windows.Forms.Button buttonAddSupplier;
        private System.Windows.Forms.Button buttonAddCustomer;
        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Label labelWellcome;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.Label label2;
    }
}
