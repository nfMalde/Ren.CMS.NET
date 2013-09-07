using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.SqlHelper
{
    public class nSqlParameterCollection : IEnumerable
    {


        private List<SqlParameter> pCol = new List<SqlParameter>();
        public nSqlParameterCollection()
        {





        }
        public void Add(SqlParameter Parameteritem)
        {
            this.pCol.Add(Parameteritem);


        }
        public void Add(string name, object val)
        {
            if (val == null) val = "";
            SqlParameter Parameteritem = new SqlParameter(name, val.ToString());
            this.pCol.Add(Parameteritem);


        }
        public int Count { get { return this.pCol.Count; } }
        public IEnumerator<SqlParameter> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }

}
