namespace Ren.CMS.Models.Backend.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Language.LanguageDefaults;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.Helpers;
    using Ren.CMS.MemberShip;
    using Ren.CMS.Models.FormDialog;

    public class CreateUser
    {
        #region Properties

        [DataType(DataType.EmailAddress, ErrorMessage = "This is not a Valid e-Mail Adress")]
        public string email
        {
            get; set;
        }

        public bool GeneratePWD
        {
            get; set;
        }

        public bool inform_user
        {
            get; set;
        }

        [DataType(DataType.Password)]
        public string password
        {
            get;set;
        }

        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("password", ErrorMessage = "ERRR_PASSWORD_DOESNT_MATCH")]
        public string password_confirm
        {
            get; set;
        }

        [Required]
        public string Pgroup
        {
            get; set;
        }

        public string secretAnswer
        {
            get; set;
        }

        public string secretQuestion
        {
            get; set;
        }

        [Required]
        public string Username
        {
            get; set;
        }

        [DataType(DataType.MultilineText)]
        public string user_comment
        {
            get; set;
        }

        public bool user_locked
        {
            get; set;
        }

        [Required]
        public string user_status
        {
            get; set;
        }

        #endregion Properties
    }

    public class CreateUserFormDialog
    {
        #region Methods

        public FormDialogSettings FormDialogSetup(HtmlHelper helper, string elementID = "formDialogCreateUser")
        {
            FormDialogSettings Settings = new FormDialogSettings();

            nProvider Provider = (nProvider) Membership.Provider;
            Settings.ElementID = elementID;
            Settings.Title = LanguageDefaultsManageUsers.LANG_FM_DIALOG_CREATE_TITLE.ReturnLangLine();
            Settings.Method = FormMethod.Post;
            Settings.URL = "/BackendHandler/Users/Create";
            Settings.Width = 600;
            Settings.Height = 340;
            Settings.Modal = true;
            Settings.SaveText = LanguageDefaultsManageUsers.LANG_FM_DIALOG_CREATE.ReturnLangLine();
            Settings.AbortText = LanguageDefaultsShared.LANG_SHARED_ABORT.ReturnLangLine();

            Settings.Elements.Add(new FormDialogElement(
                                    FormDialogElementType.Hidden,
                                    helper.ProtectID("fc_PKID"),
                                    "PKID",
                                    "",
                                    null, null, null, null, null, false));
            Settings.Elements.Add(new FormDialogElement(
                                    FormDialogElementType.Textbox,
                                    helper.ProtectID("fc_Username"),
                                    "Username",
                                     LanguageDefaultsManageUsers.LANG_FM_DIALOG_USERNAME.ReturnLangLine(),
                                    null, null, null, null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                        helper.ProtectID("fc_Pgroup"),
                                                        "Pgroup", LanguageDefaultsManageUsers.LANG_M_USERS_PGROUP.ReturnLangLine(),
                                                        getDefaultPermissiongroup(),
                                                        this.permissionGroups().Contents));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Radiobutton,
                                                        helper.ProtectID("fc_GeneratePWD"),
                                                        "GeneratePWD",
                                                         LanguageDefaultsManageUsers.LANG_FM_DIALOG_GENERATE_PW.ReturnLangLine(),
                                                        true,

                                                        new List<FormDialogDataStoreRow>(){
                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_YES.ReturnLangLine(), true),
                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_NO.ReturnLangLine(), false)}, "customRenderers.GeneratePWD", null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Password,
                                                                helper.ProtectID("fc_password"),
                                                                "password",
                                                                 LanguageDefaultsManageUsers.LANG_FM_DIALOG_PASSWORD.ReturnLangLine(),
                                                                null, null, "customRenderers.password", null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Password,
                                                                helper.ProtectID("fc_password_confirm"),
                                                                "password_confirm",
                                                                LanguageDefaultsManageUsers.LANG_FM_DIALOG_PASSWORD_CONFIRM.ReturnLangLine(),
                                                                null, null, "customRenderers.password", null, null, true));
            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("question"),
                                                        "secretQuestion",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_SECRET_QUESTION.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null, Provider.RequiresQuestionAndAnswer));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("question"),
                                                        "secretAnswer",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_SECRET_ANSWER.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null, Provider.RequiresQuestionAndAnswer));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                        helper.ProtectID("fc_user_status"),
                                                        "user_status",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS.ReturnLangLine(),
                                                        "activated",
                                                        new List<FormDialogDataStoreRow>() {
                                                            new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS_ACTIVATED.ReturnLangLine(), "activated"),
                                                            new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS_DEACTIVATED.ReturnLangLine(), "deactivated")}));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Radiobutton,
                                                        helper.ProtectID("user_locked"),
                                                        "user_locked",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_LOCK.ReturnLangLine(),
                                                        false,
                                                        new List<FormDialogDataStoreRow>(){ new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_YES.ReturnLangLine(),true),
                                                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_NO.ReturnLangLine(), false)}));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("fc_email"),
                                                        "email",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_EMAIL.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        "customRenderers.email",
                                                        null,
                                                        "email", true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                            helper.ProtectID("fc_send_mail"),
                                                            "inform_user",
                                                            LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER.ReturnLangLine(),
                                                            true,
                                                            new List<FormDialogDataStoreRow>(){
                                                                new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER_YES.ReturnLangLine(), true),
                                                                new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER_NO.ReturnLangLine(), false)

                                                            },
                                                            "customRenderers.send_mail"));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.RichTextbox,
                                    helper.ProtectID("fc_user_comment"),
                                    "user_comment",
                                    LanguageDefaultsManageUsers.LANG_FM_DIALOG_COMMENT.ReturnLangLine(),
                                    LanguageDefaultsManageUsers.LANG_FM_DIALOG_COMMENT_NO_COMMENT.ReturnLangLine()));

            return Settings;
        }

        private string getDefaultPermissiongroup()
        {
            string query = "SELECT TOP 1 groupName FROM " + (new ThisApplication().getSqlPrefix) + "PermissionGroups WHERE isDefaultGroup='true'";
            string group = "";
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection());
            if (R.HasRows)
            {
                R.Read();
                group = R[0].ToString();
            }
            R.Close();
            SQL.SysDisconnect();
            return group;
        }

        private FormDialogDataStore permissionGroups()
        {
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();

            string query = "SELECT [id],[groupName],[isGuestGroup],[isDefaultGroup] FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "PermissionGroups ORDER BY [groupName]";
            System.Data.SqlClient.SqlDataReader Groups = SQL.SysReader(query, new nSqlParameterCollection());
            FormDialogDataStore Store = new FormDialogDataStore();
            if (Groups.HasRows)
            {
                while (Groups.Read())
                {

                     Store.Contents.Add(new FormDialogDataStoreRow((string)Groups["groupName"], (string)Groups["groupName"]));

                }

            }

            Groups.Close();
            SQL.SysDisconnect();
            return Store;
        }

        #endregion Methods
    }

    public class EditUser
    {
        #region Properties

        [DataType(DataType.EmailAddress, ErrorMessage= "This is not a valid eMail Address")]
        public string email
        {
            get; set;
        }

        public bool GeneratePWD
        {
            get; set;
        }

        public bool inform_user
        {
            get; set;
        }

        [DataType(DataType.Password)]
        public string password
        {
            get; set;
        }

        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("password", ErrorMessage = "ERRR_PASSWORD_DOESNT_MATCH")]
        public string password_confirm
        {
            get; set;
        }

        [Required]
        public string Pgroup
        {
            get; set;
        }

        [Required]
        public object PKID
        {
            get; set;
        }

        public string secretAnswer
        {
            get; set;
        }

        public string secretQuestion
        {
            get; set;
        }

        [Required]
        public string Username
        {
            get; set;
        }

        [DataType(DataType.MultilineText)]
        public string user_comment
        {
            get; set;
        }

        public bool user_locked
        {
            get; set;
        }

        [Required]
        public string user_status
        {
            get; set;
        }

        #endregion Properties
    }

    public class EditUserFormDialog
    {
        #region Methods

        public FormDialogSettings FormDialogSetup(HtmlHelper helper, string elementID = "formDialogCreateUser")
        {
            FormDialogSettings Settings = new FormDialogSettings();

            nProvider Provider = (nProvider)Membership.Provider;
            Settings.ElementID = elementID;
            Settings.Title = LanguageDefaultsManageUsers.LANG_FM_DIALOG_EDIT_TITLE.ReturnLangLine();
            Settings.Method = FormMethod.Post;
            Settings.URL = "/BackendHandler/Users/Edit";
            Settings.Width = 600;
            Settings.Height = 350;
            Settings.SaveText = LanguageDefaultsShared.LANG_SHARED_SAVE.ReturnLangLine();
            Settings.AbortText = LanguageDefaultsShared.LANG_SHARED_ABORT.ReturnLangLine();

            Settings.Modal = true;
            Settings.Elements.Add(new FormDialogElement(
                                    FormDialogElementType.Hidden,
                                    helper.ProtectID("fe_PKID"),
                                    "PKID",
                                    "",
                                    null, null, null, null, null, false));
            Settings.Elements.Add(new FormDialogElement(
                                    FormDialogElementType.Textbox,
                                    helper.ProtectID("fe_Username"),
                                    "Username",
                                     LanguageDefaultsManageUsers.LANG_FM_DIALOG_USERNAME.ReturnLangLine(),
                                    null, null, null, null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                        helper.ProtectID("fe_Pgroup"),
                                                        "Pgroup", LanguageDefaultsManageUsers.LANG_M_USERS_PGROUP.ReturnLangLine(),
                                                        getDefaultPermissiongroup(),
                                                        this.permissionGroups().Contents));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Radiobutton,
                                                        helper.ProtectID("fe_GeneratePWD"),
                                                        "GeneratePWD",
                                                         LanguageDefaultsManageUsers.LANG_FM_DIALOG_GENERATE_PW.ReturnLangLine(),
                                                        true,

                                                        new List<FormDialogDataStoreRow>(){
                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_YES.ReturnLangLine(), true),
                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_NO.ReturnLangLine(), false)}, "customRenderersEditMode.GeneratePWD", null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Password,
                                                                helper.ProtectID("fe_password"),
                                                                "password",
                                                                 LanguageDefaultsManageUsers.LANG_FM_DIALOG_PASSWORD.ReturnLangLine(),
                                                                null, null, "customRenderersEditMode.password", null, null, true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Password,
                                                                helper.ProtectID("fe_password_confirm"),
                                                                "password_confirm",
                                                                LanguageDefaultsManageUsers.LANG_FM_DIALOG_PASSWORD_CONFIRM.ReturnLangLine(),
                                                                null, null, "customRenderersEditMode.password", null, null, true));
            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("question"),
                                                        "secretQuestion",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_SECRET_QUESTION.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null, Provider.RequiresQuestionAndAnswer));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("question"),
                                                        "secretAnswer",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_SECRET_ANSWER.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null, Provider.RequiresQuestionAndAnswer));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                        helper.ProtectID("fe_user_status"),
                                                        "user_status",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS.ReturnLangLine(),
                                                        "activated",
                                                        new List<FormDialogDataStoreRow>() {
                                                            new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS_ACTIVATED.ReturnLangLine(), "activated"),
                                                            new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_STATUS_DEACTIVATED.ReturnLangLine(), "deactivated")}));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Radiobutton,
                                                        helper.ProtectID("user_locked"),
                                                        "user_locked",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_LOCK.ReturnLangLine(),
                                                        false,
                                                        new List<FormDialogDataStoreRow>(){ new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_YES.ReturnLangLine(),true),
                                                                                            new FormDialogDataStoreRow(LanguageDefaultsShared.LANG_SHARED_NO.ReturnLangLine(), false)}));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Textbox,
                                                        helper.ProtectID("fe_email"),
                                                        "email",
                                                        LanguageDefaultsManageUsers.LANG_FM_DIALOG_EMAIL.ReturnLangLine(),
                                                        null,
                                                        null,
                                                        "customRenderersEditMode.email",
                                                        null,
                                                        "email", true));

            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.Combobox,
                                                            helper.ProtectID("fe_send_mail"),
                                                            "inform_user",
                                                            LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER.ReturnLangLine(),
                                                            true,
                                                            new List<FormDialogDataStoreRow>(){
                                                                new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER_YES.ReturnLangLine(), true),
                                                                new FormDialogDataStoreRow(LanguageDefaultsManageUsers.LANG_FM_DIALOG_INFORM_USER_NO.ReturnLangLine(), false)

                                                            },
                                                            "customRenderersEditMode.send_mail"));
            Settings.Elements.Add(new FormDialogElement(FormDialogElementType.RichTextbox,
                                helper.ProtectID("fc_user_comment"),
                                "user_comment",
                                LanguageDefaultsManageUsers.LANG_FM_DIALOG_COMMENT.ReturnLangLine(),
                                LanguageDefaultsManageUsers.LANG_FM_DIALOG_COMMENT_NO_COMMENT.ReturnLangLine()));

            return Settings;
        }

        private string getDefaultPermissiongroup()
        {
            string query = "SELECT TOP 1 groupName FROM " + (new ThisApplication().getSqlPrefix) + "PermissionGroups WHERE isDefaultGroup='true'";
            string group = "";
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection());
            if (R.HasRows)
            {
                R.Read();
                group = R[0].ToString();
            }
            R.Close();
            SQL.SysDisconnect();
            return group;
        }

        private FormDialogDataStore permissionGroups()
        {
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();

            string query = "SELECT [id],[groupName],[isGuestGroup],[isDefaultGroup] FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "PermissionGroups ORDER BY [groupName]";
            System.Data.SqlClient.SqlDataReader Groups = SQL.SysReader(query, new nSqlParameterCollection());
            FormDialogDataStore Store = new FormDialogDataStore();
            if (Groups.HasRows)
            {
                while (Groups.Read())
                {

                    Store.Contents.Add(new FormDialogDataStoreRow((string)Groups["groupName"], (string)Groups["groupName"]));

                }

            }

            Groups.Close();
            SQL.SysDisconnect();
            return Store;
        }

        #endregion Methods
    }
}