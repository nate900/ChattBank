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
    public partial class WithdrawForm : Form
    {
        private CustomerWithdrawController controller;

        public WithdrawForm(Customer model)
        {
            InitializeComponent();
            controller = new CustomerWithdrawController(this, model);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            controller.Refresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.ResetTextBoxes();
        }

        private void btnMakeWithdraw_Click(object sender, EventArgs e)
        {
            controller.MakeWithdraw();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            controller.GoBack();
        }

        public TextBox[] GetTextBoxes() { return new TextBox[] { this.txtAccountNo, this.txtAmount, this.txtDesc }; }
        public ListView GetListView() { return this.listView1; }
    }
}
