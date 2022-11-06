using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Models
{
    internal class Account
    {
        public int AccountId { get; set; }
        public int CustId { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }

        public Account()
        {

        }
    }
}
