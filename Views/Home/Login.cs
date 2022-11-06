using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChattBank.Controllers;

namespace ChattBank.Views.Home
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            HomeController homeController = new HomeController(this, sender, e);
            homeController.Reset();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            HomeController homeController = new HomeController(this, sender, e);
            if (!homeController.Login())
            {
                MessageBox.Show("Please enter valid credentials");
            }

        }

        public TextBox GetUsernameTxt()
        {
            return txtUsername;
        }

        public TextBox GetPasswordTxt()
        {
            return txtPassword;
        }
    }
}
