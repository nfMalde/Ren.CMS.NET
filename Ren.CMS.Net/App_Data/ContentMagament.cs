using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using nfCMS_NET.CORE;
using nfCMS_NET.CORE.SqlHelper;
using nfCMS_NET.CORE.Links;
using nfCMS_NET.CORE.Language;
using nfCMS_NET.CORE.ThisApplication;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
namespace nfCMS_NET.Pagination {
    public class nPagingCollection : IEnumerable {
        private int mpages = 0;
        public int MaxPages { get { return this.mpages; } }
        public string getDebugInfo {

            get {

                nfCMS_NET.CORE.Extras.Extras EX = new nfCMS_NET.CORE.Extras.Extras();
                return EX.var_dump(this, 10);
            
            }
        
        
        
        }

        private List<nPage> pCol = new List<nPage>();
        public nPagingCollection(int totalRows, int pageSize) {

            decimal i = Convert.ToDecimal(totalRows);
            decimal xi = Convert.ToDecimal(pageSize);
            decimal y = i / xi;
            y = Math.Ceiling(y);
            int z = Convert.ToInt32(y);



            int pages = z;
            this.mpages = pages; 

            for (int x = 0; x< pages; x++) {

                nPage P = new nPage((x+1), pageSize);
                pCol.Add(P);
            
            
            }
        
        
        
        
        }

        public int Count { get { return this.pCol.Count; } }
        public IEnumerator<nPage> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    }

    public class nPage {

        private int ppage = 0;
        private int prowstart = 0;
        private int prowend = 0;


        public nPage(int page, int pagesize) {
            int pageStart = page * pagesize+1;
            pageStart = pageStart - pagesize;
            int pageEnd = page * pagesize;
            this.ppage = page;
            this.prowstart = pageStart;
            this.prowend = pageEnd;
        }



        public int Index {

            get { return this.ppage; }
            set { this.ppage = value; }
        
        }


        public int RowStart {

            get { return this.prowstart; }
            set { this.prowstart = value; }
        
        }


        public int RowEnd {


            get { return this.prowend; }
            set { this.prowend = value; }
        
        }
    
    
    }

 



}

namespace nfCMS_NET.Content
{
    public class nContentTag {

        private string pName = null;
        private string pContentType = null;
        private int pID = -1;
        private bool pBrowsingEnabled = false;
        private string pSEOName = null;
        private void initModel(int id, string name, string contenttype, string SEOName, bool browsingEnabled = false) {
            this.pID = id;
            this.pName = name;
            this.pContentType = contenttype;
            this.pBrowsingEnabled = browsingEnabled;
            this.pSEOName = SEOName;
        
        
        }
        public nContentTag(string name, string contenttype, string SEOName, bool browsingEnabled = false) {


            this.initModel(-1, name, contenttype, SEOName, browsingEnabled);


        
        
        
        
        }
        public nContentTag(int id, string name, string contenttype, string SEOName, bool browsingEnabled = false)
        {

            this.initModel(id, name, contenttype, SEOName, browsingEnabled);

        
        
        }
       

        private bool checkTagNameExists() {

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
        public bool BrowsingEnabled {

            get { return this.pBrowsingEnabled; }
            set { this.pBrowsingEnabled = value; }
        
        
        }

        public string UrlOptimizedName {


            get { return this.pSEOName; }
            set { this.pSEOName = value; }
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
        private string savingLog = null;
        public string getSavingLog() {

            return this.savingLog;
        
        
        
        }


        private void UpdateTag(string name, string contentType, bool browsing_Enabled) {


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


            SQL.SysNonQuery(nonquery,Paras);

            SQL.SysDisconnect();

        
        
        
        
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


        public void Save() {

            if (this.checkTagIDExists())
            {

                if (!this.checkTagNameExists())
                {
                    this.UpdateTag(this.pName, this.pContentType,this.pBrowsingEnabled);

                    this.savingLog = "Tag with id #" + this.pID + " updated. New VALS: tagName=" + this.pName + " AND contentType=" + this.pContentType;
                }
                else
                {

                    this.savingLog = "This tag name is allready taken by another tag. Cannot update.";

                }

            }
            else {


                if (!this.checkTagNameExists())
                {



                    this.InsertTag(this.pName, this.pContentType, this.pBrowsingEnabled);
                    this.savingLog = "Tag saved and created.VALS: tagName=" + this.pName + " AND contentType=" + this.pContentType;

                }
                else {

                    this.savingLog = "This tag name is allready taken by another tag. Cannot insert.";
                
                
                }
            
            
            }
        
        
        
        }

        public int ID {

            get {

                return this.pID;
            
            }
            set {


                this.pID = value;
            
            }
        
        }

        public string Name {


            get {


                return this.pName;
            
            }
            set {


                this.pName = value;
            
            }
        
        }


        public string ContentType {


            get {



                return this.pContentType;
            
            
            }

            set {

                this.pContentType = value;
            
            }
        
        
        
        
        }




    
    }
    public class nContentClickCounter
    {
        private nContent cont = null;

        public nContentClickCounter(nContent _Content)
        {

            this.cont = _Content;
        }


        private bool ipallreadyClicked()
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            nfCMS_NET.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            IP = Crypto.ConvertToSHA1(IP);

            if (String.IsNullOrEmpty(IP))
            {
                return true;
            }

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string query = "SELECT * FROM "+ (new ThisApplication().getSqlPrefix ) +"Content_ClickCounter WHERE IP=@ip AND cid=@id";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@ip", IP);
            PCOL.Add("@id", this.cont.ID);
            SqlDataReader R = SQL.SysReader(query, PCOL);
            bool ret = R.HasRows;
            R.Close();
            SQL.SysDisconnect();

            return ret;
        
        }

        public void enableClickCounter(bool iprestrict = true)
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            nfCMS_NET.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            IP = Crypto.ConvertToSHA1(IP);
            if (this.cont.ID > 0)
            {
                if (!iprestrict || !this.ipallreadyClicked())
                {
                    nSqlParameterCollection PCOL = new nSqlParameterCollection();
                    SqlHelper SQL = new SqlHelper();
                    SQL.SysConnect();
                    string query = "INSERT INTO " + (new ThisApplication().getSqlPrefix) + "Content_ClickCounter(IP,cid) VALUES(@ip,@id)";
                    PCOL.Add("@ip", IP);
                    PCOL.Add("@id", this.cont.ID);
                    SQL.SysNonQuery(query,PCOL);
                    SQL.SysDisconnect();


                }
            }
        }
    
    }
    public class nContent
    {   
        public partial class nAttachment {
            private string fName = "";
            private string fPath = "";
            private string thumpnail = "";
            private string attachment_type = "";
            private string contenttype = "";
            private string Argument = "";
            private string pTitle = "";
            
      
            /// <summary>
            /// Generates a Model für Content Attachments.  
            /// </summary>
            /// <param name="contentType">Content Type of the Attachment´s Content if you want to load from the Storage Folder</param>
            /// <param name="fileName">Filename of the Attachment</param>
            /// <param name="filePath">Filepath of the Attachment</param>
            /// <param name="thumpnailname">Thumpnail Name of the Attachment. Must be saved on Storage/Misc/Thumpnails</param>
            /// <param name="attachmenttype">There are 2 Attachmenttypes registered: image or video.</param>
            /// <param name="attachmentargument">Special Argument for the Attachment. For Example in the News Module for getting the Index IMG: indeximg</param>
            public nAttachment(string contentType, string fileName, string filePath, string thumpnailname, string attachmenttype, string attachmentargument="default", string title = "") {
                if (!String.IsNullOrEmpty(contentType) && !String.IsNullOrEmpty(filePath)) {

                    throw new Exception("You can only set contenttype OR filePath! >ONE< Of these must be empty or null.");
                
                }
                this.fName = fileName;
                this.fPath = filePath;
                this.contenttype = contentType;
                this.thumpnail = thumpnailname;
                this.attachment_type = attachmenttype;
                this.Argument = attachmentargument;
                this.pTitle = title;
            
            }

            public string Title { 
            
            
            get{

                return this.pTitle;
            }

                set {


                    this.pTitle = value;

                }
            }


            public string Path {

                get {

                    if (String.IsNullOrEmpty(this.contenttype)) { 
                    
                    
                        return this.fPath +(!this.fPath.EndsWith(this.fName) ?  (this.fPath.EndsWith("/")? this.fName : "/"+ this.fName) :"");
                    
                    }
                    else{
                    
                       string p =new nfCMS_NET.CORE.ThisApplication.ThisApplication().BaseUrl + ("Storage/"+ this.contenttype +"/"+ this.fName);
                        return p;
                    
                    }
                
                
                
                }
            
            
            }
            public string AttachmentType { get { return this.attachment_type; } }
            public string AttachmentArgument{ get { return this.Argument; } }
            

        
        
        }

        #region Attribute
        //Attribute    
        private string sCreatorSpecialName = "";
        private int cID = 0;
        private string pTitle = "";
        private string cType = "";
        private object cCID = new object();
        private object cSUBID =new object();
        private string cCategory = "";
        private string cSubCategory = "";
        private object cCreatorPKID = new object();
        private string cUname = "";
        private bool cLocked = false;
        private string cSEOname = "";
        private int ratingGroupID = 0;
        private DateTime cDate = new DateTime();
        private string cMetaKeyWords = "";
        private string cMetaDescription = "";
        private string cPreviewText = "";
        private string cLongText = "";
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
        public nContent(int id, string title, string type, object cid, object subid, string category, string subcategory, object creatorPKid,
                        string username, bool locked, string seoname, int ratinggroupid, DateTime cdate, string metakeywords,
                        string metadescription, string previewtext, string longtext, int Cref = 0, string CreatorSpecialName_ = "")
        {

            this.cID = id;
            this.pTitle = title;
            this.cType = type;
            this.cCID = cid;
            this.cSUBID = subid;
            this.cCategory = category;
            this.cSubCategory = subcategory;
            this.cCreatorPKID = creatorPKid;
            this.cUname = username;
            this.cLocked = locked;
            this.cSEOname = seoname;
            this.ratingGroupID = ratinggroupid;
            this.cDate = cdate;
            this.cMetaKeyWords = metakeywords;
            this.cMetaDescription = metadescription;
            this.cPreviewText = previewtext;
            this.cLongText = longtext;
            this.sCreatorSpecialName = CreatorSpecialName_;
            this.refid = Cref;

            if (id > 0)
            {
                //Hotfix: Get Clicks
                SqlHelper SQL = new SqlHelper();
                string query = "SELECT COUNT(*) as ClickCount FROM " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_ClickCounter WHERE cid=@id";
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

        #endregion
        #region Eigentschaften
        
        public int ReferenceID {

            get { return this.refid; }
            set { this.refid = value; }
        
        
        }
        public int ClickCount
        {
            get { return this.clicks; }
        
        }
        public string TargetController {

            get { return this.controller; }
        
        
        }
        public string CreatorSpecialName{
    
    get{

        return this.sCreatorSpecialName;
    
    }
            set {


                this.sCreatorSpecialName = value;
            
            }
    
        }

        public string TargetAction {



            get { return this.actionpath; }
        
        }
        
        //Eigenschaften
        /// <summary>
        /// Returns the ID of the Content
        /// </summary>
        /// 
        public int ID {
            get {

                return this.cID;
            
            }
        
            }

        public List<nContentTag> getTags() {


            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            ThisApplication APP = new ThisApplication();

            string pref = APP.getSqlPrefix;


            string query = "SELECT * FROM " + pref + "Content_Tags LEFT OUTER JOIN  " + pref + "Content_Tags2Content ON(" + pref + "Content_Tags2Content.tagID = " + pref + "Content_Tags.id)  WHERE " + pref + "Content_Tags2Content.contentID = @cid";

            nSqlParameterCollection P = new nSqlParameterCollection();

            P.Add("@cid", this.cID);



            SqlDataReader R = SQL.SysReader(query, P);

            List<nContentTag> Buffer = new List<nContentTag>();
            while (R.Read()) {
                bool browsingOK = true;

                if (R["enableBrowsing"] != DBNull.Value)
                {
                    if (((int)R["enableBrowsing"]) == 0) browsingOK = false;
                }
                nContentTag Tag = new nContentTag((int)R["id"], (string)R["tagName"], (string)R["contentType"],(string)R["tagNameSEO"], browsingOK);



                Buffer.Add(Tag);
            
            
            
            
            }



            R.Close();
            SQL.SysDisconnect();


            return Buffer;


        
        
        
        }


        /// <summary>
        /// Returns the Title of the Content
        /// </summary>
        public string Title {

            get { return this.pTitle; }
        
        }
        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        public string ContentType {
            get {

                return this.cType;
            
            }
        
        }
        /// <summary>
        /// Returns the Category object
        /// </summary>
        public object CategoryID {

            get {


                return this.cCID;
            }
        
            }
        /// <summary>
        /// Returns the Sub category ID
        /// </summary>
        public object SubCategoryID {


            get {

                return this.cSUBID;
            
            }
        
        }
        /// <summary>
        /// Returns the Category name
        /// </summary>
        public string CategoryName {

            get {

                return this.cCategory;
            
            
            }
        
        
        }
        /// <summary>
        /// Returns the Subcategory Name
        /// </summary>
        public string SubCategoryName {

            get {

                return this.cSubCategory;
            
            }
        
        }
        /// <summary>
        /// Returns the Creators PK ID
        /// </summary>
        public object CreatorPKID {


            get {

                return this.cCreatorPKID;
            
            }
        
        
        }

        /// <summary>
        /// Returns the Username of the Creator
        /// </summary>
        public string CreatorName {

            get {

                return this.cUname;
            
            }
        
        
        }

        /// <summary>
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        public bool Locked {

            get {

                return this.cLocked;
            
            }
        
        }
        private string getColValue(string col) {
            string v = "";
            if (this.ID == 0) throw new Exception("Content not found. ID is null");
            try
            {
                SqlHelper Sql = new SqlHelper();

                Sql.SysConnect();
                col = col.Replace("'", "").Replace("-", "");
                string query = "SELECT " + col + " FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content WHERE id=@id";
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
            catch {
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
            public void GenerateLink() {
            SqlHelper Sql = new SqlHelper();
           
            Sql.SysConnect();
            string query = "SELECT TOP 1 * FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content_Types WHERE name=@name";
            SqlParameter[] P = new SqlParameter[]{ new SqlParameter("@name",this.ContentType)};
            SqlDataReader cType = Sql.SysReader(query, P);
            string link = "";
            if (cType.HasRows) {
                cType.Read();

                this.controller = cType["controller"].ToString();
                this.actionpath = cType["actionpath"].ToString();
                this.parseActionPath(this.actionpath, out this.actionpath);

                controller = controller.Replace("/", "");
                link = "/"+ controller + "/" + actionpath;
               

            
            }

            cType.Close();
            Sql.SysDisconnect();


            this.FullLink = link;
        }
        /// <summary>
        /// Returns the SEARCH ENGINE OPTIMIZED Name
        /// </summary>
        public string SEOName {

            get {

                return this.cSEOname;
            
            }
        
        }

        /// <summary>
        /// Returns the Rating Group ID
        /// </summary>
        public int RatingGroupID {

            get { return this.ratingGroupID; }
        
        
        }

        /// <summary>
        /// Returns the Creation Date
        /// </summary>
        public DateTime CreationDate {

            get {

                return this.cDate;
            
            }
        
        }

        /// <summary>
        /// Returns the COMMA SEPERATED Meta-Keywords
        /// </summary>
        public string MetaKeyWords {

            get {

                return this.cMetaKeyWords;
            
                }


                }
        /// <summary>
        /// Returns the MetaDescription
        /// </summary>
        public string MetaDescription {

            get {
                return this.cMetaDescription;
            
            }
        
        
        }
        /// <summary>
        /// Returns the PreviewText
        /// </summary>
        public string PreviewText {

            get {

                return this.cPreviewText;
            
            }
        
        
        }
        /// <summary>
        /// Returns the Content Long Text
        /// </summary>
        public string LongText
        {

            get
            {

                return this.cLongText;

            }


        }


        public List<nAttachment> Attachments(string attachmentType = "ALL", string attachmentArgument = "ALL") {
            List<nAttachment> TMP = new List<nAttachment>();
            List<SqlParameter> P = new List<SqlParameter>();
            string query = "SElECT * FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content_Attachment WHERE NID=@nid ";
            P.Add(new SqlParameter("@nid", this.ID));
            if (attachmentType != "ALL") {
                P.Add(new SqlParameter("@typ", attachmentType));
                query += "AND attachment_type=@typ ";     
             }
            if (attachmentArgument != "ALL")
            {
                P.Add(new SqlParameter("@arg", attachmentArgument));
                query += "AND AttachmentArgument=@arg ";
            }
            
            
            SqlHelper SQ = new SqlHelper();
            SQ.SysConnect();

            SqlDataReader Att = SQ.SysReader(query, P);
            while (Att.Read()) {
                nAttachment E = new nAttachment(
                    (Att["content_type"] == DBNull.Value || Att["content_type"].ToString() == null ? null : Att["content_type"].ToString()),
                     (Att["fName"] == DBNull.Value || Att["fName"].ToString() == null ? null : Att["fName"].ToString()),
                      (Att["fPath"] == DBNull.Value || Att["fPath"].ToString() == null ? null : Att["fPath"].ToString()),
                       (Att["thumpNail"] == DBNull.Value || Att["thumpNail"].ToString() == null ? null : Att["thumpNail"].ToString()),
                        (Att["attachment_type"] == DBNull.Value || Att["attachment_type"].ToString() == null ? null : Att["attachment_type"].ToString()),
                        (Att["AttachmentArgument"] == DBNull.Value || Att["AttachmentArgument"].ToString() == null ? null : Att["AttachmentArgument"].ToString()),
                        (Att["aTitle"] == DBNull.Value || Att["aTitle"].ToString() == null ? null : Att["aTitle"].ToString())
                       
                        );
                TMP.Add(E);
            
            
            
            
            }
            Att.Close();
            SQ.SysDisconnect();
            return TMP;
        
        
        }

        #endregion


    }
    public class ContentManagement
    {
        public partial class PageInitializier
        {

            private int index = 0;
            private int startid = 0;

            public PageInitializier(int i, int s)
            {

                this.index = i;
                this.startid = s-1;


            }
            public int PageIndex
            {
                get
                {

                    return this.index;

                }


            }

            public int PageStart
            {

                get
                {

                    return this.startid;

                }

            }





        }




        public List<PageInitializier> GetPages(int MSSQLTotalRows, int MSSQLstart = 1212, int MSSQLincrement = 1111, int MSSQLlimit = 10,string order="DESC")
        {
            List<PageInitializier> PP = new List<PageInitializier>();
            int increment = MSSQLincrement;
            int start = MSSQLstart;
            if(order == "DESC")
            start = (MSSQLTotalRows* increment) - (MSSQLlimit * increment);

            
            int p = 1;


            int lim = MSSQLlimit;

        
            List<PageInitializier> PageP = new List<PageInitializier>();
            int pages = MSSQLTotalRows / MSSQLlimit;
            for (p = 1; p <= pages; p++)
            {
               
                int lastid = (order=="DESC" ? start - (increment * lim) : start + (increment * lim) );

                PageInitializier P = new PageInitializier(p, start);
                start = lastid;
                PageP.Add(P);
            }




            return PageP;




        }


        public bool InsertContent(nContent eContent) {
            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();

            nSqlParameterCollection P = new nSqlParameterCollection();

            string query = "INSERT INTO dbo.nfcms_Content"+
            "(Title"+
            ",Content"+
            ",CID"+
            ",SUBID"+
            ",CreatorPKID"+
            ",Locked"+
            ",SEOname"+
            ",ratingGroupID"+
            ",cDate"+
            ",MetaKeyWords"+
            ",MetaDescription"+
            ",PreviewText"+
            ",ContentType"+
            ",LongText"+
            ",ContentRef"+
            ",CreatorSpecialName)"+
            "VALUES"+
            "(@title"+
            ",''"+
            ",@cid"+
            ",@subid"+
            ",@upkid"+
            ",@locked"+
            ",@seo"+
            ",0"+
            ",@date"+
            ",@metakeyw"+
            ",@metadesc"+
            ",@previewtext"+
            ",@contenttype"+
            ",@longtext"+
            ",@ref"+
            ",@uname)";
            P.Add("@title", eContent.Title);
            P.Add("@cid", eContent.CategoryID);
            P.Add("@subid", eContent.SubCategoryID);
            P.Add("@upkid", eContent.CreatorPKID);
            P.Add("@locked", eContent.Locked);
            P.Add("@seo", eContent.SEOName);
            P.Add("@date", eContent.CreationDate);
            P.Add("@metakeyw", eContent.MetaKeyWords);
            P.Add("@metadesc", eContent.MetaDescription);
            P.Add("@previewtext", eContent.PreviewText);
            P.Add("@contenttype", eContent.ContentType);
            P.Add("@longtext", eContent.LongText);
            P.Add("@ref", eContent.ReferenceID);
            P.Add("@uname", eContent.CreatorSpecialName);
            try
            {
                Sql.SysNonQuery(query, P);





            }
            catch(Exception e) {
                HttpContext.Current.Response.Write(e.Message);
                Sql.SysDisconnect();
                return false;
            
            }
            Sql.SysDisconnect();
            return true;
        }

        public partial class GetContent
        {
            #region Attribute

            private string[] pContentIDs = new string[100];
            private string pOrderBy = "cDate";
            private string pOrderType = "DESC";
            private string pSEOName = "";
            private int pID = 0;
            private SqlParameter[] Para = new SqlParameter[100];
            private SqlParameter[] CountPara = new SqlParameter[100];
            private string query = "";
            private int limit = 0;
            private bool plocked = false;
            private object CName = null;
            private object SUBName = null;
            
            #endregion
            private int TotalRowsP = 0;

            public int TotalRows {

                get {

                    return this.TotalRowsP;
                
                
                }
            
            }


            private string query2 = "";
            public GetContent() { 
            
            
            
            }
            public void GetContentByTag(string tagName, string[] acontenttypes, string categoryname = null, string subname = null, string OrderBy = "{prefix}Content.cDate", string OrderType = "DESC", bool locked = false, int pageIndex = 0, int pageSize = 0, int contentRef = 0)
            {
                string l_ocked = "false";
                string contenttypes = "";
                OrderBy = OrderBy.Replace("{prefix}", new ThisApplication().getSqlPrefix);
                Pagination.nPage npage = new Pagination.nPage(pageIndex, pageSize);


                if (locked) l_ocked = "true";
                this.plocked = locked;
                this.pContentIDs = acontenttypes;

                this.pOrderBy = OrderBy.Replace("'", "").Replace("-", "");
                this.pOrderType = OrderType;
                this.CName = categoryname;
                this.SUBName = subname;

                //Fixing Bug -> Give array to SQL Command

                foreach (string str in acontenttypes)
                {

                    string str2 = str.Replace("'", "").Replace("-", "");
                    contenttypes += "'" + str2 + "',";

                }
                if (contenttypes.EndsWith(",")) contenttypes = contenttypes.Remove(contenttypes.LastIndexOf(","));


                ///TOTAL CONT
                ///
                this.query2 = " SELECT COUNT(*) AS rows " +
                "FROM   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content LEFT OUTER JOIN " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content ON(" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content.contentID = " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id)  LEFT OUTER JOIN " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags ON (" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags.id = " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content.tagID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.PKID) WHERE  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked=@locked AND " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags.TagName = @tagname ";
                this.query2 += " AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType IN(@ContentTypes) " + (this.CName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName = @c" : "") + " " + (this.SUBName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.shortName = @sc" : "") + " AND id>@id ORDER BY MAX(" + this.pOrderBy + ") " + this.pOrderType;
                this.query2 = this.query2.Replace("@ContentTypes", contenttypes);


                this.query = " SELECT  dense_rank() over (order by " + OrderBy + " " + OrderType + ") as rowNo," + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef," + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.Username, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.LongText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Title, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.PreviewText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SEOname, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.cDate, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ratingGroupID,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaKeyWords,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaDescription," +
            "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.longName,   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.ref, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorSpecialName,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.langCode," +
            "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.longName as subNameLong,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.shortName as subNameShort,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.langCode as subLang " +
            "FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.PKID) LEFT OUTER JOIN " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content ON(" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content.contentID = " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id)  LEFT OUTER JOIN " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags ON (" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags.id = " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags2Content.tagID)  LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.PKID) WHERE  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked=@locked AND " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags.TagName = @tagname ";
                this.query += " AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType IN(@ContentTypes) " + (this.CName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName = @c" : "") + " " + (this.SUBName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.shortName = @sc" : "");

                if (contentRef != 0) this.query += " AND " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef=" + contentRef;
                this.query = this.query.Replace("@ContentTypes", contenttypes);

                string countCMD = "with pagination as " +
"( " +
this.query +

")" +
" " +
"select " +

"    *, (select count(*) from pagination) as TotalRows " +
"from " +
"    pagination " +
(pageIndex != 0 && pageSize != 0 ? "where RowNo between @i and @ii  " : "") +
"order by " +
 "rowNo ";
                this.query = countCMD;
                int i = npage.RowStart;
                int ii = npage.RowEnd;

                this.Para = null;
                if (this.CName == null && this.SUBName == null)
                {
                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii),
                     new SqlParameter("@tagname", tagName)

                  };
                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii),
                      new SqlParameter("@tagname", tagName)
                    };

                }
                if (this.CName != null && this.SUBName == null)
                {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};


                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                      new SqlParameter("@tagname", tagName),
                     new SqlParameter("@ii",ii)};

                }
                if (this.CName == null && this.SUBName != null)
                {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    
                     new SqlParameter("@tagname", tagName),
                    new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};

                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@tagname", tagName),
                     new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};
                }
                if (this.CName != null && this.SUBName != null)
                {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@tagname", tagName),
                      new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)
                    };


                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@tagname", tagName),
                      new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)
                    };

                }
                try
                {
                    SqlHelper Sql = new SqlHelper();
                    Sql.SysConnect();



                    SqlDataReader C = Sql.SysReader(this.query, this.Para);
                    while (C.Read())
                    {
                        this.TotalRowsP = (int)C["TotalRows"];
                        nContent E = new nContent(
                            ((int)C["id"]),
                            (C["Title"] != DBNull.Value ? C["Title"].ToString() : null),
                            (C["ContentType"] != DBNull.Value ? C["ContentType"].ToString() : null),
                            (C["CID"] != DBNull.Value ? (object)C["CID"] : null),
                            (C["SUBID"] != DBNull.Value ? (object)C["SUBID"] : null),
                            (C["longName"] != DBNull.Value ? (string)C["longName"] : null),
                            (C["subNameLong"] != DBNull.Value ? (string)C["subNameLong"] : null),
                            (C["CreatorPKID"] != DBNull.Value ? (string)C["CreatorPKID"] : null),
                            (C["Username"] != DBNull.Value ? (string)C["Username"] : null),
                            (C["Locked"] != DBNull.Value ? Convert.ToBoolean((string)C["Locked"]) : false),
                            (C["SEOname"] != DBNull.Value ? (string)C["SEOname"] : null),
                            (C["ratingGroupID"] != DBNull.Value ? (int)C["ratingGroupID"] : 0),
                            (C["cDate"] != DBNull.Value ? (DateTime)C["cDate"] : DateTime.Now),
                            (C["MetaKeyWords"] != DBNull.Value ? (string)C["MetaKeyWords"] : null),
                            (C["MetaDescription"] != DBNull.Value ? (string)C["MetaDescription"] : null),
                            (C["PreviewText"] != DBNull.Value ? (string)C["PreviewText"] : null),
                            (C["LongText"] != DBNull.Value ? (string)C["LongText"] : null),
                            //[ContentRef]
                            (C["ContentRef"] != DBNull.Value ? (int)C["ContentRef"] : 0),
                             (C["CreatorSpecialName"] != DBNull.Value ? (string)C["CreatorSpecialName"] : null)
                           );


                        temp.Add(E);










                    }
                    C.Close();

                    HttpContext.Current.Response.Write(this.query);
                    Sql.SysDisconnect();
                }
                catch (Exception e)
                {

                    string str = "<span style=\"background-color:#FFF; color:#222\">ERROR IN nfCMS: <br>" +
                                 "There is a Problem with the counting. Check out the Exception and the query:<br>" +
                                 "<b>Exception:</b> " + e.Message +
                                 "<br> <b>Query:</b>" + this.query +
                                 "<br> <br><fieldset><legend>Stack Trace</legent>" + e.StackTrace + "</fieldset><hr/></span>";


                     HttpContext.Current.Response.Write(str);
                }
            }
          
            public GetContent(string[] acontenttypes, string categoryname = null, string subname = null, string OrderBy = "{prefix}Content.cDate", string OrderType = "DESC", bool locked = false, int pageIndex = 0, int pageSize=0,int contentRef=0)
            {
                string l_ocked = "false";
                string contenttypes = "";
                OrderBy = OrderBy.Replace("{prefix}", new ThisApplication().getSqlPrefix);
                Pagination.nPage npage = new Pagination.nPage(pageIndex, pageSize);
                
            
                if (locked) l_ocked = "true";
                this.plocked = locked;
                this.pContentIDs = acontenttypes;
             
                this.pOrderBy = OrderBy.Replace("'","").Replace("-","");
                this.pOrderType = OrderType;
                this.CName = categoryname;
                this.SUBName = subname;

                //Fixing Bug -> Give array to SQL Command

                foreach(string str in acontenttypes){
                
                string str2 = str.Replace("'","").Replace("-","");
                   contenttypes+="'"+ str2 +"',";
                
                }
                    if(contenttypes.EndsWith(","))contenttypes = contenttypes.Remove(contenttypes.LastIndexOf(","));


                ///TOTAL CONT
                ///
                    this.query2 = " SELECT COUNT(*) AS rows " +
                    "FROM   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content  LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.CID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.SUBID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Users ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.CreatorPKID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Users.PKID) WHERE  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.Locked=@locked ";
                    this.query2 += " AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.ContentType IN(@ContentTypes) " + (this.CName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Categories.shortName = @c" : "") + " " + (this.SUBName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.shortName = @sc" : "") + " AND id>@id ORDER BY MAX(" + this.pOrderBy + ") " + this.pOrderType;
                    this.query2 = this.query2.Replace("@ContentTypes", contenttypes);


                    this.query = " SELECT  dense_rank() over (order by " + OrderBy + " " + OrderType + ") as rowNo," + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef, ISNULL(" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.Username,'Unknown') as Username, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.LongText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Title, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.PreviewText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SEOname, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.cDate, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ratingGroupID,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaKeyWords,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaDescription," +
                "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.longName,   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.ref, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorSpecialName,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.langCode," +
                "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.longName as subNameLong,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.shortName as subNameShort,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.langCode as subLang " +
                "FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.CID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.SUBID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Sub_Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Users ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.CreatorPKID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Users.PKID) WHERE  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) +"Content.Locked=@locked ";
                    this.query += " AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType IN(@ContentTypes) " + (this.CName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName = @c" : "") + " " + (this.SUBName != null ? "AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.shortName = @sc" : "");

                    if (contentRef != 0) this.query += " AND " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef=" + contentRef;
                this.query = this.query.Replace("@ContentTypes",contenttypes);

                string countCMD = "with pagination as " +
"( " +
this.query+

")" +
" " +
"select " +

"    *, (select count(*) from pagination) as TotalRows " +
"from " +
"    pagination " +
(pageIndex!=0 && pageSize != 0 ? "where RowNo between @i and @ii  " :"") +
"order by " +
 "rowNo ";

        
                this.query = countCMD;
                int i = npage.RowStart;
                int ii = npage.RowEnd;

                this.Para = null;
                if (this.CName == null && this.SUBName == null)
                {
                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)

                  };
                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)
                    };

                }
                if (this.CName != null && this.SUBName == null) {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};


                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                     new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};
                
                }
                if (this.CName == null && this.SUBName != null)
                {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    
 
                    new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};

                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    
                     new SqlParameter("@sc", this.SUBName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)};
                }
                if (this.CName != null && this.SUBName != null)
                {

                    this.Para = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    new SqlParameter("@sc", this.SUBName),
                      new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)
                    };


                    this.CountPara = new SqlParameter[]{
                     new SqlParameter("@locked", l_ocked),
                    new SqlParameter("@sc", this.SUBName),
                      new SqlParameter("@c", this.CName),
                     new SqlParameter("@i",i),
                     new SqlParameter("@ii",ii)
                    };

                }
                try
                {
                    SqlHelper Sql = new SqlHelper();
                    Sql.SysConnect();



                    SqlDataReader C = Sql.SysReader(this.query, this.Para);
                    while (C.Read())
                    {
                        this.TotalRowsP = (int)C["TotalRows"];
                        nContent E = new nContent(
                            ((int)C["id"]),
                            (C["Title"] != DBNull.Value ? C["Title"].ToString() : null),
                            (C["ContentType"] != DBNull.Value ? C["ContentType"].ToString() : null),
                            (C["CID"] != DBNull.Value ? (object)C["CID"] : null),
                            (C["SUBID"] != DBNull.Value ? (object)C["SUBID"] : null),
                            (C["longName"] != DBNull.Value ? (string)C["longName"] : null),
                            (C["subNameLong"] != DBNull.Value ? (string)C["subNameLong"] : null),
                            (C["CreatorPKID"] != DBNull.Value ? (string)C["CreatorPKID"] : null),
                            (C["Username"] != DBNull.Value ? (string)C["Username"] : null),
                            (C["Locked"] != DBNull.Value ? Convert.ToBoolean((string)C["Locked"]) : false),
                            (C["SEOname"] != DBNull.Value ? (string)C["SEOname"] : null),
                            (C["ratingGroupID"] != DBNull.Value ? (int)C["ratingGroupID"] : 0),
                            (C["cDate"] != DBNull.Value ? (DateTime)C["cDate"] : DateTime.Now),
                            (C["MetaKeyWords"] != DBNull.Value ? (string)C["MetaKeyWords"] : null),
                            (C["MetaDescription"] != DBNull.Value ? (string)C["MetaDescription"] : null),
                            (C["PreviewText"] != DBNull.Value ? (string)C["PreviewText"] : null),
                            (C["LongText"] != DBNull.Value ? (string)C["LongText"] : null),
                            //[ContentRef]
                            (C["ContentRef"] != DBNull.Value ? (int)C["ContentRef"] : 0),
                             (C["CreatorSpecialName"] != DBNull.Value ? (string)C["CreatorSpecialName"] : null)
                           );


                        temp.Add(E);










                    }
                    C.Close();
                    Sql.SysDisconnect();
                
               
                 }
                catch (Exception e) {

                    string str = "<span style=\"background-color:#FFF; color:#222\">ERROR IN nfCMS: <br>" +
                                 "There is a Problem with the counting. Check out the Exception and the query:<br>" +
                                 "<b>Exception:</b> " + e.Message +
                                 "<br> <b>Query:</b>" + this.query +
                                 "<br> <br><fieldset><legend>Stack Trace</legent>"+ e.StackTrace +"</fieldset><hr/></span>";

         HttpContext.Current.Response.Write(str);                
                }

                string debugquery = this.query;
                foreach (SqlParameter P in Para)
                {
                    debugquery.Replace(P.ParameterName.ToString(), "'" + P.Value.ToString() + "'");
                }

           

            }
            public string debugGetQuery() {

                return this.query;
            
            }

            private
                List<nContent> temp = new List<nContent>();
 
            public GetContent(int id, bool locked = false, int contentRef = 0)
            {
                try
                {
                    string l_ocked = "false";
                    if (locked) l_ocked = "true";
                    this.plocked = locked;
                    this.pID = id;

                    this.query = " SELECT   ISNULL(" + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.Username,'Unknown') as Username, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorSpecialName ," + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID," + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentType, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.LongText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Title, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.PreviewText, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SEOname, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.cDate, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ratingGroupID, " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaKeyWords,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.MetaDescription, " +
                    "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.longName,   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.shortName,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.ref,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.langCode," +
                    "   " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.longName as subNameLong,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.shortName as subNameShort,  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.langCode as subLang " +
                    "FROM  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.SUBID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Sub_Categories.PKID) LEFT OUTER JOIN  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users ON( " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.CreatorPKID =  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Users.PKID) WHERE  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.Locked=@locked ";
                    this.query += " AND  " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.id=@ids ORDER BY " + this.pOrderBy + " " + this.pOrderType;
                    if (contentRef != 0) this.query += " AND " + (new nfCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content.ContentRef=" + contentRef;

                    this.Para = new SqlParameter[]{
              new SqlParameter("@ids", this.pID),
                this.Para[1] = new SqlParameter("@locked", l_ocked)
            };

                    SqlHelper Sql = new SqlHelper();
                    Sql.SysConnect();



                    SqlDataReader C = Sql.SysReader(this.query, this.Para);
                    while (C.Read())
                    {

                        nContent E = new nContent(
                            ((int)C["id"]),

                            (C["Title"] != DBNull.Value ? C["Title"].ToString() : null),
                            (C["ContentType"] != DBNull.Value ? C["ContentType"].ToString() : null),
                            (C["CID"] != DBNull.Value ? (object)C["CID"] : null),
                            (C["SUBID"] != DBNull.Value ? (object)C["SUBID"] : null),
                            (C["longName"] != DBNull.Value ? (string)C["longName"] : null),
                            (C["subNameLong"] != DBNull.Value ? (string)C["subNameLong"] : null),
                            (C["CreatorPKID"] != DBNull.Value ? (string)C["CreatorPKID"] : null),
                            (C["Username"] != DBNull.Value ? (string)C["Username"] : null),
                            (C["Locked"] != DBNull.Value ? Convert.ToBoolean((string)C["Locked"]) : false),
                            (C["SEOname"] != DBNull.Value ? (string)C["SEOname"] : null),
                            (C["ratingGroupID"] != DBNull.Value ? (int)C["ratingGroupID"] : 0),
                            (C["cDate"] != DBNull.Value ? (DateTime)C["cDate"] : DateTime.Now),
                            (C["MetaKeyWords"] != DBNull.Value ? (string)C["MetaKeyWords"] : null),
                            (C["MetaDescription"] != DBNull.Value ? (string)C["MetaDescription"] : null),
                            (C["PreviewText"] != DBNull.Value ? (string)C["PreviewText"] : null),
                            (C["LongText"] != DBNull.Value ? (string)C["LongText"] : null),
                              (C["ContentRef"] != DBNull.Value ? (int)C["ContentRef"] : 0),
                            (C["CreatorSpecialName"] != DBNull.Value ? (string)C["CreatorSpecialName"] : null)
                           
                            
                            );


                        temp.Add(E);










                    }
                    C.Close();
                    Sql.SysDisconnect();
                }
                catch (Exception e) {

                    HttpContext.Current.Response.Write(e.Message);
                
                }
            }

            
            public List<nContent> getList() {
                
               
                return temp;
            }

        }


    }
}