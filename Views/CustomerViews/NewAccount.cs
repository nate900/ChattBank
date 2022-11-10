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
using ChattBank.Controllers.CustomerControllers;

namespace ChattBank.Views.CustomerViews
{
    public partial class NewAccount : Form
    {
        private CustomerNewAccountController controller;
        public NewAccount(Customer model)
        {
            InitializeComponent();
            controller = new CustomerNewAccountController(this, model);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.ResetTextBox();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            controller.CreateCustAccount();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            controller.GoBack();
        }

        public TextBox GetTextBox() { return this.txtType; }
        
    }
}
