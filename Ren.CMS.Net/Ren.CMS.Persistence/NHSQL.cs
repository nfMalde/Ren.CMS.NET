namespace Ren.CMS.CORE.nhibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;

    public static class NHSQL
    {
        #region Properties

        public static string NHibernateSQL
        {
            get; set;
        }

        #endregion Properties
    }

    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        #region Methods

        NHibernate.SqlCommand.SqlString IInterceptor.OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            // throw new Exception(sql.ToString());

            NHSQL.NHibernateSQL = sql.ToString();
            return sql;
        }

        #endregion Methods
    }
}