using ChattBank.Controllers.AdminControllers;
using ChattBank.Models.Parents.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattBank.Views.AdminViews
{
    public partial class AdminDeleteCustomer : Form
    {
        private AdminDeleteCustomerController controller;
        public AdminDeleteCustomer(Admin model)
        {
            InitializeComponent();
            controller = new AdminDeleteCustomerController(this, model);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            controller.GoBack();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            controller.DeleteCustomer();
        }

        public TextBox GetTextBox() { return txtCustomerId; }
    }
}
