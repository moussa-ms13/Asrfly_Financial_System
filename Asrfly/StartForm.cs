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
using Microsoft.EntityFrameworkCore;

namespace Asrfly
{
    public partial class StartForm : Form
    {
        private readonly IDataHelper<Users> dataHelper;

        public StartForm()
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Users>)ConfigrationObjectManager.GetObject("Users");
        }

        private async void CheckCon()
        {
            try
            {
                labelState.Text = "جاري الاتصال بقاعدة البيانات...";


                var usersList = await dataHelper.GetAllDataAsync();

                if (usersList != null && usersList.Count > 0)
                {
                    Gui.GuiUsers.UserLoginForm loginForm = new Gui.GuiUsers.UserLoginForm();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    Gui.GuiUsers.AddUserForm addUserForm = new Gui.GuiUsers.AddUserForm(0, null, true);
                    addUserForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                this.Hide();

                var result = MessageBox.Show(
                    "فشل الاتصال بقاعدة البيانات السحابية.\n\n" +
                    "السبب: " + ex.Message + "\n\n" +
                    "هل تود إعادة المحاولة؟",
                    "خطأ في الاتصال",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);

                if (result == DialogResult.Retry)
                {
                    this.Show();
                    CheckCon();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckCon();
        }
    }
}
