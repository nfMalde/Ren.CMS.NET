namespace Ren.CMS.CORE.Links
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Link
    {
        #region Fields

        private string pHoverStateClass = "";
        private string pHref = "";
        private int pID = 0;
        private string pNormalStateClass = "";
        private LinkCollection pSubLinks = new LinkCollection();
        private string pTargetAction = "";
        private string pTargetController = "";
        private string pText = "";
        private string pType = "";

        #endregion Fields

        #region Constructors

        public Link()
        {
        }

        public Link(int aID, string aType, string aTargetController, string aTargetAction, string aText, string aHref, string aNormalStateClass = "", string aHoverStateClass = "", LinkCollection aSubLinks = null)
        {
            SettingsHelper.GlobalSettingsHelper G = new SettingsHelper.GlobalSettingsHelper();

            if (G.empty(aType) || G.empty(aText) || aID <= 0)
            {
                throw new ConfigurationErrorsException("Required Attributes (Type,Text,ID) are not correct.");

            }
            else
            {
                this.pHoverStateClass = aHoverStateClass;
                this.pNormalStateClass = aNormalStateClass;
                this.pType = aType;
                this.pText = aText;
                this.pTargetController = aTargetController;
                this.pTargetAction = aTargetAction;
                this.pHref = aHref;
                this.pID = aID;

            }
        }

        #endregion Constructors

        #region Properties

        public string HoverStateClass
        {
            get { return this.pHoverStateClass; }
            set { this.pHoverStateClass = value; }
        }

        public string Href
        {
            get
            {

                return this.pHref;

            }
            set
            {

                this.pHref = value;

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

        public string NormalStateClass
        {
            get { return this.pNormalStateClass; }
            set { this.pNormalStateClass = value; }
        }

        public LinkCollection SubLinks
        {
            get
            {

                return this.pSubLinks;

            }

            set
            {

                this.pSubLinks = value;

            }
        }

        public string TargetAction
        {
            get
            {

                return this.pTargetAction;
            }
            set
            {

                this.pTargetAction = value;

            }
        }

        public string TargetController
        {
            get
            {

                return this.pTargetController;
            }
            set
            {

                this.pTargetController = value;

            }
        }

        public string Text
        {
            get
            {
                Language.Language Lng = new Language.Language("__USER__", "LINKS");
                string realText = Lng.getLine(this.pText);
                return realText;

            }
            set
            {

                this.pText = value;
            }
        }

        public string Type
        {
            get
            {

                return this.pType;

            }
            set
            {

                this.pType = value;
            }
        }

        #endregion Properties
    }

    public class LinkCollection : System.Collections.IEnumerable
    {
        #region Fields

        private List<Link> pCol = new List<Link>();

        #endregion Fields

        #region Constructors

        public LinkCollection()
        {
        }

        #endregion Constructors

        #region Methods

        public void Add(Link LinkItem)
        {
            this.pCol.Add(LinkItem);
        }

        public void AddRange(Link[] LinkRange)
        {
            this.pCol.AddRange(LinkRange);
        }

        public IEnumerator<Link> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(Link LinkItem)
        {
            this.pCol.Remove(LinkItem);
        }

        #endregion Methods
    }

    public class ReturnMainLinks
    {
        #region Fields

        private LinkCollection Col = new LinkCollection();
        private int pCount = 0;

        #endregion Fields

        #region Constructors

        public ReturnMainLinks()
        {
        }

        #endregion Constructors

        #region Methods

        public LinkCollection toLinkCollection()
        {
            Language.Language LGN = new Language.Language("__USER__", "LINKS");

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();

            string command = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE LinkRelationship='nav_main' AND LinkIsActive='true'";
            SqlDataReader Row = Sql.SysReader(command);
            if (Row.HasRows)
            {
                SettingsHelper.GlobalSettingsHelper GSet = new SettingsHelper.GlobalSettingsHelper();
                while (Row.Read())
                {

                    Link Lnk = new Link();
                    Lnk.ID = ((int)Row["id"]);
                    if (Row["LinkType"] != DBNull.Value) Lnk.Type = Row["LinkType"].ToString();
                    if (Row["LinkText"] != DBNull.Value) Lnk.Text = LGN.getLine(Row["LinkText"].ToString());
                    if (GSet.empty(Lnk.Text)) Lnk.Text = Row["LinkText"].ToString() + " NOT FOUND IN PACKAGE:" + LGN.Package;
                    if (Row["LinkHref"] != DBNull.Value) Lnk.Href = Row["LinkHref"].ToString();
                    if (Row["LinkController"] != DBNull.Value) Lnk.TargetController = Row["LinkController"].ToString();
                    if (Row["LinkAction"] != DBNull.Value) Lnk.TargetAction = Row["LinkAction"].ToString();
                    this.Col.Add(Lnk);

                }

            }
            Row.Close();

            Sql.SysDisconnect();
            return this.Col;
        }

        #endregion Methods
    }

    public class ReturnSublinks
    {
        #region Fields

        private LinkCollection Col = new LinkCollection();

        #endregion Fields

        #region Constructors

        public ReturnSublinks(int RefId)
        {
            Language.Language LGN = new Language.Language("__USER__", "LINKS");

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();

            string command = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE SublinkFrom=@RefID AND LinkIsActive='true'";
            SqlParameter[] P = new SqlParameter[] { new SqlParameter("@RefID", RefId) };

            SqlDataReader Row = Sql.SysReader(command, P);
            if (Row.HasRows)
            {
                SettingsHelper.GlobalSettingsHelper GSet = new SettingsHelper.GlobalSettingsHelper();
                while (Row.Read())
                {

                    Link Lnk = new Link();
                    Lnk.ID = ((int)Row["id"]);
                    if (Row["LinkType"] != DBNull.Value) Lnk.Type = Row["LinkType"].ToString();
                    if (Row["LinkText"] != DBNull.Value) Lnk.Text = LGN.getLine(Row["LinkText"].ToString());
                    if (GSet.empty(Lnk.Text)) Lnk.Text = Row["LinkText"].ToString() + " NOT FOUND IN PACKAGE:" + LGN.Package;
                    if (Row["LinkHref"] != DBNull.Value) Lnk.Href = Row["LinkHref"].ToString();
                    if (Row["LinkController"] != DBNull.Value) Lnk.TargetController = Row["LinkController"].ToString();
                    if (Row["LinkAction"] != DBNull.Value) Lnk.TargetAction = Row["LinkAction"].ToString();
                    this.Col.Add(Lnk);

                }

            }
            Row.Close();

            Sql.SysDisconnect();
        }

        #endregion Constructors

        #region Methods

        public LinkCollection toLinkCollection()
        {
            return this.Col;
        }

        #endregion Methods
    }
}