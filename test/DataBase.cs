using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace test
{
    public class DataBase
    {
        SqlConnection Conn;
        SqlConnection LoginConn;
        SqlDataAdapter MyAdapt;
        DataSet Ds;
        public DataBase()
        {
            Conn = new SqlConnection(Properties.Settings.Default.Database_RGRConnectionString);
            LoginConn = new SqlConnection(Properties.Settings.Default.UserDataBaseConnectionString);
        }
        public DataSet GetFullTables()
        {

            Conn.Open();
            SqlCommand[] SqlCommands = new SqlCommand[2];
            SqlCommands[0] = new SqlCommand("Select * From V_main", Conn);
            MyAdapt = new SqlDataAdapter(SqlCommands[0]);
            System.Data.DataSet dat_set = new System.Data.DataSet();
            MyAdapt.Fill(dat_set, "V_main");
            Conn.Close();
            return dat_set;
        }
        public DataTable Ex_Select_Comm(string cmd)
        {
            Conn.Open();
            DataTable dt = new DataTable();
            SqlCommand SqlCommands = new SqlCommand(cmd, Conn);
            dt.Load(SqlCommands.ExecuteReader());
            Conn.Close();
            return dt;
        }
        public DataTable Ex_Select_LoginComm(string cmd)
        {
            LoginConn.Open();
            DataTable dt = new DataTable();
            SqlCommand SqlCommands = new SqlCommand(cmd, LoginConn);
            dt.Load(SqlCommands.ExecuteReader());
            LoginConn.Close();
            return dt;
        }
        public void InsertComm(string Comm)
        {
            SqlCommand command = new SqlCommand(Comm, Conn);
            Conn.Open();
            command.ExecuteNonQuery();
            Conn.Close();
        }
        public void LoginComm(string Comm)
        {
            SqlCommand command = new SqlCommand(Comm, LoginConn);
            LoginConn.Open();
            command.ExecuteNonQuery();
            LoginConn.Close();
        }

        public int Access(string login, string password)
        {
            LoginConn.Open();
            SqlCommand Comm = new SqlCommand("SELECT * FROM Admin WHERE Login = '" + login + "' AND Password = '" + password + "';", LoginConn);
            MyAdapt = new SqlDataAdapter(Comm);
            DataSet dat_set = new DataSet();
            MyAdapt.Fill(dat_set, "Admin");
            if (dat_set.Tables[0].Rows.Count == 0)
            {
                Comm = new SqlCommand("SELECT * FROM Users WHERE Login = '" + login + "' AND Password = '" + password + "';", LoginConn);
                MyAdapt = new SqlDataAdapter(Comm);
                MyAdapt.Fill(dat_set, "Users");
                if (dat_set.Tables[1].Rows.Count == 0)
                {
                    LoginConn.Close();
                    return 2;
                }
                else
                {
                    LoginConn.Close();
                    return 1;
                }
            }

            else
            {
                LoginConn.Close();
                return 0;
            }
        }
    }
}

