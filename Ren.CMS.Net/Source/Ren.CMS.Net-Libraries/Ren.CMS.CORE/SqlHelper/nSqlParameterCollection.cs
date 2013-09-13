namespace Ren.CMS.CORE.SqlHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class nSqlParameterCollection : IEnumerable
    {
        #region Fields

        private List<SqlParameter> pCol = new List<SqlParameter>();

        #endregion Fields

        #region Constructors

        public nSqlParameterCollection()
        {
        }

        #endregion Constructors

        #region Properties

        public int Count
        {
            get { return this.pCol.Count; }
        }

        #endregion Properties

        #region Methods

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

        public IEnumerator<SqlParameter> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Methods
    }
}