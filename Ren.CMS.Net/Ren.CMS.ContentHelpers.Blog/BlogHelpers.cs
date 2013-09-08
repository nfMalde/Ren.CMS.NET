namespace Ren.CMS.ContentHelpers.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.Content;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;

    public static class BlogHelpers
    {
        #region Methods

        public static int GetCommentsCount(this nContent content)
        {
            int TOTALROWS = 0;
            SqlHelper SQL = new SqlHelper();
            ThisApplication App = new ThisApplication();

            string prefix = App.getSqlPrefix;

            string sqlquery = "SELECT COUNT(*) as xcount FROM " + prefix + "Content WHERE (ContentType = 'eComment' AND ContentRef=@id) OR (ContentType = 'eComment' AND ContentRef IN (SELECT id FROM " + prefix + "Content WHERE ContentType='eComment' AND ContentRef=@id))";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@id", content.ID);

            SQL.SysConnect();

            SqlDataReader R = SQL.SysReader(sqlquery, PCOL);
            if (R.HasRows)
            {
                R.Read();
                TOTALROWS = (int)R["xcount"];

            }
            R.Close();

            SQL.SysDisconnect();

            return TOTALROWS;
        }

        #endregion Methods
    }
}