using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace nfCMS_NET.CORE.Security {


    public class CryptoServices {
        /// <summary>
        /// Converts a string into a SHA1 Hash
        /// </summary>
        /// <param name="stringToConvert">The String that has to be converted</param>
        /// <returns>Encrypted SHA1 String</returns>
        public string ConvertToSHA1(string stringToConvert)
        {
            //Umwandlung des Eingastring in den SHA1 Hash
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(stringToConvert);
            byte[] result = sha1.ComputeHash(textToHash);

            //SHA1 Hash in String konvertieren
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }

        /// <summary>
        /// Converts a string into a MD5 Hash
        /// </summary>
        /// <param name="stringToConvert">The String that has to be converted</param>
        /// <returns>Encrypted MD5 String</returns>
        public string ConvertToMD5(string stringToConvert)
        {
            //Umwandlung des Eingastring in den MD5 Hash
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(stringToConvert);
            byte[] result = md5.ComputeHash(textToHash);

            //MD5 Hash in String konvertieren
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }
    
    
    
    }


}
namespace nfCMS_NET.CORE.FileManagement {

 

    public class nFile {
        public int id { get; set; }
        public string filepath { get; set; }
        public string aliasName { get; set; }
        public string mimetype { get; set; }
        public string needPermission { get; set; }
        public bool isActive { get; set; }
        public int ProfileID { get; set; }
        
    }

    //TODO: Controller für Userbilder 
    public class FileSettingModel {
        public int ID { get; set; }
        public string Name { get; set; }
    
        public string Value { get; set; }
    
    
    }
    public class FileManagement { 
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


        





        public string getMIMETypeForExtension(string extension) {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT TOP 1 MIMEType FROM " + TA.getSqlPrefix + "RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.nSqlParameterCollection Parameters = new SqlHelper.nSqlParameterCollection();
            Parameters.Add("@ext", extension);
            string mime = "application/octet-stream";
            SqlDataReader R = Sql.SysReader(query, Parameters);

            if (R.HasRows) {
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
        public nFile getErrorFile(string aliasName = "error.jpg") {

            return this.get404File(aliasName);
        
        
        }

        private nFile get404File(string name){

            Settings.GlobalSettings GS = new Settings.GlobalSettings();
        nFile f404 = new nFile();

                    f404.aliasName = name;
                    f404.mimetype = this.getMIMETypeForExtension(Path.GetExtension(name).ToLower());
                    if (f404.mimetype.ToLower().StartsWith("video/"))
                    {
                        object rpv = GS.getSetting("FILEMANAGEMENT_REPLACE_VIDEO").Value;
                        if (rpv == null || rpv =="")
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
        public void RegisterFile(nFile File, string postFieldName, bool deleteExistingFile = false, string[] allowedExtensions = null) {
            if (deleteExistingFile == true) this._unregisterFile(File.aliasName);
            
            
            
            this._registerFile(File, postFieldName, allowedExtensions);

        
        }


        private void _unregisterFile(string aliasFileName) {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();

            Sql.SysConnect();

            string query = "SELECT fpath FROM " + TA.getSqlPrefix + "Files WHERE aliasName=@name";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@name", aliasFileName);
            SqlDataReader R = Sql.SysReader(query, PCOL);
            if (R.HasRows) {
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
        public void RegisterExtension(string extension, string mimetype){



            this._registerExtension(extension, mimetype);
        
        }
        private void _registerExtension(string ext, string mimetype) {

            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT fileExstension,MIMEType FROM "+ TA.getSqlPrefix +"RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.SqlHelper  Sql = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@ext",ext);

            SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();
            PCOL2.Add("@ext",ext);
            PCOL2.Add("@mime",mimetype);

            Sql.SysConnect();

            string registerExt = "INSERT INTO " + TA.getSqlPrefix + "RegisteredMIMETypes (fileExstension, MIMEType) VALUES(@ext,@mime)";
            string updateExt = "UPDATE " + TA.getSqlPrefix + "RegisteredMIMETypes SET MIMEType=@mime WHERE fileExtension=@ext";
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
            else {
                R.Close();
                Sql.SysNonQuery(registerExt, PCOL2);
            
            }

            if (!R.IsClosed) R.Close();

            Sql.SysDisconnect();


        
        
        
        
        }

        private void _registerFile(nFile Filep, string postFieldName, string[] allowedExtensions = null) {

            if (!String.IsNullOrEmpty(Filep.filepath)) {

                SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SqlPara = new SqlHelper.nSqlParameterCollection();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                //Handle the File

               string path = HttpContext.Current.Request.Files[postFieldName].FileName;
               path = HttpContext.Current.Server.MapPath(path);
               
                
                //Generate Filename
               
               
              

               string targetPath = Filep.filepath;
               if (targetPath.Contains("\\")) {

                   string rootpath = HttpContext.Current.Server.MapPath("~/");
                   //Get Mappath to the Root

                   targetPath = targetPath.Replace(rootpath, "");

                   targetPath = targetPath.Replace("\\", "/");

                   if (targetPath.StartsWith("/")) targetPath = "~" + targetPath;
                   else if (!targetPath.StartsWith("~")) targetPath = "~/" + targetPath;

               
               
               }

               if (targetPath.Contains("?") || targetPath.Contains("#") || targetPath.Contains(' ')) throw new Exception("nfCMS FileManagement does not accept '?' or '#' characters or white spaces in Path");
               if (targetPath.EndsWith("/")) {

                   targetPath = targetPath.Remove(targetPath.LastIndexOf("/"));
               
               
               }
               string[] pathSplitted = targetPath.Split('/');
               if (pathSplitted.Length > 0)
               {
                   string lastDir = pathSplitted[0];

                   if (pathSplitted.Length > 1) {

                       for (int y = 1; y < pathSplitted.Length; y++) {

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
                string extens =(String.IsNullOrEmpty(Path.GetExtension(path)) ? ".unknown" : Path.GetExtension(path));

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
               else {


                   fileIsOk = true;
               }

                //File is safe
               bool uploadOK = false;
               if (fileIsOk) {


                   HttpPostedFile Sv = HttpContext.Current.Request.Files[postFieldName];
                   Sv.SaveAs(HttpContext.Current.Server.MapPath(targetPath));
                  // @File.Copy(path, targetPath);


                   if (File.Exists(HttpContext.Current.Server.MapPath(targetPath))) {

                       uploadOK = true;


                       string query = "INSERT INTO " + TA.getSqlPrefix + "Files (fpath, aliasName, needPermission, fileSize, active) VALUES(@fpath,@aliasName,@allow2groups,@fsize,1)";
                       string aliasName = (String.IsNullOrEmpty(Filep.aliasName) ? Path.GetFileName(path) : Filep.aliasName);
                       string allow2groups ="";

                       if (String.IsNullOrEmpty(Filep.needPermission)) Filep.needPermission = "";
                       SqlPara.Add("@fsize", Sv.ContentLength);
                       SqlPara.Add("@fpath", targetPath);
                       SqlPara.Add("@aliasName", aliasName);
                       SqlPara.Add("@allow2groups", Filep.needPermission);

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

                string query = "SELECT id FROM "+ TA.getSqlPrefix +"FileManagementFileSettings WHERE SettingName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@name", settingName);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                if (R.HasRows) {

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
            
            private List<string>  _acceptedMimeTypes = new List<string>();


           

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

                if (R.HasRows) {

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
                    while (P.Read()) {

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


                if (FileEntry.ProfileID != null && FileEntry.ProfileID>0)
                {
                    fakeProfileID = FileEntry.ProfileID;


                }

                nFileProfiles Prof = new nFileProfiles(fakeProfileID);


                /*MimeType
                 */
                string MIMETYPE = FileEntry.mimetype;

                //Query Prof
                IEnumerable<string> ProfileNames = from profname in this._acceptedProfiles where profname == Prof.ProfileName
                                                       
                                                       
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

                            string xmi  = mim.Remove(mim.LastIndexOf('*'));
                            if (xmi.Length <= mim.Length)
                            {
                                if (xmi == mim.Substring(0, xmi.Length))
                                {
                                    mime_accepted = true;


                                }
                            }
                        }


                                            
                    
                    }else
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
            {nFileProfiles Prof = new nFileProfiles(profileName);
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
                if (Prof.ID > 0 && !this.profileIsAccepted(profileName,controllerID))
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
            private void _init(int profileID) {
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
                          this.settings.Rows.Add((int)row["SettingID"],(string)row["SettingName"],(string)row["SettingValue"]);

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

                string query = "SELECT id FROM "+pref+"FileManagementProfiles WHERE ProfileName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", profileName);
                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows) {

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
            
            if (File.Length == postFieldNames.Length) {


                for (int x = 0; x < File.Length; x++ )
                {

                    if (deleteExistingFiles == true) this._unregisterFile(File[x].aliasName);

                    this._registerFile(File[x], postFieldNames[x], allowedExtensions);



                }
            
            
            
            }
        
        
        
        }
        public nFile getFile(string name, bool fileIsActive = true) {
            int isActive = 1;
            if(!fileIsActive) isActive = 0;
             ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
             string query = "SELECT * FROM "+ TA.getSqlPrefix +"Files WHERE aliasName=@name AND active=@active";
             SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
             SqlHelper.nSqlParameterCollection Parameters = new SqlHelper.nSqlParameterCollection();
             Parameters.Add("@name", name);
             Parameters.Add("@active", isActive);
             Settings.GlobalSettings GS = new Settings.GlobalSettings();
             Sql.SysConnect();
             SqlDataReader Files = Sql.SysReader(query, Parameters);
             List<nFile> F = new List<nFile>();
             while (Files.Read()) {

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
            else{



                return this.get404File(name);
            
            }


         
        }

    
    
    
    }





}
 
namespace nfCMS_NET.CORE.Settings{

    public struct nSettingStoreItem {

        public int ID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    
    
    }
    public class nSettingStore {

        private List<nSettingStoreItem> store = new List<nSettingStoreItem>();
        public nSettingStore(nSetting Setting) {

            string pref = new ThisApplication.ThisApplication().getSqlPrefix;
            string query = "SELECT s.id as id, ISNULL(s.val,'') as Val, ISNULL(l.langLine,'') as langLine FROM " + pref  + "SettingStores s INNER JOIN "+ pref +"SettingStores2Locales l ON(l.stid = s.id) WHERE s.sid=@id";
            string langcode = "";
          

            SqlHelper.SqlHelper Helper = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", Setting.ID);
            
            Language.Language Lng = new Language.Language("__USER__","SETTING_STORES");


            Helper.SysConnect();
            SqlDataReader R = Helper.SysReader(query, PCOL);
            while (R.Read()) {

                nSettingStoreItem Item = new nSettingStoreItem();
                Item.ID = (int)R["id"];
                Item.Label = Lng.getLine((string)R["langLine"]);
                Item.Value = ((string)R["Val"]);
                this.store.Add(Item);
            }

            R.Close();
            Helper.SysDisconnect();
        }
        public List<nSettingStoreItem> getStore { get { return this.store; } }
    
    
    
    
    }
   public struct nSettingType{
      
       public string SettingString{get{ return "Core.SettingType.String";}}

       public string SettingArray{get{ return "Core.SettingType.Array";}}


}

    public struct nValueType{
    
    
    public string ValueString{get{return "Core.ValueType.String";}}

    public string ValueArray{get{return "Core.ValueType.Array";}}
    
    
    
    }



    public class nSetting
    {



        private string plabel = "";
        private string pdescr = "";


        public int ID
        {

            get;
            set;
        }

        public object DefaultValue { get; set; }

        /// <summary>
        /// This Permission is needed to see the Setting in Frontend and edit the Value for it. If NULL every one can see and edit it. (With Permission: USR_CAN_VIEW_ACCOUNT_SETTINGS)
        /// </summary>
        public string PermissionFrontend
        {

            get;
            set;

        }

        /// <summary>
        /// This Permission is needed to see and edit it in the Backend. If NULL every one with Permission USR_IS_ADMIN can see and edit it.
        /// </summary>
        public string PermissionBackend
        {


            get;
            set;


        }

        public string CategoryName
        {

            get;
            set;
        }

        public int CategoryID
        {


            get;
            set;

        }

        public string Name
        {


            get;
            set;

        }
        private string SettingRelationP = "SETTINGS";
        public string SettingRelation { get { return this.SettingRelationP; } set { this.SettingRelationP = value; } }
        public string Label
        {

            get{
            if(this.plabel == "" && this.LabelLanguageLine != null){
            Language.Language LNG = new Language.Language("__USER__", this.SettingRelationP);
            return LNG.getLine(this.LabelLanguageLine);
            
            }else{
            
            
            return this.plabel;
            
            }
            
            
            }
            set{
            
            
            this.plabel = value;
            }


        }

        public string LabelLanguageLine
        {

            get;
            set;


        }

        public string Description
        {


               get{
            if(this.pdescr == "" && this.DescriptionLanguageLine != null){
            Language.Language LNG = new Language.Language("__USER__", this.SettingRelationP);
            return LNG.getLine(this.DescriptionLanguageLine);
            
            }else{
            
            
            return this.pdescr;
            
            }
            
            
            }
            set{
            
            
            this.pdescr = value;
            }
        }
        public string DescriptionLanguageLine
        {

            get;
            set;

        }

        /// <summary>
        /// Checks if the requested value is set.
        /// </summary>
        /// <param name="val">The value to search</param>
        /// <returns></returns>
        public bool isValue(string val)
        {

            nValueType VT = new nValueType();

            if (this.ValueType == VT.ValueArray)
            {


                string[] vals = (string[])this.Value;
                foreach (string v in vals)
                {

                    if (v == val) return true;

                }
                return false;
            }
            else
            {

                string v = (string)this.Value;
                if (v == val) return true;
                else return false;


            }


        }
        public string ValueType
        {

            get;
            set;

        }


        private object pValue = null;

        public object Value
        {


            get{

                if (this.pValue == null) return "";
                else return this.pValue;
            
            }
            set {

                this.pValue = value;
            
            
            }

        }

        public string SettingType
        {
            get;
            set;

        }


        public int toInt()
        {

            int ret = 0;
            if (this.Value == null || this.Value == "") return ret;
            if (int.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());




        }

        public bool toBoolean()
        {

            bool ret = false;

            if (bool.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());


        }

        public string[] toStringArray(bool showError = false)
        {


            try
            {

                string[] ret = (string[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new string[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new string[1].GetType());
            }

        }

        public double toDouble()
        {
            double ret = 0;
            if (this.Value == "") return 0;
            if (double.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());

        }

        public float toFloat()
        {

            float ret = 0;
            if (this.Value == "") return 0;
            if (float.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }

        public long toLong()
        {

            long ret = 0;
            if (this.Value == "") return 0;
            if (long.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }


        public decimal toDecimal()
        {


            decimal ret = 0;
            if (this.Value == "") return 0;
            if (decimal.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }


        public int[] toIntArray(bool showError = false)
        {

            
            try
            {

                int[] ret = (int[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new int[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new int[1].GetType());
            }


        }

        public float[] toFloatArray(bool showError = false)
        {

            try
            {

                float[] ret = (float[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new float[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new float[1].GetType());
            }


        }

        public long[] toLongArray(bool showError = false)
        {


            try
            {

                long[] ret = (long[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new long[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new long[1].GetType());
            }


        }


        public decimal[] toDecimalArray(bool showError = false)
        {

            try
            {

                decimal[] ret = (decimal[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new decimal[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new decimal[1].GetType());
            }


        }



        public bool[] toBooleanArray(bool showError = false)
        {

            try
            {

                bool[] ret = (bool[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new bool[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new bool[1].GetType());
            }


        }

        public char toChar() {

            char ret = ' ';


            if (char.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());

      
            
        
        
        
        }
    
    }

       

        
        
        



       
       
   

  

    public class UserSettings{

    private SqlHelper.SqlHelper myHelper = new SqlHelper.SqlHelper();
    private string sqlpref = new ThisApplication.ThisApplication().getSqlPrefix;
    private MembershipUser thisUser =null;
    public UserSettings(MembershipUser User){
        this.thisUser = User;
       
    
    }


    private bool settingExists(nSetting Setting) {

        bool exists = false;

        //Check if setting name exists
        string check = "SELECT COUNT(*) as c FROM " + sqlpref + "SettingModels WHERE SettingName LIKE @name";
        //Parameter for check
        SqlHelper.nSqlParameterCollection CheckParameter = new SqlHelper.nSqlParameterCollection();
        CheckParameter.Add("@name", Setting.Name);

        myHelper.SysConnect();

        SqlDataReader R = myHelper.SysReader(check, CheckParameter);

        if (R.HasRows) {

            R.Read();

            if ((int)R["c"] > 0) exists = true;
        
        
        }
        R.Close();
        myHelper.SysDisconnect();
        return exists;
    
    
    }


    private int insertModel(nSetting Setting) {
        int id = 0;


  

        myHelper.SysConnect();
        if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine))
        {
            string languagefix = "SELECT code FROM " + sqlpref + "Lang_Codes";
            SqlDataReader L = myHelper.SysReader(languagefix);
            if (L.HasRows)
            {
                while (L.Read())
                {
                    nfCMS_NET.CORE.Language.Language LNG = new CORE.Language.Language(L["code"].ToString(), "USER_SETTINGS");
                    string lng1 = "LANG_USR_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_LABEL_GENERATED";
                    string lng2 = "LANG_USR_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_DESCR_GENERATED";
                    if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && LNG.getLine(lng1) == "")
                    {


                        LNG.InsertLine(lng1, Setting.Label);
                        Setting.LabelLanguageLine = lng1;
                    }

                    if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && LNG.getLine(lng2) == "")
                    {

                        LNG.InsertLine(lng2, Setting.Description);
                        Setting.DescriptionLanguageLine = lng2;

                    }
                }
            }

            L.Close();
        }
        if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine)) return 0;
        //Insert the Model
        string model = "INSERT INTO " + sqlpref + "SettingModels (SettingName,SettingDefaultValue,SettingLangLineLabel,SettingLangLineDescr,SettingRelation,ValueType,CID,SettingType)" +
                        " VALUES (@SettingName,@DefaultValue,@SettingLangLineLabel,@SettingLangLineDescr,@SettingRelation,@ValueType,@CID,@SettingType)";
        //Parameters for Model
        SqlHelper.nSqlParameterCollection ModelParameters = new SqlHelper.nSqlParameterCollection();
        ModelParameters.Add("@SettingName", Setting.Name);
        ModelParameters.Add("@SettingType", Setting.SettingType);
        ModelParameters.Add("@SettingLangLineLabel", Setting.LabelLanguageLine);
        ModelParameters.Add("@SettingLangLineDescr", Setting.DescriptionLanguageLine);
        ModelParameters.Add("@SettingRelation", "USER_SETTINGS");
        ModelParameters.Add("@CID", Setting.CategoryID);


        string dval = "";
        if (Setting.ValueType == new nValueType().ValueArray)
        {



            string arraybuilderD = "a:{ ";
            if (Setting.DefaultValue.GetType() == typeof(string[]))
            {

                foreach (string str in (string[])Setting.DefaultValue)
                {



                    arraybuilderD += "\"" + HttpUtility.HtmlEncode(str) + "\" ";



                }


            }
            arraybuilderD += "}";

            dval = arraybuilderD;
        }
        else
        {

            dval = Setting.DefaultValue.ToString();

        }
        try
        {
            ModelParameters.Add("@DefaultValue", dval);
            ModelParameters.Add("@ValueType", Setting.ValueType);
            myHelper.SysConnect();
            myHelper.SysNonQuery(model, ModelParameters);
            id = myHelper.getLastId(sqlpref + "SettingModels");
            myHelper.SysDisconnect();
        }
        catch(SqlException e) {

            throw e;
        
        
        }

       
     

        return id;
    
    }
    private void insertValuesForUsers(nSetting Setting) {



        string myval = "";

        if (Setting.ValueType == new nValueType().ValueArray)
        {



            string arraybuilderD = "a:{ ";
            if (Setting.Value.GetType() == typeof(string[]))
            {

                foreach (string str in (string[])Setting.DefaultValue)
                {



                    arraybuilderD += "\"" + HttpUtility.HtmlEncode(str) + "\" ";



                }


            }
            arraybuilderD += "}";
            string[] c = (string[])Setting.DefaultValue;
            if(c.Length>0)
            myval = arraybuilderD;
        }
        else
        {

            myval = Setting.DefaultValue.ToString();

        }

        myHelper.SysConnect();
        if (String.IsNullOrEmpty(myval) || String.IsNullOrWhiteSpace(myval)) { 
        
        //Set Defaultval


            string queryx = "SELECT DefaultValue FROM " + sqlpref + "SettingModels WHERE SettingRelation='USER_SETTINGS' AND id=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@id", Setting.ID);
            SqlDataReader R = myHelper.SysReader(queryx, PCOL);
            if (R.HasRows) {

                R.Read();

                myval = R["DefaultValue"].ToString();
            
            }

            R.Close();
        
        
        }

            string query = "SELECT PKID FROM " + sqlpref + "Users";

            List<string> keys = new List<string>();
            SqlDataReader User = myHelper.SysReader(query);
            if (User.HasRows) {

                while (User.Read()) {

                    keys.Add(User["PKID"].ToString());
                
                }
            
            }
            User.Close();

            myHelper.SysDisconnect();
            foreach (string key in keys) {


                this.addUserValue(key, Setting.ID, myval);

            
            }    
    }
    private bool categoryExists(int iid, out int foundid, string name = "")
    {
        string query1 = "SELECT id FROM "+ sqlpref +"SettingCategories WHERE id=@id AND CatRel=@rel";
        string query2 = "SELECT id FROM " + sqlpref + "SettingCategories WHERE Name=@Name AND CatRel=@rel";
        SqlHelper.nSqlParameterCollection COL1 = new SqlHelper.nSqlParameterCollection();
        SqlHelper.nSqlParameterCollection COL2 = new SqlHelper.nSqlParameterCollection();
        int id = iid;
        COL1.Add("@id", id);
        COL1.Add("@rel", "USER_SETTINGS");
        COL2.Add("@Name", name);
        COL2.Add("@rel", "USER_SETTINGS");
        myHelper.SysConnect();
        bool exists = false;
        if (String.IsNullOrEmpty(name))
        {

            SqlDataReader R = myHelper.SysReader(query1, COL1);
            if (R.HasRows)
            {
                R.Read();

                
                    id = ((int)R["id"]);
                    exists = true;
               
            }
            R.Close();

        }
        else {

            SqlDataReader R = myHelper.SysReader(query2, COL2);
            if (R.HasRows)
            {

                    R.Read();
                
                    id = ((int)R["id"]);
                    exists = true;
               

            }
            R.Close();
        
        }

        myHelper.SysDisconnect();
        foundid = id;
        return exists;
    }

    private void permissions2newSetting(nSetting Setting) {
        string query = "INSERT INTO " + sqlpref + "Settings2Permissions (sid,BackEndPM,FrontEndPM) VALUES(@id,@backend,@frontend)";

        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

        PCOL.Add("@id", Setting.ID);
        PCOL.Add("@backend", Setting.PermissionBackend);
        PCOL.Add("@frontend", Setting.PermissionFrontend);


        myHelper.SysConnect();

        myHelper.SysNonQuery(query, PCOL);

        myHelper.SysDisconnect();
        
           
    
    }

   

    private int createCategory(string Name) {
        int id = 0;
        myHelper.SysConnect();

        string cmd = "INSERT INTO " + sqlpref + "SettingCategories (Name,CatRel) VALUES (@Name,@Rel)";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

        PCOL.Add("@Name", Name);
        PCOL.Add("@Rel", "USER_SETTINGS");
        myHelper.SysNonQuery(cmd, PCOL);

        id = myHelper.getLastId(sqlpref + "SettingCategories");
        myHelper.SysDisconnect();
        return id;
    
    }

    private void removeModel(int settingid) {
        string query = "DELETE " + sqlpref + "SettingModels WHERE id=@id";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
        PCOL.Add("@id", settingid);

        myHelper.SysConnect();
        myHelper.SysNonQuery(query, PCOL);
        myHelper.SysDisconnect();



    
    }
    private void removeValues(int settingid) {
        string query = "DELETE " + sqlpref + "SettingValues WHERE SettingID=@id";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
        PCOL.Add("@id", settingid);

        myHelper.SysConnect();
        myHelper.SysNonQuery(query, PCOL);
        myHelper.SysDisconnect();
    
    
    }
    private void removeUserConnections(int settingid) {

        string query = "DELETE " + sqlpref + "User2Settingvalues WHERE sid=@id";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
        PCOL.Add("@id", settingid);

        myHelper.SysConnect();
        myHelper.SysNonQuery(query, PCOL);
        myHelper.SysDisconnect();
    
    }
    private void removePermissionConnections(int settingid) {

        string query = "DELETE " + sqlpref + "Settings2Permissions WHERE sid=@id";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
        PCOL.Add("@id", settingid);

        myHelper.SysConnect();
        myHelper.SysNonQuery(query, PCOL);
        myHelper.SysDisconnect();
    
    }
    public void RemoveSetting(int settingid) { 
    
    //Remove Model
        this.removeModel(settingid);
     
        //Remove Values
        this.removeValues(settingid);
       

        //Remove UserConnections

        this.removeUserConnections(settingid);


        //Remove Permission Connections


        this.removePermissionConnections(settingid);
    
    
    }
    public bool AddSetting(nSetting Setting) { 
    //We need 4 Queries here: 1. Add the Model, 2. Add The Values for all User, 3. If Array adds the store, 4. Connect Permissions to the Setting
        //Check Setting exists
        if (this.settingExists(Setting)) return false;
        //Check Category exists, if not create it.

        if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && String.IsNullOrEmpty(Setting.Description)) return false;
        if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && String.IsNullOrEmpty(Setting.Label)) return false;
        int cid = 0;


        bool ok = true;
        if (!this.categoryExists(Setting.CategoryID, out cid)) {

            if (!this.categoryExists(0, out cid, Setting.CategoryName)) {


                cid = this.createCategory(Setting.CategoryName);
            
            
            }
        
        
        
        }
        if (cid != 0) Setting.CategoryID = cid;

        int modelid = this.insertModel(Setting);
        if (modelid != 0)
        {

            Setting.ID = modelid;

            try
            {

                this.insertValuesForUsers(Setting);
            }

            catch
            {

                ok = false;
            }


            Permissions.nPermissions PM = new Permissions.nPermissions();

            if (!PM.permissionKeyExists(Setting.PermissionFrontend)) ok = false;
            if (!PM.permissionKeyExists(Setting.PermissionBackend)) ok = false;
            if (ok != false) {
                try
                {
                    this.permissions2newSetting(Setting);
                }
                catch {

                    ok = false;
                }
            }
        }
        else {

            ok = false;
        
        }


        if (modelid != 0 && ok == false) {
            //Something went wrong, we have to remove our changes
            this.RemoveSetting(modelid);

        
        }
        return ok;
    }
    private List<nSetting> _listSettingForCat(int iid, bool ignorePermissions){
  
       myHelper.SysConnect();
       string query = "SELECT s.id as id, c.id as CatID, c.Name as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue, s.SettingDefaultValue as DefaultValue FROM "+
                      sqlpref+
                      "SettingModels s LEFT OUTER JOIN "+ sqlpref +"SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN "+ sqlpref +"SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN "+
                      sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) LEFT OUTER JOIN " + sqlpref + "User2Settingvalues ua ON(ua.sid = s.id AND ua.uid=@uid AND v.id=ua.vid) WHERE ISNULL(ua.uid,'8CBBFE36-C96E-46FC-A8AF-B6757181E799') = '8CBBFE36-C96E-46FC-A8AF-B6757181E799' AND ISNULL(ua.vid,'')=ISNULL(v.id,'') AND s.SettingRelation = 'USER_SETTINGS' AND c.id=@id AND c.CatRel = 'USER_SETTINGS'";
        SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
        Pcol.Add("@id",iid);
        Pcol.Add("@uid",thisUser.ProviderUserKey);
        SqlDataReader Sett = myHelper.SysReader(query,Pcol);

        List<nSetting> temp = new List<nSetting>();
        
        Permissions.nPermissions PM = new Permissions.nPermissions();
 
        nValueType VT = new nValueType();
        while(Sett.Read()){
        
            if(PM.hasPermission(Sett["FE_P"].ToString()) || PM.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions){
            
            
               nSetting S = new nSetting();
                S.CategoryID = iid;
                S.CategoryName = Sett["CatName"].ToString();
                S.ID = (int)Sett["id"];
                S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                S.SettingRelation = "USER_SETTINGS";
                S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                S.Name = Sett["SettingName"].ToString();
                S.PermissionBackend = Sett["BE_P"].ToString();
                S.PermissionFrontend = Sett["FE_P"].ToString();
                S.SettingType = Sett["SettingType"].ToString();
                string tempval = Sett["sValue"].ToString();
                string vtype = Sett["ValueType"].ToString();
                if (vtype == VT.ValueArray)
                {

                    //Regex a:{"..." "..."}

                    string pattern = @"a:{(.+?)\}";
                    System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                    string i = "";
                    foreach (Match AP in API)
                    {


                        string str = AP.Value;
                        i = Regex.Replace(str, pattern, "$1");



                    }

                    string pattern2 = "\"(.+?)\"";
                    System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                    string[] myvals = new string[API2.Count];
                    int x = 0;
                    foreach (Match V in API2)
                    {

                        myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));


                    }
                    S.Value = myvals;
                         System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                string dv1 = "";
                foreach (Match DV in DVs) {

                    dv1 = Regex.Replace(DV.Value, pattern, "$1");
                
                }
                MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                string[] defaultval = new string[DVx.Count];
                int y = 0;
                foreach (Match DVxx in DVx) { 
                
                
                defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value,pattern2,"$1"));
                y++;
                }

                S.DefaultValue = defaultval;

                }
                else if (vtype == VT.ValueString)
                {

                    S.DefaultValue = Sett["DefaultValue"].ToString();
                    S.Value = tempval;

                } temp.Add(S);
            
            }
      
        
        
        }
        Sett.Close();
        myHelper.SysDisconnect();     
        
    return temp;    
  
  
    }

    private List<nSetting> _listSettings(bool ignorePermissions){
    
    
       myHelper.SysConnect();
       string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue,s.SettingDefaultValue as DefaultValue FROM " +
                      sqlpref+
                      "SettingModels s LEFT OUTER JOIN "+ sqlpref +"SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN "+
                      sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "User2Settingvalues ua ON(ua.sid = s.id AND ua.uid=@uid  AND v.id=ua.vid) WHERE ISNULL(ua.uid,'8CBBFE36-C96E-46FC-A8AF-B6757181E799') = '8CBBFE36-C96E-46FC-A8AF-B6757181E799' AND ISNULL(ua.vid,'')=ISNULL(v.id,'') AND s.SettingRelation = 'USER_SETTINGS' ORDER BY c.id";
        SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
        Pcol.Add("@uid",thisUser.ProviderUserKey);
        SqlDataReader Sett = myHelper.SysReader(query,Pcol);

        List<nSetting> temp = new List<nSetting>();
        
        Permissions.nPermissions PM = new Permissions.nPermissions();
 
        nValueType VT = new nValueType();

        while(Sett.Read()){
        
            if(PM.hasPermission(Sett["FE_P"].ToString()) || PM.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions){
            
            
               nSetting S = new nSetting();
                S.CategoryID = (int)Sett["CatID"];
               
                S.CategoryName = Sett["CatName"].ToString();
                S.ID = (int)Sett["id"];
                S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                  S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                 
                  S.SettingRelation = "USER_SETTINGS";
                S.Name = Sett["SettingName"].ToString();
                S.PermissionBackend = Sett["BE_P"].ToString();
                S.PermissionFrontend = Sett["FE_P"].ToString();
                S.SettingType = Sett["SettingType"].ToString();



                string tempval = Sett["sValue"].ToString();
                string vtype = Sett["ValueType"].ToString();
                if(vtype == VT.ValueArray){
                
                //Regex a:{"..." "..."}
                
                string pattern = @"a:{(.+?)\}";
            System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
            string i = "";
            foreach (Match AP in API)
            {


                string str = AP.Value;
               i  = Regex.Replace(str, pattern, "$1");
                
           
                
                }
            
             string pattern2 = "\"(.+?)\"";  
            System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
            string[] myvals = new string[API2.Count];
           int x = 0; 
           foreach(Match V in API2){
            
            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value,pattern2,"$1"));
            
            
            }
                S.Value = myvals;
                System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                string dv1 = "";
                foreach (Match DV in DVs) {

                    dv1 = Regex.Replace(DV.Value, pattern, "$1");
                
                }
                MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                string[] defaultval = new string[DVx.Count];
                int y = 0;
                foreach (Match DVxx in DVx) { 
                
                
                defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value,pattern2,"$1"));
                y++;
                }

                S.DefaultValue = defaultval;
            
            }else if(vtype == VT.ValueString){
            
            
            S.Value = tempval;
            S.DefaultValue = Sett["DefaultValue"].ToString();
            
            }
      temp.Add(S);
        
        
        }
        }
        Sett.Close();
        myHelper.SysDisconnect();     
        
    return temp;   
    
    
    
    }
        /// <summary>
        /// Returns a List of User Settings orderd by Category
        /// </summary>
        /// <returns>List of Settings</returns>
    public List<nSetting> listSettings(bool ignorePermissions = false){
    
    
    
    return this._listSettings(ignorePermissions);
    
    
    }

      /// <summary>
    /// Lists all User Settings for the CategoryID  "categoryid"
    /// </summary>
    /// <param name="categoryid">Category ID</param>
    /// <returns>List type if nSetting</returns>
    public List<nSetting> listSettings(int categoryid, bool ignorePermissions = false){
    
    return  this._listSettingForCat(categoryid, ignorePermissions);
    
    
    }


    /// <summary>
    /// Lists all User Settings for the internal Category Name  "categoryname"
    /// </summary>
    /// <param name="categoryname">Internal Category Name</param>
    /// <returns>List type if nSetting</returns>
    public List<nSetting> listSettings(string categoryname, bool ignorePermissions = false)
    {
        int id = 0;



        if (categoryExists(0, out id, categoryname))
        {


            return this._listSettingForCat(id, ignorePermissions);

        }
        else { 
        
        return new List<nSetting>();
        }
         

    }
    
        
    private nSetting _getSetting(int id) {

        string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue, s.SettingDefaultValue as DefaultValue FROM " +
                         sqlpref +
                         "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                         sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) LEFT OUTER JOIN " + sqlpref + "User2Settingvalues ua ON(ua.sid = s.id AND ua.uid=@uid AND v.id=ua.vid) WHERE ISNULL(ua.uid,'8CBBFE36-C96E-46FC-A8AF-B6757181E799') = '8CBBFE36-C96E-46FC-A8AF-B6757181E799' AND ISNULL(ua.vid,'')=ISNULL(v.id,'') AND s.SettingRelation = 'USER_SETTINGS' AND s.id=@id ORDER BY c.id";
        SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
        Pcol.Add("@id", id);
        Pcol.Add("@uid", thisUser.ProviderUserKey);
        myHelper.SysConnect();
        Permissions.nPermissions PM = new Permissions.nPermissions();
        
        nValueType VT = new nValueType();
        SqlDataReader Sett = myHelper.SysReader(query, Pcol);
        nSetting S = new nSetting();
        if (Sett.HasRows) {
            Sett.Read();
            if (PM.hasPermission(Sett["FE_P"].ToString()) || PM.hasPermission(Sett["BE_P"].ToString()))
            {

 
                S.CategoryID = (int)Sett["CatID"];
                S.CategoryName = Sett["CatName"].ToString();
                S.ID = (int)Sett["id"];
                S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                 S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                S.SettingRelation = "USER_SETTINGS";
                    
                    S.Name = Sett["Name"].ToString();
                S.PermissionBackend = Sett["BE_P"].ToString();
                S.PermissionFrontend = Sett["FE_P"].ToString();
                S.SettingType = Sett["SettingType"].ToString();



                string tempval = Sett["sValue"].ToString();
                string vtype = Sett["ValueType"].ToString();
                if (vtype == VT.ValueArray)
                {

                    //Regex a:{"..." "..."}

                    string pattern = @"a:{(.+?)\}";
                    System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                    string i = "";
                    foreach (Match AP in API)
                    {


                        string str = AP.Value;
                        i = Regex.Replace(str, pattern, "$1");



                    }

                    string pattern2 = "\"(.+?)\"";
                    System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                    string[] myvals = new string[API2.Count];
                    int x = 0;
                    foreach (Match V in API2)
                    {

                        myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));


                    }
                    S.Value = myvals;
                    System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                string dv1 = "";
                foreach (Match DV in DVs) {

                    dv1 = Regex.Replace(DV.Value, pattern, "$1");
                
                }
                MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                string[] defaultval = new string[DVx.Count];
                int y = 0;
                foreach (Match DVxx in DVx) { 
                
                
                defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value,pattern2,"$1"));
                y++;
                }

                S.DefaultValue = defaultval;

                }
                else if (vtype == VT.ValueString)
                {

                    S.DefaultValue = Sett["DefaultValue"].ToString();
                    S.Value = tempval;

                }
            }
        
        
        
        }
        Sett.Close();
        myHelper.SysDisconnect();
        return S;
    }


    public nSetting getSetting(string name) {

        string query = "SELECT id FROM " + sqlpref + "SettingModels WHERE SettingName=@name";
        SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
        Pcol.Add("@name", name);
        myHelper.SysConnect();
        int id = 0;
        SqlDataReader R = myHelper.SysReader(query, Pcol);
        if (R.HasRows)
        {
            R.Read();
            id = (int)R["id"];
        }
        R.Close();
        myHelper.SysDisconnect();

        return this._getSetting(id);

    
    }

    public nSetting getSetting(int id) {



        return this._getSetting(id);
    }


    private void addUserValue(object guid, int sid, string val) {

        string query = "INSERT INTO " + sqlpref + "SettingValues (SettingID,SettingValue) VALUES(@id,@val)";
        string query2 = "INSERT INTO " + sqlpref + "User2Settingvalues (uid,sid,vid) VALUES(@uid,@sid,IDENT_CURRENT('" + sqlpref + "SettingValues'))";
        SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
        Pcol1.Add("@id", sid);
        Pcol1.Add("@val", val);

        SqlHelper.nSqlParameterCollection Pcol2 = new SqlHelper.nSqlParameterCollection();

        Pcol2.Add("@uid", guid.ToString());
        Pcol2.Add("@sid", sid);


        myHelper.SysConnect();
        myHelper.SysNonQuery(query, Pcol1);
        myHelper.SysNonQuery(query2, Pcol2);
        myHelper.SysDisconnect();
    
    
    }
    private void insertValue(int id, string val) {

        string query = "INSERT INTO " + sqlpref + "SettingValues (SettingID,SettingValue) VALUES(@id,@val)";
        string query2 = "INSERT INTO " + sqlpref + "User2Settingvalues (uid,sid,vid) VALUES(@uid,@sid,IDENT_CURRENT('" + sqlpref + "SettingValues'))";
        SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
        Pcol1.Add("@id", id);
        Pcol1.Add("@val", val);

        SqlHelper.nSqlParameterCollection Pcol2 = new SqlHelper.nSqlParameterCollection();

        Pcol2.Add("@uid", thisUser.ProviderUserKey);
        Pcol2.Add("@sid", id);


        myHelper.SysConnect();
        myHelper.SysNonQuery(query, Pcol1);
        myHelper.SysNonQuery(query2, Pcol2);
        myHelper.SysDisconnect();
    
    }
    private void updateValue(int id, string val, int vid) {
        string query = "UPDATE " + sqlpref + "SettingValues SET SettingValue=@val WHERE SettingID=@id AND id=@vid";
         
        SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
        Pcol1.Add("@id", id);
        Pcol1.Add("@val", val);
        Pcol1.Add("@vid", vid);
        


        myHelper.SysConnect();
        myHelper.SysNonQuery(query, Pcol1);
   
        myHelper.SysDisconnect();
       
    
    }
    private bool _setValue(nSetting Setting) {

        string querycheck = "SELECT vid FROM  " + sqlpref + "User2Settingvalues WHERE uid=@uid AND sid=@sid";
        SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
        PCOL.Add("@uid", thisUser.ProviderUserKey);
        PCOL.Add("@sid", Setting.ID);

        string val = "";

        nValueType VT = new nValueType();

        if (Setting.ValueType == VT.ValueArray)
        {

            if (Setting.Value.GetType() == typeof(string[]))
            {

                string arraybuilder = "a:{ ";

                foreach (string str in (string[])Setting.Value)
                {

                    arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";

                }
                arraybuilder += "}";
                val = arraybuilder;
            }


        }
        else {

            val = Setting.Value.ToString();
        }
        myHelper.SysConnect();
        SqlDataReader R = myHelper.SysReader(querycheck, PCOL);
        if (R.HasRows)
        {
            R.Read();

            int vid = (int)R["vid"];
            R.Close();
            this.updateValue(Setting.ID, val, vid);

        }
        else {
            R.Close();
            this.insertValue(Setting.ID, val);
        
        }

        if (!R.IsClosed) R.Close();
        myHelper.SysDisconnect();
        return true;
    }    
    public bool setValue(nSetting Setting){
    
    try{
        return this._setValue(Setting);
    }
     catch(Exception e){
        
        
     return false;   
        }
    }
    public bool setValue(int settingID, object val){
        nSetting Setting = this._getSetting(settingID);

        try {
            Setting.Value = val;
            return this._setValue(Setting);            
        
        
        }
        catch (Exception e) {


            return false;
        
        
        }

    }

    public bool setDefaultValue(nSetting Setting) {


        bool ret = true;

        try
        {

            myHelper.SysConnect();
            
            if (Setting.ValueType == new nValueType().ValueArray) {

                if (Setting.DefaultValue.GetType() == typeof(string[])) {
                    string arraybuilder = "a:{ ";

                    foreach (string str in (string[])Setting.DefaultValue) {

                        arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";
                    
                    
                    }
                    Setting.DefaultValue = arraybuilder;
                
                }
            
            }
            string query = "UPDATE " + sqlpref + "SettingModels SET SettingDefaultValue=@dv WHERE id=@id";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@dv", Setting.DefaultValue);
            Pcol.Add("@id", Setting.ID);
            myHelper.SysConnect();
            myHelper.SysNonQuery(query, Pcol);
            myHelper.SysDisconnect();

        }
        catch {

            ret = false;
        }
        return ret;
    
    }

    }
    public class GlobalSettings
    {

        private SqlHelper.SqlHelper myHelper = new SqlHelper.SqlHelper();
        private string sqlpref = "";
        
        public GlobalSettings()
        {
          //this.thisUser = new MemberShip.nProvider.CurrentUser().nUser;
            this.sqlpref = new ThisApplication.ThisApplication().getSqlPrefix;

        }


        private bool settingExists(nSetting Setting)
        {

            bool exists = false;

            //Check if setting name exists
            string check = "SELECT COUNT(*) as c FROM " + sqlpref + "SettingModels WHERE SettingName LIKE @name";
            //Parameter for check
            SqlHelper.nSqlParameterCollection CheckParameter = new SqlHelper.nSqlParameterCollection();
            CheckParameter.Add("@name", Setting.Name);

            myHelper.SysConnect();

            SqlDataReader R = myHelper.SysReader(check, CheckParameter);

            if (R.HasRows)
            {

                R.Read();

                if ((int)R["c"] > 0) exists = true;


            }
            R.Close();
            myHelper.SysDisconnect();
            return exists;


        }


        private int insertModel(nSetting Setting)
        {
            int id = 0;




            myHelper.SysConnect();
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine))
            {
                string languagefix = "SELECT code FROM " + sqlpref + "Lang_Codes";
                SqlDataReader L = myHelper.SysReader(languagefix);
                string lng1 = "LANG_GLB_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_LABEL_GENERATED";
                string lng2 = "LANG_GLB_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_DESCR_GENERATED";
                Setting.LabelLanguageLine = lng1;
                Setting.DescriptionLanguageLine = lng2;
                if (L.HasRows)
                {
                    while (L.Read())
                    {
                        nfCMS_NET.CORE.Language.Language LNG = new CORE.Language.Language(L["code"].ToString(), "GLOBAL_SETTINGS");
                 
                        if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && LNG.getLine(lng1) == "")
                        {


                            LNG.InsertLine(lng1, Setting.Label);
                            
                        }

                        if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && LNG.getLine(lng2) == "")
                        {

                            LNG.InsertLine(lng2, Setting.Description);
                            

                        }
                    }
                }

                L.Close();
            }
            myHelper.SysDisconnect();
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine)) return 0;
            //Insert the Model
            string model = "INSERT INTO " + sqlpref + "SettingModels (SettingName,SettingDefaultValue,SettingLangLineLabel,SettingLangLineDescr,SettingRelation,ValueType,CID,SettingType)" +
                            " VALUES (@SettingName,@DefaultValue,@SettingLangLineLabel,@SettingLangLineDescr,@SettingRelation,@ValueType,@CID,@SettingType)";
            //Parameters for Model
            SqlHelper.nSqlParameterCollection ModelParameters = new SqlHelper.nSqlParameterCollection();
            ModelParameters.Add("@SettingName", Setting.Name);
            ModelParameters.Add("@SettingType", Setting.SettingType);
            ModelParameters.Add("@SettingLangLineLabel", Setting.LabelLanguageLine);
            ModelParameters.Add("@SettingLangLineDescr", Setting.DescriptionLanguageLine);
            ModelParameters.Add("@SettingRelation", "GLOBAL_SETTINGS");
            ModelParameters.Add("@CID", Setting.CategoryID);


            string dval = "";
            if (Setting.ValueType == new nValueType().ValueArray)
            {



                string arraybuilderD = "a:{ ";
                if (Setting.DefaultValue.GetType() == typeof(string[]))
                {

                    foreach (string str in (string[])Setting.DefaultValue)
                    {



                        arraybuilderD += "\"" + HttpUtility.HtmlEncode(str) + "\" ";



                    }


                }
                arraybuilderD += "}";

                dval = arraybuilderD;
            }
            else
            {

                dval = Setting.DefaultValue.ToString();

            }
            try
            {
                ModelParameters.Add("@DefaultValue", dval);
                ModelParameters.Add("@ValueType", Setting.ValueType);
                myHelper.SysConnect();
                myHelper.SysNonQuery(model, ModelParameters);
                id = myHelper.getLastId(sqlpref + "SettingModels");
                myHelper.SysDisconnect();
            }
            catch (SqlException e)
            {
                myHelper.SysDisconnect();
                throw e;


            }




            return id;

        }
         
        private bool categoryExists(int iid, out int foundid, string name = "")
        {
            string query1 = "SELECT id FROM " + sqlpref + "SettingCategories WHERE id=@id AND CatRel=@rel";
            string query2 = "SELECT id FROM " + sqlpref + "SettingCategories WHERE Name=@Name AND CatRel=@rel";
            SqlHelper.nSqlParameterCollection COL1 = new SqlHelper.nSqlParameterCollection();
            SqlHelper.nSqlParameterCollection COL2 = new SqlHelper.nSqlParameterCollection();
            int id = iid;
            COL1.Add("@id", id);
            COL1.Add("@rel", "GLOBAL_SETTINGS");
            COL2.Add("@Name", name);
            COL2.Add("@rel", "GLOBAL_SETTINGS");
            myHelper.SysConnect();
            bool exists = false;
            if (String.IsNullOrEmpty(name))
            {

                SqlDataReader R = myHelper.SysReader(query1, COL1);
                if (R.HasRows)
                {
                    R.Read();


                    id = ((int)R["id"]);
                    exists = true;

                }
                R.Close();

            }
            else
            {

                SqlDataReader R = myHelper.SysReader(query2, COL2);
                if (R.HasRows)
                {

                    R.Read();

                    id = ((int)R["id"]);
                    exists = true;


                }
                R.Close();

            }

            myHelper.SysDisconnect();
            foundid = id;
            return exists;
        }

        private void permissions2newSetting(nSetting Setting)
        {
            string query = "INSERT INTO " + sqlpref + "Settings2Permissions (sid,BackEndPM,FrontEndPM) VALUES(@id,@backend,@frontend)";

            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@id", Setting.ID);
            PCOL.Add("@backend", Setting.PermissionBackend);
            PCOL.Add("@frontend", Setting.PermissionFrontend);


            myHelper.SysConnect();

            myHelper.SysNonQuery(query, PCOL);

            myHelper.SysDisconnect();



        }



        private int createCategory(string Name)
        {
            int id = 0;
            myHelper.SysConnect();

            string cmd = "INSERT INTO " + sqlpref + "SettingCategories (Name,CatRel) VALUES (@Name,@Rel)";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@Name", Name);
            PCOL.Add("@Rel", "GLOBAL_SETTINGS");
            myHelper.SysNonQuery(cmd, PCOL);

            id = myHelper.getLastId(sqlpref + "SettingCategories");
            myHelper.SysDisconnect();
            return id;

        }

        private void removeModel(int settingid)
        {
            string query = "DELETE " + sqlpref + "SettingModels WHERE id=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();




        }
        private void removeValues(int settingid)
        {
            string query = "DELETE " + sqlpref + "SettingValues WHERE SettingID=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();


        }
 
        private void removePermissionConnections(int settingid)
        {

            string query = "DELETE " + sqlpref + "Settings2Permissions WHERE sid=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();

        }
        public void RemoveSetting(int settingid)
        {

            //Remove Model
            this.removeModel(settingid);

            //Remove Values
            this.removeValues(settingid);

             

            //Remove Permission Connections


            this.removePermissionConnections(settingid);


        }
        public bool AddSetting(nSetting Setting)
        {
            //We need 4 Queries here: 1. Add the Model, 2. Add The Values for all User, 3. If Array adds the store, 4. Connect Permissions to the Setting
            //Check Setting exists
            if (this.settingExists(Setting)) return false;
            //Check Category exists, if not create it.

            if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && String.IsNullOrEmpty(Setting.Description)) return false;
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && String.IsNullOrEmpty(Setting.Label)) return false;
            int cid = 0;


            bool ok = true;
            if (!this.categoryExists(Setting.CategoryID, out cid))
            {

                if (!this.categoryExists(0, out cid, Setting.CategoryName))
                {


                    cid = this.createCategory(Setting.CategoryName);


                }



            }
            if (cid != 0) Setting.CategoryID = cid;

            int modelid = this.insertModel(Setting);
            if (modelid != 0)
            {

                Setting.ID = modelid;

                try
                {

                    this.setValue(Setting);
                }

                catch
                {

                    ok = false;
                }


                Permissions.nPermissions PM = new Permissions.nPermissions();

                if (!PM.permissionKeyExists(Setting.PermissionFrontend)) ok = false;
                if (!PM.permissionKeyExists(Setting.PermissionBackend)) ok = false;
                if (ok != false)
                {
                    try
                    {
                        this.permissions2newSetting(Setting);
                    }
                    catch
                    {

                        ok = false;
                    }
                }
            }
            else
            {

                ok = false;

            }


            if (modelid != 0 && ok == false)
            {
                //Something went wrong, we have to remove our changes
                this.RemoveSetting(modelid);


            }
            return ok;
        }
        private List<nSetting> _listSettingForCat(int iid, bool ignorePermissions)
        {

            myHelper.SysConnect();
            string query = "SELECT s.id as id, c.id as CatID, c.Name as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue FROM " +
                           sqlpref +
                           "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                           sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) WHERE s.SettingRelation = 'GLOBAL_SETTINGS' AND c.id=@id AND c.CatRel = 'GLOBAL_SETTINGS'";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@id", iid);
        
            SqlDataReader Sett = myHelper.SysReader(query, Pcol);

            List<nSetting> temp = new List<nSetting>();

            Permissions.nPermissions PM = new Permissions.nPermissions();
        
            nValueType VT = new nValueType();
            while (Sett.Read())
            {

                if (PM.hasPermission(Sett["FE_P"].ToString()) || PM.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions)
                {


                    nSetting S = new nSetting();
                    S.CategoryID = iid;
                    S.CategoryName = Sett["CatName"].ToString();
                    S.ID = (int)Sett["id"];
                    S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                     S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                    S.SettingRelation = "GLOBAL_SETTINGS"; 
                    S.Name = Sett["Name"].ToString();
                    S.PermissionBackend = Sett["BE_P"].ToString();
                    S.PermissionFrontend = Sett["FE_P"].ToString();
                    S.SettingType = Sett["SettingType"].ToString();
                    string tempval = Sett["sValue"].ToString();
                    string vtype = Sett["ValueType"].ToString();
                    if (vtype == VT.ValueArray)
                    {

                        //Regex a:{"..." "..."}

                        string pattern = @"a:{(.+?)\}";
                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                        string i = "";
                        foreach (Match AP in API)
                        {


                            string str = AP.Value;
                            i = Regex.Replace(str, pattern, "$1");



                        }

                        string pattern2 = "\"(.+?)\"";
                        System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                        string[] myvals = new string[API2.Count];
                        int x = 0;
                        foreach (Match V in API2)
                        {

                            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));


                        }
                        S.Value = myvals;
                        System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                        string dv1 = "";
                        foreach (Match DV in DVs)
                        {

                            dv1 = Regex.Replace(DV.Value, pattern, "$1");

                        }
                        MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                        string[] defaultval = new string[DVx.Count];
                        int y = 0;
                        foreach (Match DVxx in DVx)
                        {


                            defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                            y++;
                        }

                        S.DefaultValue = defaultval;

                    }
                    else if (vtype == VT.ValueString)
                    {

                        S.DefaultValue = Sett["DefaultValue"].ToString();
                        S.Value = tempval;

                    } temp.Add(S);

                }



            }
            Sett.Close();
            myHelper.SysDisconnect();

            return temp;


        }

        private List<nSetting> _listSettings(bool ignorePermissions)
        {


            myHelper.SysConnect();
            string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue,s.SettingDefaultValue as DefaultValue FROM " +
                           sqlpref +
                           "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                           sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) WHERE s.SettingRelation = 'GLOBAL_SETTINGS' ORDER BY c.id";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
       
            SqlDataReader Sett = myHelper.SysReader(query, Pcol);

            List<nSetting> temp = new List<nSetting>();

            Permissions.nPermissions PM = new Permissions.nPermissions();
             nValueType VT = new nValueType();

            while (Sett.Read())
            {

                if (PM.hasPermission(Sett["FE_P"].ToString()) || PM.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions)
                {


                    nSetting S = new nSetting();
                    S.CategoryID = (int)Sett["CatID"];

                    S.CategoryName = Sett["CatName"].ToString();
                    S.ID = (int)Sett["id"];
                    S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                     S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                    S.SettingRelation = "GLOBAL_SETTINGS"; 
                    S.Name = Sett["SettingName"].ToString();
                    S.PermissionBackend = Sett["BE_P"].ToString();
                    S.PermissionFrontend = Sett["FE_P"].ToString();
                    S.SettingType = Sett["SettingType"].ToString();



                    string tempval = Sett["sValue"].ToString();
                    string vtype = Sett["ValueType"].ToString();
                    if (vtype == VT.ValueArray)
                    {

                        //Regex a:{"..." "..."}

                        string pattern = @"a:{(.+?)\}";
                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                        string i = "";
                        foreach (Match AP in API)
                        {


                            string str = AP.Value;
                            i = Regex.Replace(str, pattern, "$1");



                        }

                        string pattern2 = "\"(.+?)\"";
                        System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                        string[] myvals = new string[API2.Count];
                        int x = 0;
                        foreach (Match V in API2)
                        {

                            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));


                        }
                        S.Value = myvals;
                        System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                        string dv1 = "";
                        foreach (Match DV in DVs)
                        {

                            dv1 = Regex.Replace(DV.Value, pattern, "$1");

                        }
                        MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                        string[] defaultval = new string[DVx.Count];
                        int y = 0;
                        foreach (Match DVxx in DVx)
                        {


                            defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                            y++;
                        }

                        S.DefaultValue = defaultval;

                    }
                    else if (vtype == VT.ValueString)
                    {


                        S.Value = tempval;
                        S.DefaultValue = Sett["DefaultValue"].ToString();

                    }
                    temp.Add(S);


                }
            }
            Sett.Close();
            myHelper.SysDisconnect();

            return temp;



        }
        /// <summary>
        /// Returns a List of User Settings orderd by Category
        /// </summary>
        /// <returns>List of Settings</returns>
        public List<nSetting> listSettings(bool ignorePermissions = false)
        {



            return this._listSettings(ignorePermissions);


        }
        /// <summary>
        /// Lists all User Settings for the internal Category Name  "categoryname"
        /// </summary>
        /// <param name="categoryname">Internal Category Name</param>
        /// <returns>List type if nSetting</returns>
        public List<nSetting> listSettings(string categoryname, bool ignorePermissions=false)
        {
            int id = 0;



            if (categoryExists(0, out id, categoryname))
            {


                return this._listSettingForCat(id, ignorePermissions);

            }
            else
            {

                return new List<nSetting>();
            }


        }
        /// <summary>
        /// Lists all User Settings for the CategoryID  "categoryid"
        /// </summary>
        /// <param name="categoryid">Category ID</param>
        /// <returns>List type if nSetting</returns>
        public List<nSetting> listSettings(int categoryid, bool ignorePermissions = false)
        {

            return this._listSettingForCat(categoryid, ignorePermissions);


        }
        private nSetting _getSetting(int id)
        {

            string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType,  ("+
  "CASE "+
	 

     "WHEN CAST(v.SettingValue as varchar) != '' THEN v.SettingValue"+
	" WHEN ISNULL(CAST(v.SettingValue as varchar),'') = '' THEN s.SettingDefaultValue"+
     
    " ELSE ISNULL(v.SettingValue,'')"+
"  END "+
") AS sValue, s.SettingDefaultValue as DefaultValue FROM " +
                             sqlpref +
                             "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                             sqlpref + "Settings2Permissions sp ON(s.id = sp.sid)  WHERE s.SettingRelation = 'GLOBAL_SETTINGS' AND s.id=@id ORDER BY c.id";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@id", id);
 
            myHelper.SysConnect();
            //Permissions.nPermissions PM = new Permissions.nPermissions();
          
            nValueType VT = new nValueType();
            SqlDataReader Sett = myHelper.SysReader(query, Pcol);
            nSetting S = new nSetting();
            if (Sett.HasRows)
            {
                Sett.Read();
               

                    S.CategoryID = (int)Sett["CatID"];
                    S.CategoryName = Sett["CatName"].ToString();
                    S.ID = (int)Sett["id"];
                    S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                    S.SettingRelation = "GLOBAL_SETTINGS";
                    S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
          
                    S.Name = Sett["SettingName"].ToString();
                    S.PermissionBackend = Sett["BE_P"].ToString();
                    S.PermissionFrontend = Sett["FE_P"].ToString();
                    S.SettingType = Sett["SettingType"].ToString();



                    string tempval = Sett["sValue"].ToString();
                    string vtype = Sett["ValueType"].ToString();
                    S.ValueType = vtype;
                    if (vtype == VT.ValueArray)
                    {

                        //Regex a:{"..." "..."}

                        string pattern = @"a:{(.+?)\}";
                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                        string i = "";
                        foreach (Match AP in API)
                        {


                            string str = AP.Value;
                            i = Regex.Replace(str, pattern, "$1");



                        }

                        string pattern2 = "\"(.+?)\"";
                        System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                        string[] myvals = new string[API2.Count];
                        int x = 0;
                        foreach (Match V in API2)
                        {

                            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));
                            x++;

                        }
                        S.Value = myvals;
                        System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                        string dv1 = "";
                        foreach (Match DV in DVs)
                        {

                            dv1 = Regex.Replace(DV.Value, pattern, "$1");

                        }
                        MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                        string[] defaultval = new string[DVx.Count];
                        int y = 0;
                        foreach (Match DVxx in DVx)
                        {


                            defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                            y++;
                        }

                        S.DefaultValue = defaultval;

                    }
                    else if (vtype == VT.ValueString)
                    {

                        S.DefaultValue = Sett["DefaultValue"].ToString();
                        S.Value = tempval;

                    }
                



            }
            Sett.Close();
            myHelper.SysDisconnect();
            return S;
        }


        public nSetting getSetting(string name)
        {

            string query = "SELECT TOP 1 id FROM " + sqlpref + "SettingModels WHERE SettingName=@name";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@name", name);
            myHelper.SysConnect();
            int id = 0;
            SqlDataReader R = myHelper.SysReader(query, Pcol);
            if (R.HasRows)
            {
                R.Read();
                id = (int)R["id"];
            }
            R.Close();
            myHelper.SysDisconnect();
            if (id == 0) return new nSetting();
            return this._getSetting(id);


        }

        public nSetting getSetting(int id)
        {



            return this._getSetting(id);
        }


         
        private void insertValue(int id, string val)
        {

            string query = "INSERT INTO " + sqlpref + "SettingValues (SettingID,SettingValue) VALUES(@id,@val)";
             SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
            Pcol1.Add("@id", id);
            Pcol1.Add("@val", val);

            

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, Pcol1);
             myHelper.SysDisconnect();

        }
        private void updateValue(int id, string val, int vid)
        {
            string query = "UPDATE " + sqlpref + "SettingValues SET SettingValue=@val WHERE SettingID=@id";

            SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
            Pcol1.Add("@id", id);
            Pcol1.Add("@val", val);
             



            myHelper.SysConnect();
            myHelper.SysNonQuery(query, Pcol1);

            myHelper.SysDisconnect();


        }
        private bool _setValue(nSetting Setting)
        {

            string querycheck = "SELECT id as vid FROM  " + sqlpref + "SettingValues WHERE SettingID=@sid";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
      
            PCOL.Add("@sid", Setting.ID);

            string val = "";

            nValueType VT = new nValueType();

            if (Setting.ValueType == VT.ValueArray)
            {

                if (Setting.Value.GetType() == typeof(string[]))
                {

                    string arraybuilder = "a:{ ";

                    foreach (string str in (string[])Setting.Value)
                    {

                        arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";

                    }
                    arraybuilder += "}";
                    val = arraybuilder;
                }


            }
            else
            {
                if (Setting.Value == null) Setting.Value = Setting.DefaultValue;
                val = Setting.Value.ToString();
            }
            myHelper.SysConnect();
            SqlDataReader R = myHelper.SysReader(querycheck, PCOL);
            if (R.HasRows)
            {


                int vid = (int)R["vid"];
                R.Close();
                this.updateValue(Setting.ID, val, vid);

            }
            else
            {
                R.Close();
                this.insertValue(Setting.ID, val);

            }

            if (!R.IsClosed) R.Close();
            myHelper.SysDisconnect();
            return true;
        }
        public bool setValue(nSetting Setting)
        {

            try
            {
                return this._setValue(Setting);
            }
            catch (Exception e)
            {


                return false;
            }
        }
        public bool setValue(int settingID, object val)
        {
            nSetting Setting = this._getSetting(settingID);

            try
            {
                Setting.Value = val;
                return this._setValue(Setting);


            }
            catch (Exception e)
            {


                return false;


            }

        }

        public bool setDefaultValue(nSetting Setting)
        {


            bool ret = true;

            try
            {

                myHelper.SysConnect();

                if (Setting.ValueType == new nValueType().ValueArray)
                {

                    if (Setting.DefaultValue.GetType() == typeof(string[]))
                    {
                        string arraybuilder = "a:{ ";

                        foreach (string str in (string[])Setting.DefaultValue)
                        {

                            arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";


                        }
                        Setting.DefaultValue = arraybuilder;

                    }

                }
                string query = "UPDATE " + sqlpref + "SettingModels SET SettingDefaultValue=@dv WHERE id=@id";
                SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
                Pcol.Add("@dv", Setting.DefaultValue);
                Pcol.Add("@id", Setting.ID);
                myHelper.SysConnect();
                myHelper.SysNonQuery(query, Pcol);
                myHelper.SysDisconnect();

            }
            catch
            {

                ret = false;
            }
            return ret;

        }

    }
   
   

} 
namespace nfCMS_NET.CORE.Helper.LinkHelper {

    public class LinkHelper {

            
        public string generateUniqueURL(nfCMS_NET.Content.nContent nContentE) {

            string url = "";



            url = "/Link/UniqueLink/" + nContentE.ID;





            return url;
        }

        public string generateUniqueURL(string url, char delimiter, int uniqueIDindex, bool stripURL = true) {

           
            try
            {

                if (stripURL == true) {

                    
                    url = url.Remove(0, url.LastIndexOf('/') + 1);

                   
                
                
                
                
                }
                string [] buffer = url.Split(delimiter);

                url ="/Link/UniqueLink/"+ buffer[uniqueIDindex];


            }
            catch (Exception e) {


                url = "ERROR for PARSING URL:       "+e.Message;

            
            }


            return url;
        }
    
    
    
    }






}

namespace nfCMS_NET.CORE.Helper.TitleHelper {


    public class TitleHelper {

        string titleTemplate = "{GS:GLOBALSETTING.GLOBABL_MAINSITENAME} | {INP:INPUT}";



        public TitleHelper() { 
        
       //Getting Current titleTemplate


            SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

            string currentTPL = GS.Read("GLOBAL_TITLE_TEMPLATE");
            if (!GS.empty(currentTPL)) {


                titleTemplate = currentTPL;
            
            }

        
        
        
        }


        private string parseTemplate(string tmpl, string input) {


            string pattern = @"{(.+?)\}";
            System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tmpl);

            foreach (Match AP in API)
            {


                string str = AP.Value;
                string col = Regex.Replace(str, pattern, "$1");
                if(col.StartsWith("{GS:")){

                    col = col.Replace("{", "");
                    col = col.Replace("}", "");
                
                
                }
                if (col.StartsWith("GS:")) {
                    
                    col = col.Replace("GS:", "");


                    SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

                    string final = tmpl.Replace(str, GS.Read(col));

                    tmpl = final;
                
                
                }


                if (col.StartsWith("{INP:"))
                {

                    col = col.Replace("{", "");
                    col = col.Replace("}", "");


                }
                if (col.StartsWith("INP:"))
                {

                    col = col.Replace("INP:", "");


                   

                    string final = tmpl.Replace(str, input);

                    tmpl = final;


                }

            }



            return tmpl;
        }


        public string CombineTitle(string TITLE) {



            return this.parseTemplate(this.titleTemplate, TITLE);
        
        }
    
    
    }





}
namespace nfCMS_NET.CORE.MenuBuilder {

    public class Menu {
        private string viewNameContainer = null;
        private string viewNameItem = null;
        private ViewContext pvt = new ViewContext();
        public Menu(ViewContext VT, string viewnameContainer = "main_menu_partial.cshtml") {

            this.viewNameContainer = viewnameContainer;
            this.pvt = VT;
                 
        
        }


         private string RenderPartialViewToString(string viewName, object model)
    {
        ViewDataDictionary ViewData = pvt.ViewData;
        TempDataDictionary TempData = pvt.TempData;
        ControllerContext CT = pvt.Controller.ControllerContext;
          
             
        if (string.IsNullOrEmpty(viewName))
            viewName = CT.RouteData.GetRequiredString("action");

      ViewData.Model = model;
      nfCMS_NET.ViewEngine.nTheming Engin = new nfCMS_NET.ViewEngine.nTheming();

        using (StringWriter sw = new StringWriter()) {
            ViewEngineResult viewResult = Engin.FindPartialView(CT, viewName, false);
            ViewContext viewContext = new ViewContext(CT, viewResult.View, ViewData, TempData, sw);
            viewResult.View.Render(viewContext, sw);

            return sw.GetStringBuilder().ToString();
        }
    }
        private Links.LinkCollection getAllSublinks(int id){
        
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();

            Sql.SysConnect();
            Links.LinkCollection Ret = new Links.LinkCollection();

            string query = "SELECT * FROM "+(new ThisApplication.ThisApplication().getSqlPrefix)+"Links WHERE LinkIsActive='1' AND SublinkFrom=@ID";
            SqlParameter[] PP = new SqlParameter[]{ new SqlParameter("@ID",id) };
            
        

            SqlDataReader L = Sql.SysReader(query,PP);
            while(L.Read()){
            
            
           Links.Link Lnk = new Links.Link((int)L["id"], (string)L["LinkType"], (string)L["LinkController"], (string)L["LinkAction"], (string)L["LinkText"], (string)L["LinkHref"],

                 (L["NormalStateClass"] != DBNull.Value ? (string)L["NormalStateClass"] : ""),
                   (L["HoverStateClass"] != DBNull.Value ? (string)L["HoverStateClass"] : ""));

        
            if(this.hasSublinks(Lnk.ID)){
            
            
        
                
                
                      Lnk.SubLinks= this.getAllSublinks(Lnk.ID);
                
                
                
                }
            
            
         
             Ret.Add(Lnk);     
            
            
            }
            L.Close();
            Sql.SysDisconnect();
        return Ret;
        
        }


        private bool hasSublinks(int id) {


            string query = "SELECT COUNT(*) as c FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE LinkIsActive='1' AND SublinkFrom=@ID";
            SqlParameter[] PP = new SqlParameter[] { new SqlParameter("@ID", id) };


            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            SqlDataReader L = Sql.SysReader(query, PP);
            L.Read();


            int count = (int)L["c"];
            L.Close();
            Sql.SysDisconnect();
            if (count > 0) return true;
            else return false;

            
        
        
        }
        public HtmlString buildMenuCaptions(string identifierName="*"){
        
            Language.Language LNG = new Language.Language("__USER__");
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            MembershipUser User = new MemberShip.nProvider.CurrentUser().nUser;

            Settings.UserSettings usr = new Settings.UserSettings(User);

            object theme  = usr.getSetting("USR_SETTING_THEME").Value;
            if (theme == null || theme == "") {

                theme = "nftheme";
            
            
            
            
            }

            string _ret = "";
           
           SqlHelper.nSqlParameterCollection ColSQL = new SqlHelper.nSqlParameterCollection();




           

            ColSQL.Add(new SqlParameter("@theme", theme));
            string query = "";

            if (identifierName != "*")
            {
                ColSQL.Add(new SqlParameter("@LinkType", identifierName));
                query = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.id = " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.linkID) INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.id=" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.identifierID)  WHERE " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.LinkIsActive='1' AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.identiferName=@LinkType AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.SublinkFrom='' AND  " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.theme=@theme";
            }
            else {


                query = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.id = " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.linkID) INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.id=" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.identifierID)  WHERE " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.LinkIsActive='1' AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.SublinkFrom='' AND  " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.theme=@theme";
         
            
            }
                
                SqlDataReader L = Sql.SysReader(query, ColSQL);
            Links.LinkCollection LCOL = new Links.LinkCollection();
          
            while (L.Read()) {

                  Links.LinkCollection SUBCOL = new Links.LinkCollection();

                    
                Links.Link Lnk = new Links.Link((int)L["id"], (string)L["LinkType"], (string)L["LinkController"], (string)L["LinkAction"], (string)L["LinkText"], (string)L["LinkHref"],

                 (L["NormalStateClass"] != DBNull.Value ? (string)L["NormalStateClass"] : ""),
                   (L["HoverStateClass"] != DBNull.Value ? (string)L["HoverStateClass"] : ""));
                
        
            if(this.hasSublinks(Lnk.ID)){
            
            
 
                
                
                      Lnk.SubLinks= this.getAllSublinks(Lnk.ID);
                
                
                
            
            
            
            }
                LCOL.Add(Lnk);

             
            
            
            }


            L.Close();
            Sql.SysDisconnect();
            nfCMS_NET.Models.Core.MenuModel MDL = new Models.Core.MenuModel();

            MDL.Links = LCOL;
            _ret = this.RenderPartialViewToString(this.viewNameContainer, MDL);
            return new HtmlString(_ret);
        }
    
    
        
    
    
    }





}

namespace nfCMS_NET.CORE.Captcha {
    using System.Web.Mvc;
    using System.Web.UI;
 
    


    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {

       
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

        public override void OnActionExecuting(ActionExecutingContext filterContext)  
        {  
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];  
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];  
            var captchaValidtor = new Recaptcha.RecaptchaValidator  
                                      {
                                          PrivateKey = "6Ldi688SAAAAALHKy2XQtksCChIJydt1G7Ry560e",  
                                          RemoteIP = filterContext.HttpContext.Request.UserHostAddress,  
                                          Challenge = captchaChallengeValue,  
                                          Response = captchaResponseValue  
                                      };  
  
            var recaptchaResponse = captchaValidtor.Validate();  
  
        // this will push the result value into a parameter in our Action  
            filterContext.ActionParameters["captchaValid"] = recaptchaResponse.IsValid;  
  
            base.OnActionExecuting(filterContext);  
        }
    }  




    public class Captcha {

        private string pkey = "";
        private string pvkey = "";


        public Captcha(string publicKey, string privateKey) {

            this.pkey = publicKey;
            this.pvkey = privateKey;
        
        
        
        }

        public bool Verify(string challenge, string response) {
            bool ret = false;
            string postData = "";
                
            
                postData += HttpUtility.UrlEncode("privatekey") + "="
                      + HttpUtility.UrlEncode(this.pvkey) + "&";

                postData += HttpUtility.UrlEncode("remoteip") + "="
               + HttpUtility.UrlEncode(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]) + "&";
                postData += HttpUtility.UrlEncode("challenge ") + "="
           + HttpUtility.UrlEncode(challenge) + "&";
                postData += HttpUtility.UrlEncode("response  ") + "="
     + HttpUtility.UrlEncode(response) + "";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://www.google.com/recaptcha/api/verify");
            myHttpWebRequest.Method = "POST";
            
            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();
            HttpContext.Current.Response.Write(pageContent);
            return pageContent.ToLower().StartsWith("true");
        
        }
    
    
    
    }



}

namespace nfCMS_NET.CORE.Extras {
    public class LocationModel {

        public string Name
        {

            get;
            set;

        }
        public string Address
        {

            get;
            set;


        }

        public bool isCurrentLocation
        {

            get;
            set;

        }
    
    }
    public class LocationBar {
        private ControllerContext context = null;
        private List<LocationModel> _Locations = new List<LocationModel>();
        private string _linkTemplate_partialview = "location_bar_link";
        private string _locationBarTemplate_partialview = "location_bar";
        
        public LocationBar(ControllerContext controllerContext) {

            this.context = controllerContext;


        
        
        
        
        }

        public void AddLocation(LocationModel Location) {



            this._Locations.Add(Location);
        
        
        
        }

        public void AddLocation(string name, string address, bool currentLocation = false)
        {
            LocationModel Location = new LocationModel();
            Location.Name = name;
            Location.Address = address;
            Location.isCurrentLocation = currentLocation;

            this._Locations.Add(Location);



        } 
        /// <summary>
        /// Saves the Location Collection inside the ViewData["LocationBarCollection"];
        /// </summary>
        public void Render(){

            this.context.Controller.ViewData["LocationBarCollection"] = this._Locations;
        
        }
        
        }
    
    
    
     
    public class Extras
    {
        public Extras() { }


        public bool ViewExists(string name, ControllerContext ControllerContextVar)
        {   

          

            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContextVar, name, null);
            return (result.View != null);
        }

        public string var_dump(object obj, int recursion)
        {
            StringBuilder result = new StringBuilder();

            // Protect the method against endless recursion
            if (recursion < 5)
            {
                // Determine object type
                Type t = obj.GetType();

                // Get array with properties for this object
                PropertyInfo[] properties = t.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        // Get the property value
                        object value = property.GetValue(obj, null);

                        // Create indenting string to put in front of properties of a deeper level
                        // We'll need this when we display the property name and value
                        string indent = String.Empty;
                        string spaces = "|   ";
                        string trail = "|...";

                        if (recursion > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, spaces, recursion - 1).ToString();
                        }

                        if (value != null)
                        {
                            // If the value is a string, add quotation marks
                            string displayValue = value.ToString();
                            if (value is string) displayValue = String.Concat('"', displayValue, '"');

                            // Add property name and value to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, displayValue);

                            try
                            {
                                if (!(value is ICollection))
                                {
                                    // Call var_dump() again to list child properties
                                    // This throws an exception if the current property value
                                    // is of an unsupported type (eg. it has not properties)
                                    result.Append(var_dump(value, recursion + 1));
                                }
                                else
                                {
                                    // 2009-07-29: added support for collections
                                    // The value is a collection (eg. it's an arraylist or generic list)
                                    // so loop through its elements and dump their properties
                                    int elementCount = 0;
                                    foreach (object element in ((ICollection)value))
                                    {
                                        string elementName = String.Format("{0}[{1}]", property.Name, elementCount);
                                        indent = new StringBuilder(trail).Insert(0, spaces, recursion).ToString();

                                        // Display the collection element name and type
                                        result.AppendFormat("{0}{1} = {2}\n", indent, elementName, element.ToString());

                                        // Display the child properties
                                        result.Append(var_dump(element, recursion + 2));
                                        elementCount++;
                                    }

                                    result.Append(var_dump(value, recursion + 1));
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            // Add empty (null) property to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, "null");
                        }
                    }
                    catch
                    {
                        // Some properties will throw an exception on property.GetValue()
                        // I don't know exactly why this happens, so for now i will ignore them...
                    }
                }
            }

            return result.ToString();
        }


    }

}
namespace nfCMS_NET.CORE.Permissions {


    public class nPermissions {

        private MembershipUser myuser  = new MemberShip.nProvider.CurrentUser().nUser;





        public bool permissionKeyExists(string key)
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
        public bool hasPermission(string permissionKey) {

            bool ret = false;


            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();

            Sql.SysConnect();

            if (this.myuser != null) { 
            //Get Permissiongroup
            string query = "SELECT PermissionGroup FROM "+ (new ThisApplication.ThisApplication().getSqlPrefix ) +"Users WHERE PKID=@p";
            SqlParameter[] P = new SqlParameter[]{ new SqlParameter("@p",myuser.ProviderUserKey.ToString())};
            SqlDataReader R = Sql.SysReader(query,P);
            if(R.HasRows){
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
                new SqlParameter("@usr", this.myuser.ProviderUserKey)
                
                
                
                };
                SqlDataReader UserP = Sql.SysReader(checkPerm2, PPP);
                if (UserP.HasRows) {


                    UserP.Read();


                    val1 = (UserP["val"].ToString().ToLower() == "true" ? true : false);
                
                
                }
                UserP.Close();
                ret = val1;
               }



            R.Close();
            }
            Sql.SysDisconnect();
            return ret;



        
        
        
        
        }
    
    
    
    
    
    
    
    }




}


namespace nfCMS_NET.CORE.Links
{   



    #region Link Model
    public class Link {

        private string pType = "";
        private string pTargetController = "";
        private string pTargetAction = "";
        private string pText = "";
        private string pHref = "";
        private int pID = 0;
        private string pNormalStateClass = "";
        private string pHoverStateClass = "";
        private LinkCollection pSubLinks = new LinkCollection();

        public Link() { 
        
        }
        public Link(int aID, string aType, string aTargetController, string aTargetAction, string aText, string aHref, string aNormalStateClass = "", string aHoverStateClass = "", LinkCollection aSubLinks = null) {
            SettingsHelper.GlobalSettingsHelper G = new SettingsHelper.GlobalSettingsHelper();

            if (G.empty(aType) || G.empty(aText) || aID<=0)
            {
                throw new ConfigurationErrorsException("Required Attributes (Type,Text,ID) are not correct.");


            }
            else {
                this.pHoverStateClass = aHoverStateClass;
                this.pNormalStateClass = aNormalStateClass; 
                this.pType = aType;
                this.pText = aText;
                this.pTargetController = aTargetController;
                this.pTargetAction = aTargetAction;
                this.pHref = aHref;
                this.pID = aID;
              
            
            }
        
        
        }
        public LinkCollection SubLinks{
        
        
                get{
                
                
                        return this.pSubLinks;

                
                
                }
                

                set{
                
                
                      this.pSubLinks = value;
                
                }
        
        
        }
        #region Property NormalStateClass
        public string NormalStateClass {

            get { return this.pNormalStateClass; }
            set { this.pNormalStateClass = value; }        
        
        }

        #endregion
        #region Property HoverStateClass
        public string HoverStateClass
        {

            get { return this.pHoverStateClass; }
            set { this.pHoverStateClass = value; }

        }

        #endregion
        #region Property ID

        public int ID {

            get {

                return this.pID;
            
            
            }
            set {

                this.pID = value;
            
            }
        
        
        }

    #endregion
        #region Property Href

        public string Href {

            get {

                return this.pHref;

            
            }
            set {

                this.pHref = value;
            
            
            }
        
        
        }

        #endregion
        #region Property Type
        public string Type {
            get {


                return this.pType;
            
            }
            set {



                this.pType = value;
            }
        
        }
        #endregion
        #region Property TargetController
        public string TargetController {


            get {


                return this.pTargetController;
            }
            set {

                this.pTargetController = value;
            
            }
        
        
        }
        #endregion
        #region Property TargetAction

        public string TargetAction {

            get {

                return this.pTargetAction;
            }
            set {

                this.pTargetAction = value;
            
            
            }
        
        
        }

        #endregion
        #region Property Text
        public string Text
        {
            get
            {
                Language.Language Lng = new Language.Language("__USER__", "LINKS");
                string realText = Lng.getLine(this.pText);
                return realText;

            }
            set
            {



                this.pText = value;
            }

        }
        #endregion

    }
    #endregion
    #region Link Collection
    public class LinkCollection : System.Collections.IEnumerable
    {

        private List<Link> pCol = new List<Link>();
        public LinkCollection() { }

   
        
        public void Add(Link LinkItem) {
            this.pCol.Add(LinkItem);
        
        }
        public void AddRange(Link[] LinkRange) {

            this.pCol.AddRange(LinkRange);
    
        
        }
        public void Remove(Link LinkItem) {

            this.pCol.Remove(LinkItem);
        
        
        }
        public IEnumerator<Link> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    }
   
    #endregion
    
#region Navi-Links

    public class ReturnMainLinks {

        private LinkCollection Col = new LinkCollection();
        private int pCount = 0;

        public ReturnMainLinks() { }

        public LinkCollection toLinkCollection() {

            Language.Language LGN = new Language.Language("__USER__","LINKS");

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();

            string command = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE LinkRelationship='nav_main' AND LinkIsActive='true'";
            SqlDataReader Row = Sql.SysReader(command);
            if (Row.HasRows) {
                SettingsHelper.GlobalSettingsHelper GSet = new SettingsHelper.GlobalSettingsHelper();
                while (Row.Read()) {

                    Link Lnk = new Link();
                    Lnk.ID = ((int)Row["id"]);
                    if (Row["LinkType"] != DBNull.Value) Lnk.Type = Row["LinkType"].ToString();
                    if (Row["LinkText"] != DBNull.Value) Lnk.Text = LGN.getLine(Row["LinkText"].ToString());
                    if (GSet.empty(Lnk.Text)) Lnk.Text = Row["LinkText"].ToString() + " NOT FOUND IN PACKAGE:" + LGN.Package;
                    if (Row["LinkHref"] != DBNull.Value) Lnk.Href = Row["LinkHref"].ToString();
                    if (Row["LinkController"] != DBNull.Value) Lnk.TargetController = Row["LinkController"].ToString();
                    if (Row["LinkAction"] != DBNull.Value) Lnk.TargetAction = Row["LinkAction"].ToString();
                    this.Col.Add(Lnk);



                
                
                
                }

            
            
            }
            Row.Close();

            Sql.SysDisconnect();
            return this.Col;

        
        }
    
    
    
    }
    public class ReturnSublinks {
        private LinkCollection Col = new LinkCollection();
        public ReturnSublinks(int RefId) {



            Language.Language LGN = new Language.Language("__USER__", "LINKS");

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();

            string command = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE SublinkFrom=@RefID AND LinkIsActive='true'";
            SqlParameter[] P = new SqlParameter[] { new SqlParameter("@RefID", RefId) };

            SqlDataReader Row = Sql.SysReader(command,P);
            if (Row.HasRows)
            {
                SettingsHelper.GlobalSettingsHelper GSet = new SettingsHelper.GlobalSettingsHelper();
                while (Row.Read())
                {

                    Link Lnk = new Link();
                    Lnk.ID = ((int)Row["id"]);
                    if (Row["LinkType"] != DBNull.Value) Lnk.Type = Row["LinkType"].ToString();
                    if (Row["LinkText"] != DBNull.Value) Lnk.Text = LGN.getLine(Row["LinkText"].ToString());
                    if (GSet.empty(Lnk.Text)) Lnk.Text = Row["LinkText"].ToString() + " NOT FOUND IN PACKAGE:" + LGN.Package;
                    if (Row["LinkHref"] != DBNull.Value) Lnk.Href = Row["LinkHref"].ToString();
                    if (Row["LinkController"] != DBNull.Value) Lnk.TargetController = Row["LinkController"].ToString();
                    if (Row["LinkAction"] != DBNull.Value) Lnk.TargetAction = Row["LinkAction"].ToString();
                    this.Col.Add(Lnk);






                }



            }
            Row.Close();

            Sql.SysDisconnect(); 

        
        
        }
        public LinkCollection toLinkCollection() {


            return this.Col;
        }
    
    
    }
#endregion 
}

namespace nfCMS_NET.CORE.SettingsHelper {

    public class GlobalSettingsHelper {
        public bool SomethingIsEmpty(object[] mixed)
        {

            bool _ret = false;

            foreach (object obj in mixed)
            {

                string test = obj.ToString();

                if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test)) _ret = true;


            }

            return _ret;

        }
        public bool SomethingIsEmpty(object[] mixed, out string emptyobjects)
        {

            bool _ret = false;
            string invalid = "";
            foreach (object obj in mixed)
            {

                string test = obj.ToString();

                if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test))
                {
                    _ret = true;

                    invalid += (String.IsNullOrEmpty(invalid) ? obj.GetType().Name : "," + obj.GetType().Name);

                }

            }
            emptyobjects = invalid;
            return _ret;

        }
        public GlobalSettingsHelper() { }

        
        public bool empty(object obj) {
            bool _return = false;
            string ob = obj.ToString();

            if (String.IsNullOrEmpty(ob)) _return = true;
            if (String.IsNullOrWhiteSpace(ob)) _return = true;

             return _return;
        
        }
        private string pref = new ThisApplication.ThisApplication().getSqlPrefix;

        public string Read(string name) {
           
            Settings.nValueType VT = new Settings.nValueType();
            string _return = "";
            
            Settings.nSetting x = new Settings.GlobalSettings().getSetting(name);
            if (x.Value == null) return "";
            if (x.ValueType == VT.ValueString) _return = x.Value.ToString();

            return _return;

        
        
        
        }

        /// <summary>
        /// Writes an global setting into the database.
        /// </summary>
        /// <param name="name">Name of the Setting</param>
        /// <param name="svalue">Value of the Setting</param>
        /// <param name="newName">If changing the name type here the new name</param>
        public void Write(string name, string svalue, string newName="") {

            
            //if (!empty(name) && !empty(svalue)) {
            //    Settings.GlobalSettings GS = new Settings.GlobalSettings();

            //    Settings.nSetting Temp = new Settings.nSetting();
            //    Settings.nSetting n = GS.getSetting(name);
            //    if (!string.IsNullOrEmpty(newName))
            //    {

                    

            //        Temp.CategoryName = n.CategoryName;
            //        Temp.CategoryID = n.CategoryID;
            //        Temp.PermissionFrontend = n.PermissionFrontend;
            //        Temp.PermissionBackend = n.PermissionBackend;
                    
                       
                  
            //    }
            //    else {

            //        Temp.ID = 0;
            //        Temp.CategoryID = 0;
            //        Temp.CategoryName = "MISC";
            //        n.ID = 0;
            //    }
            //    Temp.Value = svalue;
            //    Temp.ValueType = new Settings.nValueType().ValueString;
            //    Temp.SettingType = new Settings.nSettingType().SettingString;
            //    if (Temp.ValueType == n.ValueType)
            //    {

            //        GS.RemoveSetting(n.ID);


            //        Temp.SettingType = n.SettingType;
            //        GS.AddSetting(Temp);

            //    }
            //    else {

            //        if (n.ID == 0) GS.AddSetting(Temp);
                
            //    }
                
               
            

            
          //  }
        
        
        }
    
    
    }


}

namespace nfCMS_NET.CORE.Language {

    public class Language {

        private string lngcode = null;
        private MembershipUser User = new MemberShip.nProvider.CurrentUser().nUser;
        ThisApplication.ThisApplication AppT = new ThisApplication.ThisApplication();
        /// <summary>
        /// Init the Language  Class with a given Lang-Code
        /// </summary>
        /// <param name="langcode">Lang-Code for this Instance</param>
        /// <param name="package">The Package for the Language Instance (Default: Root)</param>
        public Language(string langcode, string package="Root") {
            if (langcode == "__USER__" && HttpContext.Current.Request.IsAuthenticated) {
                if(User == null)User = new MemberShip.nProvider.CurrentUser().nUser;
                Settings.UserSettings Us = new Settings.UserSettings(User);
            
               object lc = Us.getSetting("langCode").Value;
               if (lc != null) langcode = lc.ToString();
               else langcode = "";
            }
            if (String.IsNullOrEmpty(langcode)) {


                SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

                langcode = GS.Read("GLOBAL_DEFAULT_LANGUAGE");
            
            
            }
            if (String.IsNullOrEmpty(langcode) || langcode == "__USER__")
            {

            //    langcode = System.Configuration.ConfigurationManager.AppSettings["nfcmsLangAtFirstRun"].ToString();
                langcode = "de-DE";
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
        public string GetInstant() {

            return this.getLine(lineName);
        
        }

        /// <summary>
        /// Instant init. This Init activates the  "GetInstant()" Function and returns directly an language line.
        /// Example: Name = new Language("name","Root","deDE").GetInstant();
        /// </summary>
        /// <param name="LangLineName">Name of the language line</param>
        /// <param name="PackageName">Name of the Package</param>
        /// <param name="code">Language code (default: deDE)</param>
        public Language(string LangLineName, string PackageName, string code = "deDe") {

            this.lineName = LangLineName;
            this.pPackage = PackageName;
            this.lngcode = code;
        
        
        }

        private string pPackage ="Root";
        /// <summary>
        /// Sets the Package for this instance. Default: Root
        /// </summary>
        public string Package {
            get{return this.pPackage;}
            set{this.pPackage = value; }
        
        
        }

        /// <summary>
        /// Inserts an Language Line.
        /// </summary>
        /// <param name="name">Name of the Line</param>
        /// <param name="Content">Content of the Line</param>
        public void InsertLine(string name,string Content){
        if(this.getLine(name)!="") throw new Exception("Lang Line allready exists for this Package!");
        string cmd = "INSERT INTO " + AppT.getSqlPrefix + "Language (Name, Content, Package, Code) VALUES (@Name,@Content,@Package,@Code)";
        SqlParameter[] Parameters = new SqlParameter[]{
        new SqlParameter("@Name", name),
        new SqlParameter("@Content", Content),
        new SqlParameter("@Package", this.pPackage),
        new SqlParameter("@Code", this.lngcode),

        
        
        };
        SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            try{
            Sql.SysConnect();
            Sql.SysNonQuery(cmd,Parameters);
            Sql.SysDisconnect();
            
            
            }
            catch(SqlException e){
            
            throw e;
            
            }
            finally{
            
            
            }

        }

        /// <summary>
        /// Loads a Language Line String from the Package (Set by this.Package / Default: Root)
        /// </summary>
        /// <param name="name">Name of the Language Line</param>
        /// <returns>(String)Content</returns>
        public string getLine(string name){
           
        SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
        string cmd = "SELECT TOP 1 * FROM " + AppT.getSqlPrefix + "Language WHERE Package=@Package AND Name=@Name AND Code=@Code";
        SqlParameter[] P = new SqlParameter[] {
        
        new SqlParameter("@Package",this.pPackage),
        new SqlParameter("@Name",name),
        new SqlParameter("@Code",this.lngcode)

        
        
        };
        string line = "";
            try{
            
            Sql.SysConnect();
            SqlDataReader Reader = Sql.SysReader(cmd, P);
            Reader.Read();
            if (Reader.HasRows) line = ((string)Reader["Content"]);

            Sql.SysDisconnect();
            }
            catch (SqlException e) { line = e.Message; }
            finally{}

        
        return line;
        }
    
    
    }
    
    }

namespace nfCMS_NET.CORE.ThisApplication {
    public class ThisApplication {
        public ThisApplication() { 
        
        
        }

        private void createCFGifNotExists(string name,string defaultvalue) {

            if (ConfigurationManager.AppSettings[name] == null) {


                ConfigurationManager.AppSettings.Add(name, defaultvalue);
            
            }
        
        
        }
        /// <summary>
        /// Checks a boundle of Configvars for EXISTS. If any of them doesnt exists this function will create them.
        /// </summary>
        /// <param name="config">Config Syntax:  name=value  for example:  myconfigvar=MyValue</param>
        public void checkConfig(string[] config) {

            for (int x = 0; x < config.Length; x++)
            {

                string[] v = config[x].Split('=');

                if (v.Length > 2 || v.Length < 2) throw new Exception("Incorrect Syntax for Config var: Corrct Format:  name=value    example: <pre><br> MyConfig=true<br>");

                string valueStr = "";
                for (int z = 1; z < v.Length; z++) {
                    valueStr += v[z] + "=";
                }
                if (valueStr.EndsWith("=")) valueStr = valueStr.Remove(valueStr.LastIndexOf("="));
                this.createCFGifNotExists(v[0], valueStr);
            };
        
        
        }

        public string BaseUrl {

            get {

                try
                {
                
                

              

                    if(HttpContext.Current.Request.Url.Scheme.ToLower() == "https")

                        return System.Configuration.ConfigurationManager.AppSettings["nfcmsBaseUrlHTTPS"];
                    else return System.Configuration.ConfigurationManager.AppSettings["nfcmsBaseUrlHTTP"];
                
                
                
                }
                catch {
                    return null;
                
                }
            
            }
        
        }

        public string ApplicationName
        {
            get { return HttpContext.Current.Request.ApplicationPath.Substring(1, HttpContext.Current.Request.ApplicationPath.Length - 1); }

        }

        public string getSqlPrefix {

            get {

                string str = "";
                try
                {
                    str = ConfigurationManager.AppSettings["nfcmsSQLPrefix"];
                }
                catch (StackOverflowException e) {


                    throw e;
                
                }
                return str;
            }
            set {

                @ConfigurationManager.AppSettings["nfcmsSQLPrefix"] = value;
            
            }
        
        
        
        }
    
    }


}

namespace nfCMS_NET.CORE.SqlHelper
{

    public class nSqlParameterCollection : IEnumerable
    {


        private List<SqlParameter> pCol = new List<SqlParameter>();
        public nSqlParameterCollection()
        {





        }
        public void Add(SqlParameter Parameteritem)
        {
            this.pCol.Add(Parameteritem);


        }
        public void Add(string name, object val)
        {
            SqlParameter Parameteritem = new SqlParameter(name, val.ToString());
            this.pCol.Add(Parameteritem);


        }
        public int Count { get { return this.pCol.Count; } }
        public IEnumerator<SqlParameter> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }


    public class SqlHelper
    {

        private string parseQuery(string query)
        {

            return query.Replace("__PREFIX__", (new ThisApplication.ThisApplication().getSqlPrefix));


        }






        private string GetConnectionString(string str)
        {
            //variable to hold our return value
            string conn = string.Empty;
            //check if a value was provided
            if (!string.IsNullOrEmpty(str))
            {
                //name provided so search for that connection
                conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            else
            //name not provided, get the 'default' connection
            {
                throw new Exception("ERROR IN SYS.CORE", new Exception("Required Connection String name is empty or null"));
            }
            //return the value
            return conn;
        }
        private SqlConnection SysConnection = new SqlConnection();

        /// <summary>
        /// Fills an System.Data.DataSet by using a internal SqlDataAdapter (System.Data.SqlClient)
        /// </summary>
        /// <param name="queryString">Select Querystring for the Adapter</param>
        /// <returns>System.Data.DataSet</returns>
        public static DataSet SysUseAdapter(string queryString)
        {
            DataSet Set = new DataSet();

            string constr = ConfigurationManager.ConnectionStrings["nfCMS"].ConnectionString;
            using (SqlConnection ncon = new SqlConnection(constr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, ncon);
                adapter.Fill(Set);
            }
            return Set;
        }
        public SqlCommand SysCommand(string querystring)
        {
            SqlCommand CMD = new SqlCommand(querystring);
            CMD.Connection = this.SysConnection;
            return CMD;

        }



        public SqlHelper()
        {

            SqlConnection temp = new SqlConnection();
            temp.ConnectionString = this.GetConnectionString("nfCMS");
            ///* connection string building for sql and oledb from 1st line to 5th line*/
            SqlConnectionStringBuilder Obj_sqnbuild =
       new SqlConnectionStringBuilder();//making the instance for 
            //the sqlConnection String builder
            //Obj_sqnbuild.InitialCatalog = temp.;
            //Obj_sqnbuild.DataSource = "DBSERVER";
            //Obj_sqnbuild.UserID = "sa";
            //Obj_sqnbuild.Password = "db_Ser3er_2009";
            Obj_sqnbuild.ConnectionString = temp.ConnectionString;
            Obj_sqnbuild.Add("Max pool size", 1500);
            Obj_sqnbuild.Add("Min pool size", 20);
            Obj_sqnbuild.Add("Pooling", true);

            this.SysConnection.ConnectionString = Obj_sqnbuild.ConnectionString;

        }
        /// <summary>
        /// Returns the Last ID of a Table
        /// </summary>
        /// <param name="table">Tablename with PREFIX</param>
        /// <returns>-1 if no id was found or the id as integer</returns>
        public int getLastId(string table)
        {

            string query = "SELECT IDENT_CURRENT(@table) as id";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@table", table);
            SqlDataReader R = this.SysReader(query, PCOL);
            int id = 0;
            if (R.HasRows)
            {

                R.Read();
                string res = R["id"].ToString();
                int.TryParse(res, out id);
            }
            R.Close();
            return id;
        }

        /// <summary>
        /// Connects to the Database
        /// </summary>
        public void SysConnect()
        {
            try
            {
                if (this.SysConnection.State == ConnectionState.Open)
                {

                    this.SysConnection.Close();
                }
                this.SysConnection.Open();

            }
            catch (SqlException e2) { throw e2; }

            if (this.SysConnection.State != ConnectionState.Open)
            {

                throw new Exception("Unable connect. Server is reachable and but we get no OK");

            }

        }

        /// <summary>
        /// Disconnects from the Database
        /// </summary>
        public void SysDisconnect()
        {

            if (this.SysConnection.State == System.Data.ConnectionState.Open)
            {

                this.SysConnection.Close();


            }

        }

        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter Array (System.Data.SqlClient.SqlParameter[])</param>
        public void SysNonQuery(string commandstring, nSqlParameterCollection parameter = null)
        {





            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }


            }
            Command.ExecuteNonQuery();
            Command.Dispose();

        }


        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter Array (System.Data.SqlClient.SqlParameter[])</param>
        public void SysNonQuery(string commandstring, SqlParameter[] parameter = null)
        {





            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }


            }
            Command.ExecuteNonQuery();
            Command.Dispose();

        }
        /// <summary>
        /// Executes an non query command to the Database.
        /// </summary>
        /// <param name="commandstring">SQL Command String for executing - INSERT/UPDATE/DELETE etc.(string)</param>
        /// <param name="parameter">Optional: Parameter List</param>
        public void SysNonQuery(string commandstring, List<SqlParameter> parameter = null)
        {





            SqlCommand Command = new SqlCommand(commandstring);
            Command.Connection = this.SysConnection;
            if (parameter != null)
            {
                foreach (SqlParameter P in parameter)
                {
                    if (P.Value == null) P.Value = DBNull.Value;

                    Command.Parameters.Add(P);
                }


            }
            Command.ExecuteNonQuery();
            Command.Dispose();

        }

        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, SqlParameter[] parameter = null)
        {

            SqlCommand C = new SqlCommand(querystring);
            C.Connection = this.SysConnection;
            if (parameter != null) C.Parameters.AddRange(parameter);
            return C.ExecuteReader();
        }



        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, List<SqlParameter> parameter)
        {

            SqlCommand C = new SqlCommand(querystring);
            C.Connection = this.SysConnection;
            if (parameter != null)
            {

                foreach (SqlParameter P in parameter)
                {
                    C.Parameters.Add(P);
                }

            }
            return C.ExecuteReader();
        }
        /// <summary>
        /// Executes an System.Data.SqlClient.SqlDataReader to the Database.
        /// </summary>
        /// <param name="querystring">Selection String for the query (SELECT * FROM)  (string)</param>
        /// <param name="parameter">Optional: Parameters for the SQL Query (System.Data.SqlClient.SqlParameter[]</param>
        /// <returns>Opened SqlDataReader</returns>
        public SqlDataReader SysReader(string querystring, nSqlParameterCollection parameter)
        {

            SqlCommand C = new SqlCommand(querystring);

            C.Connection = this.SysConnection;
            if (parameter != null)
            {

                foreach (SqlParameter P in parameter)
                {
                    C.Parameters.Add(P);
                }

            }

            return C.ExecuteReader();
        }
        public int SysCount(string SQLTABLE, string identifycol = "", string identifyvalue = "")
        {

            string c = "SELECT * FROM " + new ThisApplication.ThisApplication().getSqlPrefix + SQLTABLE;
            SqlParameter[] P = new SqlParameter[1];
            if (!String.IsNullOrEmpty(identifycol) && !String.IsNullOrEmpty(identifyvalue))
            {

                c += " WHERE " + identifycol + " = @p";

                P[0] = new SqlParameter("@p", identifyvalue);
            }

            SqlDataReader R = this.SysReader(c, P);
            int i = 0;
            while (R.Read())
            {

                i++;

            }
            R.Close();

            return i;
        }


        public dynamic getModel(object TableModel)
        {
            //Checkup for required things doh!

            if (TableModel.GetType().GetProperty("TableName") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.string) TableName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (TableModel.GetType().GetProperty("IdentityName") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.string) IdentityName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (TableModel.GetType().GetProperty("IdentityValue") == null) throw new Exception("SqlHelper :: getModel(object TableModel) - Missing required Property '(System.object) IdentityValue' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);

            string table = TableModel.GetType().GetProperty("TableName").GetValue(TableModel, null).ToString();

            string identityCol = TableModel.GetType().GetProperty("IdentityName").GetValue(TableModel, null).ToString();
            object identityValue = TableModel.GetType().GetProperty("IdentityValue").GetValue(TableModel, null);

            if (String.IsNullOrEmpty(table)) throw new Exception("SqlHelper :: getModel(object TableModel) - Property cannot be empty '(System.string) TableName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (String.IsNullOrEmpty(identityCol)) throw new Exception("SqlHelper :: getModel(object TableModel) - Property cannot be empty '(System.string) IdentityName' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
            if (String.IsNullOrEmpty(identityValue.ToString())) throw new Exception("SqlHelper :: getModel(object TableModel) -Property cannot be empty '(System.object) IdentityValue' in class (" + TableModel.GetType() + ") " + TableModel.GetType().Name);
                
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string pref = TA.getSqlPrefix;
            identityCol = identityCol.Replace("'", "").Replace("-", "").Replace("#", "");

            System.Reflection.PropertyInfo[] Propcols = TableModel.GetType().GetProperties();
            string selectColumns = "";

            List<string> TblCols = new List<string>();
            foreach (System.Reflection.PropertyInfo Prop in Propcols)
            {
                if (Prop.Name != "TableName" && Prop.Name != "IdentityName" && Prop.Name != "IdentityValue")
                {
                    if (selectColumns == "") selectColumns = Prop.Name;
                    else selectColumns += ", " + Prop.Name;
                    TblCols.Add(Prop.Name);
                }

            }
            string query = "SELECT TOP 1 " + selectColumns + "  FROM " + pref + table + " WHERE " + identityCol + " = @id";

            nSqlParameterCollection Pcol = new nSqlParameterCollection();

            Pcol.Add("@id", identityValue);
            dynamic model = new { };
            SqlDataReader R = this.SysReader(query, Pcol);
            object temp = "";
            if (R.HasRows)
            {
                while (R.Read())
                {

                    foreach (string col in TblCols) {

                        if (R[col] != null) {

                            TableModel.GetType().GetProperty(col).SetValue(TableModel, Convert.ChangeType(R[col].ToString(), TableModel.GetType().GetProperty(col).PropertyType), null);

                        
                        }
                    
                    
                    
                    }
                }

            }


            model = TableModel;

            //Required Props for Models!!!
            model.TableName = table;
            model.IdentityName = identityCol;
            model.IdentityValue = identityValue;


            return model;
        }
    }
}