using ChattBank.Models;
using ChattBank.Models.Parents.Children;
using ChattBank.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Controllers.CustomerControllers
{
    public class CustomerNewAccountController
    {
        private Customer model;
        private NewAccount _form;

        public CustomerNewAccountController(NewAccount form, Customer model)
        {
            this._form = form;
            this.model = model;
        }

        public void CreateCustAccount()
        {
            Account newAccount = new Account();

            // make sure the 'type' is a valid bank account type
            string bankType = _form.GetTextBox().Text;
            bankType = bankType.ToUpper();
            string[] bankTypes = { "SAV", "MMA", "CHK" };
            bool isValid = false;
            // loop through each bank account type
            foreach (var type in bankTypes)
            {
                if (bankType.Equals(type))
                {
                    newAccount.Type = type;
                    isValid = true;
                }
            }

            // if the bank type entered by the customer is not valid, then let them know
            if (!isValid)
            {
                MessageBox.Show("Please enter a valid bank account type: SAV, MMA, CHK");
                ResetTextBox();
                return;
            }
            // get the max account number
            int acctNo = Int32.Parse(newAccount.ReadMAXAcctNo()) + 1;

            // set other account information
            newAccount.AccountId = acctNo.ToString();
            newAccount.CustId = model.Username;
            newAccount.Balance = 0;

            // insert account
            if (newAccount.Create()) MessageBox.Show("Success!!!\nYou created a new account");
            else MessageBox.Show("Something went wrong with creating a new account. Please reach out to ChattBank");
            ResetTextBox();
        }

        // method to take the customer back to the home page
        public void GoBack()
        {
            CustomerHome form = new CustomerHome(model);
            form.Show();
            this._form.Close();
        }

        public void ResetTextBox()
        {
            _form.GetTextBox().Clear();
        }
    }
}
