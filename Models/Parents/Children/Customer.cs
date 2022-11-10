using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChattBank.Models.Parents.Children
{
    public class Customer : Person
    {
        // public fields for a customer object
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        private List<Account> _accounts;

        // private database utility class
        private class Db : Database { public Db() { } }

        private Db db = new Db();

        public Customer() 
        {
            _accounts = new List<Account>();
        }

        // setter and getter for _accounts object
        public List<Account> Accounts() { return _accounts; }
        public void SetAccounts(List<Account> accounts) { this._accounts = accounts; }

        // InsertDB method
        public bool Create()
        {
            bool result = false;
            db.cmd = "INSERT INTO Customers VALUES('" + (Int32.Parse(this.ReadMAXCustId()) + 1) + "', '" + Password + "', '" + Fname + "', '" + Lname + "' , '" + Address + "', '" + Email + "');";
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

        // Select method
        // selects a customer given an id
        // the method returns true if there was a record found
        public bool Read(string id)
        {
            bool result = false;
            db.cmd = "SELECT * FROM Customers WHERE CustID = '" + id + "';";
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
                Username = dr.GetString(0);
                Password = dr.GetString(1);
                Fname = dr.GetString(2);
                Lname = dr.GetString(3);
                Address = dr.GetString(4);
                Email = dr.GetString(5);
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

        public List<Customer> ReadAll()
        {
            List<Customer> customers = new List<Customer>();
            db.cmd = "SELECT * FROM Customers;";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // read data
                Customer cust;
                while (dr.Read())
                {
                    cust = new Customer();
                    cust.Username = dr.GetString(0);
                    cust.Password = dr.GetString(1);
                    cust.Fname = dr.GetString(2);
                    cust.Lname = dr.GetString(3);
                    cust.Address = dr.GetString(4);
                    cust.Email = dr.GetString(5);
                    customers.Add(cust);
                }
                
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
            return customers;
        }

        public string ReadMAXCustId()
        {
            string MAXCustId = "";
            db.cmd = "SELECT MAX(CustId) FROM Customers;";
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
                MAXCustId = dr.GetString(0);
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
            return MAXCustId;
        }

        // GetAccounts() returns true if there were records found. The method searches for all customer Accounts
        public bool GetAccounts()
        {
            bool result = false;
            this._accounts = new List<Account>();
            db.cmd = "SELECT * FROM Accounts WHERE Cid = '" + Username + "';";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // Acount object to hold an account's information
                Account acct;
                // read data
                while (dr.Read())
                {
                    acct = new Account();
                    acct.AccountId = dr.GetString(0);
                    acct.CustId = dr.GetString(1);
                    acct.Type = dr.GetString(2);
                    acct.Balance = dr.GetDecimal(3);
                    this._accounts.Add(acct);
                    result = true;
                }

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

        // UpdateDB method
        public bool Update()
        {
            bool result = false;
            db.cmd = "UPDATE Customers SET CustPassword = '" + Password + "', CustFirstName = '" + Fname + "', CustLastName = '" + Lname + "', CustAddress = '" + Address + "', CustEmail = '" + Email + "' WHERE CustID = '" + Username + "';";
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

        // Delete method
        public bool Delete()
        {
            bool result = false;
            db.cmd = "DELETE FROM Customers WHERE CustID = '" + Username + "';";
            db.OleDbDataAdapter.DeleteCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.DeleteCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                // delete from database
                db.OleDbConnection.Open();
                int n = db.OleDbDataAdapter.DeleteCommand.ExecuteNonQuery();
                if(n == 1) result = true;
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
