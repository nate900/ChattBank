using ChattBank.Models.Parents.Children;
using ChattBank.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Controllers.AdminControllers
{
    public class AdminCreateCustomerController
    {
        // data fields
        private Admin model;
        private AdminCreateCustomer _form;

        public AdminCreateCustomerController(AdminCreateCustomer form, Admin model)
        {
            this._form = form;
            this.model = model;
        }

        public void CreateCustomer()
        {
            // get textbox data
            TextBox[] txtboxes = this._form.GetTextBoxes();
            // create a customer model
            Customer customer = new Customer();
            // insert a new row into database
            customer.Fname = txtboxes[0].Text;
            customer.Lname = txtboxes[1].Text;
            customer.Email = txtboxes[2].Text;
            customer.Password = txtboxes[3].Text;
            customer.Address = txtboxes[4].Text;

            if (customer.Create())
            {
                MessageBox.Show("Success!! Customer was created");
            }
            else
            {
                MessageBox.Show("Something went wrong with creating the new customer");
            }
        }

        public void Reset()
        {
            foreach(var box in this._form.GetTextBoxes())
            {
                box.Clear();
            }
        }

        public void GoBack()
        {
            AdminHome form = new AdminHome(model);
            form.Show();
            this._form.Close();
        }
    }
}
