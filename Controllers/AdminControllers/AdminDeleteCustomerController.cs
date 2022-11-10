using ChattBank.Models.Parents.Children;
using ChattBank.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Controllers.AdminControllers
{
    public class AdminDeleteCustomerController
    {
        // data fields
        private Admin model;
        private AdminDeleteCustomer _form;

        public AdminDeleteCustomerController(AdminDeleteCustomer form, Admin model)
        {
            this._form = form;
            this.model = model;
        }

        public void DeleteCustomer()
        {
            // validate textbox data and delete the customer
            if (String.IsNullOrEmpty(this._form.GetTextBox().Text))
            {
                MessageBox.Show("Please enter a valid customer ID");
                return;
            }
            if(!int.TryParse(this._form.GetTextBox().Text, out int id))
            {
                MessageBox.Show("Please enter a valid customer ID");
                return;
            }

            // delete the customer
            Customer customer = new Customer();
            if (!customer.Read(this._form.GetTextBox().Text)) 
            {
                MessageBox.Show("Please enter a valid customer ID");
                return;
            }
            else
            {
                if (customer.GetAccounts())
                {
                    foreach (var acct in customer.Accounts())
                    {
                        acct.Delete();
                    }

                    customer.Delete();

                    MessageBox.Show("Success!! The customer has been deleted");
                }else if (customer.Delete())
                {
                    MessageBox.Show("Success!! The customer has been deleted");
                }
                else
                {
                    MessageBox.Show("Something went wrong with deleting the customer");
                }
            }
        }

        public void GoBack()
        {
            AdminHome form = new AdminHome(model);
            form.Show();
            this._form.Close();
        }

        public void Reset()
        {
            this._form.GetTextBox().Clear();
        }
    }
}
