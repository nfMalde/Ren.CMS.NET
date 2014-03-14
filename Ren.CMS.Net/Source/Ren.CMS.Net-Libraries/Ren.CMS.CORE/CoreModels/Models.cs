namespace Ren.CMS.Models.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Links;

    public class BackendShortcut
    {
        #region Properties

        public string action
        {
            get; set;
        }

        public string IconCls
        {
            get; set;
        }

        [Required]
        public int id
        {
            get; set;
        }

        [Required]
        public string PosX
        {
            get; set;
        }

        [Required]
        public string PosY
        {
            get; set;
        }

        public string ShortCutText
        {
            get; set;
        }

        #endregion Properties
    }

    public class FlexyGridPostParameters
    {
        #region Properties

        [Required]
        public int page
        {
            get; set;
        }

        public string qtype
        {
            get; set;
        }

        public string query
        {
            get; set;
        }

        [Required]
        public int rp
        {
            get; set;
        }

        [Required]
        public string sortname
        {
            get; set;
        }

        [Required]
        public string sortorder
        {
            get; set;
        }

        #endregion Properties
    }

    public class MenuModel
    {
        #region Properties

        [Required]
        public LinkCollection Links
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public string getCorrectCSSClass(ControllerContext CTT, Link Lnk)
        {
            if (isLinkAcitve(CTT, Lnk))
            {

                return Lnk.HoverStateClass;

            }
            else
            {

                return Lnk.NormalStateClass;

            }
        }

        public bool isLinkAcitve(ControllerContext CTT, Link Lnk)
        {
            ControllerContext CT = CTT;

            string currentC = CT.RouteData.GetRequiredString("controller").ToLower();
            string currentA = CT.RouteData.GetRequiredString("action").ToLower();
            bool isok = false;
            //Stripping Action
            if (currentC == Lnk.TargetController.ToLower()) isok = true;
            else isok = false;

            if (!isok) return isok;
            if (String.IsNullOrEmpty(Lnk.TargetAction) || String.IsNullOrWhiteSpace(Lnk.TargetAction) || Lnk.TargetAction == "*")
            {

                isok = true;

            }

            else
            {

                if (currentA == Lnk.TargetAction.ToLower())
                {

                    isok = true;

                }
                else
                {

                    isok = false;

                }

            }

            return isok;
        }

        #endregion Methods
    }

    public class nContentPostModel
    {
        #region Properties

        /// <summary>
        /// Returns the Category object
        /// </summary>
        /// 
        [Required]
        public string CategoryID
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        /// 
        [Required]
        public string ContentType
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the Username of the Creator
        /// </summary>
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the Creators PK ID
        /// </summary>
        ///
      
        public string CreatorPKID
        {
            get;
            set;
        }

        public string CreatorSpecialName
        {
            get; set;
        }

        //Eigenschaften
        /// <summary>
        /// Returns the ID of the Content
        /// </summary>
        /// 
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        ///  
        public bool Locked
        {
            get;
            set;
        }

        public int ReferenceID
        {
            get;
            set;
        }

        public int[] Tags
        {
            get; set;
        }

        [Required]
        public IEnumerable<nContentPostModelText> Texts
        {
            get; set;
        }

        #endregion Properties
    }

    public class nContentPostModelText
    {
        #region Properties

        public bool Active
        {
            get; set;
        }

        public string LangCode
        {
            get; set;
        }

        /// <summary>
        /// Returns the Content Long Text
        /// </summary>
        ///
        [Required]
        [UIHint("tinymce_jquery_full"),
        AllowHtml]
        public string LongText
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the MetaDescription
        /// </summary>
        /// 
        [MaxLength(250)]
        public string MetaDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the COMMA SEPERATED Meta-Keywords
        /// </summary>
        /// 
        public string MetaKeyWords
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the PreviewText
        /// </summary>
        /// 
       
        [MaxLength(400)]
        public string PreviewText
        {
            get;
            set;
        }

        public string SEOName
        {
            get; set;
        }

        public int TextID
        {
            get; set;
        }

        /// <summary>
        /// Returns the Title of the Content
        /// </summary>
        /// 
        [Required]
        public string Title
        {
            get;
            set;
        }

        #endregion Properties
    }

    public class nContentTextBinder
    {
        #region Properties

        public List<bool> Active
        {
            get; set;
        }

        public List<string> LangCode
        {
            get; set;
        }

        public List<string> LongText
        {
            get; set;
        }

        public List<string> MetaDescription
        {
            get; set;
        }

        public List<string> MetaKeyWords
        {
            get; set;
        }

        public List<string> PreviewText
        {
            get; set;
        }

        public List<string> SEOName
        {
            get; set;
        }

        public List<int> TextID
        {
            get; set;
        }

        public List<string> Title
        {
            get; set;
        }

        #endregion Properties

        #region Methods
 

        #endregion Methods
    }

    public class SendMailModel
    {
        #region Properties

        public string Body
        {
            get; set;
        }

        public int MailID
        {
            get; set;
        }

        public string Receiptient
        {
            get; set;
        }

        public string Subject
        {
            get; set;
        }

        #endregion Properties
    }
}