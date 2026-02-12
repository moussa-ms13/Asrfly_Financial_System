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

namespace Asrfly.Gui.GuiUsers
{
    public partial class AddUserForm : Form
    {
        private readonly int ID;
        private readonly UsersControl categoryUserControl;
        private readonly bool firstStart;
        private Users users;
        private readonly IDataHelper<Users> dataHelper;
        private readonly IDataHelper<UsersRoles> dataHelperUsersRoles;
        private readonly GuiLoading.LoadingForm loadingForm;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private Dictionary<string, bool> ListOfRoles = new Dictionary<string, bool>();

        public AddUserForm(int Id, UsersControl ctegoryUserControl, bool FirstStart)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Users>)ConfigrationObjectManager.GetObject("Users");
            dataHelperUsersRoles = (IDataHelper<UsersRoles>)ConfigrationObjectManager.GetObject("UsersRoles");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigrationObjectManager.GetObject("SystemRecords");

            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.categoryUserControl = ctegoryUserControl;
            firstStart = FirstStart;
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

                    if (firstStart == true)
                    {
                        MessageBox.Show("اعد تشغيل البرنامج لطفا");
                        Application.Exit();
                    }
                    else
                    {
                        Close();
                    }
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

        private void AddCategoryForm_Load(object sender, EventArgs e)
        {
            loadingForm.Show();
            SetFiledData();
            loadingForm.Hide();
            if (firstStart == true)
            {
                buttonSave.Visible = false;
            }
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
            if (textBoxName.Text == string.Empty
                || textBoxUserName.Text == string.Empty
                || textBoxPassword.Text == string.Empty
                )
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
            users = new Users
            {
                FullName = textBoxName.Text,
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                Phone = textBoxPhoneNumber.Text,
                Email = textBoxEmail.Text,
                AddedDate = DateTime.Now,
            };

            var result = await dataHelper.AddAsync(users);

            if (result == 1)
            {
                SetRoles();

                var newUserId = users.Id;

                for (int i = 0; i < ListOfRoles.Count; i++)
                {
                    UsersRoles usersRoles = new UsersRoles
                    {
                        UserId = newUserId,
                        Key = ListOfRoles.Keys.ToList()[i],
                        Value = ListOfRoles.Values.ToList()[i]
                    };
                    await dataHelperUsersRoles.AddAsync(usersRoles);
                }

                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " اضافة مستخدم",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة مستخدم  " + users.UserName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);

                if (categoryUserControl != null)
                    categoryUserControl.LoadData();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetRoles()
        {
            ListOfRoles.Clear();
            ListOfRoles.Add(checkBoxHome.Name, checkBoxHome.Checked);
            ListOfRoles.Add(checkBoxCategory.Name, checkBoxCategory.Checked);
            ListOfRoles.Add(checkBoxSupplier.Name, checkBoxSupplier.Checked);
            ListOfRoles.Add(checkBoxCustoemr.Name, checkBoxCustoemr.Checked);
            ListOfRoles.Add(checkBoxProjects.Name, checkBoxProjects.Checked);
            ListOfRoles.Add(checkBoxUsers.Name, checkBoxUsers.Checked);
            ListOfRoles.Add(checkBoxSettings.Name, checkBoxSettings.Checked);
            ListOfRoles.Add(checkBoxSystemRecords.Name, checkBoxSystemRecords.Checked);

            ListOfRoles.Add(checkBoxAccessCategory.Name, checkBoxAccessCategory.Checked);
            ListOfRoles.Add(checkBoxAccessSupllier.Name, checkBoxAccessSupllier.Checked);
            ListOfRoles.Add(checkBoxAccessCusteorm.Name, checkBoxAccessCusteorm.Checked);
            ListOfRoles.Add(checkBoxAccessProjects.Name, checkBoxAccessProjects.Checked);
            ListOfRoles.Add(checkBoxAccessUsers.Name, checkBoxAccessUsers.Checked);
            ListOfRoles.Add(checkBoxAccessOutcome.Name, checkBoxAccessOutcome.Checked);
            ListOfRoles.Add(checkBoxAccesIncome.Name, checkBoxAccesIncome.Checked);

            ListOfRoles.Add(checkBoxAdd.Name, checkBoxAdd.Checked);
            ListOfRoles.Add(checkBoxDelete.Name, checkBoxDelete.Checked);
            ListOfRoles.Add(checkBoxEdit.Name, checkBoxEdit.Checked);
            ListOfRoles.Add(checkBoxExport.Name, checkBoxExport.Checked);
            ListOfRoles.Add(checkBoxSearch.Name, checkBoxSearch.Checked);
            ListOfRoles.Add(checkBoxExplor.Name, checkBoxExplor.Checked);
        }

        private async Task<bool> EditData()
        {
            users = new Users
            {
                Id = ID,
                FullName = textBoxName.Text,
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                Phone = textBoxPhoneNumber.Text,
                Email = textBoxEmail.Text,
                AddedDate = DateTime.Now,
            };

            var result = await dataHelper.EditAsync(users);
            if (result == 1)
            {
                var rolesData = await dataHelperUsersRoles.GetAllDataAsync();

                var ListOfRolesId = rolesData.Where(x => x.UserId == ID).Select(X => X.Id).ToList();

                for (int j = 0; j < ListOfRolesId.Count; j++)
                {
                    var roleId = ListOfRolesId[j];
                    await dataHelperUsersRoles.DeleteAsync(roleId);
                }

                SetRoles();

                for (int i = 0; i < ListOfRoles.Count; i++)
                {
                    UsersRoles usersRoles = new UsersRoles
                    {
                        UserId = ID,
                        Key = ListOfRoles.Keys.ToList()[i],
                        Value = ListOfRoles.Values.ToList()[i]
                    };
                    await dataHelperUsersRoles.AddAsync(usersRoles);
                }

                SystemRecords systemRecords = new SystemRecords
                {
                    Title = " تعديل مستخدم",
                    UserName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل مستخدم  " + users.UserName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);

                if (categoryUserControl != null)
                    categoryUserControl.LoadData();

                return true;
            }
            else
            {
                return false;
            }
        }

        private async void SetFiledData()
        {
            if (ID > 0)
            {
                users = await dataHelper.FindAsync(ID);

                var rolesData = await dataHelperUsersRoles.GetAllDataAsync();
                var currentRoles = rolesData.Where(X => X.UserId == ID).ToList();

                var ListOfRolesValues = currentRoles.Select(x => x.Value).ToList();

                if (users != null && ListOfRolesValues.Count >= 21)
                {
                    textBoxName.Text = users.FullName;
                    textBoxUserName.Text = users.UserName;
                    textBoxPassword.Text = users.Password;
                    textBoxPhoneNumber.Text = users.Phone;
                    textBoxEmail.Text = users.Email;

                    checkBoxHome.Checked = ListOfRolesValues[0];
                    checkBoxCategory.Checked = ListOfRolesValues[1];
                    checkBoxSupplier.Checked = ListOfRolesValues[2];
                    checkBoxCustoemr.Checked = ListOfRolesValues[3];
                    checkBoxProjects.Checked = ListOfRolesValues[4];
                    checkBoxUsers.Checked = ListOfRolesValues[5];
                    checkBoxSettings.Checked = ListOfRolesValues[6];
                    checkBoxSystemRecords.Checked = ListOfRolesValues[7];

                    checkBoxAccessCategory.Checked = ListOfRolesValues[8];
                    checkBoxAccessSupllier.Checked = ListOfRolesValues[9];
                    checkBoxAccessCusteorm.Checked = ListOfRolesValues[10];
                    checkBoxAccessProjects.Checked = ListOfRolesValues[11];
                    checkBoxAccessUsers.Checked = ListOfRolesValues[12];
                    checkBoxAccessOutcome.Checked = ListOfRolesValues[13];
                    checkBoxAccesIncome.Checked = ListOfRolesValues[14];

                    checkBoxAdd.Checked = ListOfRolesValues[15];
                    checkBoxDelete.Checked = ListOfRolesValues[16];
                    checkBoxEdit.Checked = ListOfRolesValues[17];
                    checkBoxExport.Checked = ListOfRolesValues[18];
                    checkBoxSearch.Checked = ListOfRolesValues[19];
                    checkBoxExplor.Checked = ListOfRolesValues[20];
                }
                else if (users == null)
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AddUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (firstStart == true)
            {
                Application.Exit();
            }
        }
    }
}
