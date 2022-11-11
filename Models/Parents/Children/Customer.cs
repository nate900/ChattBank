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
        private List<Deposit> _deposits;
        private List<Withdraw> _withdraws;

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

        // setter and getter for _deposits object
        public List<Deposit> Deposits() { return _deposits; }
        public void SetDeposits(List<Deposit> deposits) { this._deposits = deposits; }

        // setter and getter for _withdraws object
        public List<Withdraw> Withdraws() { return _withdraws; }
        public void SetWithdraws(List<Withdraw> withdraws) { this._withdraws = withdraws; }

        // InsertDB method
        public bool Create()
        {
            this.Username = (Int32.Parse(this.ReadMAXCustId()) + 1).ToString();
            bool result = false;
            db.cmd = "INSERT INTO Customers VALUES('" + Username + "', '" + Password + "', '" + Fname + "', '" + Lname + "' , '" + Address + "', '" + Email + "');";
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

        // GetAccounts() returns true if there were records found. The method searches for all customer deposits
        public bool GetDeposits()
        {
            bool result = false;
            this._deposits = new List<Deposit>();
            db.cmd = "SELECT * FROM Deposits WHERE CustomerID = '" + Username + "';";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // Acount object to hold an account's information
                Deposit depo;
                // read data
                while (dr.Read())
                {
                    depo = new Deposit();
                    depo.ID = dr.GetString(0);
                    depo.Account.AccountId = dr.GetString(1);
                    depo.Customer.Username = dr.GetString(2);
                    depo.Amount = dr.GetDecimal(3);
                    depo.Desc = dr.GetString(4);
                    depo.Time = dr.GetDateTime(5);
                    this._deposits.Add(depo);
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

        // GetAccounts() returns true if there were records found. The method searches for all customer withdrawals
        public bool GetWithdraws()
        {
            bool result = false;
            this._withdraws = new List<Withdraw>();
            db.cmd = "SELECT * FROM Withdrawals WHERE CustomerID = '" + Username + "';";
            db.OleDbDataAdapter.SelectCommand.CommandText = db.cmd;
            db.OleDbDataAdapter.SelectCommand.Connection = db.OleDbConnection;
            Console.WriteLine(db.cmd);
            System.Diagnostics.Debug.WriteLine(db.cmd);
            try
            {
                db.OleDbConnection.Open();
                System.Data.OleDb.OleDbDataReader dr = db.OleDbDataAdapter.SelectCommand.ExecuteReader();

                // Acount object to hold an account's information
                Withdraw withdraw;
                // read data
                while (dr.Read())
                {
                    withdraw = new Withdraw();
                    withdraw.ID = dr.GetString(0);
                    withdraw.Account.AccountId = dr.GetString(1);
                    withdraw.Customer.Username = dr.GetString(2);
                    withdraw.Amount = dr.GetDecimal(3);
                    withdraw.Desc = dr.GetString(4);
                    withdraw.Time = dr.GetDateTime(5);
                    this._withdraws.Add(withdraw);
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
