namespace Ren.CMS.Models.Backend.Content
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.MemberShip;
    using Ren.CMS.nModules.Helper;
using System.Web;

    public class AddAttachmentModel
    {
        [Required]
        public int ContentId { get; set; }

        [LocaleDisplayName("LANG_BACKEND_CONTENT_ATTACHMENT_PHYSICAL", "BACKEND", "__USER__")]
        public bool Physical { get; set; }

        [RequiredIf("Physical", true)]
        [LocaleDisplayName("LANG_BACKEND_CONTENT_ATTACHMENT_FILE", "BACKEND", "__USER__")]
        public HttpPostedFileBase File { get; set; }

        [RequiredIf("Physical", false)]
        [LocaleDisplayName("LANG_BACKEND_CONTENT_ATTACHMENT_URL", "BACKEND", "__USER__")]
        public string Url { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int ArgumentId { get; set; }

        
    }

    public class CategoryModel
    {
        #region Properties

        [Required]
        public string contentType
        {
            get; set;
        }

        public object ID
        {
            get;
            set;
        }

        [Required]
        public string longName
        {
            get; set;
        }

        [Required]
        public string shortName
        {
            get; set;
        }

        public string subFrom
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public List<object> CategoryList()
        {
            SqlHelper SQL = new SqlHelper();
               nSqlParameterCollection Col =  new nSqlParameterCollection();

            string query = "SELECT * FROM " + new ThisApplication().getSqlPrefix + "Categories";
            if(!String.IsNullOrEmpty(this.contentType))
            {

                query = "SELECT * FROM " + new ThisApplication().getSqlPrefix + "Categories WHERE contentType=@ct";
                Col.Add("@ct", this.contentType);
            }
            List<object> _list = new List<object>();
            SQL.SysConnect();
            SqlDataReader R = SQL.SysReader(query, Col );
            if (R.HasRows)
            {
                _list.Add(new { id ="", shortName = "Keine Kategorie", longName = "", subFrom = "", contentType = "" });

                while (R.Read())
                {

                    _list.Add(new { id = (object)R["PKID"], shortName = (string)R["shortName"], longName = (string)R["longName"], subFrom = (R["subFrom"] != DBNull.Value ? (string)R["subFrom"] : "") , contentType = (string)R["contentType"]});

                }

            }
            R.Close();
            SQL.SysDisconnect();

            return _list;
        }

        #endregion Methods
    }

    public class ContentTagFormDialog
    {
        #region Properties

        public string elID
        {
            get; set;
        }

        public string gridID
        {
            get; set;
        }

        public string method
        {
            get; set;
        }

        public string title
        {
            get; set;
        }

        public string url
        {
            get; set;
        }

        #endregion Properties
    }

    public class ContentTypes
    {
        #region Fields

        private List<object> ctList = new List<object>();

        #endregion Fields

        #region Constructors

        public ContentTypes()
        {
            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language("__USER__", "CONTENT_TYPES");

            SqlHelper SQL = new SqlHelper();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;

            string query = "SELECT * FROM " + prefix + "Content_Types";

            SQL.SysConnect();

            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection());

            if (R.HasRows)
            {
                while (R.Read())
                {
                    string ctLangLine = Lang.getLine("LANG_CTYPE_"+ ((string)R["name"]).ToUpper());

                    if(String.IsNullOrEmpty(ctLangLine)) ctLangLine = (string)R["name"];
                    this.ctList.Add(new
                    {
                        ctype= (string)R["name"],
                        name = ctLangLine,
                        controller = (string)R["controller"],
                        actionpath = (string)R["actionpath"]

                    });
                }

            }

            R.Close();

            SQL.SysDisconnect();
        }

        #endregion Constructors

        #region Methods

        public List<object> ObjectList()
        {
            return this.ctList;
        }

        #endregion Methods
    }

    public class vAttachmentRemark
    {
        public int RemarkTypeId { get; set; }
        public string RemarkText { get; set; }
        public int RemarkId { get; set; }
    }

    public class EditAttachment
    {
        #region Properties

        public string id
        {
            get; set;
        }

        public List<vAttachmentRemark> Remarks { get; set; }

        public int RoleId { get; set; }

        public int ArgumentId { get; set; }

        public List<Ren.CMS.Content.nContentAttachmentTexts> Texts { get; set; }
        #endregion Properties
    }

    public class jsTreeCategoryModel
    {
        #region Properties

        [Required]
        public string ContentType
        {
            get; set;
        }

        public string node_id
        {
            get; set;
        }

        [Required]
        public string operation
        {
            get; set;
        }

        #endregion Properties
    }

    public class MngContentTag
    {
        #region Properties

        [Required]
        public string contentType
        {
            get; set;
        }

        public int enableBrowsing
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        [Required]
        public string tagName
        {
            get; set;
        }

        #endregion Properties
    }

    public class TreeViewCategory
    {
        #region Properties

        [Required]
        public string ContentType
        {
            get; set;
        }

        public string ct
        {
            get; set;
        }

        public string excludePKID
        {
            get; set;
        }

        public string node_id
        {
            get; set;
        }

        [Required]
        public string operation
        {
            get; set;
        }

        public string Selector
        {
            get; set;
        }

        #endregion Properties
    }

    public class TreeViewCatMover
    {
        #region Properties

        [Required]
        public string id
        {
            get; set;
        }

        public string parent
        {
            get; set;
        }

        #endregion Properties
    }

    public class ValidateSEOModel
    {
        #region Properties

        [Required]
        public string title
        {
            get; set;
        }

        #endregion Properties
    }
}