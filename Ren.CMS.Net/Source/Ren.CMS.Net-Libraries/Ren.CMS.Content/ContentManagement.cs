namespace Ren.CMS.Content
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using NHibernate.Criterion;

    using Ren.CMS.CORE.FileManagement;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.Persistence;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;
    using Ren.CMS.Persistence.Repositories;

    public class ContentManagement
    {
        #region Methods

        public bool bindTagsToContent(int contentID, int[] tagsIds)
        {
            SqlHelper SQL = new SqlHelper();

            ThisApplication TA = new ThisApplication();

            string prefix = TA.getSqlPrefix;
            SQL.SysConnect();
            foreach (int tagID in tagsIds)
            {
                string query = "DELETE " + prefix + "Content_Tags2Content WHERE contentID=@cid AND tagID = @tid";
                nSqlParameterCollection PCOLDEL = new nSqlParameterCollection();

                PCOLDEL.Add("@cid", contentID);
                PCOLDEL.Add("@tid", tagID);

                SQL.SysNonQuery(query, PCOLDEL);

                string query2 = "INSERT INTO " + prefix + "Content_Tags2Content (contentID, tagID) VALUES(@cid,@tid)";
                nSqlParameterCollection PCOLINS = new nSqlParameterCollection();

                PCOLINS.Add("@cid", contentID);
                PCOLINS.Add("@tid", tagID);
                SQL.SysNonQuery(query2, PCOLINS);
            }

            SQL.SysDisconnect();
            return true;
        }

        public bool DeleteAttachment(string ID)
        {
            ContentAttachmentRepository Arepo = new ContentAttachmentRepository();

            var A = Arepo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachment>(c => c.Pkid == Guid.Parse(ID)));

            //Is Filemanagement Used?
            try
            {
                FileManagement FM = new FileManagement();

                if (FM.getFile(A.FName, false).id > 0)
                {
                    FM.DeleteFile(A.FName);
                }
                else
                {
                    //Maybe no fileManagement was used

                    string pathC = System.IO.Path.Combine(A.FPath, A.FName);

                    while (pathC.Contains("//") || pathC.Contains("\\\\"))
                    {
                        pathC = pathC.Replace("//", "/");
                        pathC = pathC.Replace("\\\\", "\\");
                    };

                    Uri PathCheck = new Uri(pathC);

                    if (PathCheck.IsFile)
                    {
                        string mappath = pathC;
                        //Try to get Mappath
                        try
                        {

                            mappath = HttpContext.Current.Server.MapPath(pathC);

                        }
                        catch (Exception)
                        {
                            //We got an error
                            //

                        }

                        if (System.IO.File.Exists(mappath))
                            System.IO.File.Delete(mappath);

                    }

                }
                //Otherwhise its a URL
            }
            catch (Exception) { }

                    Arepo.Delete(A);

                    return true;
        }

        public bool DeleteContent(int id)
        {
            ContentRepository Repo = new ContentRepository();

            var entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == id));

            if (entity == null || entity.Id < 1)
                return false;

            ContentAttachmentRepository ARepo = new ContentAttachmentRepository();

            var attachs = ARepo.GetMany(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.Nid == entity.Id));
            attachs.ToList().ForEach(e => this.DeleteAttachment(e.Pkid.ToString()));

            var refconts = Repo.GetMany(NHibernate.Criterion.Expression.Where<TContent>(e => e.ContentRef == entity.Id));

            ContentTags2ContentRepository RepoT = new ContentTags2ContentRepository();

            var t2c = RepoT.GetMany(NHibernate.Criterion.Expression.Where<ContentTags2Content>(e => e.ContentID == id));

            t2c.ToList().ForEach(t => RepoT.Delete(t));

            if (refconts.Count() > 0)
            {
                refconts.ToList().ForEach(c => DeleteContent(c.Id));
            }

            Repo.Delete(entity);

            return true;
        }

        public List<PageInitializier> GetPages(int MSSQLTotalRows, int MSSQLstart = 1212, int MSSQLincrement = 1111, int MSSQLlimit = 10, string order = "DESC")
        {
            List<PageInitializier> PP = new List<PageInitializier>();
            int increment = MSSQLincrement;
            int start = MSSQLstart;
            if (order == "DESC")
                start = (MSSQLTotalRows * increment) - (MSSQLlimit * increment);

            int p = 1;

            int lim = MSSQLlimit;

            List<PageInitializier> PageP = new List<PageInitializier>();
            int pages = MSSQLTotalRows / MSSQLlimit;
            for (p = 1; p <= pages; p++)
            {

                int lastid = (order == "DESC" ? start - (increment * lim) : start + (increment * lim));

                PageInitializier P = new PageInitializier(p, start);
                start = lastid;
                PageP.Add(P);
            }

            return PageP;
        }

        public bool InsertContent(nContent eContent)
        {
            bool r = this.InsertContent(ref eContent);

            return r;
        }

        public bool InsertContent(ref nContent eContent)
        {
            try
            {
            ContentRepository Repo = new ContentRepository();
            TContent M = new TContent();
            M.Cid = Guid.Parse(eContent.CategoryID.ToString());
              //  M.Category = new BaseRepository<Category>().GetOne(expression: NHibernate.Criterion.Expression.Where<Category>(c => c.Pkid == M.Cid)) ?? new Category();

            M.CDate = eContent.CreationDate;
            M.ContentRef = eContent.ReferenceID;
            M.ContentType = eContent.ContentType;
            M.CreatorPKID = Guid.Parse(eContent.CreatorPKID.ToString());
            M.CreatorSpecialName = eContent.CreatorSpecialName;

            M.Locked = eContent.Locked;
            M.Texts = new List<ContentText>();
            M.RatingGroupID = 0;
            List<ContentText> Tx = new List<ContentText>();
            foreach(nContentText t in eContent.Texts)
            {

                Tx.Add(
                    new ContentText()
                    {
                        Title = t.Title,
                        Seoname = t.SEOName,
                        LangCode = t.LangCode,
                        LongText = t.LongText,
                        PreviewText = t.PreviewText,
                        MetaDescription = t.MetaDescription,
                        MetaKeyWords = t.MetaKeyWords

                    });

            }

            M.Texts = Tx;

               object id = Repo.AddAndGetId(M);

              int AddedID = 0;

            if(!int.TryParse(id.ToString(), out AddedID))
                {

                    throw new Exception("Got invalid id back from SQL Server. Statement was maybe successfull but Server was unable to return an ID!");

                }

            var c  = new GetContent(id: AddedID, locked: true).getList();

            if (c != null && c.Count > 0)
            {

                eContent = c.FirstOrDefault();
                return true;
            }

            }
            catch (Exception e)
            {

                return false;

            }

            return false;
        }

        public bool UpdateContent(nContent eContent)
        {
            //Build Content Entity

            ContentRepository Repo = new ContentRepository();

            var pContent = Repo.GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == eContent.ID));
            if (pContent != null && pContent.Id > 0)
            {
                var texts = pContent.Texts;
                List<ContentText> TX = new List<ContentText>();

                foreach (ContentText text in pContent.Texts)
                {
                    if (eContent.Texts.Any(e => e.Id == text.Id))
                    {
                        var etexts = eContent.Texts.Where(e => e.Id == text.Id).FirstOrDefault();
                        text.LangCode = etexts.LangCode;
                        text.LongText = etexts.LongText;
                        text.MetaDescription = etexts.MetaDescription;
                        text.PreviewText = etexts.PreviewText;
                        text.Seoname = etexts.SEOName;
                        text.Title = etexts.Title;
                        TX.Add(text);

                    }
                    continue;

                }

                pContent.Texts = TX;
                pContent.Locked = eContent.Locked;
                pContent.RatingGroupID = eContent.RatingGroupID;
                pContent.CDate = eContent.CreationDate;
                pContent.Cid = Guid.Parse(eContent.CategoryID.ToString());
                pContent.ContentRef = eContent.ReferenceID;
                pContent.ContentType = eContent.ContentType;
                pContent.CreatorPKID = Guid.Parse(eContent.CreatorPKID.ToString());
                pContent.CreatorSpecialName = eContent.CreatorSpecialName;

                Repo.Update(pContent);

                return true;

            }

            return false;
        }

        #endregion Methods

        #region Nested Types

        public partial class GetContent
        {
            #region Fields

            private object CName = null;
            private SqlParameter[] CountPara = new SqlParameter[100];
            private int limit = 0;
            private SqlParameter[] Para = new SqlParameter[100];
            private string[] pContentIDs = new string[100];
            private int pID = 0;
            private bool plocked = false;
            private string pOrderBy = "cDate";
            private string pOrderType = "DESC";
            private string pSEOName = "";
            private string query = "";
            private string query2 = "";
            private object SUBName = null;
            private List<nContent> temp = new List<nContent>();
            private int TotalRowsP = 0;

            #endregion Fields

            #region Constructors

            public GetContent()
            {
            }

            public GetContent(string[] acontenttypes, string categoryname = null, string OrderBy = "cDate", string OrderType = "DESC", bool locked = false, int pageIndex = 0, int pageSize = 0, int contentRef = 0, bool countReferences = false, string[] referenceContentTypes = null)
            {
                OrderBy = OrderBy.Replace("{prefix}Content.", "");

                ContentRepository ContentRepo = new ContentRepository();
                int total = 0;
                var results = (!locked ? ContentRepo.Pagination(ref total,
                    pageSize, pageIndex,
                    acontenttypes,
                    category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,

                    orderBy: OrderBy,
                    orderType: OrderType) : ContentRepo.Pagination(ref total,
                    pageSize, pageIndex,
                    acontenttypes,
                    category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,
                    locked: locked,
                    orderBy: OrderBy,
                    orderType: OrderType));

                TotalRowsP = total;
                foreach (TContent c in results)
                {

                    BaseRepository<Category> category = new BaseRepository<Category>();
                    BaseRepository<User> usr = new BaseRepository<User>();

                    List<nContentText> t = new List<nContentText>();

                    c.Texts.ToList().ForEach(e =>

                        t.Add(new nContentText()
                        {

                            Id = e.Id,
                            LangCode = e.LangCode,
                            LongText = e.LongText,
                            PreviewText = e.PreviewText,
                            MetaKeyWords = e.MetaKeyWords,
                            MetaDescription = e.MetaDescription,
                            SEOName = e.Seoname,
                            Title = e.Title

                        }));

                    temp.Add(new nContent(c.Id,
                        t,

                        c.ContentType,
                        c.Cid,
                        c.Category.LongName,
                        c.CreatorPKID,
                        c.User.Username,
                        c.Locked,

                        0,
                        (DateTime)c.CDate,

                        (int)c.ContentRef,
                        c.CreatorSpecialName));

                }
            }

            public GetContent(string[] acontenttypes, string[] languages, string categoryname = null, string OrderBy = "cDate", string OrderType = "DESC", bool locked = false, int pageIndex = 0, int pageSize = 0, int contentRef = 0, bool countReferences = false, string[] referenceContentTypes = null)
            {
                OrderBy = OrderBy.Replace("{prefix}Content.", "");

                ContentRepository ContentRepo = new ContentRepository();
                int total = 0;
                var results = (!locked ? ContentRepo.Pagination(ref total,
                    pageSize, pageIndex,
                    acontenttypes,
                    category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,

                    orderBy: OrderBy,
                    orderType: OrderType, languages: languages) : ContentRepo.Pagination(ref total,
                    pageSize, pageIndex,
                    acontenttypes,
                    category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,
                    locked: locked,
                    orderBy: OrderBy,
                    orderType: OrderType, languages: languages));

                TotalRowsP = total;
                foreach (TContent c in results)
                {

                    BaseRepository<Category> category = new BaseRepository<Category>();
                    BaseRepository<User> usr = new BaseRepository<User>();

                    List<nContentText> t = new List<nContentText>();

                    c.Texts.ToList().ForEach(e =>

                        t.Add(new nContentText()
                        {

                            Id = e.Id,
                            LangCode = e.LangCode,
                            LongText = e.LongText,
                            PreviewText = e.PreviewText,
                            MetaKeyWords = e.MetaKeyWords,
                            MetaDescription = e.MetaDescription,
                            SEOName = e.Seoname,
                            Title = e.Title

                        }));

                    temp.Add(new nContent(c.Id,
                        t,

                        c.ContentType,
                        c.Cid,
                        c.Category.LongName,
                        c.CreatorPKID,
                        c.User.Username,
                        c.Locked,

                        0,
                        (DateTime)c.CDate,

                        (int)c.ContentRef,
                        c.CreatorSpecialName));

                }
            }

            public GetContent(int id, bool locked = false, int contentRef = 0)
            {
                ContentRepository Repo = new ContentRepository();

                var c = (!locked ?
                    Repo.GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == id && e.Locked == locked && e.ContentRef == contentRef))
                    :
                    Repo.GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == id && e.ContentRef == contentRef)))
                    ;
                List<nContentText> t = new List<nContentText>();

                c.Texts.ToList().ForEach(e =>

                    t.Add(new nContentText()
                    {

                        Id = e.Id,
                        LangCode = e.LangCode,
                        LongText = e.LongText,
                        PreviewText = e.PreviewText,
                        MetaKeyWords = e.MetaKeyWords,
                        MetaDescription = e.MetaDescription,
                        SEOName = e.Seoname,
                        Title = e.Title

                    }));
                temp.Add(new nContent(
                       id: c.Id,
                        contentTexts: t,
                       type: c.ContentType,
                       cid: c.Cid.ToString(),
                       category: c.Category.LongName,
                       creatorPKid: c.CreatorPKID,
                       username: c.User.Username,
                       locked: c.Locked,

                       ratinggroupid: 0,
                       cdate: (c.CDate != null ? (DateTime)c.CDate : new DateTime()),

                       CreatorSpecialName_: c.CreatorSpecialName,

                       Cref: (c.ContentRef != null ? (int)c.ContentRef: 0)));
            }

            #endregion Constructors

            #region Properties

            public int TotalRows
            {
                get
                {

                    return this.TotalRowsP;

                }
            }

            #endregion Properties

            #region Methods

            public string debugGetQuery()
            {
                return this.query;
            }

            public void GetContentByTag(string tagName, string[] acontenttypes, string categoryname = null, string OrderBy = "cDate", string OrderType = "DESC", bool locked = false, int pageIndex = 0, int pageSize = 0, int contentRef = 0)
            {
                ContentTagRepository Tagx = new ContentTagRepository();
                var xTag = Tagx.GetTagByName(tagName);

                ContentRepository Repo = new ContentRepository();
                int total = 0;
                var contents = (!locked ? Repo.Pagination(
                   ref total,
                   pageSize: pageSize,
                   pageIndex: pageIndex,
                   contentTypes: acontenttypes,
                   category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,
                   orderBy: OrderBy,
                   orderType: OrderType,
                   Tag: xTag,
                   locked: locked) :
                   Repo.Pagination(
                   ref total,
                   pageSize: pageSize,
                   pageIndex: pageIndex,
                   contentTypes: acontenttypes,
                   category: new BaseRepository<Category>().GetOne(Expression.Where<Category>(e => e.LongName == categoryname)) ?? null,
                   orderBy: OrderBy,
                   orderType: OrderType,
                   Tag: xTag));
                TotalRowsP = total;

                foreach (TContent c in contents.ToList<TContent>())
                {

                    List<nContentText> t = new List<nContentText>();

                    c.Texts.ToList().ForEach(e =>

                        t.Add( new nContentText() {

                        Id = e.Id,
                        LangCode = e.LangCode,
                        LongText = e.LongText,
                        PreviewText = e.PreviewText,
                        MetaKeyWords = e.MetaKeyWords,
                        MetaDescription = e.MetaDescription,
                        SEOName = e.Seoname,
                        Title = e.Title

                        }));

                    temp.Add(new nContent(
                        id: c.Id,
                        contentTexts: t,

                        type: c.ContentType,
                        cid: c.Cid.ToString(),
                        category: c.Category.LongName,
                        creatorPKid: c.CreatorPKID,
                        username: c.User.Username,
                        locked: c.Locked,

                        ratinggroupid: 0,
                        cdate: (DateTime)c.CDate,

                        CreatorSpecialName_: c.CreatorSpecialName,

                        Cref: (int)c.ContentRef));

                }
            }

            public List<nContent> getList()
            {
                return temp;
            }

            #endregion Methods
        }

        public partial class PageInitializier
        {
            #region Fields

            private int index = 0;
            private int startid = 0;

            #endregion Fields

            #region Constructors

            public PageInitializier(int i, int s)
            {
                this.index = i;
                this.startid = s - 1;
            }

            #endregion Constructors

            #region Properties

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

            #endregion Properties
        }

        public partial class TagManagement
        {
            #region Methods

            public List<nContentTag> GetAllTags(string contentType = null, int page = 1, int pageSize = 0, string OrderBy = "tagName", string OrderType = "ASC")
            {
                SqlHelper SQL = new SqlHelper();

                SQL.SysConnect();

                ThisApplication TA = new ThisApplication();

                OrderBy = OrderBy.Replace("'", "");
                OrderBy = OrderBy.Replace("-", "");

                OrderType = OrderType.Replace("'", "");
                OrderType = OrderType.Replace("-", "");

                if (String.IsNullOrWhiteSpace(OrderBy) || String.IsNullOrEmpty(OrderBy))
                {

                    OrderBy = "tagName";

                }

                if (String.IsNullOrWhiteSpace(OrderType) || String.IsNullOrEmpty(OrderType))
                {

                    OrderType = "tagName";

                }
                string prefix = TA.getSqlPrefix;

                string query = "";
                nSqlParameterCollection PCOL = new nSqlParameterCollection();
                if (contentType == null)
                {

                    query = "SELECT dense_rank() over (order by " + OrderBy + " " + OrderType + ") as rowNo,id, contentType,tagName,enableBrowsing,tagNameSEO FROM " + prefix + "Content_Tags";

                }
                else
                {
                    query = "SELECT dense_rank() over (order by " + OrderBy + " " + OrderType + ") as rowNo,id, contentType,tagName,enableBrowsing,tagNameSEO FROM " + prefix + "Content_Tags WHERE contentType = @ct";
                    PCOL.Add("@ct", contentType);

                }
                string finalquery = "WITH pagination as ( ";
                finalquery += query;
                finalquery += ")";

                if (page < 1) page = 1;

                finalquery += " ";
                if (pageSize == 0)
                {
                    finalquery += "SELECT * FROM pagination ORDER BY rowNo";
                }
                else
                {
                    int start = (page - 1 * pageSize);
                    int end = (page * pageSize);
                    PCOL.Add("@i", start);
                    PCOL.Add("@ii", end);

                    finalquery += "SELECT * FROM pagination WHERE rowNo BETWEEN @i AND @ii ORDER BY rowNo";

                }

                SqlDataReader Tags = SQL.SysReader(finalquery, PCOL);

                List<nContentTag> _list = new List<nContentTag>();

                if (Tags.HasRows)
                {

                    while (Tags.Read())
                    {

                        bool browse = ((int)Tags["enableBrowsing"] == 1 ? true : false);
                        _list.Add(new nContentTag((int)Tags["id"], (string)Tags["tagName"], (string)Tags["contentType"], (string)Tags["tagNameSEO"], browse));

                    }

                }

                Tags.Close();

                SQL.SysDisconnect();
                return _list;
            }

            public int getTotalRows(string contentType)
            {
                SqlHelper SQL = new SqlHelper();

                SQL.SysConnect();

                ThisApplication TA = new ThisApplication();

                string prefix = TA.getSqlPrefix;

                string query = "";
                nSqlParameterCollection PCOL = new nSqlParameterCollection();
                if (contentType == null)
                {

                    query = "SELECT COUNT(*) as xsum FROM " + prefix + "Content_Tags ORDER BY tagName ASC";

                }
                else
                {
                    query = "SELECT COUNT(*) as xsum FROM " + prefix + "Content_Tags WHERE contentType = @ct ORDER BY tagName ASC";
                    PCOL.Add("@ct", contentType);

                }

                SqlDataReader Tags = SQL.SysReader(query, PCOL);

                int count = 0;

                if (Tags.HasRows)
                {

                    while (Tags.Read())
                    {

                        count = (int)Tags["xsum"];

                    }

                }

                Tags.Close();
                SQL.SysDisconnect();

                return count;
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}