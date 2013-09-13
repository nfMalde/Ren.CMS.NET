namespace Ren.CMS.Content
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;

    public class nContentTag
    {
        #region Fields

        private bool pBrowsingEnabled = false;
        private string pContentType = null;
        private int pID = -1;
        private string pName = null;
        private string pSEOName = null;
        private string savingLog = null;

        #endregion Fields

        #region Constructors

        public nContentTag(string name, string contenttype, string SEOName, bool browsingEnabled = false)
        {
            this.initModel(-1, name, contenttype, SEOName, browsingEnabled);
        }

        public nContentTag(int id, string name, string contenttype, string SEOName, bool browsingEnabled = false)
        {
            this.initModel(id, name, contenttype, SEOName, browsingEnabled);
        }

        #endregion Constructors

        #region Properties

        public bool BrowsingEnabled
        {
            get { return this.pBrowsingEnabled; }
            set { this.pBrowsingEnabled = value; }
        }

        public string ContentType
        {
            get
            {

                return this.pContentType;

            }

            set
            {

                this.pContentType = value;

            }
        }

        public int ID
        {
            get
            {

                return this.pID;

            }
            set
            {

                this.pID = value;

            }
        }

        public string Name
        {
            get
            {

                return this.pName;

            }
            set
            {

                this.pName = value;

            }
        }

        public string UrlOptimizedName
        {
            get { return this.pSEOName; }
            set { this.pSEOName = value; }
        }

        #endregion Properties

        #region Methods

        public string getSavingLog()
        {
            return this.savingLog;
        }

        public void Save()
        {
            if (this.checkTagIDExists())
            {

                if (!this.checkTagNameExists())
                {
                    this.UpdateTag(this.pName, this.pContentType, this.pBrowsingEnabled);

                    this.savingLog = "Tag with id #" + this.pID + " updated. New VALS: tagName=" + this.pName + " AND contentType=" + this.pContentType;
                }
                else
                {

                    this.savingLog = "This tag name is allready taken by another tag. Cannot update.";

                }

            }
            else
            {

                if (!this.checkTagNameExists())
                {

                    this.InsertTag(this.pName, this.pContentType, this.pBrowsingEnabled);
                    this.savingLog = "Tag saved and created.VALS: tagName=" + this.pName + " AND contentType=" + this.pContentType;

                }
                else
                {

                    this.savingLog = "This tag name is allready taken by another tag. Cannot insert.";

                }

            }
        }

        private bool checkTagIDExists()
        {
            if (this.pID == -1) return false;
            SqlHelper SQL = new SqlHelper();
            ThisApplication APP = new ThisApplication();
            SQL.SysConnect();

            string pref = APP.getSqlPrefix;

            string query = "SELECT COUNT(*) as tagCount FROM " + pref + "Content_Tags WHERE id=@id";

            nSqlParameterCollection PCOL = new nSqlParameterCollection();

            PCOL.Add("@id", this.pID);

            SqlDataReader C = SQL.SysReader(query, PCOL);

            C.Read();

            int count = 0;
            if (C["tagCount"] != DBNull.Value)
            {

                count = (int)C["tagCount"];

            }
            else { return true; }

            C.Close();

            SQL.SysDisconnect();
            if (count > 0) return true;
            else return false;
        }

        private bool checkTagNameExists()
        {
            SqlHelper SQL = new SqlHelper();
            ThisApplication APP = new ThisApplication();
            SQL.SysConnect();

            string pref = APP.getSqlPrefix;

            string query = "SELECT COUNT(*) as tagCount FROM " + pref + "Content_Tags WHERE contentType@ct AND tagName=@name";

            nSqlParameterCollection PCOL = new nSqlParameterCollection();

            PCOL.Add("@ct", this.pContentType);
            PCOL.Add("@name", this.pName);
            SqlDataReader C = SQL.SysReader(query, PCOL);

            C.Read();

            int count = 0;
            if (C["tagCount"] != DBNull.Value)
            {

                count = (int)C["tagCount"];

            }
            else { return true; }

            C.Close();

            SQL.SysDisconnect();
            if (count > 0) return true;
            else return false;
        }

        private void initModel(int id, string name, string contenttype, string SEOName, bool browsingEnabled = false)
        {
            this.pID = id;
            this.pName = name;
            this.pContentType = contenttype;
            this.pBrowsingEnabled = browsingEnabled;
            this.pSEOName = SEOName;
        }

        private void InsertTag(string name, string contentType, bool browsing_Enabled)
        {
            nSqlParameterCollection Paras = new nSqlParameterCollection();
            ThisApplication APP = new ThisApplication();

            string pref = APP.getSqlPrefix;

            Paras.Add("@name", name);
            Paras.Add("@contentType", contentType);
            Paras.Add("@enableBrowsing", (browsing_Enabled == true ? 1 : 0));

            string nonquery = "INSERT INTO " + pref + "Content_Tags (tagName,contentType,enableBrowsing) VALUES (@name, @contentType,@enableBrowsing)";

            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();

            SQL.SysNonQuery(nonquery, Paras);

            SQL.SysDisconnect();
        }

        private void UpdateTag(string name, string contentType, bool browsing_Enabled)
        {
            nSqlParameterCollection Paras = new nSqlParameterCollection();
            ThisApplication APP = new ThisApplication();

            string pref = APP.getSqlPrefix;

            Paras.Add("@id", this.pID);
            Paras.Add("@name", name);
            Paras.Add("@contentType", contentType);
            Paras.Add("@enableBrowsing", (browsing_Enabled == true ? 1 : 0));

            string nonquery = "UPDATE " + pref + "Content_Tags SET tagName=@name, contentType=@contentType,enableBrowsing = @enableBrowsing WHERE id=@id";

            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();

            SQL.SysNonQuery(nonquery, Paras);

            SQL.SysDisconnect();
        }

        #endregion Methods
    }
}