using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChattBank.Models;
using ChattBank.Models.Parents.Children;
using ChattBank.Views.CustomerViews;
using static System.Collections.Specialized.BitVector32;

namespace ChattBank.Controllers.CustomerControllers
{
    // Controller for the customer user
    public class CustomerHomeController
    {
        // private model field
        private Customer model;
        private CustomerHome _form;

        // constructor
        public CustomerHomeController(CustomerHome form, Customer customer)
        {
            model = customer;
            _form = form;
            PopulateName();
            PopulateHomeData();
        }

        // method to populate the customer's name to the current view form
        public void PopulateName()
        {
            if (_form != null)
                _form.GetWelcomeLabel().Text += " " + model.Fname;
        }

        // method to populate customer accounts in a list form to the home view
        public void PopulateHomeData()
        {
            if (!model.GetAccounts())
            {
                MessageBox.Show("Looks like you need to create an account");
            }
            else
            {
                // need to get the list of customer accounts from DB
                ListView listView = _form.GetAccountsList();
                listView.Items.Clear();
                ListViewItem item;
                Account account;
                for (int i = 0; i < model.Accounts().Count; i++)
                {
                    account = model.Accounts()[i];
                    string[] accountsInfo = { account.AccountId, account.Type, account.Balance.ToString("C") };
                    item = new ListViewItem(accountsInfo);
                    listView.Items.Add(item);
                }
            }

            // display all deposits to the customer
            if (model.GetDeposits())
            {
                ListView listView = _form.GetAccountActivity();
                listView.Items.Clear();
                ListViewItem item;
                Deposit depo;
                for(int i = 0; i < model.Deposits().Count; i++)
                {
                    depo = model.Deposits()[i];
                    string[] depoInfo = { "Deposit", depo.Account.AccountId, depo.Amount.ToString("C"), depo.Desc, depo.Time.ToString("yyyy/MM/dd") };
                    item = new ListViewItem(depoInfo);
                    listView.Items.Add(item);
                }
            }

            // display all withdrawals to the customer
            if (model.GetWithdraws())
            {
                ListView listView = _form.GetAccountActivity();
                ListViewItem item;
                Withdraw withdraw;
                for (int i = 0; i < model.Withdraws().Count; i++)
                {
                    withdraw = model.Withdraws()[i];
                    string[] withdrawInfo = { "Withdraw", withdraw.Account.AccountId, withdraw.Amount.ToString("C"), withdraw.Desc, withdraw.Time.ToString("yyyy/MM/dd") };
                    item = new ListViewItem(withdrawInfo);
                    listView.Items.Add(item);
                }
            }
        }

        public void GoNewAccount()
        {
            NewAccount form = new NewAccount(model);
            form.Show();
            this._form.Close();
        }

        public void GoDeposit()
        {
            DepositForm form = new DepositForm(model);
            form.Show();
            this._form.Close();
        }

        public void GoWithdraw()
        {
            WithdrawForm form = new WithdrawForm(model);
            form.Show();
            this._form.Close();
        }

        public void GoMyInfo()
        {
            MyInfo form = new MyInfo(model);
            form.Show();
            _form.Close();
        }
    }
}
