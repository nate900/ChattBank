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
    public partial class MyInfo : Form
    {
        private CustomerMyInfoController controller;
        public MyInfo(Customer model)
        {
            InitializeComponent();
            this.controller = new CustomerMyInfoController(this, model);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            controller.UpdateMyInfo();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.ResetTextBoxes();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            controller.GoCustHome();
        }

        // method that returns all the textboxes for the current form object
        public TextBox[] GetTextBoxes()
        {
            TextBox[] list = {txtCustId, txtFname, txtLname, txtEmail, txtPassword, txtState };

            return list;
        }
    }
}
