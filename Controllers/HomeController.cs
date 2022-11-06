using ChattBank.Views.Home;
using ChattBank.Views.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Controllers
{
    internal class HomeController
    {
        private Login _form;
        private object _sender;
        private EventArgs _e;

        public HomeController(Login form, object sender, EventArgs e)
        {
            _form = form;
            _sender = sender;
            _e = e;
        }

        public bool Login()
        {
            string username, password = "";
            username = _form.GetUsernameTxt().Text;
            password = _form.GetPasswordTxt().Text;

            if(username.Equals("admin") || username.Equals("Admin"))
            {
                if (password.Equals("123"))
                {
                    AdminHome adminHome = new AdminHome();
                    adminHome.Show();
                    _form.Close();
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            _form.GetPasswordTxt().Clear();
            _form.GetUsernameTxt().Clear();
        }
    }
}
