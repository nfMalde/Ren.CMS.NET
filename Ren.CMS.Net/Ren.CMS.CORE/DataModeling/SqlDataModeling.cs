namespace Ren.CMS.CORE.DataModeling
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.ThisApplication;

    public static class SqlDataModeling
    {
        #region Methods

        public static IEnumerable<Tentity> Get<Tentity>(Expression<Func<Tentity, bool>> WhereClause = null)
            where Tentity : new()
        {
            throw new Exception(CompileAndGetWhere(WhereClause));
            PropertyInfo[] PropInfo = typeof(Tentity).GetProperties();
            List<string> Columns = ColumnList<Tentity>();
            var func = WhereClause.Body as Func<Tentity, bool>;
             BinaryExpression Expr = WhereClause.Body as BinaryExpression;
            BinaryExpression temp = Expr;

            bool lefFinished = false;
            while (!lefFinished)
            {
                var newExpr = temp.Left as BinaryExpression;
                var Proper = temp.Left as MemberExpression;
                if (Proper != null)
                {
                    lefFinished = true;
                }
            }
            var ex = Expr.Left as BinaryExpression;

            MemberExpression Left = (Expr.Left) as MemberExpression;
            MemberExpression Right = (Expr.Right) as MemberExpression;

            throw new Exception(Right.Member.Name.ToString());

            List<Tentity> RowSets = new List<Tentity>();
            string query = "SELECT";
            foreach (string Col in Columns)
            {

                query += Col + ",";
            }

            if (query.EndsWith(","))
            {
                query = query.Remove(query.LastIndexOf(','));
            }

            //Get Table Name
            string tableName = typeof(Tentity).Name;
            tableName = new ThisApplication().getSqlPrefix + tableName;

            query += " FROM " + tableName;

            SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();

            SQL.SysConnect();

            SqlDataReader Rows = SQL.SysReader(query, new SqlHelper.nSqlParameterCollection());

            if (Rows.HasRows)
            {
                while (Rows.Read())
                {

                    Tentity entity = new Tentity();
                    foreach (string col in Columns)
                        entity.GetType().GetProperty(col).SetValue(entity, (object)Rows[col]);

                    RowSets.Add(entity);

                }

            }

            Rows.Close();
            SQL.SysDisconnect();

            if (WhereClause != null)
            {

                return RowSets.Where(func);

            }

            return RowSets;
        }

        public static bool Update<Tentity>(string getBy, Tentity newEntity)
        {
            List<string> Columns = ColumnList<Tentity>();
            SqlHelper.nSqlParameterCollection SQLCOL = new SqlHelper.nSqlParameterCollection();
            Columns.ForEach(e => SQLCOL.Add("@"+ e, newEntity.GetType().GetProperty(e).GetValue(newEntity)));

            PropertyInfo GetBy = newEntity.GetType().GetProperty(getBy);
            object val = GetBy.GetValue(newEntity);
            string tableName = GetTableName<Tentity>();

            SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            string query = "UPDATE " + tableName +" SET ";

            foreach (string col in Columns)
            {
                KeyAttribute Keyattr = typeof(Tentity).GetProperty(col).GetCustomAttribute<KeyAttribute>();
                if (Keyattr == null)
                {

                    query += " " + col + "=@" + col+",";
                    PCOL.Add("@" + col, newEntity.GetType().GetProperty(col).GetValue(newEntity));
                }

            }

            if (query.EndsWith(","))
            {
                query = query.Remove(query.LastIndexOf(","));

            }
            query += " WHERE " + GetBy.Name + "=@" + GetBy.Name;

            PCOL.Add("@" + GetBy.Name, val);

            SQL.SysNonQuery(query, PCOL);

            SQL.SysDisconnect();

            return true;
        }

        private static List<string> ColumnList<Tentity>()
        {
            PropertyInfo[] PropInfo = typeof(Tentity).GetProperties();
            List<string> Columns = new List<string>();
            foreach (PropertyInfo Prop in PropInfo)
            {

                Columns.Add(Prop.Name);
            }

            return Columns;
        }

        private static string CompileAndGetWhere<TType>(
            Expression<Func<TType, bool>> expr)
        {
            // hacks all the way
                    dynamic operation = expr.Body;
                    dynamic left = operation.Left;
                    dynamic right = operation.Right;

                    var ops = new Dictionary<ExpressionType, String>();
                    ops.Add(ExpressionType.Equal, "=");
                    ops.Add(ExpressionType.GreaterThan, ">");
                    // add all required operations here
                    var c = expr.Compile();
                    var c2 = c.GetInvocationList();
                    // Instead of SELECT *, select all required fields, since you know the type
                    var q = "x";

                    return q;
        }

        private static string getLeftParameterFromExpression(Expression<object> expression)
        {
            BinaryExpression Expr = expression.Body as BinaryExpression;
            MemberExpression Mem = (Expr.Left) as MemberExpression;

            return "";
        }

        private static string GetTableName<Tentity>(bool withPrefix = true)
        {
            string entityName = typeof(Tentity).Name;

            if (entityName.EndsWith("Entity"))
            {

                string tbl = entityName.Substring(0, entityName.Length - ("Entity").Length);
                if (withPrefix)
                {
                    ThisApplication TA = new ThisApplication();
                    tbl = TA.getSqlPrefix + tbl;

                }

                return tbl;

            }

            throw new Exception("Data Modeling Error: Wrong Entity Name convention for " + entityName + ". Entity class name has to start with the Table name and ends with Entity. Example for the Table \"Products\" the class name has to be: ProductsEntity");
        }

        #endregion Methods
    }
}