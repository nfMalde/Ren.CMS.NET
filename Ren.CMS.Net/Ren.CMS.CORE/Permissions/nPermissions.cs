namespace Ren.CMS.CORE.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    public static class nPermissions
    {
        #region Methods

        public static bool hasPermission(string permissionKey)
        {
            bool ret = false;

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();

            Sql.SysConnect();

            if (getCurrentUser() != null)
            {
                //Get Permissiongroup
                string query = "SELECT PermissionGroup FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Users WHERE PKID=@p";
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@p", getCurrentUser().ProviderUserKey.ToString()) };
                SqlDataReader R = Sql.SysReader(query, P);
                if (R.HasRows)
                {
                    R.Read();
                    string groupid = R["PermissionGroup"].ToString();
                    R.Close();
                    string checkPerm = "SELECT val FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Permissions2Groups WHERE groupID=@g AND pk = @pk";

                    SqlParameter[] PP = new SqlParameter[] {

                new SqlParameter("@g", groupid),
                new SqlParameter("@pk", permissionKey)

                };

                    SqlDataReader Perm1 = Sql.SysReader(checkPerm, PP);
                    bool val1 = false;
                    try
                    {
                        if (Perm1.HasRows)
                        {
                            Perm1.Read();

                            val1 = Convert.ToBoolean(Perm1["val"].ToString());
                        }
                    }

                    catch { }
                    Perm1.Close();

                    string checkPerm2 = "SELECT val FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Permissions2Users WHERE groupID=@g AND pk = @pk AND usr=@usr";
                    SqlParameter[] PPP = new SqlParameter[] {

                              new SqlParameter("@g", groupid),
                new SqlParameter("@pk", permissionKey),
                new SqlParameter("@usr", getCurrentUser().ProviderUserKey)

                };
                    SqlDataReader UserP = Sql.SysReader(checkPerm2, PPP);
                    if (UserP.HasRows)
                    {

                        UserP.Read();

                        val1 = (UserP["val"].ToString().ToLower() == "true" ? true : false);

                    }
                    UserP.Close();
                    ret = val1;
                }

                R.Close();
            }
            Sql.SysDisconnect();

            if (!ret && permissionKey != "FULL_ACCESS")
            {

                ret = hasPermission("FULL_ACCESS");
            }

            return ret;
        }

        public static bool permissionKeyExists(string key)
        {
            SqlHelper.SqlHelper myHelper = new SqlHelper.SqlHelper();
            string sqlpref = new ThisApplication.ThisApplication().getSqlPrefix;
            if (key == null) key = "WRITE_COMMENTS";
            string query = "SELECT COUNT(*) as c FROM " + sqlpref + "Permissionkeys WHERE pkey=@pkey";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@pkey", key);

            myHelper.SysConnect();

            SqlDataReader R = myHelper.SysReader(query, PCOL);
            bool exists = false;
            if (R.HasRows)
            {
                R.Read();
                if ((int)R["c"] > 0) exists = true;

            }
            R.Close();
            myHelper.SysDisconnect();

            return exists;
        }

        public static bool RegisterPermissionKey(string PermissionKey, bool DefaultValue, string languageLineName)
        {
            if (!checkForInvalidCharacter(PermissionKey))
            {
                return false;
            }

            return _registerPermissionKey(PermissionKey, DefaultValue, languageLineName);
        }

        public static bool RegisterPermissionKey(string PermissionKey, bool DefaultValue, Language.LanguageDefaults.LanguageDefaultValues LanguageDefaults)
        {
            if(!checkForInvalidCharacter(PermissionKey))
            {
                return false;
            }

            Language.Language Lang = new Language.Language("__USER__", "PERMISSION_KEYS");
            string langName = "LANG_" + PermissionKey + "_DESCRIPTION";
            string LangContent = Lang.getLine(langName, LanguageDefaults.ToDictionary());

            return _registerPermissionKey(PermissionKey, DefaultValue, langName);
        }

        private static bool checkForInvalidCharacter(string PKEY)
        {
            if (permissionKeyExists(PKEY) || PKEY.ToLower() == "full_access")
                return false;

            foreach (char c in PKEY)
            {
                if (c == ' ') return false;
            }
            return true;
        }

        private static MembershipUser getCurrentUser()
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                return null;

            return  Membership.Provider.GetUser(new System.Web.UI.Page().User.Identity.Name.ToString(), false);

            //>
        }

        private static bool _registerPermissionKey(string PermissionKey, bool DefaultValue, string languageLineName)
        {
            SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string prefix = TA.getSqlPrefix;

            string script = "INSERT INTO " + prefix + "Permissionkeys (pkey, defaultVal, langLine) VALUES(@pkey, @defval, @lang)";

            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@pkey", PermissionKey);
            PCOL.Add("@defval", DefaultValue.ToString());
            PCOL.Add("@lang", languageLineName);
            try
            {
                SQL.SysConnect();

                SQL.SysNonQuery(script, PCOL);

                SQL.SysDisconnect();

                return true;
            }
            catch(Exception e)
            {
                throw e;
            }

            return false;
        }

        #endregion Methods
    }

    public class nPermissionValAttribute : AuthorizeAttribute
    {
        #region Properties

        public string NeededPermissionKeys
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool ok = validateRights(NeededPermissionKeys);

            return ok;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            throw new HttpException(403, "Error: You don´t have the permission to do that!");
        }

        private bool AND(string[] k)
        {
            foreach (string v in k)
            {
                if (!nPermissions.hasPermission(v.Replace(" ", "").Replace("  ", "")))
                    return false;

            }

            return true;
        }

        private bool OR(string[] k)
        {
            foreach (string v in k)
            {
                if (nPermissions.hasPermission(v.Replace(" ","").Replace("  ","")))
                    return true;

            }

            return false;
        }

        private bool validateRights(string input)
        {
            if (input != null && input != String.Empty)
            {
                if (input.Contains('|'))
                {
                    //OR!

                  return OR(input.Split('|'));
                }
                else if (input.Contains('&'))
                {
                    return AND(input.Split('&'));
                }
                else
                {
                    return nPermissions.hasPermission(input);
                }

            }

            return false;
        }

        #endregion Methods
    }
}