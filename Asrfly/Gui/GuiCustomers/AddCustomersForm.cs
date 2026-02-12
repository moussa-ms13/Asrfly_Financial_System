using Asrfly.Code;
using Asrfly.Core;
using Asrfly.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asrfly.Gui.GuiCustomers
{
    public partial class AddCustomersForm : Form
    {
        private readonly int ID;
        private readonly CustomersUserControl categoryUserControl;
        private Customers customers;
        private readonly IDataHelper<Customers> dataHelper;
        private readonly GuiLoading.LoadingForm loadingForm;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;

        public AddCustomersForm(int Id, CustomersUserControl ctegoryUserControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Customers>)ConfigrationObjectManager.GetObject("Customers");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigrationObjectManager.GetObject("SystemRecords");

            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.categoryUserControl = ctegoryUserControl;
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
                    if (ID == 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageCollections.ShowAddNotificaiton();
                    }
                    else
                    {
                        MessageCollections.ShowUpdateNotificaiton();
                    }
                    Close();
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
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
                    if (ID == 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageCollections.ShowAddNotificaiton();
                    }
                    else
                    {
                        MessageCollections.ShowUpdateNotificaiton();
                    }
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
                loadingForm.Hide();
            }
        }

        private async void AddCategoryForm_Load(object sender, EventArgs e)
        {
            loadingForm.Show();
            await SetFiledData();
            loadingForm.Hide();
        }
        #endregion

        #region Methods

        private async Task<bool> SaveData()
        {
            if (ID == 0)
            {
                return await AddData();
            }
            else
            {
                return await EditData();
            }
        }

        private bool IsFiledsEmpty()
        {
            if (textBoxName.Text == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> AddData()
        {
            customers = new Customers
            {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                Details = richTextBoxDetails.Text,
                AddedDate = DateTime.Now,
                Email = textBoxEmail.Text,
            };

            var result = await dataHelper.AddAsync(customers);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " اضافة عميل",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة عميل  " + customers.Name,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);

                if (categoryUserControl != null) categoryUserControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> EditData()
        {
            customers = new Customers
            {
                Id = ID,
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                Details = richTextBoxDetails.Text,
                AddedDate = DateTime.Now,
                Email = textBoxEmail.Text,
            };

            var result = await dataHelper.EditAsync(customers);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " تعديل عميل",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل عميل  " + customers.Name,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);

                if (categoryUserControl != null) categoryUserControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task SetFiledData()
        {
            if (ID > 0)
            {
                customers = await dataHelper.FindAsync(ID);
                if (customers != null)
                {
                    textBoxName.Text = customers.Name;
                    textBoxEmail.Text = customers.Email;
                    textBoxPhoneNumber.Text = customers.PhoneNumber;
                    textBoxAddress.Text = customers.Address;
                    textBoxBalance.Text = customers.Balance.ToString();
                    richTextBoxDetails.Text = customers.Details;
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }
        #endregion
    }
}
