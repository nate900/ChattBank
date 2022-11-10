using ChattBank.Controllers.CustomerControllers;
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

namespace ChattBank.Views.CustomerViews
{
    public partial class DepositForm : Form
    {
        private CustomerDepositController controller;
        public DepositForm(Customer model)
        {
            InitializeComponent();
            controller = new CustomerDepositController(this, model);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            controller.GoBack();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.ResetTextBoxes();
        }

        private void btnMakeDeposit_Click(object sender, EventArgs e)
        {
            controller.MakeDeposit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            controller.Refresh();
        }

        public TextBox[] GetTextBoxes() { return new TextBox[] { txtAcctNo, txtDepoAmount }; }

        public ListView GetListView() { return this.listView1; }
    }
}
