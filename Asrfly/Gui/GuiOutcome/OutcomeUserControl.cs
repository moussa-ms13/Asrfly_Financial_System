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

namespace Asrfly.Gui.GuiOutcome
{
    public partial class OutcomeUserControl : UserControl
    {
        private readonly IDataHelper<Outcome> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static OutcomeUserControl _OutcomeUserControl;
        private int RowId;
        private readonly GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        private int ProjectId;

        public OutcomeUserControl(int ProjectId)
        {
            InitializeComponent();
            SetRoles();
            this.ProjectId = ProjectId;
            dataHelper = (IDataHelper<Outcome>)ConfigrationObjectManager.GetObject("Outcome");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigrationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        #region Events
        private async void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            if (data != null)
            {
                var dataId = data.Select(x => x.Id).ToArray();
                int index = comboBoxPageNo.SelectedIndex;
                int IndexNoOfRow = index * Properties.Settings.Default.DataGridViewRowNo;

                dataGridView1.DataSource = data.Where(x => x.Id >= dataId[IndexNoOfRow] && x.ProjectId == ProjectId).Take(Properties.Settings.Default.DataGridViewRowNo).ToList();
                SetColumnsTitle();
                data.Clear();
            }
            else
            {
                MessageCollections.ShowErrorServer();
            }
            loadingForm.Hide();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddOutcomeForm addOutcomeForm = new AddOutcomeForm(0, ProjectId, this);
            addOutcomeForm.Show();
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
                                    Details = "تم حذف عملية الصرف ذي الرقم التعريفي " + RowId.ToString(),
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemRecords);
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
            if (data != null)
            {
                using (var reader = FastMember.ObjectReader.Create(data))
                {
                    dataTable.Load(reader);
                }
                loadingForm.Hide();
                DataTable dataTableArranged = SetDataTableColumns(dataTable);
                ExportAsXlsxFile(dataTableArranged);
            }
            else
            {
                loadingForm.Hide();
                MessageCollections.ShowErrorServer();
            }
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

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                var outcomeObj = await dataHelper.FindAsync(id);

                if (outcomeObj != null && !string.IsNullOrEmpty(outcomeObj.Image))
                {
                    Asrfly.Gui.GuiImageViewer.ImageViewerForm viewer = new Asrfly.Gui.GuiImageViewer.ImageViewerForm();
                    viewer.SetImage(outcomeObj.Image);
                    viewer.ShowDialog();
                }
                else
                {
                    Edit();
                }
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region Methods

        public async void LoadData()
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();

            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                AddColumn("Id", "المعرف", "Id");
                AddColumn("CategoryName", "اسم الصنف", "CategoryName");
                AddColumn("SupplierName", "المورد", "SupplierName");
                AddColumn("OutcomeDate", "تاريخ الصرف", "OutcomeDate");
                AddColumn("RecNo", "رقم الوصل", "RecNo");
                AddColumn("Amount", "المبلغ", "Amount");
                AddColumn("Details", "التفاصيل", "Details");
            }

            if (data != null)
            {
                var projectData = data.Where(x => x.ProjectId == ProjectId).ToList();

                dataGridView1.DataSource = projectData.Take(Properties.Settings.Default.DataGridViewRowNo).ToList();

                comboBoxPageNo.Items.Clear();
                if (projectData.Count > 0)
                {
                    double value = (Convert.ToDouble(projectData.Count) / Convert.ToDouble(Properties.Settings.Default.DataGridViewRowNo));
                    int NoOfPage = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    for (int i = 0; i < NoOfPage; i++) comboBoxPageNo.Items.Add(i);
                }
            }
            else
            {
                MessageCollections.ShowErrorServer();
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

        private void SetColumnsTitle()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "المعرف";
                dataGridView1.Columns[1].HeaderText = "الصنف";
                dataGridView1.Columns[2].HeaderText = "المورد";
                dataGridView1.Columns[3].HeaderText = "تاريخ الصرف";
                dataGridView1.Columns[4].HeaderText = "رقم الوصل";
                dataGridView1.Columns[5].HeaderText = "المبلغ";
                dataGridView1.Columns[6].HeaderText = "التفاصيل";

                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;

                if (dataGridView1.Columns.Contains("Image"))
                {
                    dataGridView1.Columns["Image"].Visible = false;
                }
                else if (dataGridView1.Columns.Count > 13)
                {
                    dataGridView1.Columns[13].Visible = false;
                }
            }
        }

        private void Edit()
        {
            if (dataGridView1.RowCount > 0)
            {
                RowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                AddOutcomeForm addOutcomeForm = new AddOutcomeForm(RowId, ProjectId, this);
                addOutcomeForm.Show();
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
            var data = await dataHelper.SearchAsync(SearchItem);

            if (data != null)
            {
                dataGridView1.DataSource = data.Where(x => x.ProjectId == ProjectId).ToList();
                if (dataGridView1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitle();
                }
                data.Clear();
            }
            else
            {
                MessageCollections.ShowErrorServer();
            }
            loadingForm.Hide();
        }

        private DataTable SetDataTableColumns(DataTable dataTable)
        {
            dataTable.Columns["Id"].SetOrdinal(0);
            dataTable.Columns["Id"].ColumnName = "المعرف";
            dataTable.Columns["CategoryName"].SetOrdinal(1);
            dataTable.Columns["CategoryName"].ColumnName = "اسم الصنف";
            dataTable.Columns["SupplierName"].SetOrdinal(2);
            dataTable.Columns["SupplierName"].ColumnName = "المورد ";
            dataTable.Columns["OutcomeDate"].SetOrdinal(3);
            dataTable.Columns["OutcomeDate"].ColumnName = "تاريخ الصرف";
            dataTable.Columns["RecNo"].SetOrdinal(4);
            dataTable.Columns["RecNo"].ColumnName = "رقم الوصل";
            dataTable.Columns["Amount"].SetOrdinal(5);
            dataTable.Columns["Amount"].ColumnName = "المبلغ";
            dataTable.Columns["Details"].SetOrdinal(6);
            dataTable.Columns["Details"].ColumnName = "التفاصيل";

            dataTable.Columns.Remove("Categories");
            dataTable.Columns.Remove("CategoryId");
            dataTable.Columns.Remove("ProjectId");
            dataTable.Columns.Remove("Projects");
            dataTable.Columns.Remove("SupplierId");
            dataTable.Columns.Remove("Suppliers");

            if (dataTable.Columns.Contains("Image"))
            {
                dataTable.Columns.Remove("Image");
            }

            dataTable.AcceptChanges();
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
                    using (XLWorkbook xLWorkbook = new XLWorkbook())
                    {
                        xLWorkbook.AddWorksheet(dataTableArranged, "Data");
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
        }
        #endregion
    }
}
