using Asrfly.Code;
using Asrfly.Core;
using Asrfly.Data;
using Asrfly.Gui.GuiCategories;
using Asrfly.Gui.GuiSuppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asrfly.Gui.GuiOutcome
{
    public partial class AddOutcomeForm : Form
    {
        private readonly int ID;
        private readonly OutcomeUserControl categoryUserControl;
        private Outcome outcome;
        private int CategoryId;
        private int SupplierId;
        private int ProjectId;
        private readonly IDataHelper<Outcome> dataHelper;
        private readonly IDataHelper<Suppliers> dataHelperSuppliers;
        private readonly IDataHelper<Categories> dataHelperCategories;
        private readonly GuiLoading.LoadingForm loadingForm;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;

        private string capturedImage = string.Empty;

        public AddOutcomeForm(int Id, int ProjectId, OutcomeUserControl ctegoryUserControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Outcome>)ConfigrationObjectManager.GetObject("Outcome");
            dataHelperSuppliers = (IDataHelper<Suppliers>)ConfigrationObjectManager.GetObject("Suppliers");
            dataHelperCategories = (IDataHelper<Categories>)ConfigrationObjectManager.GetObject("Categories");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigrationObjectManager.GetObject("SystemRecords");

            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.categoryUserControl = ctegoryUserControl;
            this.ProjectId = ProjectId;
        }

        #region Events
        private async void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (IsFiledsEmpty())
            {
                MessageCollections.ShowFiledsRequired();
            }
            else
            {
                loadingForm.Show();
                if (await SaveData())
                {
                    if (ID == 0) MessageCollections.ShowAddNotificaiton();
                    else MessageCollections.ShowUpdateNotificaiton();
                    Close();
                }
                else MessageCollections.ShowErrorServer();
                loadingForm.Hide();
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsFiledsEmpty())
            {
                MessageCollections.ShowFiledsRequired();
            }
            else
            {
                loadingForm.Show();
                if (await SaveData())
                {
                    if (ID == 0) MessageCollections.ShowAddNotificaiton();
                    else MessageCollections.ShowUpdateNotificaiton();
                }
                else MessageCollections.ShowErrorServer();
                loadingForm.Hide();
            }
        }

        private async void buttonSaveAndPrint_Click(object sender, EventArgs e)
        {
            if (IsFiledsEmpty())
            {
                MessageCollections.ShowFiledsRequired();
            }
            else
            {
                loadingForm.Show();
                if (await SaveData())
                {
                    loadingForm.Hide();
                    ReceiptPrinter printer = new ReceiptPrinter();
                    printer.Print(outcome);

                    if (ID == 0) MessageCollections.ShowAddNotificaiton();
                    else MessageCollections.ShowUpdateNotificaiton();
                }
                else
                {
                    loadingForm.Hide();
                    MessageCollections.ShowErrorServer();
                }
            }
        }

        private async void AddOutcomeForm_Load(object sender, EventArgs e)
        {
            loadingForm.Show();
            await SetFiledData();
            loadingForm.Hide();
        }

        private void linkLabelUploadImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر صورة الوصل";
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    capturedImage = Convert.ToBase64String(imageBytes);

                    if (labelPath != null)
                    {
                        labelPath.Text = Path.GetFileName(openFileDialog.FileName);
                        labelPath.ForeColor = Color.Green;
                    }
                    MessageBox.Show("تم إرفاق الملف بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("حدث خطأ: " + ex.Message); }
            }
        }
        #endregion

        #region Methods
        private async Task<bool> SaveData()
        {
            if (ID == 0)
            {
                var SupplierName = comboBoxsupplier.SelectedItem.ToString();
                var CategoyName = comboBoxCategory.SelectedItem.ToString();
                await Task.Run(() => SetCategoryId(CategoyName));
                await Task.Run(() => SetSupplierId(SupplierName));
                return await AddData();
            }
            else
            {
                var SupplierName = comboBoxsupplier.SelectedItem.ToString();
                var CategoyName = comboBoxCategory.SelectedItem.ToString();
                await Task.Run(() => SetCategoryId(CategoyName));
                await Task.Run(() => SetSupplierId(SupplierName));
                return await EditData();
            }
        }

        private bool IsFiledsEmpty()
        {
            return comboBoxCategory.SelectedItem == null || comboBoxsupplier.SelectedItem == null || textBoxAmount.Text == string.Empty;
        }

        private async Task<bool> AddData()
        {
            outcome = new Outcome
            {
                CategoryName = comboBoxCategory.SelectedItem.ToString(),
                SupplierName = comboBoxsupplier.SelectedItem.ToString(),
                RecNo = textBoxRecNo.Text,
                Details = richTextBoxDetails.Text,
                Amount = Convert.ToDouble(textBoxAmount.Text),
                OutcomeDate = dateTimePickerDate.Value,
                CategoryId = CategoryId,
                SupplierId = SupplierId,
                ProjectId = ProjectId,
                Image = capturedImage
            };
            var result = await dataHelper.AddAsync(outcome);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " اضافة عملية صرف",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة عملية صرف  " + outcome.CategoryName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);
                if (categoryUserControl != null) categoryUserControl.LoadData();
                return true;
            }
            return false;
        }

        private async Task<bool> EditData()
        {
            outcome = new Outcome
            {
                Id = ID,
                CategoryName = comboBoxCategory.SelectedItem.ToString(),
                SupplierName = comboBoxsupplier.SelectedItem.ToString(),
                RecNo = textBoxRecNo.Text,
                Details = richTextBoxDetails.Text,
                Amount = Convert.ToDouble(textBoxAmount.Text),
                OutcomeDate = dateTimePickerDate.Value,
                CategoryId = CategoryId,
                SupplierId = SupplierId,
                ProjectId = ProjectId,
                Image = capturedImage
            };
            var result = await dataHelper.EditAsync(outcome);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " تعديل عملة صرف",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل عملة صرف  " + outcome.CategoryName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);
                if (categoryUserControl != null) categoryUserControl.LoadData();
                return true;
            }
            return false;
        }

        private async Task SetFiledData()
        {
            var ListSuppliers = await dataHelperSuppliers.GetAllDataAsync();
            if (ListSuppliers != null)
            {
                comboBoxsupplier.DataSource = ListSuppliers.Select(x => x.Name).ToList();
                AutoCompleteStringCollection autoCompleteString = new AutoCompleteStringCollection();
                autoCompleteString.AddRange(ListSuppliers.Select(x => x.Name).ToArray());
                comboBoxsupplier.AutoCompleteCustomSource = autoCompleteString;
            }

            var ListCategories = await dataHelperCategories.GetAllDataAsync();
            if (ListCategories != null)
            {
                comboBoxCategory.DataSource = ListCategories.Select(x => x.Name).ToList();
                AutoCompleteStringCollection autoCompleteStringCategories = new AutoCompleteStringCollection();
                autoCompleteStringCategories.AddRange(ListCategories.Select(x => x.Name).ToArray());
                comboBoxCategory.AutoCompleteCustomSource = autoCompleteStringCategories;
            }

            if (ID > 0)
            {
                outcome = await dataHelper.FindAsync(ID);
                if (outcome != null)
                {
                    comboBoxCategory.SelectedItem = outcome.CategoryName;
                    comboBoxsupplier.SelectedItem = outcome.SupplierName;
                    textBoxRecNo.Text = outcome.RecNo;
                    richTextBoxDetails.Text = outcome.Details;
                    textBoxAmount.Text = outcome.Amount.ToString();
                    dateTimePickerDate.Value = outcome.OutcomeDate;
                    CategoryId = outcome.CategoryId;
                    SupplierId = outcome.SupplierId ?? 0;
                    ProjectId = outcome.ProjectId;

                    if (!string.IsNullOrEmpty(outcome.Image))
                    {
                        capturedImage = outcome.Image;
                        if (labelPath != null) { labelPath.Text = "يوجد ملف مرفق محفوظ"; labelPath.ForeColor = Color.Blue; }
                    }
                    else { if (labelPath != null) { labelPath.Text = "لا يوجد ملف محفوظ"; labelPath.ForeColor = Color.Gray; } }
                }
                else MessageCollections.ShowErrorServer();
            }
        }

        private void SetCategoryId(string CategoyName)
        {
            var cat = dataHelperCategories.GetAllData().FirstOrDefault(x => x.Name == CategoyName);
            if (cat != null) CategoryId = cat.Id;
        }
        private void SetSupplierId(string SupplierName)
        {
            var sup = dataHelperSuppliers.GetAllData().FirstOrDefault(x => x.Name == SupplierName);
            if (sup != null) SupplierId = sup.Id;
        }

        private async void linkLabelNewCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddCategoryForm addCategoryForm = new AddCategoryForm(0, new CategoryUserControl());
            if (addCategoryForm.ShowDialog() == DialogResult.OK)
            {
                var ListCategories = await dataHelperCategories.GetAllDataAsync();
                if (ListCategories != null)
                {
                    comboBoxCategory.DataSource = ListCategories.Select(x => x.Name).ToList();
                }
            }
        }

        private async void linkLabelNewSupplier_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddSuppliersForm addSuppliersForm = new AddSuppliersForm(0, new SuppliersUserControl());
            if (addSuppliersForm.ShowDialog() == DialogResult.OK)
            {
                var ListSuppliers = await dataHelperSuppliers.GetAllDataAsync();
                if (ListSuppliers != null)
                {
                    comboBoxsupplier.DataSource = ListSuppliers.Select(x => x.Name).ToList();
                }
            }
        }
        #endregion
    }
}
