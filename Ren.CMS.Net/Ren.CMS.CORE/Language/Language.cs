using Ren.CMS.CORE.SettingsHelper;
using Ren.CMS.CORE.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Ren.CMS.CORE.Language
{

    public class Language
    {

        private string lngcode = null;
        private MembershipUser User = new MemberShip.nProvider.CurrentUser().nUser;
        ThisApplication.ThisApplication AppT = new ThisApplication.ThisApplication();
        /// <summary>
        /// Init the Language  Class with a given Lang-Code
        /// </summary>
        /// <param name="langcode">Lang-Code for this Instance</param>
        /// <param name="package">The Package for the Language Instance (Default: Root)</param>
        public Language(string langcode, string package = "Root")
        {
            if (langcode == "__USER__")
            {
                langcode = Helper.CurrentLanguageHelper.CurrentLanguage;
            }

            this.lngcode = langcode;
            this.pPackage = package;


        }
        /// <summary>
        /// Init the Language Class and sets the Lang-Code to "deDE"
        /// </summary>
        /// <param name="package">The Package for the Language Instance (Default: Root)</param>
        public Language()
        {






        }
        private string lineName = "";
        public string GetInstant()
        {

            return this.getLine(lineName);

        }

        /// <summary>
        /// Instant init. This Init activates the  "GetInstant()" Function and returns directly an language line.
        /// Example: Name = new Language("name","Root","deDE").GetInstant();
        /// </summary>
        /// <param name="LangLineName">Name of the language line</param>
        /// <param name="PackageName">Name of the Package</param>
        /// <param name="code">Language code (default: deDE)</param>
        public Language(string LangLineName, string PackageName, string code = "deDe")
        {

            this.lineName = LangLineName;
            this.pPackage = PackageName;
            this.lngcode = code;


        }

        private string pPackage = "Root";
        /// <summary>
        /// Sets the Package for this instance. Default: Root
        /// </summary>
        public string Package
        {
            get { return this.pPackage; }
            set { this.pPackage = value; }


        }
        public bool LanglineExists(string LangName, string LangPackage, string LangCode)
        {


            if (LangCode == "__USER__" && HttpContext.Current.Request.IsAuthenticated)
            {
                if (User == null) User = new MemberShip.nProvider.CurrentUser().nUser;
                Settings.UserSettings Us = new Settings.UserSettings(User);

                object lc = Us.getSetting("langCode").Value;
                if (lc != null) LangCode = lc.ToString();
                else LangCode = "";
            }
            if (String.IsNullOrEmpty(LangCode))
            {


                SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

                LangCode = GS.Read("GLOBAL_DEFAULT_LANGUAGE");


            }

            string check = "SELECT * FROM " + AppT.getSqlPrefix + "Language WHERE Name=@name AND Package=@package AND Code=@code";

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            bool exists = false;
            SqlDataReader R = Sql.SysReader(check, new nSqlParameterCollection(){ {"@name", LangName },
                                                                                  {"@package", LangPackage},
                                                                                  {"@code", LangCode}});
            exists = R.HasRows;


            R.Close();
            Sql.SysDisconnect();
            return exists;
        }
        /// <summary>
        /// Inserts an Language Line.
        /// </summary>
        /// <param name="name">Name of the Line</param>
        /// <param name="Content">Content of the Line</param>
        public void InsertLine(string name, string Content, bool overwriteDB = false)
        {

            //Checking for langline

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();

            if (this.LanglineExists(name, Package, this.lngcode) && !overwriteDB)
                throw new Exception("Language Line " + name + "(" + this.lngcode + ") does allready exists in Package: " + Package);


            string cmd = "INSERT INTO " + AppT.getSqlPrefix + "Language (Name, Content, Package, Code) VALUES (@Name,@Content,@Package,@Code)";
            SqlParameter[] Parameters = new SqlParameter[]{
        new SqlParameter("@Name", name),
        new SqlParameter("@Content", Content),
        new SqlParameter("@Package", this.pPackage),
        new SqlParameter("@Code", this.lngcode),

        
        
        };
            try
            {

                Sql.SysNonQuery(cmd, Parameters);
                Sql.SysDisconnect();


            }
            catch (SqlException e)
            {

                throw e;

            }
            finally
            {


            }

        }

        private string registerDefaultValues(string forLangLine, string forPackage, Dictionary<string, string> DefaultReturnValue)
        {

            string returnValue = null;

            //Database Action

            CORE.SqlHelper.SqlHelper SQL = new CORE.SqlHelper.SqlHelper();

            CORE.ThisApplication.ThisApplication TA = new CORE.ThisApplication.ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "SELECT code FROM " + prefix + "Lang_Codes";
            CORE.SqlHelper.nSqlParameterCollection SQLCOL = new CORE.SqlHelper.nSqlParameterCollection();

            SQL.SysConnect();

            SqlDataReader Codes = SQL.SysReader(query, SQLCOL);
            Language Lang = null;
            if (Codes.HasRows)
            {

                while (Codes.Read())
                {

                    Lang = new Language((string)Codes["code"], forPackage);

                    if (DefaultReturnValue[(string)Codes["code"]] == null)
                    {

                        DefaultReturnValue.Add((string)Codes["code"], DefaultReturnValue.Where(code => code.Value != null).First().Value);

                    }
                    Lang.InsertLine(forLangLine, DefaultReturnValue[(string)Codes["code"]]);


                }

            }
            Codes.Close();

            Lang = new CORE.Language.Language(this.lngcode, forPackage);
            returnValue = Lang.getLine(forLangLine);
            if (returnValue == "")
            {
                returnValue = forLangLine;

            }
            SQL.SysDisconnect();

            return returnValue;


        }

        /// <summary>
        /// Loads a Language Line String from the Package (Set by this.Package / Default: Root)
        /// </summary>
        /// <param name="name">Name of the Language Line</param>
        /// <returns>(String)Content</returns>
        public string getLine(string name, Dictionary<string, string> DefaultReturnValue = null)
        {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            string cmd = "SELECT TOP 1 * FROM " + AppT.getSqlPrefix + "Language WHERE Package=@Package AND Name=@Name AND Code=@Code";
            SqlParameter[] P = new SqlParameter[] {
        
        new SqlParameter("@Package",this.pPackage),
        new SqlParameter("@Name",name),
        new SqlParameter("@Code",this.lngcode)

        
        
        };
            string line = "";
            try
            {

                Sql.SysConnect();
                SqlDataReader Reader = Sql.SysReader(cmd, P);
                Reader.Read();
                if (Reader.HasRows)
                {
                    line = ((string)Reader["Content"]);

                }
                Sql.SysDisconnect();
            }
            catch (SqlException e) { line = e.Message; }
            finally { }

            if (!LanglineExists(name, Package, this.lngcode) && DefaultReturnValue != null)
            {

                line = this.registerDefaultValues(name, this.pPackage, DefaultReturnValue);

            }
            if (String.IsNullOrEmpty(line)) line = name;

            return line;
        }


    }

}
