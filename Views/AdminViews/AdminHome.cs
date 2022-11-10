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
    public partial class AdminHome : Form
    {
        private AdminController controller;
        public AdminHome(Admin model)
        {
            InitializeComponent();
            controller = new AdminController(this, model);
        }

        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            controller.GoToCreateCustomer();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            controller.GoToDeleteCustomer();
        }

        public ListView GetListView() { return this.listView1; }
    }
}
