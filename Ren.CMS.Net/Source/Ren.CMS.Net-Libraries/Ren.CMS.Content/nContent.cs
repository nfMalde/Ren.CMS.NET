namespace Ren.CMS.Content
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Repositories;
    using Ren.CMS.CORE.Helper;
using System.Web;

    public class nContent
    {
        #region Fields

        /// <summary>
        /// returns the action path of the Content (requires the Call  GenerateLink() )
        /// </summary>
        public string actionpath = null;

        /// <summary>
        /// returns the controller of the Content (requires the Call  GenerateLink() )
        /// </summary>
        public string controller = null;

        /// <summary>
        /// Returns the Full Link for an A Tag for Example.
        /// </summary>
        public string FullLink = null;

        private string cCategory = "";
        private object cCID = new object();
        private object cCreatorPKID = new object();
        private DateTime cDate = new DateTime();
        private int cID = 0;
        private int clicks = 0;
        private bool cLocked = false;
        private string cSubCategory = "";
        private object cSUBID = new object();
        private string cType = "";
        private string cUname = "";
        private List<nContentText> pcontentTexts = new List<nContentText>();
        private string pLinkActionPath = "";
        private string pLinkController = "";
        private int ratingGroupID = 0;
        private int refid = 0;

        //Attribute
        private string sCreatorSpecialName = "";

        #endregion Fields

        #region Constructors

        //Kunstruktor
        /// <summary>
        /// Generates a nContent Model.
        /// </summary>
        /// <param name="id">ID of the Content</param>
        /// <param name="title">Title of the Content</param>
        /// <param name="type">Type of the Content e.g. eNews</param>
        /// <param name="cid">object of the Category</param>
        /// <param name="subid">object of the sub category</param>
        /// <param name="category">Name of the category</param>
        /// <param name="subcategory">Name of the sub category</param>
        /// <param name="creatorPKid">PK object of the User who created the content</param>
        /// <param name="username">Username of the User who created the content</param>
        /// <param name="locked">Boolean: Is Content Locked(Not Visible)?</param>
        /// <param name="seoname">Search Engine Optimized Name. Usually used for direkt URL Call to the Content</param>
        /// <param name="ratinggroupid">Rating Group ID (int) This is for ContentType eArticle only</param>
        /// <param name="cdate">Created Datetime of the Content</param>
        /// <param name="metakeywords">Meta Tag Key Words for Search Engine Optimizing</param>
        /// <param name="metadescription">Meta Tag Description for Search Engine Optimizing(Default: PreviewText)</param>
        /// <param name="previewtext">Previewtext of the Content. Displayed in the Overview and on some Modules Sites.</param>
        /// <param name="longtext">The finally Text of the Content. Used in Detail View of the Content.</param>
        public nContent(int id, IEnumerable<nContentText> contentTexts, string type, object cid, string category, object creatorPKid,
            string username, bool locked, int ratinggroupid, DateTime cdate, int Cref = 0, string CreatorSpecialName_ = "")
        {
            this.cID = id;
            this.pcontentTexts = contentTexts.ToList();
            this.cType = type;
            this.cCID = cid;

            this.cCategory = category;

            this.cCreatorPKID = creatorPKid;
            this.cUname = username;
            this.cLocked = locked;

            this.ratingGroupID = ratinggroupid;
            this.cDate = cdate;

            this.sCreatorSpecialName = CreatorSpecialName_;
            this.refid = Cref;

            if (id > 0)
            {
                this.Attachments = new nAttachmentManager(id);
                //Hotfix: Get Clicks
                SqlHelper SQL = new SqlHelper();
                string query = "SELECT COUNT(*) as ClickCount FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_ClickCounter WHERE cid=@id";
                nSqlParameterCollection PCOL = new nSqlParameterCollection();
                PCOL.Add("@id", id);
                SQL.SysConnect();
                SqlDataReader Clicks = SQL.SysReader(query, PCOL);
                if (Clicks.HasRows)
                {

                    while (Clicks.Read())
                    {
                        this.clicks = (int)Clicks["ClickCount"];

                    }

                }
                Clicks.Close();
                SQL.SysDisconnect();


            }
        }

        public nContent(Models.Core.nContentPostModel ContentPostModel)
        {
            this.cID = ContentPostModel.ID;

            ContentPostModel.Texts.ToList().ForEach(e =>

                this.pcontentTexts.Add(new nContentText()
                {
                    Id = e.TextID,
                    LangCode = e.LangCode,
                    LongText = e.LongText,
                    MetaDescription = e.MetaDescription,
                    MetaKeyWords = e.MetaKeyWords,
                    PreviewText = e.PreviewText,
                    SEOName = e.SEOName,
                    Title = e.Title
                }));

            this.cType = ContentPostModel.ContentType;
            this.cCID = ContentPostModel.CategoryID;

            this.cCategory = "";

            this.cCreatorPKID = ContentPostModel.CreatorPKID;
            this.cUname = "";
            this.cLocked = ContentPostModel.Locked;

            this.ratingGroupID = 0;
            this.cDate = ContentPostModel.CreationDate;

            this.sCreatorSpecialName = ContentPostModel.CreatorSpecialName;
            this.refid = ContentPostModel.ReferenceID;

            if (ContentPostModel.ID > 0)
            {
                this.Attachments = new nAttachmentManager(ContentPostModel.ID);
                //Hotfix: Get Clicks
                SqlHelper SQL = new SqlHelper();
                string query = "SELECT COUNT(*) as ClickCount FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_ClickCounter WHERE cid=@id";
                nSqlParameterCollection PCOL = new nSqlParameterCollection();
                PCOL.Add("@id", ContentPostModel.ID);
                SQL.SysConnect();
                SqlDataReader Clicks = SQL.SysReader(query, PCOL);
                if (Clicks.HasRows)
                {

                    while (Clicks.Read())
                    {
                        this.clicks = (int)Clicks["ClickCount"];

                    }

                }
                Clicks.Close();
                SQL.SysDisconnect();
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Returns the Category object
        /// </summary>
        public object CategoryID
        {
            get
            {

                return this.cCID;
            }
            set
            {
                this.cCID = value;
            }
        }

        /// <summary>
        /// Returns the Category name
        /// </summary>
        public string CategoryName
        {
            get
            {

                return this.cCategory;

            }
            set
            {
                this.cCategory = value;
            }
        }

        public int ClickCount
        {
            get { return this.clicks; }
            set { this.clicks = value; }
        }

        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        public string ContentType
        {
            get
            {

                return this.cType;

            }

            set
            {
                this.cType = value;
            }
        }

        /// <summary>
        /// Returns the Creation Date
        /// </summary>
        public DateTime CreationDate
        {
            get
            {

                return this.cDate;

            }
            set
            {
                this.cDate = value;    
            }
        }

        /// <summary>
        /// Returns the Username of the Creator
        /// </summary>
        public string CreatorName
        {
            get
            {

                return this.cUname;

            }
            set { this.cUname = value; }
        }

        /// <summary>
        /// Returns the Creators PK ID
        /// </summary>
        public object CreatorPKID
        {
            get
            {

                return this.cCreatorPKID;

            }
        }

        public string CreatorSpecialName
        {
            get
            {

                return this.sCreatorSpecialName;

            }
            set
            {

                this.sCreatorSpecialName = value;

            }
        }

        //Eigenschaften
        /// <summary>
        /// Returns the ID of the Content
        /// </summary>
        /// 
        public int ID
        {
            get
            {

                return this.cID;

            }
        }

        /// <summary>
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        public bool Locked
        {
            get
            {

                return this.cLocked;

            }
            set
            {
                this.cLocked = value;
            }
        }

        /// <summary>
        /// Returns the Rating Group ID
        /// </summary>
        public int RatingGroupID
        {
            get { return this.ratingGroupID; }
        }

        public int ReferenceID
        {
            get { return this.refid; }
            set { this.refid = value; }
        }

        /// <summary>
        /// Returns the Subcategory Name
        /// </summary>
        public string SubCategoryName
        {
            get
            {

                return this.cSubCategory;

            }
        }

        public string TargetAction
        {
            get { return this.actionpath; }
        }

        public string TargetController
        {
            get { return this.controller; }
        }

        public List<nContentText> Texts
        {
            get {
               return this.pcontentTexts;
            }
            set {
                this.pcontentTexts = value;
            }
        }

        #endregion Properties
 

        #region Methods
 

        /// <summary>
        /// This Functions generates the final Controller Name and Action Path
        /// </summary>
        ///  
        /// 
        public void GenerateLink()
        {
            SqlHelper Sql = new SqlHelper();

            Sql.SysConnect();
            string query = "SELECT TOP 1 * FROM  " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Types WHERE name=@name";
            SqlParameter[] P = new SqlParameter[] { new SqlParameter("@name", this.ContentType) };
            SqlDataReader cType = Sql.SysReader(query, P);
            string link = "";
            if (cType.HasRows)
            {
                cType.Read();

                this.controller = cType["controller"].ToString();
                this.actionpath = cType["actionpath"].ToString();
                this.parseActionPath(this.actionpath, out this.actionpath);

                controller = controller.Replace("/", "");
                link = "/" + controller + "/" + actionpath;

            }

            cType.Close();
            Sql.SysDisconnect();

            this.FullLink = link;
        }

        public List<nContentTag> getTags()
        {
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            ThisApplication APP = new ThisApplication();

            string pref = APP.getSqlPrefix;

            string query = "SELECT * FROM " + pref + "Content_Tags LEFT OUTER JOIN  " + pref + "Content_Tags2Content ON(" + pref + "Content_Tags2Content.tagID = " + pref + "Content_Tags.id)  WHERE " + pref + "Content_Tags2Content.contentID = @cid";

            nSqlParameterCollection P = new nSqlParameterCollection();

            P.Add("@cid", this.cID);

            SqlDataReader R = SQL.SysReader(query, P);

            List<nContentTag> Buffer = new List<nContentTag>();
            while (R.Read())
            {
                bool browsingOK = true;

                if (R["enableBrowsing"] != DBNull.Value)
                {
                    if (((int)R["enableBrowsing"]) == 0) browsingOK = false;
                }
                nContentTag Tag = new nContentTag((int)R["id"], (string)R["tagName"], (string)R["contentType"], (string)R["tagNameSEO"], browsingOK);

                Buffer.Add(Tag);

            }

            R.Close();
            SQL.SysDisconnect();

            return Buffer;
        }
 
        private string getColValue(string col)
        {
            string v = "";
            if (this.ID == 0) throw new Exception("Content not found. ID is null");
            try
            {
                BaseRepository<ContentText> _Repo = new BaseRepository<ContentText>();
                var Texts = _Repo.GetMany(where: NHibernate.Criterion.Expression.Where<ContentText>(x => x.ContentId == this.ID));
                ContentText myText = null;
                if (Texts.Any(e => e.LangCode == CurrentLanguageHelper.CurrentLanguage))
                {
                    myText = Texts.Where(t => t.LangCode == CurrentLanguageHelper.CurrentLanguage).First();
                }
                else if (Texts.Any(e => e.LangCode == CurrentLanguageHelper.DefaultLanguage))
                {
                    myText = Texts.Where(t => t.LangCode == CurrentLanguageHelper.DefaultLanguage).First();
                }
                else if (Texts.Count() > 0)
                {
                    myText = Texts.FirstOrDefault();
                }
                else
                    throw new Exception("Unable to load holy shit Text. Dude you should really look at  your f*ucking DB!!!!!!!");
 
                col = col.Replace("'", "").Replace("-", "");
                var Properties = myText.GetType().GetProperties();
                if(Properties.Any(e => e.Name.ToLower() == col.ToLower()))
                {
                    var property = Properties.Where(e => e.Name.ToLower() == col.ToLower()).First();
                    v = property.GetValue(myText).ToString();
                }
                else 
                {

                    v = "Unknown Column Dude";
                }

            }
            catch (Exception E) 
            {
                v = "Problems with: " + col;
                throw E;
            }

            return v;
        }

        private void parseActionPath(string input, out string output)
        {
            string pattern = @"{(.+?)\}";
            System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(input);

            foreach (Match AP in API)
            {

                string str = AP.Value;
                string col = Regex.Replace(str, pattern, "$1");
                string final = input.Replace(str, this.getColValue(col));

                input = final;

            }
            output = input;
        }

        #endregion Methods

        public nAttachmentManager Attachments { get; set; }
       
    }

    public class nContentText
    {
        #region Properties

        public int Id
        {
            get; set;
        }

        public string LangCode
        {
            get; set;
        }

        public string LongText
        {
            get; set;
        }

        public string MetaDescription
        {
            get; set;
        }

        public string MetaKeyWords
        {
            get; set;
        }

        public string PreviewText
        {
            get; set;
        }

        public string SEOName
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        #endregion Properties
    }
}