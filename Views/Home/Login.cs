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
        // home controller object variable
        private HomeController homeController;
        public Login()
        {
            InitializeComponent();
            // initialize homecontroller object variable to an object
            homeController = new HomeController(this);
        }

        // button to reset login info
        private void btnReset_Click(object sender, EventArgs e)
        {
            homeController.Reset();
        }

        // button to login the user
        private void btnLogin_Click(object sender, EventArgs e)
        {
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
