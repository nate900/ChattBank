using ChattBank.Models;
using ChattBank.Models.Parents.Children;
using ChattBank.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Controllers.CustomerControllers
{
    public class CustomerWithdrawController
    {
        private WithdrawForm _form;
        private Customer model;

        public CustomerWithdrawController(WithdrawForm form, Customer model)
        {
            _form = form;
            this.model = model;
            PopulateAccounts();
        }

        public void PopulateAccounts()
        {
            ListView list = this._form.GetListView();
            this.model.GetAccounts();
            list.Items.Clear();
            ListViewItem item;
            foreach (var acct in this.model.Accounts())
            {
                string[] acctInfo = { acct.AccountId, acct.Type, acct.Balance.ToString("C") };
                item = new ListViewItem(acctInfo);
                list.Items.Add(item);
            }
        }

        public bool MakeWithdraw()
        {
            // initialize a new withdraw object
            Withdraw withdraw = new Withdraw();
            withdraw.Customer.Username = this.model.Username;
            TextBox[] txtboxes = this._form.GetTextBoxes();
            bool result = true;
            // validate account number
            if (!String.IsNullOrEmpty(txtboxes[0].Text))
            {
                List<Account> accts = this.model.Accounts();
                for (int i = 0; i < accts.Count; i++)
                {
                    if (txtboxes[0].Text.Trim().Equals(accts[i].AccountId))
                    {
                        // match was found
                        result = true;
                        withdraw.Account.AccountId = accts[i].AccountId;
                        break;
                    }
                    else
                    {
                        // there was not a match
                        result = false;
                    }
                }
            }
            // if there was no match
            if (!result)
            {
                MessageBox.Show("Please enter one of your account numbers");
                return result;
            }

            // validate the description
            if (!String.IsNullOrEmpty(txtboxes[2].Text))
            {
                withdraw.Desc = txtboxes[2].Text;
            }

            // validate withdrawal
            decimal withdrawAmount = 0;
            if (!String.IsNullOrEmpty(txtboxes[1].Text))
            {
                if (decimal.TryParse(txtboxes[1].Text, out decimal amount))
                {
                    withdrawAmount = decimal.Parse(txtboxes[1].Text);
                }
                else
                {
                    MessageBox.Show("Please enter a valid decimal amount");
                    result = false;
                }

                if (withdrawAmount <= 0)
                {
                    MessageBox.Show("Please enter a value greater than zero");
                    result = false;
                }
            }

            // make deposit
            if (result)
            {
                withdraw.Amount = withdrawAmount;
                Account acct = new Account();
                if (acct.Read(txtboxes[0].Text))
                {
                    acct.Balance -= withdrawAmount;
                    if (acct.Update())
                    {
                        if (withdraw.Create())
                        {
                            MessageBox.Show("Success!!! You made a withdrawal");
                        }
                    }
                    else
                    {
                        MessageBox.Show("There was a problem making your withdrawal");
                        result = false;
                    }
                }
                else
                {
                    MessageBox.Show("There was a problem with the account number you entered. Please try again");
                    result = false;
                }
            }
            ResetTextBoxes();
            PopulateAccounts();
            return result;
        }

        public void ResetTextBoxes()
        {
            foreach(var box in this._form.GetTextBoxes())
            {
                box.Clear();
            }
        }

        public void GoBack()
        {
            CustomerHome form = new CustomerHome(model);
            form.Show();
            this._form.Close();
        }

        public void Refresh()
        {
            PopulateAccounts();
        }
    }
}
