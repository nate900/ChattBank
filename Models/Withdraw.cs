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
        public string ID { get; set; }
        public Account Account { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public string Desc { get; set; }
        public DateTime Time { get; set; }

        // private database utility class
        private class Db : Database { public Db() { } }

        private Db db = new Db();

        // constructor
        public Withdraw(Customer cust, Account acct)
        {
            this.Customer = cust;
            this.Account = acct;
            Time = DateTime.Now;
        }

        public Withdraw()
        {
            this.Customer = new Customer();
            this.Account = new Account();
            Time = DateTime.Now;
        }

        public bool Create()
        {
            bool result = false;
            db.cmd = "INSERT INTO Withdrawals VALUES('" + (Int32.Parse(this.ReadMAXWithdrawId()) + 1) + "', '" + Account.AccountId + "', '" + Customer.Username + "', " + Amount + ", '" + Desc + "', '" + Time.ToString("yyyy/MM/dd") + "');";
            db.OleDbDataAdapter.InsertCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.InsertCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                // insert into database
                db.OleDbConnection.Open();
                int n = db.OleDbDataAdapter.InsertCommand.ExecuteNonQuery();
                if (n == 1) result = true;
                Console.WriteLine((n == 1) ? "Data Inserted" : "Error: did not insert data properly");
                System.Diagnostics.Debug.WriteLine((n == 1) ? "Data Inserted" : "Error: did not insert data properly");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                db.OleDbConnection.Close();
            }
            return result;
        }

        public bool Read()
        {
            bool result = false;
            db.cmd = "SELECT * FROM Withdrawals WHERE ID = '" + ID + "';";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // read data
                result = dr.Read();
                Account.AccountId = dr.GetString(1);
                Customer.Username = dr.GetString(2);
                Amount = dr.GetDecimal(3);
                Desc = dr.GetString(4);
                Time = dr.GetDateTime(5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                db.OleDbConnection.Close();
            }
            return result;
        }

        public string ReadMAXWithdrawId()
        {
            string MAXId = "";
            db.cmd = "SELECT MAX(ID) FROM Withdrawals;";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // read data
                dr.Read();
                MAXId = dr.GetString(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                db.OleDbConnection.Close();
            }
            return MAXId;
        }

        public bool Update()
        {
            bool result = false;
            db.cmd = "UPDATE Withdrawals SET AccountID = '" + Account.AccountId + "', CustomerID = '" + Customer.Username + "', Amount = " + Amount + ", Desc = '" + Desc + "', Time = '" + Time + "' WHERE ID = '" + ID + "';";
            db.OleDbDataAdapter.UpdateCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.UpdateCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                // update the database
                db.OleDbConnection.Open();
                int n = db.OleDbDataAdapter.UpdateCommand.ExecuteNonQuery();
                if (n == 1) result = true;
                Console.WriteLine((n == 1) ? "Data Updated" : "Error: did not update data properly");
                System.Diagnostics.Debug.WriteLine((n == 1) ? "Data Updated" : "Error: did not update data properly");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                db.OleDbConnection.Close();
            }
            return result;
        }

        public bool Delete()
        {
            bool result = false;
            db.cmd = "DELETE FROM Withdrawals WHERE ID = '" + ID + "';";
            db.OleDbDataAdapter.DeleteCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.DeleteCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                // delete from database
                db.OleDbConnection.Open();
                int n = db.OleDbDataAdapter.DeleteCommand.ExecuteNonQuery();
                if (n == 1) result = true;
                Console.WriteLine((n == 1) ? "Data Deleted" : "Error: did not delete data properly");
                System.Diagnostics.Debug.WriteLine((n == 1) ? "Data Deleted" : "Error: did not delete data properly");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                db.OleDbConnection.Close();
            }
            return result;
        }
    }
}
