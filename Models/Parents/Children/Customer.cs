using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Models.Parents.Children
{
    internal class Customer : Person
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer()
        {

        }
    }
}
