using ChattBank.Models.Parents.Children;
using ChattBank.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace ChattBank.Controllers.AdminControllers
{
    // Controller for the admin user
    public class AdminController
    {
        // data fields
        private Admin model;
        private AdminHome _form;

        // constructor
        public AdminController(AdminHome form, Admin model)
        {
            this._form = form;
            this.model = model;
            PopulateData();
        }

        public void PopulateData()
        {
            ListView list = this._form.GetListView();

            // customer object to hold a customer's data
            Customer customer = new Customer();
            // get all customers in the database
            List<Customer> customers = customer.ReadAll();
            ListViewItem newItem = null;
            // populate each customer's data to the admin control panel
            foreach(var cust in customers)
            {
                string[] custInfo = { cust.Username, cust.Fname, cust.Lname, cust.Email, cust.Address };
                newItem = new ListViewItem(custInfo);
                this._form.GetListView().Items.Add(newItem);
            }
        }

        // method that takes the admin user to a form where he or she can create a new customer
        public void GoToCreateCustomer()
        {
            AdminCreateCustomer form = new AdminCreateCustomer(this.model);
            form.Show();
            this._form.Close();
        }

        // method that takes the admin user to a form where he or she can delete a specific customer
        public void GoToDeleteCustomer()
        {
            AdminDeleteCustomer form = new AdminDeleteCustomer(this.model);
            form.Show();
            this._form.Close();
        }

    }
}
