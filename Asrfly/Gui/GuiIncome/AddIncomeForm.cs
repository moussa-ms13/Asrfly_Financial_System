using Asrfly.Code;
using Asrfly.Core;
using Asrfly.Data;
using Asrfly.Gui.GuiCategories;
using Asrfly.Gui.GuiCustomers;
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

namespace Asrfly.Gui.GuiIncome
{
    public partial class AddIncomeForm : Form
    {
        private readonly int ID;
        private readonly IncomeUserControl categoryUserControl;
        private Income income;
        private int CategoryId;
        private int SupplierId;
        private int ProjectId;
        private readonly IDataHelper<Income> dataHelper;
        private readonly IDataHelper<Customers> dataHelperCustomers;
        private readonly IDataHelper<Categories> dataHelperCategories;
        private readonly GuiLoading.LoadingForm loadingForm;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private string capturedImage = string.Empty;

        public AddIncomeForm(int Id, int ProjectId, IncomeUserControl ctegoryUserControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Income>)ConfigrationObjectManager.GetObject("Income");
            dataHelperCustomers = (IDataHelper<Customers>)ConfigrationObjectManager.GetObject("Customers");
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

                    try
                    {
                        ReceiptPrinter printer = new ReceiptPrinter();
                        printer.Print(income);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("تم الحفظ ولكن حدث خطأ في الطباعة: " + ex.Message);
                    }

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

        private async void AddIncomeForm_Load(object sender, EventArgs e)
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
                    if (labelPath != null) labelPath.Text = Path.GetFileName(openFileDialog.FileName);
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
            income = new Income
            {
                CategoryName = comboBoxCategory.SelectedItem.ToString(),
                SupplierName = comboBoxsupplier.SelectedItem.ToString(),
                RecNo = textBoxRecNo.Text,
                Details = richTextBoxDetails.Text,
                Amount = Convert.ToDouble(textBoxAmount.Text),
                IncomeDate = dateTimePickerDate.Value,
                CategoryId = CategoryId,
                SupplierId = SupplierId,
                ProjectId = ProjectId,
                Image = capturedImage
            };
            var result = await dataHelper.AddAsync(income);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " اضافة عملية قبض",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة عملية قبض  " + income.CategoryName,
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
            income = new Income
            {
                Id = ID,
                CategoryName = comboBoxCategory.SelectedItem.ToString(),
                SupplierName = comboBoxsupplier.SelectedItem.ToString(),
                RecNo = textBoxRecNo.Text,
                Details = richTextBoxDetails.Text,
                Amount = Convert.ToDouble(textBoxAmount.Text),
                IncomeDate = dateTimePickerDate.Value,
                CategoryId = CategoryId,
                SupplierId = SupplierId,
                ProjectId = ProjectId,
                Image = capturedImage
            };
            var result = await dataHelper.EditAsync(income);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " تعديل عملة قبض",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل عملة قبض  " + income.CategoryName,
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
            var ListCustomers = await dataHelperCustomers.GetAllDataAsync();
            if (ListCustomers != null)
            {
                comboBoxsupplier.DataSource = ListCustomers.Select(x => x.Name).ToList();
                AutoCompleteStringCollection autoCompleteString = new AutoCompleteStringCollection();
                autoCompleteString.AddRange(ListCustomers.Select(x => x.Name).ToArray());
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
                income = await dataHelper.FindAsync(ID);
                if (income != null)
                {
                    comboBoxCategory.SelectedItem = income.CategoryName;
                    comboBoxsupplier.SelectedItem = income.SupplierName;
                    textBoxRecNo.Text = income.RecNo;
                    richTextBoxDetails.Text = income.Details;
                    textBoxAmount.Text = income.Amount.ToString();
                    dateTimePickerDate.Value = income.IncomeDate;
                    CategoryId = income.CategoryId;
                    SupplierId = income.SupplierId??0;
                    ProjectId = income.ProjectId;

                    if (!string.IsNullOrEmpty(income.Image))
                    {
                        capturedImage = income.Image;
                        if (labelPath != null) labelPath.Text = "يوجد ملف مرفق محفوظ";
                    }
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
            var sup = dataHelperCustomers.GetAllData().FirstOrDefault(x => x.Name == SupplierName);
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

        private async void linkLabelNewCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddCustomersForm addCustomersForm = new AddCustomersForm(0, new CustomersUserControl());
            if (addCustomersForm.ShowDialog() == DialogResult.OK)
            {
                var ListCustomers = await dataHelperCustomers.GetAllDataAsync();
                if (ListCustomers != null)
                {
                    comboBoxsupplier.DataSource = ListCustomers.Select(x => x.Name).ToList();
                }
            }
        }
        #endregion
    }
}
