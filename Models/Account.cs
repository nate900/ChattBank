using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace ChattBank.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string CustId { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }

        // private database utility class
        private class Db : Database { public Db() { } }

        private Db db = new Db();

        public Account() { }

        // InsertDB method
        public bool Create()
        {
            bool result = false;
            db.cmd = "INSERT INTO Accounts VALUES('" + AccountId + "', '" + CustId + "', '" + Type + "', " + Balance + ");";
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
            db.cmd = "SELECT * FROM Accounts WHERE AcctNo = '" + id + "';";
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
                AccountId = dr.GetString(0);
                CustId = dr.GetString(1);
                Type = dr.GetString(2);
                Balance = dr.GetDecimal(3);
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

        public string ReadMAXAcctNo()
        {
            string MAXAcctNo = "";
            db.cmd = "SELECT MAX(AcctNo) FROM Accounts;";
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
                MAXAcctNo = dr.GetString(0);
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
            return MAXAcctNo;
        }

        // UpdateDB method
        public bool Update()
        {
            bool result = false;
            db.cmd = "UPDATE Accounts SET Cid = '" + CustId + "', Type = '" + Type + "', balance = " + Balance + " WHERE AcctNo = '" + AccountId + "';";
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
            db.cmd = "DELETE FROM Accounts WHERE AcctNo = '" + AccountId + "';";
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
