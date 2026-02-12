using Asrfly.Code;
using Asrfly.Core;
using Asrfly.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asrfly.Gui.GuiProjects
{
    public partial class AddProjectForm : Form
    {
        private readonly int ID;
        private readonly ProjectsUserControl categoryUserControl;
        private Projects projects;
        private readonly IDataHelper<Projects> dataHelper;
        private readonly IDataHelper<Customers> dataHelperCustomers;
        private readonly GuiLoading.LoadingForm loadingForm;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;

        public AddProjectForm(int Id, ProjectsUserControl ctegoryUserControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Projects>)ConfigrationObjectManager.GetObject("Projects");
            dataHelperCustomers = (IDataHelper<Customers>)ConfigrationObjectManager.GetObject("Customers");
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
                        MessageCollections.ShowAddNotificaiton();
                        this.DialogResult = DialogResult.OK;
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
            projects = new Projects
            {
                Name = textBoxName.Text,
                Customer = comboBoxCustomer.Text,
                Company = textBoxComapny.Text,
                StartDate = dateTimePickerStartDate.Value,
                FinishDate = dateTimePickerFinish.Value,
                Address = textBoxAddress.Text,
                Details = richTextBoxDetails.Text,
                Income = Convert.ToDouble(textBoxIncome.Text),
                Outcome = Convert.ToDouble(textBoxOutcome.Text),
                Revenue = Convert.ToDouble(textBoxRevenue.Text),
                AddedDate = DateTime.Now,
            };

            var result = await dataHelper.AddAsync(projects);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " اضافة مشروع",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة مشروع  " + projects.Name,
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
            projects = new Projects
            {
                Id = ID,
                Name = textBoxName.Text,
                Customer = comboBoxCustomer.Text,
                Company = textBoxComapny.Text,
                StartDate = dateTimePickerStartDate.Value,
                FinishDate = dateTimePickerFinish.Value,
                Address = textBoxAddress.Text,
                Details = richTextBoxDetails.Text,
                Income = Convert.ToDouble(textBoxIncome.Text),
                Outcome = Convert.ToDouble(textBoxOutcome.Text),
                Revenue = Convert.ToDouble(textBoxRevenue.Text),
                AddedDate = DateTime.Now,
            };

            var result = await dataHelper.EditAsync(projects);
            if (result == 1)
            {
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " تعديل مشروع",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل مشروع  " + projects.Name,
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
            var ListCusotmers = await dataHelperCustomers.GetAllDataAsync();

            if (ListCusotmers != null)
            {
                comboBoxCustomer.DataSource = ListCusotmers.Select(x => x.Name).ToList();

                AutoCompleteStringCollection autoCompleteString = new AutoCompleteStringCollection();
                autoCompleteString.AddRange(ListCusotmers.Select(x => x.Name).ToArray());
                comboBoxCustomer.AutoCompleteCustomSource = autoCompleteString;

            }

            if (ID > 0)
            {
                projects = await dataHelper.FindAsync(ID);
                if (projects != null)
                {
                    textBoxName.Text = projects.Name;
                    comboBoxCustomer.Text = projects.Customer;
                    textBoxComapny.Text = projects.Company;
                    dateTimePickerStartDate.Value = projects.StartDate;
                    dateTimePickerFinish.Value = projects.FinishDate;
                    textBoxAddress.Text = projects.Address;
                    richTextBoxDetails.Text = projects.Details;
                    textBoxIncome.Text = projects.Income.ToString();
                    textBoxOutcome.Text = projects.Outcome.ToString();
                    textBoxRevenue.Text = projects.Revenue.ToString();
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
