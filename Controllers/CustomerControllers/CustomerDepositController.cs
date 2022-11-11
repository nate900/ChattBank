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
    public class CustomerDepositController
    {
        private DepositForm _form;
        private Customer model;

        public CustomerDepositController(DepositForm form, Customer model)
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
            foreach(var acct in this.model.Accounts())
            {
                string[] acctInfo = { acct.AccountId, acct.Type, acct.Balance.ToString("C") };
                item = new ListViewItem(acctInfo);
                list.Items.Add(item);
            }
        }

        public bool MakeDeposit()
        {
            // make a deposit object
            Deposit depo = new Deposit();
            depo.Customer = model;
            TextBox[] txtboxes = this._form.GetTextBoxes();
            bool result = true;
            // validate account number
            if (!String.IsNullOrEmpty(txtboxes[0].Text))
            {
                List<Account> accts = this.model.Accounts();
                for(int i = 0; i < accts.Count; i++)
                {
                    if (txtboxes[0].Text.Trim().Equals(accts[i].AccountId))
                    {
                        // match was found
                        result = true;
                        depo.Account = accts[i];
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
                depo.Desc = txtboxes[2].Text;
            }

            // validate deposit
            decimal depoAmount = 0;
            if (!String.IsNullOrEmpty(txtboxes[1].Text))
            {
                if(decimal.TryParse(txtboxes[1].Text, out decimal amount))
                {
                    depoAmount = decimal.Parse(txtboxes[1].Text);
                    depo.Amount = depoAmount;
                }
                else
                {
                    MessageBox.Show("Please enter a valid decimal amount");
                    result = false;
                }

                if(depoAmount <= 0)
                {
                    MessageBox.Show("Please enter a value greater than zero");
                    result = false;
                }
            }

            // make deposit
            if (result)
            {
                Account acct = new Account();
                if (acct.Read(txtboxes[0].Text))
                {
                    acct.Balance += depoAmount;
                    if (acct.Update())
                    {
                        if (depo.Create())
                        {
                            MessageBox.Show("Success!!! You made a deposit");
                        }
                    }
                    else
                    {
                        MessageBox.Show("There was a problem making your deposit");
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

        public void Refresh()
        {
            PopulateAccounts();
        }

        public void GoBack()
        {
            CustomerHome form = new CustomerHome(model);
            form.Show();
            this._form.Close();
        }

        public void ResetTextBoxes()
        {
            foreach (var txtbox in this._form.GetTextBoxes())
            {
                txtbox.Clear();
            }
        }
    }
}
