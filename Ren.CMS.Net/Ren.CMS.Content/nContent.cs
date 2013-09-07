using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.ThisApplication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ren.CMS.CORE.nhibernate.Domain;

namespace Ren.CMS.Content
{

    public class nContentText
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string SEOName { get; set; }
        public string PreviewText { get; set; }
        public string LongText { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string LangCode { get; set; }


    
    
    
    }

    public class nContent
    {
        private List<nContentText> pcontentTexts = new List<nContentText>();

        public partial class nAttachment
        {

            private string pkID = "";
            private string fName = "";
            private string fPath = "";
            private string thumpnail = "";
            private string attachment_type = "";
            private string contenttype = "";
            private string Argument = "";
            private string pTitle = "";
            private string pRemark = "";
       

            /// <summary>
            /// Generates a Model für Content Attachments.  
            /// </summary>
            /// <param name="contentType">Content Type of the Attachment´s Content if you want to load from the Storage Folder</param>
            /// <param name="fileName">Filename of the Attachment</param>
            /// <param name="filePath">Filepath of the Attachment</param>
            /// <param name="thumpnailname">Thumpnail Name of the Attachment. Must be saved on Storage/Misc/Thumpnails</param>
            /// <param name="attachmenttype">There are 2 Attachmenttypes registered: image or video.</param>
            /// <param name="attachmentargument">Special Argument for the Attachment. For Example in the News Module for getting the Index IMG: indeximg</param>
            public nAttachment(string contentType, string fileName, string filePath, string thumpnailname, string attachmenttype, string attachmentargument = "default", string title = "", string remark="")
            {

                this.fName = fileName;
                this.fPath = filePath;
                this.contenttype = contentType;
                this.thumpnail = thumpnailname;
                this.attachment_type = attachmenttype;
                this.Argument = attachmentargument;
                this.pTitle = title;
                this.pRemark = remark;

            }
            public nAttachment(string id, string contentType, string fileName, string filePath, string thumpnailname, string attachmenttype, string attachmentargument = "default", string title = "", string remark ="")
            {

                this.pkID = id;
                this.fName = fileName;
                this.fPath = filePath;
                this.contenttype = contentType;
                this.thumpnail = thumpnailname;
                this.attachment_type = attachmenttype;
                this.Argument = attachmentargument;
                this.pTitle = title;
                this.pRemark = remark;

            }

            public string PKID
            {

                get { return this.pkID; }
                set { this.pkID = value; }
            }

            public string Title
            {


                get
                {

                    return this.pTitle;
                }

                set
                {


                    this.pTitle = value;

                }
            }


            public string Path
            {

                get
                {

                    return (this.fPath + "/" + this.fName).Replace("//","/"); //Bad Request preventing



                }
                set {

                    string p = value;

                    this.fPath = p.Remove(p.LastIndexOf(System.IO.Path.GetFileName(p)));
                    this.fName = System.IO.Path.GetFileName(p);
                
                }


            }
            public string AttachmentType { get { return this.attachment_type; } }
            public string AttachmentArgument { get { return this.Argument; } }

            public string ThumpnailPath { get { return this.thumpnail; } set { this.thumpnail = value; } }
            public string AttachmentRemark { get { return this.pRemark; } set { this.pRemark = value; } }



        }

        #region Attribute
        //Attribute    
        private string sCreatorSpecialName = "";
        private int cID = 0;
 
        private string cType = "";
        private object cCID = new object();
        private object cSUBID = new object();
        private string cCategory = "";
        private string cSubCategory = "";
        private object cCreatorPKID = new object();
        private string cUname = "";
        private bool cLocked = false;
 
        private int ratingGroupID = 0;
        private DateTime cDate = new DateTime();
 
        private string pLinkController = "";
        private string pLinkActionPath = "";
        private int refid = 0;
        private int clicks = 0;
        #endregion
        #region Kunstruktor
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
        #endregion
        #region Eigentschaften


        public List<nContentText> Texts { get {
           return this.pcontentTexts;
        }
            set {
                this.pcontentTexts = value;
            }
        }

        public int ReferenceID
        {

            get { return this.refid; }
            set { this.refid = value; }


        }
        public int ClickCount
        {
            get { return this.clicks; }

        }
        public string TargetController
        {

            get { return this.controller; }


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

        public string TargetAction
        {



            get { return this.actionpath; }

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

 
        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        public string ContentType
        {
            get
            {

                return this.cType;

            }

        }
        /// <summary>
        /// Returns the Category object
        /// </summary>
        public object CategoryID
        {

            get
            {


                return this.cCID;
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
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        public bool Locked
        {

            get
            {

                return this.cLocked;

            }

        }
        private string getColValue(string col)
        {
            string v = "";
            if (this.ID == 0) throw new Exception("Content not found. ID is null");
            try
            {
                SqlHelper Sql = new SqlHelper();

                Sql.SysConnect();
                col = col.Replace("'", "").Replace("-", "");
                string query = "SELECT " + col + " FROM  " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content WHERE id=@id";
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@id", this.ID) };


                SqlDataReader Value = Sql.SysReader(query, P);
                if (Value.HasRows)
                {

                    Value.Read();

                    v = Value[col].ToString();
                }
                else
                {

                    v = "NOT FOUND";

                }

                Value.Close();
                Sql.SysDisconnect();
            }
            catch
            {
                v = "Problems with: " + col;

            }

            return v;
        }
        /// <summary>
        /// returns the controller of the Content (requires the Call  GenerateLink() )
        /// </summary>
        public string controller = null;
        /// <summary>
        /// returns the action path of the Content (requires the Call  GenerateLink() )
        /// </summary>
        public string actionpath = null;
        /// <summary>
        /// Returns the Full Link for an A Tag for Example.
        /// </summary>
        public string FullLink = null;


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
       

        /// <summary>
        /// Returns the Rating Group ID
        /// </summary>
        public int RatingGroupID
        {

            get { return this.ratingGroupID; }


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

        }

      

        public void RegisterNewAttachment(string attachmentType, string fileName, string filePath, string thumpNail = "", string AttachmentArgument = "", string AttachmentTitle = "")
        {
            Ren.CMS.CORE.nhibernate.Repositories.ContentAttachmentRepository Repo = new CORE.nhibernate.Repositories.ContentAttachmentRepository();

            Ren.CMS.CORE.nhibernate.Domain.ContentAttachment Model = new CORE.nhibernate.Domain.ContentAttachment();
            Model.Nid = this.ID;
            Model.ATitle = AttachmentTitle;
            Model.AttachmentArgument = AttachmentArgument;
            Model.AttachmentRemarks = String.Empty;
            Model.AttachmentType = attachmentType;
            Model.ContentType = this.ContentType;
            Model.FName = fileName;
            Model.FPath = filePath;
            Model.Pkid = Guid.NewGuid();
            Model.ThumpNail = thumpNail ?? fileName ?? filePath;
            Repo.Add(Model);
             

        }
        public List<nAttachment> Attachments(string attachmentType = "ALL", string attachmentArgument = "ALL")
        {

            Ren.CMS.CORE.nhibernate.Repositories.ContentAttachmentRepository Repo = new CORE.nhibernate.Repositories.ContentAttachmentRepository();
            var list = (attachmentType != "ALL" && attachmentArgument != "ALL" ?
                Repo.GetMany(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.AttachmentType == attachmentType && e.AttachmentArgument == attachmentArgument && e.Nid == this.ID)) :
                (attachmentType == "ALL" && attachmentArgument != "ALL" ?
                Repo.GetMany(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.AttachmentArgument == attachmentArgument && e.Nid == this.ID)) :
                (attachmentType != "ALL" && attachmentArgument == "ALL" ?
                Repo.GetMany(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.AttachmentType == attachmentType && e.Nid == this.ID)) :
                Repo.GetMany(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.Nid == this.ID)))));
               
            List<nAttachment> TMP = new List<nAttachment>();

            list.ToList<ContentAttachment>().ForEach(e =>

               TMP.Add(new nAttachment(e.Pkid.ToString(), e.ContentType, e.FName, e.FPath, e.ThumpNail, e.AttachmentType, e.AttachmentArgument, e.ATitle, e.AttachmentRemarks)));
             
            return TMP;


        }

        #endregion


    }
}
