using Asrfly.Code;
using Asrfly.Core;
using Asrfly.Data;
using ClosedXML.Excel;
using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Gui.GuiProjects
{
    public partial class ProjectsUserControl : UserControl
    {
        private readonly IDataHelper<Projects> dataHelper;
        private IDataHelper<Income> dataHelperIncome;
        private IDataHelper<Outcome> dataHelperOutcome;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static ProjectsUserControl _ProjectsUserControl;
        private int RowId;
        private readonly GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        private double IncomeAmount;
        private double OutcomeAmount;
        private List<int> ListOfProjectId=new List<int>();

        public ProjectsUserControl()
        {
            InitializeComponent();
            SetRoles();
            dataHelper = (IDataHelper<Projects>)ConfigrationObjectManager.GetObject("Projects");
            dataHelperIncome = (IDataHelper<Income>)ConfigrationObjectManager.GetObject("Income");
            dataHelperOutcome = (IDataHelper<Outcome>)ConfigrationObjectManager.GetObject("Outcome");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigrationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }
        #region Events
        private async void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            var dataId = data.Select(x => x.Id).ToArray();
            int index = comboBoxPageNo.SelectedIndex;
            int IndexNoOfRow = index * Properties.Settings.Default.DataGridViewRowNo;

            dataGridView1.DataSource = data.Where(x => x.Id >= dataId[IndexNoOfRow]).Take(Properties.Settings.Default.DataGridViewRowNo).ToList();
            if (dataGridView1.DataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                
            }
            loadingForm.Hide();
            data.Clear();

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddProjectForm addProjectsForm = new AddProjectForm(0, this);
            addProjectsForm.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var Deleteresult = MessageCollections.ShowDeleteDialog();
                if (Deleteresult)
                {
                    IdList.Clear();
                    SetIdRowForDelete();
                    loadingForm.Show();
                    if (IdList.Count > 0)
                    {
                        for (int i = 0; i < IdList.Count; i++)
                        {
                            RowId = IdList[i];
                            var result = await dataHelper.DeleteAsync(RowId);
                            if (result == 1)
                            {
                                SystemRecords systemRecords = new SystemRecords
                                {
                                    Title = "عملية حذف",
                                    UserName = Properties.Settings.Default.UserName,
                                    Details = "تم حذف المشروع ذي الرقم التعريفي " + RowId.ToString(),
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemRecords);

                                // 
                                MessageCollections.ShowDeleteNotificaiton();
                            }
                            else
                            {
                                MessageCollections.ShowErrorServer();
                            }
                        }
                        LoadData();
                    }
                    else
                    {
                        MessageCollections.ShowRequiredDeleteRow();
                    }

                    loadingForm.Hide();
                }

            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }

        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            using (var reader = FastMember.ObjectReader.Create(data))
            {
                dataTable.Load(reader);
            }
            loadingForm.Hide();
            DataTable dataTableArranged = SetDataTableColumns(dataTable);

            ExportAsXlsxFile(dataTableArranged);

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProjectExplor();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            ProjectExplor();
        }
        #endregion

        #region Methods
        public static ProjectsUserControl Instance()
        {
            return _ProjectsUserControl ?? (new ProjectsUserControl());
        }

        private void ProjectExplor()
        {
            if (dataGridView1.RowCount > 0)
            {
                RowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                ProjectManagmentForm projectManagmentForm = new ProjectManagmentForm(RowId);
                projectManagmentForm.Show();
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
        }
        public async void LoadData()
        {
            loadingForm.Show();

            var data = await dataHelper.GetAllDataAsync();
            ListOfProjectId = data.Select(x => x.Id).ToList();
            await Task.Run(() => UpdateData(ListOfProjectId));


            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();


                AddColumn("Id", "المعرف", "Id");
                AddColumn("Name", "اسم المشروع", "Name");
                AddColumn("Customer", "العميل", "Customer");
                AddColumn("Company", "الشركة المنفذة", "Company");
                AddColumn("Income", "المقبوضات", "Income");
                AddColumn("Outcome", "المصروفات", "Outcome");
                AddColumn("Revenue", "الأرباح", "Revenue");
                AddColumn("Details", "التفاصيل", "Details");
                AddColumn("AddedDate", "تاريخ الإضافة", "AddedDate");

            }

            dataGridView1.DataSource = data.Take(Properties.Settings.Default.DataGridViewRowNo).ToList();

            comboBoxPageNo.Items.Clear();
            if (data.Count > 0)
            {
                double value = (Convert.ToDouble(data.Count) / Convert.ToDouble(Properties.Settings.Default.DataGridViewRowNo));
                int NoOfPage = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                for (int i = 0; i < NoOfPage; i++) comboBoxPageNo.Items.Add(i);
            }

            loadingForm.Hide();
        }

        private void AddColumn(string name, string headerText, string dataPropertyName)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.HeaderText = headerText;
            col.DataPropertyName = dataPropertyName;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(col);
        }

        private void Edit()
        {
            if (dataGridView1.RowCount > 0)
            {
                RowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                AddProjectForm addProjectsForm = new AddProjectForm(RowId, this);
                addProjectsForm.Show();
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
        }

        private void SetIdRowForDelete()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    IdList.Add(Convert.ToInt32(row.Cells[0].Value));
                }
            }
        }

        public async void Search()
        {
            loadingForm.Show();
            SearchItem = textBoxSearch.Text;
            dataGridView1.DataSource = await dataHelper.SearchAsync(SearchItem);
            if (dataGridView1.DataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
               
            }
            loadingForm.Hide();
        }

        private DataTable SetDataTableColumns(DataTable dataTable)
        {
            dataTable.Columns["Id"].SetOrdinal(0);
            dataTable.Columns["Id"].ColumnName = "المعرف";
            dataTable.Columns["Name"].SetOrdinal(1);
            dataTable.Columns["Name"].ColumnName = "الاسم";
            dataTable.Columns["Customer"].SetOrdinal(2);
            dataTable.Columns["Customer"].ColumnName = "العميل ";
            dataTable.Columns["Address"].SetOrdinal(3);
            dataTable.Columns["Address"].ColumnName = "العنوان";
            dataTable.Columns["Company"].SetOrdinal(4);
            dataTable.Columns["Company"].ColumnName = " الشركة المنفذة";
            dataTable.Columns["StartDate"].SetOrdinal(5);
            dataTable.Columns["StartDate"].ColumnName = "بداية المشروع";
            dataTable.Columns["FinishDate"].SetOrdinal(6);
            dataTable.Columns["FinishDate"].ColumnName = "نهاية المشروع";
            dataTable.Columns["Details"].SetOrdinal(7);
            dataTable.Columns["Details"].ColumnName = " التفاصيل";
            dataTable.Columns["Income"].SetOrdinal(8);
            dataTable.Columns["Outcome"].ColumnName = " المقبوضات";
            dataTable.Columns["Income"].SetOrdinal(9);
            dataTable.Columns["Outcome"].ColumnName = " المصروفات";
            dataTable.Columns["Revenue"].SetOrdinal(10);
            dataTable.Columns["Revenue"].ColumnName = " الارباح";
            dataTable.Columns["AddedDate"].SetOrdinal(11);
            dataTable.Columns["AddedDate"].ColumnName = " طابع زمني";
            return dataTable;
        }

        private void ExportAsXlsxFile(DataTable dataTableArranged)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "تصدير الملف على شكل اكسل";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Excel File (.xlsx)|*.xlsx";
            saveFileDialog.RestoreDirectory = true;
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook xLWorkbook = new XLWorkbook()) // Creat Excel File
                    {
                        xLWorkbook.AddWorksheet(dataTableArranged, "Data"); // Add Sheet
                        using (MemoryStream ma = new MemoryStream())
                        {
                            xLWorkbook.SaveAs(ma);
                            File.WriteAllBytes(saveFileDialog.FileName, ma.ToArray());
                        }
                    }
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void UpdateData(List<int> ListOfProjectId)
        {
          
            for (int i = 0; i < ListOfProjectId.Count; i++)
            {
                var ProjectId = ListOfProjectId[i];
                try
                {
                    IncomeAmount = dataHelperIncome.GetAllData()
                    .Where(x => x.ProjectId == ProjectId)
                    .Select(x => x.Amount).ToArray().Sum();
                    OutcomeAmount = dataHelperOutcome.GetAllData()
                    .Where(x => x.ProjectId == ProjectId)
                    .Select(x => x.Amount).ToArray().Sum();
                }
                catch { }
                Projects projects = dataHelper.GetAllData()
                    .Where(x => x.Id == ProjectId).First();
                projects.Income = IncomeAmount;
                projects.Outcome = OutcomeAmount;
                projects.Revenue = IncomeAmount-OutcomeAmount;
                dataHelper.Edit(projects);
            }

        }
        private void SetRoles()
        {
            if (!UsersRolesManager.GetRole("checkBoxAdd"))
            {
                buttonAdd.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxDelete"))
            {
                buttonDelete.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxEdit"))
            {
                buttonEdit.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxExport"))
            {
                buttonExport.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxSearch"))
            {
                panel1.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxExplor"))
            {
                buttonOpen.Visible = false;
            }


        }

        #endregion

    }
}

