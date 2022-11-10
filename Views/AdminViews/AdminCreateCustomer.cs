using ChattBank.Controllers.AdminControllers;
using ChattBank.Models.Parents.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattBank.Views.AdminViews
{
    public partial class AdminCreateCustomer : Form
    {
        private AdminCreateCustomerController controller;
        public AdminCreateCustomer(Admin model)
        {
            InitializeComponent();
            controller = new AdminCreateCustomerController(this, model);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            controller.GoBack();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.Reset();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            controller.CreateCustomer();
        }

        public TextBox[] GetTextBoxes() { return new TextBox[] { txtFname, txtLname, txtEmail, txtPassword, txtAddress }; }
    }
}
