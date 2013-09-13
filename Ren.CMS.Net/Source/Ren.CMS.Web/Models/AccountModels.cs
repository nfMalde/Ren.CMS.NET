namespace Ren.CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.MemberShip;

    public class ChangeAvatar
    {
        #region Properties

        public string MaxHeightPixel
        {
            get {

               return this.nMaxHeight.toInt() + "px";

            }
        }

        public string MaxWidthPixel
        {
            get
            {

                return this.nMaxWidth.toInt() + "px";

            }
        }

        public string MinHeightPixel
        {
            get
            {

                return this.nMinHeight.toInt() + "px";

            }
        }

        public string MinWidthPixel
        {
            get
            {

                return this.nMinWidth.toInt() + "px";

            }
        }

        public nSetting nMaxFileSize
        {
            get; set;
        }

        public nSetting nMaxHeight
        {
            get; set;
        }

        public nSetting nMaxWidth
        {
            get; set;
        }

        public nSetting nMinHeight
        {
            get; set;
        }

        public nSetting nMinWidth
        {
            get; set;
        }

        public string ParsedMaxFileSize
        {
            get {
                decimal filesize = this.nMaxFileSize.toDecimal();
                string sizeUnit = "Byte";
                if (filesize > 1024) {

                    filesize = filesize / 1024;
                    sizeUnit = "KB";

                    if (filesize > 1024) {

                        sizeUnit = "MB";
                        filesize = filesize / 1024;

                        if (filesize > 1024) {

                            sizeUnit = "GB";
                            filesize = filesize / 1024;

                        }

                    }

                }

                string ret = "";
                if (filesize == 0) ret = "None";
                else ret = filesize + sizeUnit;

                return ret;
            }
        }

        public string picname
        {
            get;
            set;
        }

        public ProfileManagement.GenericProfileModel ProfileImage
        {
            get; set;
        }

        public string[] ProfilePictureFileTypes
        {
            get; set;
        }

        public MvcHtmlString ProfilePictureFileTypesHTMLText
        {
            get
            {

                string strre = "";
                foreach (string str in this.ProfilePictureFileTypes)
                {

                    if (strre == "") strre = "*" + str;
                    else strre += ", *" + str;

                }

                return new MvcHtmlString(strre);
            }
        }

        public MembershipUser User
        {
            get; set;
        }

        #endregion Properties
    }

    public class ChangePasswordModel
    {
        #region Properties

        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort bestätigen")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Das neue Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword
        {
            get; set;
        }

        [Required]
        [StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort")]
        public string NewPassword
        {
            get; set;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuelles Kennwort")]
        public string OldPassword
        {
            get; set;
        }

        #endregion Properties
    }

    public class EditProfileModel
    {
        #region Properties

        public List<ProfileManagement.GenericProfileModel> basicInfo
        {
            get; set;
        }

        public nSetting nMaxFileSize
        {
            get; set;
        }

        public nSetting nMaxHeight
        {
            get; set;
        }

        public nSetting nMaxWidth
        {
            get; set;
        }

        public nSetting nMinHeight
        {
            get; set;
        }

        public nSetting nMinWidth
        {
            get; set;
        }

        public string picname
        {
            get;
            set;
        }

        public ProfileManagement.GenericProfileModel ProfileImage
        {
            get; set;
        }

        public string[] ProfilePictureFileTypes
        {
            get; set;
        }

        public MvcHtmlString ProfilePictureFileTypesHTMLText
        {
            get {

            string strre = "";
            foreach (string str in this.ProfilePictureFileTypes)
            {

                if (strre == "") strre = "*" + str;
                else strre += ", *" + str;

            }

            return new MvcHtmlString(strre);
            }
        }

        public List<ProfileManagement.GenericProfileModel> social
        {
            get; set;
        }

        public string title
        {
            get; set;
        }

        public MembershipUser User
        {
            get; set;
        }

        #endregion Properties
    }

    public class LogOnModel
    {
        #region Properties

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password
        {
            get; set;
        }

        [Display(Name = "Speichern?")]
        public bool RememberMe
        {
            get; set;
        }

        [Required]
        [Display(Name = "Benutzername")]
        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }

    public class RegisterModel
    {
        #region Properties

        [DataType(DataType.Password)]
        [Display(Name = "Kennwort bestätigen")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Das Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword
        {
            get; set;
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail-Adresse")]
        public string Email
        {
            get; set;
        }

        [Required]
        [StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password
        {
            get; set;
        }

        [Required]
        [Display(Name = "Benutzername")]
        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }

    public class Settings
    {
    }

    public class WrappedJsonResult : JsonResult
    {
        #region Methods

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("<html><body><textarea id=\"jsonResult\" name=\"jsonResult\">");
            base.ExecuteResult(context);
            context.HttpContext.Response.Write("</textarea></body></html>");
            context.HttpContext.Response.ContentType = "text/html";
        }

        #endregion Methods
    }
}