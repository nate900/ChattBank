using ChattBank.Models.Parents.Children;
using ChattBank.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (ValidateInfo())
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
                    MessageBox.Show("The customer's ID is : " + customer.Username);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Something went wrong with creating the new customer");
                }
            }
        }

        public bool ValidateInfo()
        {
            /*
             * first name
             * last name
             * email
             * password
             * state
             */

            // start by validating the first and last names
            TextBox[] txtboxes = this._form.GetTextBoxes();
            if (String.IsNullOrEmpty(txtboxes[0].Text))
            {
                MessageBox.Show("Please enter a first name");
                return false;
            }

            // validate last name
            if (String.IsNullOrEmpty(txtboxes[1].Text))
            {
                MessageBox.Show("Please enter a last name");
                return false;
            }

            // validate email
            Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            if (!validateEmailRegex.IsMatch(txtboxes[2].Text))
            {
                MessageBox.Show("Please enter a valid email address");
                return false;
            }

            // validate password
            if (String.IsNullOrEmpty(txtboxes[3].Text))
            {
                if (txtboxes[3].Text.Length < 4)
                {
                    MessageBox.Show("Please enter a password greater than 3 characters");
                    return false;
                }
            }

            // validate state
            if (String.IsNullOrEmpty(txtboxes[4].Text))
            {
                MessageBox.Show("Please enter a state name or acronym");
                return false;
            }
            return true;
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
