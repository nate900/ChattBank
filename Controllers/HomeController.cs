using ChattBank.Views.Home;
using ChattBank.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChattBank.Models.Parents.Children;
using ChattBank.Views.CustomerViews;

namespace ChattBank.Controllers
{
    // The home controller
    public class HomeController
    {
        // private fields
        private Login _form;
        private Customer custModel;

        // contructor
        public HomeController(Login form)
        {
            _form = form;
            custModel = new Customer();
        }

        // public login function to login the admin or customer
        public bool Login()
        {
            string username, password = "";
            username = _form.GetUsernameTxt().Text;
            password = _form.GetPasswordTxt().Text;

            if(username.Equals("admin") || username.Equals("Admin"))
            {
                if (password.Equals("123"))
                {
                    Admin admin = new Admin();
                    admin.Username = "admin";
                    admin.Password = "123";
                    admin.Insert();
                    AdminHome adminHome = new AdminHome(admin);
                    adminHome.Show();
                    _form.Close();
                    return true;
                }
            }

            // if the admin cannot login, then try to login a customer
            if (custModel.Read(username))
            {
                if (custModel.Username.Equals(username) && custModel.Password.Equals(password))
                {
                    CustomerHome customerHome = new CustomerHome(custModel);
                    customerHome.Show();
                    _form.Close();
                    return true;
                }
            }
            return false;
        }

        // reset function to reset the login credentials to an empty string
        public void Reset()
        {
            _form.GetPasswordTxt().Clear();
            _form.GetUsernameTxt().Clear();
        }
    }
}
