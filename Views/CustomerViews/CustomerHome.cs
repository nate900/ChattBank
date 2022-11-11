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
using ChattBank.Models.Parents.Children;

namespace ChattBank.Views.CustomerViews
{
    public partial class CustomerHome : Form
    {
        // controller variable
        private CustomerHomeController controller;

        // constructor that takes a customer model object and passes it to the CustomerController object to perform operations
        public CustomerHome(Customer model)
        {
            InitializeComponent();
            controller = new CustomerHomeController(this, model);
        }

        // button event to add a new customer account relating to the current customer user
        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            controller.GoNewAccount();
        }

        // deposit button to make assertions to the specified account
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            controller.GoDeposit();
        }

        // withdrawal button to make deductions from the specified account
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            controller.GoWithdraw();
        }

        // gives the current customer access to his, or her, information
        private void btnMyInfo_Click(object sender, EventArgs e)
        {
            controller.GoMyInfo();
        }

        // method to return the reference to the listView1 object
        public ListView GetAccountsList()
        {
            return this.listView1;
        }

        public ListView GetAccountActivity()
        {
            return this.listView2;
        }

        public Label GetWelcomeLabel() { return this.lblWelcome; }
    }
}
