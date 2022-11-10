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
                ListView listView = _form.GetListView();
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

                /*ListView accountsList = _form.GetListView();
                ListViewItem item;
                foreach (var account in model.Accounts())
                {
                    item = new ListViewItem();
                    
                    accountsList.Items.Add(item);
                }*/
            }
            /*for (int i = 0; i < sSchedule.Count; i++)
            {
                Section sSection = sSchedule.GetSection(i);
                ListViewItem item = new ListViewItem(sSection.CRN.ToString());
                item.SubItems.Add(sSection.CourseID.ToString());
                item.SubItems.Add(sSection.TimeDay.ToString());
                item.SubItems.Add(sSection.RoomNo.ToString());
                item.SubItems.Add(sSection.InstructorID.ToString());
                studentSchedule.Items.Add(item);
            }*/
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
