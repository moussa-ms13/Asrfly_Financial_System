namespace Asrfly.Gui.GuiIncome
{
    partial class AddIncomeForm
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            buttonSave = new System.Windows.Forms.Button();
            buttonSaveAndPrint = new System.Windows.Forms.Button();
            buttonSaveAndClose = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            labelPath = new System.Windows.Forms.Label();
            linkLabelUploadImage = new System.Windows.Forms.LinkLabel();
            label14 = new System.Windows.Forms.Label();
            linkLabelNewSupplier = new System.Windows.Forms.LinkLabel();
            dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            linkLabelNewCategory = new System.Windows.Forms.LinkLabel();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            comboBoxsupplier = new System.Windows.Forms.ComboBox();
            textBoxAmount = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            textBoxRecNo = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonSave);
            panel1.Controls.Add(buttonSaveAndPrint);
            panel1.Controls.Add(buttonSaveAndClose);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 553);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(636, 76);
            panel1.TabIndex = 0;
            // 
            // buttonSave
            // 
            buttonSave.Image = Properties.Resources.icons8_save_32px_1;
            buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSave.Location = new System.Drawing.Point(14, 9);
            buttonSave.Margin = new System.Windows.Forms.Padding(5);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(139, 55);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "حفظ ";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonSaveAndPrint
            // 
            buttonSaveAndPrint.Image = Properties.Resources.icons8_print_32px;
            buttonSaveAndPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSaveAndPrint.Location = new System.Drawing.Point(170, 9);
            buttonSaveAndPrint.Margin = new System.Windows.Forms.Padding(5);
            buttonSaveAndPrint.Name = "buttonSaveAndPrint";
            buttonSaveAndPrint.Size = new System.Drawing.Size(213, 55);
            buttonSaveAndPrint.TabIndex = 8;
            buttonSaveAndPrint.Text = "حفظ وطباعة";
            buttonSaveAndPrint.UseVisualStyleBackColor = true;
            buttonSaveAndPrint.Click += buttonSaveAndPrint_Click;
            // 
            // buttonSaveAndClose
            // 
            buttonSaveAndClose.Image = Properties.Resources.icons8_save_32px;
            buttonSaveAndClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSaveAndClose.Location = new System.Drawing.Point(409, 9);
            buttonSaveAndClose.Margin = new System.Windows.Forms.Padding(5);
            buttonSaveAndClose.Name = "buttonSaveAndClose";
            buttonSaveAndClose.Size = new System.Drawing.Size(213, 55);
            buttonSaveAndClose.TabIndex = 6;
            buttonSaveAndClose.Text = "حفظ وغلق";
            buttonSaveAndClose.UseVisualStyleBackColor = true;
            buttonSaveAndClose.Click += buttonSaveAndClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(529, 52);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 25);
            label1.TabIndex = 0;
            label1.Text = "صنف";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.Color.Red;
            label5.Location = new System.Drawing.Point(71, 44);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 25);
            label5.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = System.Drawing.Color.Red;
            label6.Location = new System.Drawing.Point(71, 129);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(0, 25);
            label6.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(500, 116);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(58, 25);
            label2.TabIndex = 0;
            label2.Text = "المورد ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(501, 424);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 25);
            label3.TabIndex = 0;
            label3.Text = "التفاصيل";
            // 
            // richTextBoxDetails
            // 
            richTextBoxDetails.Location = new System.Drawing.Point(18, 398);
            richTextBoxDetails.Name = "richTextBoxDetails";
            richTextBoxDetails.Size = new System.Drawing.Size(383, 70);
            richTextBoxDetails.TabIndex = 5;
            richTextBoxDetails.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelPath);
            groupBox1.Controls.Add(linkLabelUploadImage);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(linkLabelNewSupplier);
            groupBox1.Controls.Add(dateTimePickerDate);
            groupBox1.Controls.Add(linkLabelNewCategory);
            groupBox1.Controls.Add(comboBoxCategory);
            groupBox1.Controls.Add(comboBoxsupplier);
            groupBox1.Controls.Add(richTextBoxDetails);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBoxAmount);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(textBoxRecNo);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(610, 529);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "ملعومات الصرف";
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.ForeColor = System.Drawing.Color.Red;
            labelPath.Location = new System.Drawing.Point(117, 489);
            labelPath.Name = "labelPath";
            labelPath.Size = new System.Drawing.Size(123, 25);
            labelPath.TabIndex = 13;
            labelPath.Text = "لم يتم اختيار ملف";
            // 
            // linkLabelUploadImage
            // 
            linkLabelUploadImage.AutoSize = true;
            linkLabelUploadImage.Location = new System.Drawing.Point(256, 489);
            linkLabelUploadImage.Name = "linkLabelUploadImage";
            linkLabelUploadImage.Size = new System.Drawing.Size(145, 25);
            linkLabelUploadImage.TabIndex = 12;
            linkLabelUploadImage.TabStop = true;
            linkLabelUploadImage.Text = "تحميل صورة الوصل";
            linkLabelUploadImage.LinkClicked += linkLabelUploadImage_LinkClicked;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(467, 489);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(101, 25);
            label14.TabIndex = 11;
            label14.Text = "صورة الوصل";
            // 
            // linkLabelNewSupplier
            // 
            linkLabelNewSupplier.AutoSize = true;
            linkLabelNewSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            linkLabelNewSupplier.Location = new System.Drawing.Point(413, 124);
            linkLabelNewSupplier.Name = "linkLabelNewSupplier";
            linkLabelNewSupplier.Size = new System.Drawing.Size(34, 20);
            linkLabelNewSupplier.TabIndex = 7;
            linkLabelNewSupplier.TabStop = true;
            linkLabelNewSupplier.Text = "جديد";
            linkLabelNewSupplier.LinkClicked += linkLabelNewCustomer_LinkClicked;
            // 
            // dateTimePickerDate
            // 
            dateTimePickerDate.Location = new System.Drawing.Point(18, 188);
            dateTimePickerDate.Name = "dateTimePickerDate";
            dateTimePickerDate.Size = new System.Drawing.Size(383, 30);
            dateTimePickerDate.TabIndex = 2;
            // 
            // linkLabelNewCategory
            // 
            linkLabelNewCategory.AutoSize = true;
            linkLabelNewCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            linkLabelNewCategory.Location = new System.Drawing.Point(417, 48);
            linkLabelNewCategory.Name = "linkLabelNewCategory";
            linkLabelNewCategory.Size = new System.Drawing.Size(34, 20);
            linkLabelNewCategory.TabIndex = 8;
            linkLabelNewCategory.TabStop = true;
            linkLabelNewCategory.Text = "جديد";
            linkLabelNewCategory.LinkClicked += linkLabelNewCategory_LinkClicked;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new System.Drawing.Point(18, 44);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(383, 33);
            comboBoxCategory.TabIndex = 0;
            // 
            // comboBoxsupplier
            // 
            comboBoxsupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxsupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            comboBoxsupplier.FormattingEnabled = true;
            comboBoxsupplier.Location = new System.Drawing.Point(18, 116);
            comboBoxsupplier.Name = "comboBoxsupplier";
            comboBoxsupplier.Size = new System.Drawing.Size(383, 33);
            comboBoxsupplier.TabIndex = 1;
            // 
            // textBoxAmount
            // 
            textBoxAmount.Location = new System.Drawing.Point(18, 332);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new System.Drawing.Size(383, 30);
            textBoxAmount.TabIndex = 4;
            textBoxAmount.Text = "0";
            textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(521, 338);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(46, 25);
            label10.TabIndex = 0;
            label10.Text = "المبلغ";
            // 
            // textBoxRecNo
            // 
            textBoxRecNo.Location = new System.Drawing.Point(18, 260);
            textBoxRecNo.Name = "textBoxRecNo";
            textBoxRecNo.Size = new System.Drawing.Size(383, 30);
            textBoxRecNo.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(486, 258);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(82, 25);
            label9.TabIndex = 0;
            label9.Text = "رقم الوصل";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(478, 192);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(104, 25);
            label8.TabIndex = 0;
            label8.Text = "تاريخ الصرف";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = System.Drawing.Color.Red;
            label12.Location = new System.Drawing.Point(478, 338);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(20, 25);
            label12.TabIndex = 0;
            label12.Text = "*";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = System.Drawing.Color.Red;
            label11.Location = new System.Drawing.Point(457, 188);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(20, 25);
            label11.TabIndex = 0;
            label11.Text = "*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.Color.Red;
            label4.Location = new System.Drawing.Point(407, 116);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(20, 25);
            label4.TabIndex = 0;
            label4.Text = "*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = System.Drawing.Color.Red;
            label7.Location = new System.Drawing.Point(498, 52);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(20, 25);
            label7.TabIndex = 0;
            label7.Text = "*";
            // 
            // AddIncomeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(636, 629);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddIncomeForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "اضافة / تعديل صرف";
            Load += AddIncomeForm_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonSaveAndPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.ComboBox comboBoxsupplier;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRecNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelNewSupplier;
        private System.Windows.Forms.LinkLabel linkLabelNewCategory;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.LinkLabel linkLabelUploadImage;
        private System.Windows.Forms.Label label14;
    }
}