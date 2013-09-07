using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Ren.CMS.CORE.nhibernate
{
    public static class NHSQL
    {
        public static string NHibernateSQL { get; set; }
    }

    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        NHibernate.SqlCommand.SqlString
            IInterceptor.OnPrepareStatement
                (NHibernate.SqlCommand.SqlString sql)
        {
           // throw new Exception(sql.ToString());

            NHSQL.NHibernateSQL = sql.ToString();
            return sql;
        }
    }
}
