using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            int i = 0;
            model.Username = textboxes[i++].Text;
            model.Fname = textboxes[i++].Text;
            model.Lname = textboxes[i++].Text;
            model.Email = textboxes[i++].Text;
            model.Password = textboxes[i++].Text;
            model.Address = textboxes[i++].Text;

            model.Update();
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
