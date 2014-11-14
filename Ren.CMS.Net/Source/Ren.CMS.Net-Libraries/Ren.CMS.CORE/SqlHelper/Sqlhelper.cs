namespace Ren.CMS.CORE.SqlHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    public class SqlHelper
    {
        #region Fields

        private int lastAffectedRows = 0;
        private SqlConnection SysConnection = new SqlConnection();

        #endregion Fields

        #region Constructors

        public SqlHelper()
        {
            SqlConnection temp = new SqlConnection();
            temp.ConnectionString = this.GetConnectionString("ren_cms");
            ///* connection string building for sql and oledb from 1st line to 5th line*/
            SqlConnectionStringBuilder Obj_sqnbuild =
               new SqlConnectionStringBuilder();//making the instance for
            //the sqlConnection String builder
            //Obj_sqnbuild.InitialCatalog = temp.;
            //Obj_sqnbuild.DataSource = "DBSERVER";
            //Obj_sqnbuild.UserID = "sa";
            //Obj_sqnbuild.Password = "db_Ser3er_2009";
            Obj_sqnbuild.ConnectionString = temp.ConnectionString;
            Obj_sqnbuild.Add("Max pool size", 1500);
            Obj_sqnbuild.Add("Min pool size", 20);
            Obj_sqnbuild.Add("Pooling", true);

            this.SysConnection.ConnectionString = Obj_sqnbuild.ConnectionString;
        }

        #endregion Constructors

        #region Properties

        public int AffectedRows
        {
            get { return this.lastAffectedRows; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Fills an System.Data.DataSet by using a internal SqlDataAdapter (System.Data.SqlClient)
        /// </summary>
        /// <param name="queryString">Select Querystring for the Adapter</param>
        /// <returns>System.Data.DataSet</returns>
        public static DataSet SysUseAdapter(string queryString)
        {
            DataSet Set = new DataSet();

            string constr = ConfigurationManager.ConnectionStrings["ren_cms"].ConnectionString;
            using (SqlConnection ncon = new SqlConnection(constr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, ncon);
                adapter.Fill(Set);
            }
            return Set;
        }

        /// <summary>
        /// Returns the Last ID of a Table
        /// </summary>
        /// <param name="table">Tablename with PREFIX</param>
        /// <returns>-1 if no id was found or the id as integer</returns>
        public int getLastId(string table)
        {
            string query = "SELECT IDENT_CURRENT(@table) as id";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@table", table);
            SqlDataReader R = this.SysReader(query, PCOL);
            int id = 0;
            if (R.HasRows)
            {

                R.Read();
                string res = R["id"].ToString();
                int.TryParse(res, out id);
            }
            R.Close();
            return id;
        }

        public dynamic getModel(object TableModel)
        {
            //Checkup for required things doh!

            if (TableModel.GetType().GetProperty("TableName") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.string) TableName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (TableModel.GetType().GetProperty("IdentityName") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.string) IdentityName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (TableModel.GetType().GetProperty("IdentityValue") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.object) IdentityValue' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);

            string table = TableModel.GetType().GetProperty("TableName").GetValue(TableModel, null).ToString();

            string identityCol = TableModel.GetType().GetProperty("IdentityName").GetValue(TableModel, null).ToString();
            object identityValue = TableModel.GetType().GetProperty("IdentityValue").GetValue(TableModel, null);

            if (String.IsNullOrEmpty(table)) throw new Exception("SqlHelper :: getModel(object TableModel) - Property cannot be empty '(System.string) TableName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (String.IsNullOrEmpty(identityCol)) throw new Exception("SqlHelper :: getModel(object TableModel) - Property cannot be empty '(System.string) IdentityName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (String.IsNullOrEmpty(identityValue.ToString())) throw new Exception("SqlHelper :: getModel(object TableModel) -Property cannot be empty '(System.object) IdentityValue' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);

            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string pref = TA.getSqlPrefix;
            identityCol = identityCol.Replace("'", "").Replace("-", "").Replace("#", "");

            System.Reflection.PropertyInfo[] Propcols = TableModel.GetType().GetProperties();
            string selectColumns = "";

            List<string> TblCols = new List<string>();
            foreach (System.Reflection.PropertyInfo Prop in Propcols)
            {
                if (Prop.Name != "TableName" && Prop.Name != "IdentityName" && Prop.Name != "IdentityValue")
                {
                    if (selectColumns == "") selectColumns = Prop.Name;
                    else selectColumns += ", " + Prop.Name;
                    TblCols.Add(Prop.Name);
                }

            }
            string query = "SELECT TOP 1 " + selectColumns + "  FROM " + pref + table + " WHERE " + identityCol + " = @id";

            nSqlParameterCollection Pcol = new nSqlParameterCollection();

            Pcol.Add("@id", identityValue);
            dynamic model = new { };
            SqlDataReader R = this.SysReader(query, Pcol);
            object temp = "";
            if (R.HasRows)
            {
                while (R.Read())
                {

                    foreach (string col in TblCols)
                    {

                        if (R[col] != null)
                        {

                            TableModel.GetType().GetProperty(col).SetValue(TableModel, Convert.ChangeType(R[col].ToString(), TableModel.GetType().GetProperty(col).PropertyType), null);

                        }

                    }
                }

            }

            model = TableModel;

            //Required Props for Models!!!
            model.TableName = table;
            model.IdentityName = identityCol;
            model.IdentityValue = identityValue;

            return model;
        }

        public SqlCommand SysCommand(string querystring)
        {
            SqlCommand CMD = new SqlCommand(querystring);
            CMD.Connection = this.SysConnection;
            return CMD;
        }

        /// <summary>
        /// Connects to the Database
        /// </summary>
        public void SysConnect()
        {
            try
            {
                if (this.SysConnection.State == ConnectionState.Open)
                {

                    this.SysConnection.Close();
                }
                this.SysConnection.Open();

            }
            catch (SqlException e2) { throw e2; }

            if (this.SysConnection.State != ConnectionState.Open)
            {

                throw new Exception("Unable connect. Server is reachable and but we get no OK");

            }
        }

        public int SysCount(string SQLTABLE, string identifycol = "", string identifyvalue = "")
        {
            string c = "SELECT * FROM " + new ThisApplication.ThisApplication().getSqlPrefix + SQLTABLE;
            SqlParameter[] P = new SqlParameter[1];
            if (!String.IsNullOrEmpty(identifycol) && !String.IsNullOrEmpty(identifyvalue))
            {

                c += " WHERE " + identifycol + " = @p";

                P[0] = new SqlParameter("@p", identifyvalue);
            }

            SqlDataReader R = this.SysReader(c, P);
            int i = 0;
            while (R.Read())
            {

                i++;

            }
            R.Close();

            return i;
        }

        /// <summary>
        /// Disconnects from the Database
        /// </summary>
        public void SysDisconnect()
        {
            if (this.SysConnection.State == System.Data.ConnectionState.Open)
            {

                this.SysConnection.Close();

            }
        }

        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter Array (System.Data.SqlClient.SqlParameter[])</param>
        public void SysNonQuery(string commandstring, nSqlParameterCollection parameter = null)
        {
            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }

            }
            Command.ExecuteNonQuery();

            Command.Dispose();
        }

        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter Array (System.Data.SqlClient.SqlParameter[])</param>
        public void SysNonQuery(string commandstring, SqlParameter[] parameter = null)
        {
            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }

            }

            Command.ExecuteNonQuery();

            Command.Dispose();
        }

        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter List</param>
        public void SysNonQuery(string commandstring, List<SqlParameter> parameter = null)
        {
            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }

            }
            Command.ExecuteNonQuery();

            Command.Dispose();
        }

        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, SqlParameter[] parameter = null)
        {
            SqlCommand C = new SqlCommand(querystring);
            C.Connection = this.SysConnection;
            if (parameter != null) C.Parameters.AddRange(parameter);

            return C.ExecuteReader();
        }

        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, List<SqlParameter> parameter)
        {
            SqlCommand C = new SqlCommand(querystring);
            C.Connection = this.SysConnection;
            if (parameter != null)
            {

                foreach (SqlParameter P in parameter)
                {
                    C.Parameters.Add(P);
                }

            }
            return C.ExecuteReader();
        }

        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, nSqlParameterCollection parameter)
        {
            SqlCommand C = new SqlCommand(querystring);

            C.Connection = this.SysConnection;
            if (parameter != null)
            {

                foreach (SqlParameter P in parameter)
                {
                    C.Parameters.Add(P);
                }

            }

            return C.ExecuteReader();
        }

        private string GetConnectionString(string str)
        {
            //variable to hold our return value
            string conn = string.Empty;
            //check if a value was provided
            if (!string.IsNullOrEmpty(str))
            {

                //name provided so search for that connection
                conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            else
            //name not provided, get the 'default' connection
            {
                throw new Exception("ERROR IN SYS.CORE", new Exception("Required Connection String name is empty or null"));
            }
            //return the value
            return conn;
        }

        private string parseQuery(string query)
        {
            return query.Replace("__PREFIX__", (new ThisApplication.ThisApplication().getSqlPrefix));
        }

        #endregion Methods
    }
}