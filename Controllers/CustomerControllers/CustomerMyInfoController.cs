using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChattBank.Models.Parents.Children;
using ChattBank.Views.CustomerViews;

namespace ChattBank.Controllers.CustomerControllers
{
    public class CustomerMyInfoController
    {
        private Customer model;
        private MyInfo _form;

        public CustomerMyInfoController(MyInfo form, Customer model)
        {
            _form = form;
            this.model = model;
            PopulateMyInfo();
        }

        public void PopulateMyInfo()
        {
            TextBox[] textboxes = _form.GetTextBoxes();
            int i = 0;
            textboxes[i++].Text = model.Username;
            textboxes[i++].Text = model.Fname;
            textboxes[i++].Text = model.Lname;
            textboxes[i++].Text = model.Email;
            textboxes[i++].Text = model.Password;
            textboxes[i++].Text = model.Address;
        }

        public void GoCustHome()
        {
            CustomerHome form = new CustomerHome(model);
            form.Show();
            _form.Close();
        }

        public void UpdateMyInfo()
        {
            TextBox[] textboxes = _form.GetTextBoxes();
            if (ValidateMyInfo())
            {
                int i = 0;
                model.Username = textboxes[i++].Text;
                model.Fname = textboxes[i++].Text;
                model.Lname = textboxes[i++].Text;
                model.Email = textboxes[i++].Text;
                model.Password = textboxes[i++].Text;
                model.Address = textboxes[i++].Text;
                model.Update();
            }
        }

        public bool ValidateMyInfo()
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
            if (String.IsNullOrEmpty(txtboxes[1].Text))
            {
                MessageBox.Show("Please enter a first name");
                return false;
            }

            // validate last name
            if (String.IsNullOrEmpty(txtboxes[2].Text))
            {
                MessageBox.Show("Please enter a last name");
                return false;
            }

            // validate email
            Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            if (!validateEmailRegex.IsMatch(txtboxes[3].Text))
            {
                MessageBox.Show("Please enter a valid email address");
                return false;
            }

            // validate password
            if (String.IsNullOrEmpty(txtboxes[4].Text))
            {
                if (txtboxes[4].Text.Length < 4)
                {
                    MessageBox.Show("Please enter a password greater than 3 characters");
                    return false;
                }
            }

            // validate state
            if (String.IsNullOrEmpty(txtboxes[5].Text))
            {
                MessageBox.Show("Please enter a state name or acronym");
                return false;
            }
            return true;
        }

        public void ResetTextBoxes()
        {
            TextBox[] txtboxes = this._form.GetTextBoxes();
            for(int i = 1; i < txtboxes.Length; i++)
            {
                txtboxes[i].Clear();
            }

        }
    }
}
