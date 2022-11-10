using ChattBank.Models.Parents.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Models
{
    public class Withdraw
    {
        public int ID { get; set; }
        public Account Account { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public string Desc { get; set; }
        public DateTime Time { get; set; }

        // private database utility class
        private class Db : Database { public Db() { } }

        private Db db = new Db();

        public Withdraw()
        {

        }

        public bool Create()
        {
            return false;
        }

        public bool Read()
        {
            return false;
        }

        public bool Update()
        {
            return false;
        }

        public bool Delete()
        {
            return false;
        }
    }
}
