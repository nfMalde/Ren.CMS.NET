using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.CORE.FileManagement
{



    public class nFile
    {
        public int id { get; set; }
        public string filepath { get; set; }
        public string aliasName { get; set; }
        public string mimetype { get; set; }
        public string needPermission { get; set; }
        public bool isActive { get; set; }
        public int ProfileID { get; set; }

    }

    //TODO: Controller für Userbilder 
    public class FileSettingModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }


    }
    public class FileManagement
    {
        /*
         * Structure dbo.nfcms_Files
         * id int identity(1,1)
         * fpath text
         * aliasName varchar(255)
 
         * allow2groups varchar(255)
         * active int 
         * Needed Globalsetting:
         * 
         * FILEMANAGEMENT_WATERMARK
         * FILEMANAGEMENT_REPLACE_IMAGE
         * FILEMANAGEMENT_REPLACE_VIDEO
         * 
         * **/

        private int lastFileId = 0;
        private nFile lastFile = null;

        private string[] knownImageMime = {"image/cis-cod",	
"image/cmu-raster",	
"image/fif"	,
"image/gif"	,
"image/ief"	,
"image/jpeg"	,
"image/jpeg",
"image/jpeg",
"image/png",	
"image/tiff",	
"image/tiff",
"image/vasa",		
"image/vnd.wap.wbmp",
"image/x-freehand",
"image/x-freehand",
"image/x-freehand",
"image/x-icon" ,
"image/x-portable-anymap",
"image/x-portable-bitmap",
"image/x-portable-graymap",
"image/x-portable-pixmap",
"image/x-rgb",
"image/x-windowdump",
"image/x-xbitmap" ,
"image/x-xpixmap"  };

        string[] knownImageExt = {".cod",
".ras",
".fif",
".gif",
".ief",
".jpeg",
".jpg",
".jpe",
".png",
".tiff",
".tif",
".mcf",
".wbmp",
".fh4",
".fh5",
".fhc",
".ico",
".pnm",
".pbm",
".pgm",
".ppm",
".rgb",
".xwd",
".xbm",
".xpm"};


        string[] knownVideoMime = {"video/mpeg",
"video/x-msvideo",
"video/x-sgi-movie",
"video/mpeg",
"video/mpeg",
"video/quicktime",
"video/quicktime",
"video/vnd.vivo",
"video/vnd.vivo",
"video/mp4",
"video/x-ms-wmv" };
        string[] knownVideoExt = {".mpeg",
".avi",
".movie",
".mpg",
".mpe",
".qt",
".mov",
".viv",
".vivo",
".mp4",
".wmv" };
        public bool isImage(HttpPostedFileBase F)
        {

            string CT = F.ContentType.ToLower();

            string EXT = Path.GetExtension(F.FileName).ToLower();



            foreach (string c in this.knownImageMime)
            {

                if (c == CT) return true;



            }

            foreach (string ext in this.knownImageExt)
            {
                if (EXT == ext) return true;


            }



            return false;
        }


        public bool isVideo(HttpPostedFileBase F)
        {

            string CT = F.ContentType;

            string EXT = Path.GetExtension(F.FileName);



            foreach (string c in this.knownVideoMime)
            {

                if (c == CT) return true;



            }

            foreach (string ext in this.knownVideoExt)
            {
                if (EXT == ext) return true;


            }



            return false;

        }


        public void DeleteFile(string aliasName)
        {

            nFile F = this.getFile(aliasName, false);
            string mapPATH = HttpContext.Current.Server.MapPath(F.filepath);
            if (System.IO.File.Exists(mapPATH))
            {

                System.IO.File.Delete(mapPATH);


            }
            SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();

            string q = "DELETE " + new ThisApplication.ThisApplication().getSqlPrefix + "Files WHERE id=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", F.id);
            SQL.SysConnect();
            SQL.SysNonQuery(q, PCOL);
            SQL.SysDisconnect();
        }
        public string getMIMETypeForExtension(string extension)
        {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT TOP 1 MIMEType FROM " + TA.getSqlPrefix + "RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.nSqlParameterCollection Parameters = new SqlHelper.nSqlParameterCollection();
            Parameters.Add("@ext", extension);
            string mime = "application/octet-stream";
            SqlDataReader R = Sql.SysReader(query, Parameters);

            if (R.HasRows)
            {
                R.Read();

                mime = (string)R["MIMEType"];




            }

            R.Close();

            Sql.SysDisconnect();

            return mime;




        }
        /// <summary>
        /// Gets the replacement file for a given filename. Image files will recieve a 404 Image and Videos a 404 Video
        /// </summary>
        /// <param name="aliasName">aliasName of the image that was not found</param>
        /// <returns>nFile Model</returns>
        public nFile getErrorFile(string aliasName = "error.jpg")
        {

            return this.get404File(aliasName);


        }

        private nFile get404File(string name)
        {

            Settings.GlobalSettings GS = new Settings.GlobalSettings();
            nFile f404 = new nFile();

            f404.aliasName = name;
            f404.mimetype = this.getMIMETypeForExtension(Path.GetExtension(name).ToLower());
            if (f404.mimetype.ToLower().StartsWith("video/"))
            {
                object rpv = GS.getSetting("FILEMANAGEMENT_REPLACE_VIDEO").Value;
                if (rpv == null || rpv == "")
                {
                    rpv = "/Storage/Default/replacement_default.flv";
                    f404.mimetype = this.getMIMETypeForExtension(".flv");
                }
                f404.filepath = rpv.ToString();

            }
            else if (f404.mimetype.ToLower().StartsWith("image/"))
            {

                object rpv = GS.getSetting("FILEMANAGEMENT_REPLACE_IMAGE").Value;
                if (rpv == null || rpv == "")
                {
                    rpv = "/Storage/Default/replacement_default.jpg";
                    f404.mimetype = this.getMIMETypeForExtension(".jpg");
                }
                f404.filepath = rpv.ToString();



            }
            else
            {


                f404.filepath = "/Storage/Default/filenotfound.html";
                f404.mimetype = this.getMIMETypeForExtension(".html");

            }



            return f404;

        }







        /// <summary>
        /// Register a single file to the FileManagementSystem.
        /// </summary>
        /// <param name="File">nFile Model for registering</param>
        /// <param name="postFieldName">HTTP Post Field Name</param>
        /// <param name="allowedExtensions">Optional: A String array of allowed extensions (.ext,.txt,.aspx ...etc)</param>
        /// <param name="deleteExistingFile">If true, files with the nFile.aliasName of the uploaded File will be deleted before.</param>
        public void RegisterFile(nFile File, string postFieldName, bool deleteExistingFile = false, string[] allowedExtensions = null)
        {
            if (deleteExistingFile == true) this._unregisterFile(File.aliasName);



            this._registerFile(File, postFieldName, allowedExtensions);


        }


        private void _unregisterFile(string aliasFileName)
        {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();

            Sql.SysConnect();

            string query = "SELECT fpath FROM " + TA.getSqlPrefix + "Files WHERE aliasName=@name";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@name", aliasFileName);
            SqlDataReader R = Sql.SysReader(query, PCOL);
            if (R.HasRows)
            {
                R.Read();

                string fp = R["fpath"].ToString();
                fp = HttpContext.Current.Server.MapPath(fp);
                File.Delete(fp);




            }

            R.Close();


            SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();

            PCOL2.Add("@aname", aliasFileName);
            string delete = "DELETE " + TA.getSqlPrefix + "Files WHERE aliasName=@aname";
            Sql.SysNonQuery(delete, PCOL2);

            Sql.SysDisconnect();


        }
        /// <summary>
        /// Registers an file extension to the filemanagement system. Usually there is no need to execute this. After registering a new file the contenttype and extension will be registered too.
        /// </summary>
        /// <param name="extension">The File Extension with dot.  (Example: .jpg, .jpeg, .gif)</param>
        /// <param name="mimetype">The MIME Type of the File for example  image/Jpeg</param>
        public void RegisterExtension(string extension, string mimetype)
        {



            this._registerExtension(extension, mimetype);

        }
        private void _registerExtension(string ext, string mimetype)
        {
            ext = ext.ToLower();
            mimetype = mimetype.ToLower();
            for (int x = 0; x < this.knownImageExt.Length; x++)
            {
                if (this.knownImageExt[x] == ext)
                {
                    if (this.knownImageMime.Length > x)
                    {

                        mimetype = this.knownImageMime[x];

                    }

                }

            }

            for (int y = 0; y < this.knownVideoExt.Length; y++)
            {
                if (this.knownVideoExt[y] == ext)
                {
                    if (this.knownVideoMime.Length > y)
                    {

                        mimetype = this.knownVideoMime[y];

                    }

                }

            }
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT fileExstension,MIMEType FROM " + TA.getSqlPrefix + "RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@ext", ext);

            SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();
            PCOL2.Add("@ext", ext);
            PCOL2.Add("@mime", mimetype);

            Sql.SysConnect();

            string registerExt = "INSERT INTO " + TA.getSqlPrefix + "RegisteredMIMETypes (fileExstension, MIMEType) VALUES(@ext,@mime)";
            string updateExt = "UPDATE " + TA.getSqlPrefix + "RegisteredMIMETypes SET MIMEType=@mime WHERE fileExstension=@ext";
            SqlDataReader R = Sql.SysReader(query, PCOL);
            if (R.HasRows)
            {
                R.Read();

                string mime2 = R["MIMEType"].ToString();

                R.Close();
                if (mimetype != mime2)
                {


                    Sql.SysNonQuery(updateExt, PCOL2);



                }




            }
            else
            {
                R.Close();
                Sql.SysNonQuery(registerExt, PCOL2);

            }

            if (!R.IsClosed) R.Close();

            Sql.SysDisconnect();






        }

        private void _registerFile(nFile Filep, string postFieldName, string[] allowedExtensions = null)
        {

            if (!String.IsNullOrEmpty(Filep.filepath))
            {

                SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SqlPara = new SqlHelper.nSqlParameterCollection();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                //Handle the File

                string path = HttpContext.Current.Request.Files[postFieldName].FileName;
                path = HttpContext.Current.Server.MapPath(path);


                //Generate Filename




                string targetPath = Filep.filepath;
                if (targetPath.Contains("\\"))
                {

                    string rootpath = HttpContext.Current.Server.MapPath("~/");
                    //Get Mappath to the Root

                    targetPath = targetPath.Replace(rootpath, "");

                    targetPath = targetPath.Replace("\\", "/");

                    if (targetPath.StartsWith("/")) targetPath = "~" + targetPath;
                    else if (!targetPath.StartsWith("~")) targetPath = "~/" + targetPath;



                }

                if (targetPath.Contains("?") || targetPath.Contains("#") || targetPath.Contains(' ')) throw new Exception("nfCMS FileManagement does not accept '?' or '#' characters or white spaces in Path");
                if (targetPath.EndsWith("/"))
                {

                    targetPath = targetPath.Remove(targetPath.LastIndexOf("/"));


                }
                string[] pathSplitted = targetPath.Split('/');
                if (pathSplitted.Length > 0)
                {
                    string lastDir = pathSplitted[0];

                    if (pathSplitted.Length > 1)
                    {

                        for (int y = 1; y < pathSplitted.Length; y++)
                        {

                            lastDir = lastDir + "/" + pathSplitted[y];
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(lastDir)))
                            {

                                //Create Directory if not exists
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(lastDir));

                            }

                        }


                    }

                }

                Security.CryptoServices CS = new Security.CryptoServices();

                string targetFileName = DateTime.Now + Path.GetFileNameWithoutExtension(path);
                string extens = (String.IsNullOrEmpty(Path.GetExtension(path)) ? ".unknown" : Path.GetExtension(path));

                targetFileName = CS.ConvertToSHA1(targetFileName) + extens;
                targetPath = targetPath + "/" + targetFileName;

                Filep.filepath = targetPath;
                bool fileIsOk = false;
                if (allowedExtensions != null)
                {

                    if (allowedExtensions.Length == 0) fileIsOk = true;
                    else
                    {


                        foreach (string ext in allowedExtensions)
                        {

                            if (ext.ToLower() == Path.GetExtension(path.ToLower())) fileIsOk = true;

                        }

                    }


                }
                else
                {


                    fileIsOk = true;
                }

                //File is safe
                bool uploadOK = false;
                if (fileIsOk)
                {


                    HttpPostedFile Sv = HttpContext.Current.Request.Files[postFieldName];
                    Sv.SaveAs(HttpContext.Current.Server.MapPath(targetPath));
                    // @File.Copy(path, targetPath);


                    if (File.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                    {

                        uploadOK = true;


                        string query = "INSERT INTO " + TA.getSqlPrefix + "Files (fpath, aliasName, needPermission, fileSize, active, ProfileID) VALUES(@fpath,@aliasName,@allow2groups,@fsize,1,@ProfileID)";
                        string aliasName = (String.IsNullOrEmpty(Filep.aliasName) ? Path.GetFileName(path) : Filep.aliasName);
                        string allow2groups = "";

                        if (String.IsNullOrEmpty(Filep.needPermission)) Filep.needPermission = "";
                        SqlPara.Add("@fsize", Sv.ContentLength);
                        SqlPara.Add("@fpath", targetPath);
                        SqlPara.Add("@aliasName", aliasName);
                        SqlPara.Add("@allow2groups", Filep.needPermission);
                        if (Filep.ProfileID < 1) Filep.ProfileID = 1;
                        SqlPara.Add("@ProfileID", Filep.ProfileID);




                        Sql.SysConnect();

                        Sql.SysNonQuery(query, SqlPara);



                        Sql.SysDisconnect();
                        //Register Extension / Refresh Extension Registration
                        this._registerExtension(Path.GetExtension(Sv.FileName), Sv.ContentType);

                    }


                }



            }


        }

        public partial class nFileProfileManagement
        {
            #region PrivateFunctions
            private bool valueExists(int settingID, int profileID)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string query = "SELECT * FROM " + TA.getSqlPrefix + "FileSettingValues WHERE ProfileID=@profid AND SettingID=@settid";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                SQL.SysConnect();
                PCOL.Add("@profid", profileID);
                PCOL.Add("@settid", settingID);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                bool ret = R.HasRows;
                R.Close();
                SQL.SysDisconnect();

                return ret;

            }

            private bool settingExists(string name)
            {

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();


                string query = "SELECT * FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                SQL.SysConnect();

                PCOL.Add("@name", name);
                SqlDataReader R = SQL.SysReader(query, PCOL);
                bool ret = R.HasRows;
                R.Close();
                SQL.SysDisconnect();


                return ret;


            }

            private void addSetting(int profileid, string name, string value = "")
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();


                string query = "INSERT INTO " + TA.getSqlPrefix + "FileManagementFileSettings (SettingName)  VALUES(@name)";


                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", name);

                SQL.SysConnect();

                SQL.SysNonQuery(query, PCOL);

                int id = SQL.getLastId(TA.getSqlPrefix + "FileManagementFileSettings");

                string query2 = "INSERT INTO " + TA.getSqlPrefix + "FileManagementProfiles2FileSettings(ProfileID,SettingID) VALUES(@pid,@id)";
                SqlHelper.nSqlParameterCollection PCOl2 = new SqlHelper.nSqlParameterCollection();
                PCOl2.Add("@pid", profileid);
                PCOl2.Add("@id", id);

                SQL.SysNonQuery(query2, PCOl2);
                SQL.SysDisconnect();


                //Now the Value

                this.addValue(profileid, id, value);



            }
            private void addValue(int profileID, int settingID, string value)
            {

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string query3 = "INSERT INTO " + TA.getSqlPrefix + "FileSettingValues (ProfileID,SettingID,SettingValue) VALUES(@pid,@id,@val)";
                SqlHelper.nSqlParameterCollection PCOl3 = new SqlHelper.nSqlParameterCollection();

                PCOl3.Add("@pid", profileID);
                PCOl3.Add("@id", settingID);
                PCOl3.Add("@val", value);
                SQL.SysNonQuery(query3, PCOl3);
            }
            private void changeValue(int profileID, string settingName, string newValue)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();


                string query = "UPDATE " + TA.getSqlPrefix + "FileSettingValues SET SettingValue=@val WHERE ProfileID=@pid AND SettingID=(SELECT id FROM " +
                                TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name)";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@val", newValue);
                PCOL.Add("@pid", profileID);
                PCOL.Add("@name", settingName);

                SQL.SysNonQuery(query, PCOL);

                SQL.SysDisconnect();

            }
            private void update_profile(int profileID, string newName)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();
                string query = "UPDATE " + pref + "FileManagementProfiles SET ProfileName=@name WHERE id=@id";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@name", newName);
                PCOL.Add("@id", profileID);

                SQL.SysNonQuery(query, PCOL);


                SQL.SysDisconnect();


            }


            private void delete_profile(int profileID)
            {

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();
                string query = "DELETE " + pref + "FileManagementProfiles WHERE id=@id";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@id", profileID);

                SQL.SysNonQuery(query, PCOL);
                SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();

                PCOL2.Add("@id", profileID);
                string query2 = "DELETE " + pref + "FileManagementProfiles2FileSettings WHERE ProfileID=@id";

                SQL.SysNonQuery(query2, PCOL2);


                SQL.SysDisconnect();
            }

            #endregion

            #region Public Methods
            public int createProfile(string name, List<FileSettingModel> settings)
            {

                nFileProfiles Prof = new nFileProfiles(name);

                if (Prof.ID == 0)
                {

                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                    PCOL.Add("@name", name);

                    string query = "INSERT INTO " + TA.getSqlPrefix + "FileManagementProfiles (ProfileName) VALUES(@name)";
                    SQL.SysConnect();
                    SQL.SysNonQuery(query, PCOL);

                    SQL.SysDisconnect();

                    Prof = new nFileProfiles(name);



                }
                else
                {
                    throw new Exception("Profile Name allready exists");

                }
                foreach (FileSettingModel Setting in settings)
                {

                    if (!this.settingExists(Setting.Name))
                    {
                        this.addSetting(Prof.ID, Setting.Name, Setting.Value);
                    }
                    else
                    {
                        FileSettingModel temp = Prof.getProfileSetting(Setting.Name);

                        if (this.valueExists(temp.ID, Prof.ID))
                            this.changeValue(temp.ID, Setting.Name, Setting.Value);
                        else
                            this.addValue(Prof.ID, temp.ID, Setting.Value);
                    }



                }




                return Prof.ID;
            }
            public void updateProfile(int profileid, string newName)
            {
                this.update_profile(profileid, newName);


            }

            public void deleteProfile(int profileID)
            {
                this.delete_profile(profileID);
            }
            public void setSetting(int profileID, string settingName, string value)
            {

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();


                int id = 0;

                string query = "SELECT id FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@name", settingName);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                if (R.HasRows)
                {

                    R.Read();
                    id = (int)R["id"];


                }
                R.Close();
                SQL.SysDisconnect();
                if (id == 0) this.addSetting(profileID, settingName, value);
                else
                {
                    if (this.valueExists(id, profileID)) this.changeValue(profileID, settingName, value);
                    else this.addValue(profileID, id, value);
                }
            }
            public void setSetting(int profileID, int settingID, string value)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();


                string settingName = "";

                string query = "SELECT SettingName FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE id=@id";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@id", settingID);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                if (R.HasRows)
                {

                    R.Read();
                    settingName = (string)R["SettingName"];


                }
                R.Close();
                SQL.SysDisconnect();
                if (settingName == "") throw new Exception("Setting does not exists");

                if (valueExists(settingID, profileID)) this.changeValue(profileID, settingName, value);
                else this.addValue(profileID, settingID, value);



            }
            public void setSetting(FileSettingModel Setting, int profileID)
            {
                if (this.settingExists(Setting.Name))
                {

                    if (this.valueExists(Setting.ID, profileID))
                    {
                        this.changeValue(profileID, Setting.Name, Setting.Value);

                    }
                    else
                    {
                        this.addValue(profileID, Setting.ID, Setting.Value);

                    }

                }
                else
                {
                    this.addSetting(profileID, Setting.Name, Setting.Value);

                }
            }
            #endregion

        }

        public partial class FilemanagementControllers
        {
            private List<string> _acceptedProfiles = new List<string>();

            private List<string> _acceptedMimeTypes = new List<string>();




            public FilemanagementControllers(string controllerName)
            {
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FilemanagementControllers WHERE ControllerName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", controllerName);
                int id = 0;

                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {

                    while (R.Read())
                    {

                        id = (int)R["id"];


                    }



                }


                R.Close();

                if (id != 0)
                {

                    string profiles = "SELECT p.ProfileName as ProfileName FROM " + pref + "FileManagementProfiles p INNER JOIN " + pref + "FilemanagementControllersAcceptProfiles a ON(a.pid = p.id) WHERE a.cid=@id";
                    SqlHelper.nSqlParameterCollection PCOl2 = new SqlHelper.nSqlParameterCollection();

                    PCOl2.Add("@id", id);

                    SqlDataReader P = SQL.SysReader(profiles, PCOl2);
                    while (P.Read())
                    {

                        this._acceptedProfiles.Add((string)P["ProfileName"]);


                    }
                    P.Close();

                    string types = "SELECT MimeType FROM " + pref + "FilemanagementControllersAcceptMimeTypes WHERE cid=@id";
                    SqlHelper.nSqlParameterCollection PCOL3 = new SqlHelper.nSqlParameterCollection();
                    PCOL3.Add("@id", id);
                    SqlDataReader M = SQL.SysReader(types, PCOL3);
                    if (M.HasRows)
                    {
                        while (M.Read())
                        {
                            this._acceptedMimeTypes.Add((string)M["MimeType"]);
                        }
                    }
                    M.Close();


                }

                SQL.SysDisconnect();
                this._id = id;

            }
            private int _id = 0;
            public int getID()
            {

                return this._id;

            }
            public bool FileIsAccepted(nFile FileEntry)
            {

                /*Profile
                 */

                //If NULL or 0  set to DEFAULT ID: 1
                int fakeProfileID = 1;


                if (FileEntry.ProfileID != null && FileEntry.ProfileID > 0)
                {
                    fakeProfileID = FileEntry.ProfileID;


                }

                nFileProfiles Prof = new nFileProfiles(fakeProfileID);


                /*MimeType
                 */
                string MIMETYPE = FileEntry.mimetype;

                //Query Prof
                IEnumerable<string> ProfileNames = from profname in this._acceptedProfiles
                                                   where profname == Prof.ProfileName


                                                   select profname;

                bool profile_accepted = false;
                foreach (string found in ProfileNames)
                {
                    profile_accepted = true;
                    break;
                }


                //Query MIME


                IEnumerable<string> MIMES = from mimex in this._acceptedMimeTypes where mimex.ToLower() == FileEntry.mimetype.ToLower() || mimex.Contains('*') select mimex;
                bool mime_accepted = false;
                foreach (string mim in MIMES)
                {
                    if (mim.Contains('*'))
                    {

                        if (mim == "*") mime_accepted = true;
                        else
                        {

                            string xmi = mim.Remove(mim.LastIndexOf('*'));
                            if (xmi.Length <= mim.Length)
                            {
                                if (xmi == mim.Substring(0, xmi.Length))
                                {
                                    mime_accepted = true;


                                }
                            }
                        }




                    }
                    else
                        mime_accepted = true;
                    break;


                }




                if (mime_accepted && profile_accepted) return true;
                else return false;



            }

            public bool ControllerExists(string controllerName)
            {
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FilemanagementControllers WHERE ControllerName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", controllerName);
                int id = 0;

                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {
                    R.Close();
                    SQL.SysDisconnect();
                    return true;
                }
                if (!R.IsClosed)
                {
                    R.Close();
                    SQL.SysDisconnect();
                    return false;

                }
                return false;
            }
            public int registerFilemanagementController(string controllername)
            {

                if (this.ControllerExists(controllername))
                {

                    return -1;
                }
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SPP = new SqlHelper.nSqlParameterCollection();
                SPP.Add("@name", controllername);
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;

                string nonQuery = "INSERT INTO " + prefix + "FilemanagementControllers (ControllerName) VALUES(@name)";
                SQL.SysConnect();
                SQL.SysNonQuery(nonQuery, SPP);
                int newID = SQL.getLastId(prefix + "FilemanagementControllers");
                SQL.SysDisconnect();
                this._id = newID;
                return newID;

            }
            private bool profileIsAccepted(string profileName, int controllerID)
            {
                nFileProfiles Prof = new nFileProfiles(profileName);
                if (Prof.ID > 0)
                {
                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@pid", Prof.ID);

                    string query = "SELECT * FROM " + prefix + "FilemanagementControllersAcceptProfiles WHERE pid=@pid AND cid=@cid";
                    SQL.SysConnect();

                    SqlDataReader R = SQL.SysReader(query, PP);
                    bool exists = false;
                    if (R.HasRows)
                    {

                        exists = true;
                    }
                    R.Close();

                    SQL.SysDisconnect();

                    return exists;

                }


                return true;


            }
            public void addIfNotAcceptedProfile(string profileName, int controllerID)
            {

                nFileProfiles Prof = new nFileProfiles(profileName);
                if (Prof.ID > 0 && !this.profileIsAccepted(profileName, controllerID))
                {

                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@pid", Prof.ID);

                    string query = "INSERT INTO " + prefix + "FilemanagementControllersAcceptProfiles (pid,cid) VALUES(@pid, @cid)";

                    SQL.SysConnect();

                    SQL.SysNonQuery(query, PP);
                    SQL.SysDisconnect();
                }



            }


            private bool mimeIsAccepted(string mime, int controllerID)
            {

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;

                SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                PP.Add("@cid", controllerID);
                PP.Add("@mime", mime);

                string query = "SELECT * FROM " + prefix + "FilemanagementControllersAcceptMimeTypes WHERE MimeType=@mime AND cid=@cid";
                SQL.SysConnect();

                SqlDataReader R = SQL.SysReader(query, PP);
                bool exists = false;
                if (R.HasRows)
                {

                    exists = true;
                }
                R.Close();

                SQL.SysDisconnect();

                return exists;

            }

            public void addIfNotAcceptedMime(string mime, int controllerID)
            {
                if (this.mimeIsAccepted(mime, controllerID))
                {
                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@mime", mime);

                    string query = "INSERT INTO " + prefix + "FilemanagementControllersAcceptMimeTypes (MimeType,cid) VALUES(@mime, @cid)";

                    SQL.SysConnect();

                    SQL.SysNonQuery(query, PP);
                    SQL.SysDisconnect();
                }
            }


        }

        public partial class nFileProfiles
        {

            private DataTable settings = new DataTable();
            private string _profilename = "";
            private int _id = 0;
            private void _init(int profileID)
            {
                settings.Columns.Add("id", typeof(int));
                settings.Columns.Add("SettingName", typeof(string));
                settings.Columns.Add("Value", typeof(string));
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();

                SQL.SysConnect();
                string query = "SELECT * FROM " + prefix + "FileManagementProfiles WHERE id=@id";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@id", profileID);
                SqlDataReader MainRow = SQL.SysReader(query, PCOL);

                if (MainRow.HasRows)
                {
                    MainRow.Read();

                    this._profilename = (string)MainRow["ProfileName"];
                    this._id = (int)MainRow["id"];


                }

                MainRow.Close();

                if (this._profilename != "")
                {
                    string getSettings = "SELECT s.id as SettingID, s.SettingName as SettingName, v.SettingValue as SettingValue FROM " + prefix + "FileManagementFileSettings s INNER JOIN " + prefix + "FileManagementProfiles2FileSettings p ON(s.id = p.SettingID)" +
                                         " INNER JOIN " + prefix + "FileSettingValues v ON(s.id = v.SettingID) WHERE p.ProfileID = @id AND v.ProfileID=@id";

                    SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();
                    PCOL2.Add("@id", profileID);

                    SqlDataReader row = SQL.SysReader(getSettings, PCOL2);

                    if (row.HasRows)
                    {

                        while (row.Read())
                        {
                            this.settings.Rows.Add((int)row["SettingID"], (string)row["SettingName"], (string)row["SettingValue"]);

                        }

                    }
                    row.Close();
                }



            }

            public nFileProfiles(int profileID)
            {

                this._init(profileID);

            }

            public nFileProfiles(string profileName)
            {
                int id = 0;

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FileManagementProfiles WHERE ProfileName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", profileName);
                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {

                    while (R.Read())
                    {

                        id = (int)R["id"];

                    }

                }

                R.Close();
                SQL.SysDisconnect();


                this._init(id);



            }

            public FileSettingModel getProfileSetting(string settingName)
            {

                FileSettingModel MDL = new FileSettingModel();
                DataRow[] Row = this.settings.Select("SettingName='" + settingName + "'");
                if (Row.Length > 0)
                {
                    MDL.Name = Row[0]["Name"].ToString();
                    MDL.ID = (int)Row[0]["id"];
                    MDL.Value = (string)Row[0]["Value"];

                }
                else
                {

                }

                return MDL;
            }


            public int ID
            {
                get
                {

                    return this._id;


                }

            }

            public string ProfileName
            {
                get
                {
                    return this._profilename;
                }
            }






        }
        /// <summary>
        /// Registers a couple of files to the FileManagementSystem. Requires one postfield with type "file" per  File
        /// </summary>
        /// <param name="File">nFile Model Array for registering</param>
        /// <param name="postFieldNames">String Array of postFieldNames</param>
        /// <param name="allowedExtensions">Optional: A String array of allowed extensions (.ext,.txt,.aspx ...etc)</param>
        /// <param name="deleteExistingFiles">If true, files with the nFile.aliasName of the uploaded File will be deleted before.</param>
        public void RegisterFiles(nFile[] File, string[] postFieldNames, bool deleteExistingFiles = false, string[] allowedExtensions = null)
        {

            if (File.Length == postFieldNames.Length)
            {


                for (int x = 0; x < File.Length; x++)
                {

                    if (deleteExistingFiles == true) this._unregisterFile(File[x].aliasName);

                    this._registerFile(File[x], postFieldNames[x], allowedExtensions);



                }



            }



        }
        public nFile getFile(string name, bool fileIsActive = true)
        {
            int isActive = 1;
            if (!fileIsActive) isActive = 0;
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT * FROM " + TA.getSqlPrefix + "Files WHERE aliasName=@name AND active=@active";
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection Parameters = new SqlHelper.nSqlParameterCollection();
            Parameters.Add("@name", name);
            Parameters.Add("@active", isActive);
            Settings.GlobalSettings GS = new Settings.GlobalSettings();
            Sql.SysConnect();
            SqlDataReader Files = Sql.SysReader(query, Parameters);
            List<nFile> F = new List<nFile>();
            while (Files.Read())
            {

                //Validate File
                if (Files["aliasName"] == DBNull.Value) continue;
                if (Files["fpath"] == DBNull.Value) continue;
                //  if (Files["mimetype"] == DBNull.Value) continue;
                if (Files["active"] == DBNull.Value) continue;

                //Prepare Data
                nFile Temp = new nFile();

                Temp.aliasName = (string)Files["aliasName"];


                Temp.needPermission = Files["needPermission"].ToString();
                Temp.filepath = (string)Files["fpath"];
                //   Temp.mimetype = (string)Files["mimetype"];
                Temp.id = (int)Files["id"];
                Temp.isActive = ((int)Files["active"] == 1 ? true : false);
                Temp.mimetype = this.getMIMETypeForExtension(Path.GetExtension(Temp.filepath));

                if (Files["ProfileID"] != DBNull.Value)
                {


                    Temp.ProfileID = (int)Files["ProfileID"];

                }
                else
                {

                    Temp.ProfileID = 1;

                }

                F.Add(Temp);






            }

            Files.Close();
            Sql.SysDisconnect();

            if (F.Count > 0)
            {

                if (File.Exists(HttpContext.Current.Server.MapPath(F[0].filepath)))
                {




                    return F[0];
                }
                else
                {


                    return this.get404File(name);

                }



            }
            else
            {



                return this.get404File(name);

            }



        }




    }





}