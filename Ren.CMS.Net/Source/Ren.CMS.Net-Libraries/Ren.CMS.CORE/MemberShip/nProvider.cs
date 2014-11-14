namespace Ren.CMS.MemberShip
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE.SettingsHelper;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;

    public class nProvider : MembershipProvider
    {
        #region Fields

        private string applicationName;
        private string connectionString;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private int newPasswordLength = 8;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private string passwordStrengthRegularExpression;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private string sqlprefix = new ThisApplication().getSqlPrefix;
        string test = "";
        private int userIsOnlineTimeWindow;

        #endregion Fields

        #region Constructors

        public nProvider()
        {
            MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
            string defaultProvider = membershipSection.DefaultProvider;
            ProviderSettings providerSettings = membershipSection.Providers[defaultProvider];

            this.applicationName = (providerSettings.Parameters["applicationName"] != null ? providerSettings.Parameters["applicationName"].ToString() : "");
            this.enablePasswordReset = (providerSettings.Parameters["enablePasswordReset"] != null ? parseBoolean(providerSettings.Parameters["enablePasswordReset"].ToString()) : false);
            this.enablePasswordRetrieval = (providerSettings.Parameters["enablePasswordRetrieval"] != null ? parseBoolean(providerSettings.Parameters["enablePasswordRetrieval"].ToString()) : false);
            this.maxInvalidPasswordAttempts = (providerSettings.Parameters["maxInvalidPasswordAttempts"] != null ? parseInt(providerSettings.Parameters["maxInvalidPasswordAttempts"].ToString()) : 0);
            this.minRequiredNonAlphanumericCharacters = (providerSettings.Parameters["minRequiredNonAlphanumericCharacters"] != null ? parseInt(providerSettings.Parameters["minRequiredNonAlphanumericCharacters"].ToString()) : 0);
            this.minRequiredPasswordLength = (providerSettings.Parameters["minRequiredPasswordLength"] != null ? parseInt(providerSettings.Parameters["minRequiredPasswordLength"].ToString()) : 0);
            this.newPasswordLength = (providerSettings.Parameters["newPasswordLength"] != null ? parseInt(providerSettings.Parameters["newPasswordLength"].ToString()) : 0);
            this.passwordAttemptWindow = (providerSettings.Parameters["passwordAttemptWindow"] != null ? parseInt(providerSettings.Parameters["passwordAttemptWindow"].ToString()) : 0);
            this.passwordFormat = (providerSettings.Parameters["passwordFormat"] != null ? parsePasswordFormat(providerSettings.Parameters["passwordFormat"].ToString()) : MembershipPasswordFormat.Encrypted);
            this.passwordStrengthRegularExpression = (providerSettings.Parameters["passwordStrengthRegularExpression"] != null ? providerSettings.Parameters["passwordStrengthRegularExpression"].ToString() : "");
            this.requiresQuestionAndAnswer = (providerSettings.Parameters["requiresQuestionAndAnswer"] != null ? parseBoolean(providerSettings.Parameters["requiresQuestionAndAnswer"].ToString()) : false);
            this.requiresUniqueEmail = (providerSettings.Parameters["requiresUniqueEmail"] != null ? parseBoolean(providerSettings.Parameters["requiresUniqueEmail"].ToString()) : false);
            this.userIsOnlineTimeWindow = (providerSettings.Parameters["userIsOnlineTimeWindow"] != null ? parseInt(providerSettings.Parameters["userIsOnlineTimeWindow"].ToString()) : 0);
        }

        #endregion Constructors

        #region Properties

        public override string ApplicationName
        {
            get
            {
                return this.applicationName;
            }
            set
            {
                this.applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return this.enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return this.enablePasswordRetrieval;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return passwordFormat;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return this.requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get { return this.requiresUniqueEmail; }
        }

        public int UserIsOnlineTimeWindow
        {
            get { return this.userIsOnlineTimeWindow; }
            set { this.userIsOnlineTimeWindow = value; }
        }

        #endregion Properties

        #region Methods

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            SqlHelper dbo = new SqlHelper();
            bool ret = false;
            try
            {

                dbo.SysConnect();
                if (this.ValidateUser(username, oldPassword))
                {
                    newPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(newPassword)));
                    SqlParameter[] Paras = new SqlParameter[4];
                    Paras[0] = new SqlParameter("@username", username);
                    Paras[1] = new SqlParameter("@oldPassword", oldPassword);
                    Paras[2] = new SqlParameter("@newPassword", newPassword);
                    Paras[3] = new SqlParameter("@App", this.ApplicationName);
                    dbo.SysNonQuery("UPDATE " + sqlprefix + "Users SET Password=@newPassword WHERE Username=@username AND Password=@oldPassword AND ApplicationName=@App", Paras);
                    ret = true;
                }

            }
            finally
            {

            }
            dbo.SysDisconnect();

            return ret;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            SqlHelper dbo = new SqlHelper();
            bool ret = false;
            try
            {
                dbo.SysConnect();
                if (this.ValidateUser(username, password))
                {

                    SqlParameter[] Paras = new SqlParameter[] {

                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password),
                    new SqlParameter("@question", newPasswordQuestion),
                    new SqlParameter("@answer",newPasswordAnswer),
                    new SqlParameter("@app", this.ApplicationName)

                    };

                    dbo.SysNonQuery("UPDATE " + sqlprefix + "Users SET PasswordQuestion=@question,PasswordAnswer=@answer WHERE Username=@username AND Password=@password AND ApplicationName=@app", Paras);
                    ret = true;

                }
                else
                {

                    ret = false;
                }

            }
            finally
            {

            }
            dbo.SysDisconnect();

            return ret;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return this.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status, string permissionGroup = "")
        {
            SqlHelper Helper = new SqlHelper();
            Helper.SysConnect();

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                u = null;
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }
                string cmd = "INSERT INTO " + sqlprefix + "Users " +
                          " (PKID, Username,Loginname, Password, Email, PasswordQuestion, " +
                          " PasswordAnswer, IsApproved," +
                          " Comment, CreationDate, LastPasswordChangedDate, LastActivityDate," +
                          " ApplicationName, IsLockedOut, LastLockedOutDate," +
                          " FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart, " +
                          " FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart)" +
                          " Values(@PKID,@Username, @Loginname, @Password, @Email, @PasswordQuestion,@PasswordAnswer,@IsApproved, @Comment, @CreationDate, @LastPasswordChangedDate, @LastActivityDate, @ApplicationName, @IsLockedOut, @LastLockedOutDate, @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart, @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart)";

                SqlParameter[] Parameters = new SqlParameter[] {

                new SqlParameter("@PKID", providerUserKey),

                new SqlParameter("@Username", username),
                new SqlParameter("@Loginname", username),
                new SqlParameter("@Password",  Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)))),
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordQuestion", passwordQuestion),
                new SqlParameter("@PasswordAnswer", passwordAnswer),
                new SqlParameter("@IsApproved", "No"),
                new SqlParameter("@Comment", "Created on <"+ DateTime.Now +">"),
                new SqlParameter("@CreationDate",DateTime.Now),
                new SqlParameter("@LastPasswordChangedDate", createDate),
                new SqlParameter("@LastActivityDate", createDate),
                new SqlParameter("@ApplicationName", this.ApplicationName),
                new SqlParameter("@IsLockedOut", "No"),
                new SqlParameter("@LastLockedOutDate", null),
                new SqlParameter("@FailedPasswordAttemptCount", 0),
                new SqlParameter("@FailedPasswordAttemptWindowStart", 0),
                new SqlParameter("@FailedPasswordAnswerAttemptCount", 0),
                new SqlParameter("@FailedPasswordAnswerAttemptWindowStart", 0),

                };

                status = MembershipCreateStatus.Success;
                try
                {
                    Helper.SysNonQuery(cmd, Parameters);
                    u = this.GetUser(providerUserKey, false);
                    this.SetPermissionGroup(u, permissionGroup);

                }
                catch(Exception e)
                {

                    status = MembershipCreateStatus.ProviderError;
                    throw e;
                }

            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            Helper.SysDisconnect();

            return u;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return true;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection Col = new MembershipUserCollection();
            string countCMD = "with pagination as " +
            "( " +
            " SELECT dense_rank() over (order by Username) as rowNo,PKID, Username, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate," +
                  " IsSubscriber" +
                  " FROM " + sqlprefix + "Users WHERE Email LIKE '%'+ @Email +'%'" +

            ")" +
            " " +
            "select " +

            "    *, (select count(*) from pagination) as TotalRows " +
            "from " +
            "    pagination " +
            "where " +
            "    RowNo between @i and @ii  " +
            "order by " +
            "    rowNo";

            int i = (pageIndex * pageSize) - pageSize;
            int ii = pageIndex * pageSize;

            SqlParameter[] Paras = new SqlParameter[]{

            new SqlParameter("@i",i),
              new SqlParameter("@Email",emailToMatch),
            new SqlParameter("@ii",ii)

            };
            SqlHelper Sql = new SqlHelper();
            int itotalRecords = 0;
            try
            {

                Sql.SysConnect();
                SqlDataReader Reader = Sql.SysReader(countCMD, Paras
                    );

                while (Reader.Read())
                {
                    MembershipUser u = this.GetUser(Reader.GetValue(0), false);
                    if (u != null) Col.Add(u);
                    itotalRecords = ((int)Reader["TotalRows"]);
                }

            }
            catch
            {
                itotalRecords = 0;
            }
            finally
            {

            }
            Sql.SysDisconnect();

            totalRecords = itotalRecords;
            return Col;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection Col = new MembershipUserCollection();
            string countCMD = "with pagination as " +
            "( " +
            " SELECT dense_rank() over (order by Username) as rowNo,PKID, Username, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate," +
                  " IsSubscriber" +
                  " FROM " + sqlprefix + "Users WHERE Username LIKE '%'+ @user_Name +'%'" +

            ")" +
            " " +
            "select " +

            "    *, (select count(*) from pagination) as TotalRows " +
            "from " +
            "    pagination " +
            "where " +
            "    RowNo between @i and @ii  " +
            "order by " +
            "    rowNo";

            int i = (pageIndex * pageSize) - pageSize;
            int ii = pageIndex * pageSize;

            SqlParameter[] Paras = new SqlParameter[]{

            new SqlParameter("@i",i),
              new SqlParameter("@user_Name",usernameToMatch),
            new SqlParameter("@ii",ii)

            };
            SqlHelper Sql = new SqlHelper();
            int itotalRecords = 0;
            try
            {

                Sql.SysConnect();
                SqlDataReader Reader = Sql.SysReader(countCMD, Paras
                    );

                while (Reader.Read())
                {
                    MembershipUser u = this.GetUser(Reader.GetValue(0), false);
                    if (u != null) Col.Add(u);
                    itotalRecords = ((int)Reader["TotalRows"]);
                }

            }
            catch
            {
                itotalRecords = 0;
            }
            finally
            {

                }
            Sql.SysDisconnect();

            totalRecords = itotalRecords;
            return Col;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection Col = new MembershipUserCollection();
            string countCMD = "with pagination as " +
            "( " +
            " SELECT dense_rank() over (order by Username) as rowNo,PKID, Username, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate," +
                  " IsSubscriber" +
                  " FROM " + sqlprefix + "Users" +

            ")" +
            " " +
            "select " +

            "    *, (select count(*) from pagination) as TotalRows " +
            "from " +
            "    pagination " +
            "where " +
            "    RowNo between @i and @ii  " +
            "order by " +
            "    rowNo";

            int i = (pageIndex * pageSize) - pageSize;
            int ii = pageIndex * pageSize;

            SqlParameter[] Paras = new SqlParameter[]{

            new SqlParameter("@i",i),
            new SqlParameter("@ii",ii)

            };
            SqlHelper Sql = new SqlHelper();
            int itotalRecords = 0;
            try
            {

                Sql.SysConnect();
                SqlDataReader Reader = Sql.SysReader(countCMD, Paras
                    );

                while (Reader.Read())
                {
                    MembershipUser u = this.GetUser(Reader.GetValue(0), false);
                    if (u != null) Col.Add(u);
                    itotalRecords = ((int)Reader["TotalRows"]);
                }

            }
            catch
            {
                itotalRecords = 0;
            }
            finally
            {

                    }
            Sql.SysDisconnect();

            totalRecords = itotalRecords;
            return Col;
        }

        public override int GetNumberOfUsersOnline()
        {
            int _return = 0;

            int TimeWindow = 15;
            try
            {
                TimeWindow = this.UserIsOnlineTimeWindow;

            }
            catch { }
            DateTime LA = DateTime.Now;
            TimeSpan TS = new TimeSpan(0, TimeWindow, 0);
            LA.Subtract(TS);

            SqlHelper Sql = new SqlHelper();
            string cmd = "SELECT COUNT(*) as UserOnline FROM " + sqlprefix + "Users WHERE LastActivityDate>=?";
            SqlParameter[] P = new SqlParameter[] {

            new SqlParameter("@LastActivityDate", LA)

            };
            try
            {

                Sql.SysConnect();
                SqlDataReader Reader = Sql.SysReader(cmd, P);
                Reader.Read();
                _return = ((int)Reader["UserOnline"]);
                Reader.Close();
            }
            catch { }
            finally
            {

            }
            Sql.SysDisconnect();
            return _return;
        }

        public override string GetPassword(string username, string answer)
        {
            MembershipUser u = this.GetUser(username, false);

            if (u != null)
            {
                if (answer == null)
                {
                    Dictionary<string,object> Userdata = this.GetUserDataRow(u.ProviderUserKey);
                    answer = Userdata.Where(user => user.Key == "PasswordAnswer").First().Value.ToString();

                }
                SqlHelper Sql = new SqlHelper();
                Sql.SysConnect();

                SqlParameter[] P = new SqlParameter[] {

                    new SqlParameter("@PasswordAnswer", answer),
                    new SqlParameter("@Username",username)

                };

                SqlDataReader R = Sql.SysReader("SELECT TOP 1 * FROM " + sqlprefix + "Users WHERE (PasswordAnswer=@PasswordAnswer OR PasswordAnswer IS NULL) AND Username=@Username",
                                                    P);
                if (R.HasRows)
                {

                    R.Read();
                    byte[] pw = Encoding.Unicode.GetBytes(R["Password"].ToString());
                    string pwstr = R["Password"].ToString();
                    R.Close();
                    Sql.SysDisconnect();

                    if (this.PasswordFormat == MembershipPasswordFormat.Clear)
                        return base.DecryptPassword(pw).ToString();
                    else return pwstr;
                }
                else
                {
                    R.Close();
                    Sql.SysDisconnect();
                    throw new MembershipPasswordException("No Match");
                }
            }
            else
            {

                return "";
            }
        }

        public string GetPasswordAnswerForUser(object PKID)
        {
            string query = "SELECT PasswordAnswer as answer FROM " + (new ThisApplication().getSqlPrefix) + "Users WHERE PKID=@PKID";
            nSqlParameterCollection PCOL = new nSqlParameterCollection(){
                                                                            {"@PKID", PKID}
            };
            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();

            SqlDataReader Answer = SQL.SysReader(query, PCOL);
            string answer = "";
            if (Answer.HasRows)
            {

                Answer.Read();

                answer = (Answer["answer"] != DBNull.Value ? (string)Answer["answer"] : "");

            }
            Answer.Close();

            SQL.SysDisconnect();
            return answer;
        }

        public string GetPermissionGroupNameForUser(object PKID)
        {
            string query = "SELECT PermissionGroup as groupName FROM " + (new ThisApplication().getSqlPrefix) + "Users WHERE PKID=@PKID";
            nSqlParameterCollection PCOL = new nSqlParameterCollection(){
                                                                            {"@PKID", PKID}
            };
            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();

            SqlDataReader Group =  SQL.SysReader(query, PCOL);
            string groupname = "undefined";
            if (Group.HasRows)
            {

                Group.Read();

                groupname = (Group["groupName"] != DBNull.Value ? (string)Group["groupName"] : "NULL");

            }
            Group.Close();

            SQL.SysDisconnect();
            return groupname;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SqlHelper Sql = new SqlHelper();

            string cmd = "SELECT PKID, Username, Email, PasswordQuestion," +
                 " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                 " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate," +
                 " IsSubscriber, CustomerID" +
                 " FROM " + sqlprefix + "Users  WHERE Username = @Username AND ApplicationName = @ApplicationName";
            SqlParameter[] Parameters = new SqlParameter[] {
            new SqlParameter("@Username", username),
            new SqlParameter("@ApplicationName", ApplicationName)

            };

            MembershipUser u = null;

            SqlDataReader reader = null;

            try
            {
                Sql.SysConnect();

                reader = Sql.SysReader(cmd, Parameters);

                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);

                    if (userIsOnline)
                    {
                        string updateCmd = "UPDATE " + sqlprefix + "Users  " +
                                  "SET LastActivityDate = @LastActivityDate " +
                                  "WHERE Username = @Username AND Applicationname = @ApplicationName";
                        SqlParameter[] Parameters2 = new SqlParameter[] {

                            new SqlParameter("@Username",username),
                            new SqlParameter("@LastActivityDate",DateTime.Now),
                            new SqlParameter("@ApplicationName", ApplicationName)

                        };
                        Sql.SysNonQuery(updateCmd, Parameters2);
                    }
                }

            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write(e.Message);
                HttpContext.Current.Response.Write(e.StackTrace);
                HttpContext.Current.Response.Write("APPID="+ApplicationName);
                HttpContext.Current.Response.Write("USERNAME=" + username);
                HttpContext.Current.Response.Write("Provider=" + this.Name);

            }
            finally
            {

            }
            if (reader != null) { reader.Close(); }

            Sql.SysDisconnect();
            return u;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            Guid PKID = Guid.NewGuid();
            string[] pkid = new string[10];
            if (providerUserKey.GetType() == typeof(string[]))
                pkid = (string[])providerUserKey;
            else
                pkid[0] = providerUserKey.ToString();

            Guid.TryParse(pkid[0], out PKID);
            SqlHelper Sql = new SqlHelper();
            string cmd = "SELECT PKID, Username, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate," +
                  " IsSubscriber" +
                  " FROM " + sqlprefix + "Users  WHERE PKID = @PKID";
            SqlParameter[] Parameters = new SqlParameter[] {
            new SqlParameter("@PKID",PKID)

            };

            MembershipUser u = null;
            SqlDataReader reader = null;

            try
            {
                Sql.SysConnect();

                reader = Sql.SysReader(cmd, Parameters);

                if (reader.HasRows)
                {

                    reader.Read();
                    u = GetUserFromReader(reader);

                    if (userIsOnline)
                    {
                        string updateCmd = "UPDATE " + sqlprefix + "Users  " +
                                  "SET LastActivityDate = @LastActivityDate " +
                                  "WHERE PKID = @PKID";

                        SqlParameter[] Parameters2 = new SqlParameter[] {

                        new SqlParameter("@LastActivityDate", DateTime.Now),
                        new SqlParameter("@PKID", pkid[0])

                        };
                        Sql.SysNonQuery(updateCmd, Parameters2);

                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                }
            if (reader != null) { reader.Close(); }

            Sql.SysDisconnect();

            return u;
        }

        public Dictionary<string, object> GetUserDataRow(object PKID)
        {
            string[] pkid = new string[11];
            if (PKID.GetType() == typeof(string[]))
                pkid = (string[])PKID;
            else
                pkid[0] = PKID.ToString();

            string query = "SELECT TOP 1 * FROM " + (new ThisApplication().getSqlPrefix) + "Users WHERE PKID=@PKID";
            nSqlParameterCollection PCOL = new nSqlParameterCollection(){
                                                                            {"@PKID", pkid[0]}
            };
            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();
            Dictionary<string, object> data = new Dictionary<string, object>();

            SqlDataReader Row = SQL.SysReader(query, PCOL);

            if (Row.HasRows)
            {
                Row.Read();

                for(int i = 0; i< Row.FieldCount; i++)
                    data.Add(Row.GetName(i),
                        (Row[i] != DBNull.Value ?
                        (object)Row[i] : ""));

            }
            Row.Close();

            SQL.SysDisconnect();
            return data;
        }

        public override string GetUserNameByEmail(string email)
        {
            if (String.IsNullOrEmpty(email)) return String.Empty;
            SqlHelper Sql = new SqlHelper();
            string cmd = "SELECT TOP 1 * FROM " + sqlprefix + "Users WHERE Email=@Email";
            SqlParameter[] Parameter = new SqlParameter[] {

            new SqlParameter("@Email",email)

            };
            string __return = "";
            try
            {

                Sql.SysConnect();

                SqlDataReader User = Sql.SysReader(cmd, Parameter);
                User.Read();
                if (User.HasRows) __return = ((string)User["Username"]);
                User.Close();

            }
            catch { }
            finally
            {

            }

            Sql.SysDisconnect();
            return __return;
        }

        public string GetUsersPermissionGroup(object providerUserKey)
        {
            string[] id = new string[10];
            if (providerUserKey.GetType() == typeof(string[]))
            {

                id = (string[])providerUserKey;

            }
            else
            {
                id[0] = providerUserKey.ToString();
            }

            string query = "SELECT PermissionGroup FROM " + (new ThisApplication().getSqlPrefix) + "Users WHERE PKID=@id";
            SqlHelper SQL = new SqlHelper();
            string group = "";

            SQL.SysConnect();

            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection() { { "@id", id[0] } });

            if (R.HasRows)
            {
                group = R[0].ToString();
            }

            R.Close();
            SQL.SysDisconnect();

            return group;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustomMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Membership Provider");
            }

            base.Initialize(name, config);

            this.applicationName = GetConfigValue(config["applicationName"],
                          System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            this.maxInvalidPasswordAttempts = Convert.ToInt32(
                          GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            this.passwordAttemptWindow = Convert.ToInt32(
                          GetConfigValue(config["passwordAttemptWindow"], "10"));
            this.minRequiredNonAlphanumericCharacters = Convert.ToInt32(
                     GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            this.minRequiredPasswordLength = Convert.ToInt32(
                          GetConfigValue(config["minRequiredPasswordLength"], "6"));
            this.enablePasswordReset = Convert.ToBoolean(
                          GetConfigValue(config["enablePasswordReset"], "true"));
            this.passwordStrengthRegularExpression = Convert.ToString(
                           GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        }

        public bool LockUser(string Username)
        {
            bool __return = false;
            MembershipUser u = this.GetUser(Username, true);
            if (u != null)
            {

                if (!u.IsLockedOut)
                {

                    SqlHelper Sql = new SqlHelper();
                    string cmd = "UPDATE " + sqlprefix + "Users SET IsLockedOut=@IsLockedOut WHERE PKID=@PKID";
                    SqlParameter[] Parameter = new SqlParameter[]{

                    new SqlParameter("@IsLockedOut",true),
                    new SqlParameter("@PKID",u.ProviderUserKey)

                    };
                    try
                    {
                        Sql.SysConnect();

                        Sql.SysNonQuery(cmd, Parameter);

                        __return = true;
                    }
                    catch { }
                    finally { Sql.SysDisconnect(); }

                }

            }
            return __return;
        }

        public override string ResetPassword(string loginname, string answer)
        {
            if (this.GetPassword(loginname, answer) != "")
            {
                MembershipUser u = this.GetUser(loginname, false);

                int min = this.MinRequiredPasswordLength;
                string[] passwordletters = new string[] {
                //small
                "a","b","c","d,","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                //Big
                "A","B","C","D,","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                //Numbers
                "1","2","3","4","5","6", "7","8","9","0",
                //Special Chars
                "!","§","$","%","&","?","ß","="

                };

                string newPWD = "";

                for (int x = 0; x <= min; x++)
                {

                    Random Rnd = new Random();
                    Rnd.Next(0, passwordletters.Length - 1);
                    int i = Convert.ToInt32(Rnd);
                    newPWD += passwordletters[i];

                }
                //New PWD retrieved. Update User.
                this.ChangePassword(loginname, this.GetPassword(loginname, answer), newPWD);

                return newPWD;
            }
            else
            {
                throw new MembershipPasswordException("Answer not correct");

            }
        }

        public void SetPermissionGroup(MembershipUser U, string groupName)
        {
            if (String.IsNullOrEmpty(groupName)) groupName = this.getDefaultPermissiongroup();

            SqlHelper SQL = new SqlHelper();
            string query = "UPDATE " + (new ThisApplication().getSqlPrefix) + "Users SET PermissionGroup=@group WHERE PKID=@id";
            string check = "SELECT * FROM " + (new ThisApplication().getSqlPrefix) + "PermissionGroups WHERE groupName=@name";
            SQL.SysConnect();
            SqlDataReader Row = SQL.SysReader(check, new nSqlParameterCollection() { { "@name", groupName } });
            if (Row.HasRows)
            {
                Row.Close();

                SQL.SysNonQuery(query, new nSqlParameterCollection() { { "@group", groupName}, {"@id", U.ProviderUserKey} });

            }
            SQL.SysDisconnect();
        }

        public override bool UnlockUser(string loginName)
        {
            bool __return = false;
            MembershipUser u = GetUser(loginName, false);
            if (u != null)
            {

                if (u.IsLockedOut)
                {

                    SqlHelper Sql = new SqlHelper();
                    string cmd = "UPDATE " + sqlprefix + "Users SET IsLockedOut=@IsLockedOut WHERE PKID=@PKID";
                    SqlParameter[] Parameter = new SqlParameter[]{

                    new SqlParameter("@IsLockedOut",false),
                    new SqlParameter("@PKID",u.ProviderUserKey)

                    };
                    try
                    {
                        Sql.SysConnect();

                        Sql.SysNonQuery(cmd, Parameter);

                        __return = true;
                    }
                    catch { }
                    finally { Sql.SysDisconnect(); }

                }

            }
            return __return;
        }

        public override void UpdateUser(MembershipUser user)
        {
            string pGroup = this.GetUsersPermissionGroup(user.ProviderUserKey);

            this.UpdateUser(user, pGroup);
        }

        public void UpdateUser(MembershipUser user, string permissionGroup)
        {
            ThisApplication TAPP = new ThisApplication();
            string QUERY = "UPDATE " + TAPP.getSqlPrefix + "Users " +
               " SET Email = @Email, Comment = @Comment," +
               " IsApproved = @IsApproved, PermissionGroup=@Group" +
               " WHERE Username = @Username AND ApplicationName = @ApplicationName";

            SqlHelper Helper = new SqlHelper();
            try
            {

                Helper.SysConnect();

                SqlParameter[] Parameter = new SqlParameter[] {
                new SqlParameter("@Email",user.Email),
                new SqlParameter("@Comment",user.Comment),
                new SqlParameter("@IsApproved",user.IsApproved),
                new SqlParameter("@Username",user.UserName),
                new SqlParameter("@ApplicationName", new ThisApplication().ApplicationName),
                new SqlParameter("@Group", permissionGroup)

                };

                Helper.SysNonQuery(QUERY, Parameter);

            }
            catch
            {

            }
            finally
            {

                Helper.SysDisconnect();

            }
        }

        public void UpdateUsername(object providerUserkey, string newUsername, out MembershipCreateStatus status)
        {
            SqlHelper SQL = new SqlHelper();

            Guid PKID = Guid.NewGuid();
            string[] pkid = new string[10];
            if (providerUserkey.GetType() == typeof(string[]))
                pkid = (string[])providerUserkey;
            else
                pkid[0] = providerUserkey.ToString();

            Guid.TryParse(pkid[0], out PKID);

            string exec = "UPDATE " + (new ThisApplication().getSqlPrefix) + "Users SET Username=@name WHERE PKID=@id";
            string check = "SELECT * FROM " + (new ThisApplication().getSqlPrefix) + "Users WHERE Username=@name AND PKID != @id";
            SQL.SysConnect();

            SqlDataReader Check = SQL.SysReader(check, new nSqlParameterCollection(){{"@name",newUsername} , {"@id", PKID}});
            if (Check.HasRows)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                Check.Close();
                SQL.SysDisconnect();

            }
            else
            {
                Check.Close();

                SQL.SysNonQuery(exec, new nSqlParameterCollection() { { "@name", newUsername }, { "@id", PKID } });
                status = MembershipCreateStatus.Success;
            }
            SQL.SysDisconnect();
        }

        public override bool ValidateUser(string loginname, string password)
        {
            bool _isValid = false;
            string password_clear = password;
            password = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));

            SqlHelper Helper = new SqlHelper();
            string cmd = "SELECT TOP 1 * FROM " + sqlprefix + "Users WHERE Username=@Loginname AND (Password=@Password OR Password = @PWclear)";
            SqlParameter[] Paras = new SqlParameter[] {

                    new SqlParameter("@Loginname",loginname),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@PWclear", password_clear)

            };

            try
            {
                Helper.SysConnect();

                SqlDataReader reader = Helper.SysReader(cmd, Paras);
                if (reader.HasRows) _isValid = true;
                reader.Close();
                Helper.SysDisconnect();
            }
            catch { _isValid = false; }

            return _isValid;
        }

        private string ByteArrayToString(byte[] arr)
        {
            System.Text.UnicodeEncoding enc = new System.Text.UnicodeEncoding();
            return enc.GetString(arr);
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
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

        private MembershipUser GetUserFromReader(SqlDataReader reader)
        {
            MembershipUser u = null;
            try
            {
                object providerUserKey = reader["PKID"].ToString();
                string username = ((string)reader["Username"]);
                string email = (reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "");

                string passwordQuestion = "";
                if (reader["PasswordQuestion"] != DBNull.Value)
                    passwordQuestion = reader["PasswordQuestion"].ToString();

                string comment = "";
                if (reader["Comment"] != DBNull.Value)
                    comment = reader["Comment"].ToString();

                bool isApproved = (reader["IsApproved"] != DBNull.Value && reader["IsApproved"].ToString().ToLower() == "true" ? true : false );
                bool isLockedOut = (reader["IsLockedOut"] != DBNull.Value && reader["IsLockedOut"].ToString().ToLower() == "true" ? true : false);

                DateTime creationDate =  (reader["CreationDate"] != DBNull.Value ? ((DateTime)reader["CreationDate"]) : DateTime.Now);

                DateTime lastLoginDate = (reader["CreationDate"] != DBNull.Value ? ((DateTime)reader["CreationDate"]) : DateTime.Now);

                DateTime lastActivityDate = (reader["LastActivityDate"] != DBNull.Value ? ((DateTime)reader["LastActivityDate"]) : DateTime.Now);

                DateTime lastPasswordChangedDate = (reader["LastPasswordChangedDate"] != DBNull.Value ? ((DateTime)reader["LastPasswordChangedDate"]) : DateTime.Now);

                DateTime lastLockedOutDate = (reader["LastLockedOutDate"] != DBNull.Value ? ((DateTime)reader["LastLockedOutDate"]) : DateTime.Now);

                u = new MembershipUser(this.Name,
                                                                  username,
                                                                  providerUserKey,
                                                                  email,
                                                                  passwordQuestion,
                                                                  comment,
                                                                  isApproved,
                                                                  isLockedOut,
                                                                  creationDate,
                                                                  lastLoginDate,
                                                                  lastActivityDate,
                                                                  lastPasswordChangedDate,
                                                                  lastLockedOutDate);
            }

            catch(Exception e){

                u = new MembershipUser("nProvider", "e3Guest", new GlobalSettingsHelper().Read("GLOBAL_GUESTPKID"), "", "", e.Message, false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);

            }
            if (reader != null) {

                if (!reader.IsClosed) reader.Close();

            }
            return u;
        }

        private bool parseBoolean(string val)
        {
            bool outb = false;
            bool.TryParse(val, out outb);

            return outb;
        }

        private int parseInt(string val)
        {
            int outi = 0;

            int.TryParse(val, out outi);

            return outi;
        }

        private MembershipPasswordFormat parsePasswordFormat(string val)
        {
            MembershipPasswordFormat format = MembershipPasswordFormat.Encrypted;
            switch (val)
            {
                case "Hashed":
                    format = MembershipPasswordFormat.Hashed;
                    break;

                case "Encrypted":
                    format = MembershipPasswordFormat.Encrypted;
                    break;

                case "Clear":
                    format = MembershipPasswordFormat.Clear;
                    break;
            }

            return format;
        }

        private byte[] StringToByteArray(string str)
        {
            System.Text.UnicodeEncoding enc = new System.Text.UnicodeEncoding();
            return enc.GetBytes(str);
        }

        #endregion Methods

        #region Nested Types

        public partial class CurrentUser
        {
            #region Fields

            private MembershipUser curUser = null;
            private string guestpkid = new GlobalSettingsHelper().Read("GLOBAL_GUESTPKID");

            #endregion Fields

            #region Constructors

            public CurrentUser()
            {
                nProvider N = (nProvider)Membership.Provider;
                try
                {
                    if (HttpContext.Current.Request.IsAuthenticated)
                    {
                        try
                        {

                            string uname = new System.Web.UI.Page().User.Identity.Name.ToString();
                            this.curUser = (N.GetUser(uname, true));
                  }
                        catch (Exception e2) {

                    throw e2;

                        }

                        }
                    else
                    {

                        Guid GuestID = Guid.NewGuid();
                        Guid.TryParse(guestpkid, out GuestID);
                        this.curUser = N.GetUser(GuestID, true);

                    }
                }
                catch (Exception e){

                        this.curUser = new MembershipUser("nProvider", "Guest", this.guestpkid, "", "", e.Message, false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);

                }
            }

            #endregion Constructors

            #region Properties

            public MembershipUser nUser
            {
                get {

                    return this.curUser; }
            }

            #endregion Properties

            #region Methods

            public bool isGuest()
            {
                if (this.nUser == null)
                    return true;

                if (this.nUser.ProviderUserKey.ToString().ToLower() == guestpkid.ToLower()) return true;
                else return false;
            }

            #endregion Methods
        }

        #endregion Nested Types
    }

    public class ProfileManagement
    {
        #region Fields

        /* Table Profile_Vars
         * Name,
         * ViewName,
         * LangLine,
         * Active,
         * ShowInProfile
         * ---------------------------------------------
         * Table Profile_User_Values
         * VarName,Value,LangText
        */
        private string guestpkid = null;

        #endregion Fields

        #region Constructors

        public ProfileManagement()
        {
            guestpkid = new GlobalSettingsHelper().Read("GLOBAL_GUESTPKID");
        }

        #endregion Constructors

        #region Methods

        public List<GenericProfileModel> getCollection(string section = "",bool getUnvisibleVarsToo = true)
        {
            List<GenericProfileModel> Temp = new List<GenericProfileModel>();

            SqlHelper S = new SqlHelper();
            S.SysConnect();
            nSqlParameterCollection P = new nSqlParameterCollection();
            if(section!="")P.Add("@s",section);
            if (!getUnvisibleVarsToo) P.Add("@uv", getUnvisibleVarsToo);

            ///////////////////////////////////+
            string query = "SELECT Name FROM "+ new ThisApplication().getSqlPrefix +"Profile_Vars ";
            string add = "";

            if(section!="" || !getUnvisibleVarsToo){

                add = "WHERE";

                if(section!="")add+=" Section=@s ";
                if(!getUnvisibleVarsToo) add+=(add!="WHERE" ? " AND ":"")+" ShowInProfile=@uv";

            }
            query = query+add;

            SqlDataReader R = S.SysReader(query, P);
            if (R.HasRows)
            {
                while (R.Read())
                {
                    Temp.Add(this.GetProfileVarByName(R["Name"].ToString()));

                }
            }
            S.SysDisconnect();
            return Temp;
        }

        public GenericProfileModel GetProfileVarByName(string name)
        {
            GenericProfileModel ret = new GenericProfileModel("", "", "", "", false, false);
            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();
            string query = "SELECT TOP 1 * FROM " + new ThisApplication().getSqlPrefix + "Profile_Vars WHERE Name=@name";
            try
            {

                nSqlParameterCollection P = new nSqlParameterCollection();
                P.Add(new SqlParameter("@name", name));
                SqlDataReader R = Sql.SysReader(query, P);
                if (R.HasRows)
                {
                    /* Table Profile_Vars
                    * Name,
                    * ViewName,
                    * LangLine,
                    * Active,
                    * ShowInProfile
                    * Section
                    * ---------------------------------------------
                    * Table Profile_User_Values
                    * VarName,VarValue
                   */
                    R.Read();
                    ret = new GenericProfileModel(((string)R["Name"]), ((string)R["LangLine"]), ((string)R["Section"]), ((string)R["ViewName"]), Convert.ToBoolean((string)R["Active"]), Convert.ToBoolean(((string)R["ShowInProfile"])));

                }
                R.Close();
                Sql.SysDisconnect();
            }
            catch(Exception e) {

                string rret = "ERROR IN QUERY: " + query;
                throw new Exception(e.Message, new Exception(rret));
            }
            return ret;
        }

        #endregion Methods

        #region Nested Types

        public partial class GenericProfileModel
        {
            #region Fields

            private string guestpkid = new GlobalSettingsHelper().Read("GLOBAL_GUESTPKID");
            private Ren.CMS.CORE.Language.Language LNG = new CORE.Language.Language("__USER__", "Profilevalues");
            private bool pactive = false;
            private string plangline = null;
            private string pname = null;
            private string psection = null;
            private bool pshowinprofile = false;
            private string pview = null;
            private string sqlprefix = new ThisApplication().getSqlPrefix;

            #endregion Fields

            #region Constructors

            /// <summary>
            /// Generates an Profile Model for adding or updating an Profile Var, including the Methods to get User Value or sets a User Value
            /// </summary>
            /// <param name="name"></param>
            /// <param name="langline"></param>
            /// <param name="section"></param>
            /// <param name="view"></param>
            public GenericProfileModel(string name, string langline, string section, string view = "profile_partial_default.cshtml", bool active = true, bool showinprofile = true)
            {
                this.pname = name;
                this.plangline = langline;
                this.psection = section;
                this.pview = view;
                this.pshowinprofile = showinprofile;
                this.pactive = active;
            }

            #endregion Constructors

            #region Properties

            public bool Active
            {
                get
                {

                    return this.pactive;
                }
                set { this.pactive = value; }
            }

            /// <summary>
            /// Return the Language Text for the Labels. (Langcode by Current User)
            /// </summary>
            public string LabelText
            {
                get {return this.LNG.getLine(this.LangLine); }
            }

            public string LangLine
            {
                set { this.plangline = value; }
                get { return this.plangline; }
            }

            public string Name
            {
                get { return this.pname; }
            }

            public string PartialView
            {
                get
                {

                    return this.pview;

                }
                set { this.pview = value; }
            }

            public string Section
            {
                set { this.psection = value; }
                get { return this.psection; }
            }

            public bool ShowInProfile
            {
                get { return this.pshowinprofile; }
                set { this.pshowinprofile = value; }
            }

            #endregion Properties

            #region Methods

            public string getUserValue(string userpkid = "__CURRENTID__")
            {
                string query = "SELECT TOP 1 * FROM " + sqlprefix + "Profile_User_Values WHERE VarName=@name AND PKID=@pid";

                try
                {
                    if (userpkid == "__CURRENTID__") userpkid = new nProvider.CurrentUser().nUser.ProviderUserKey.ToString();
                    SqlHelper Sql = new SqlHelper();
                    Sql.SysConnect();
                    nSqlParameterCollection P = new nSqlParameterCollection();
                    P.Add("@name", this.pname);
                    P.Add("@pid", userpkid);
                    SqlDataReader R = Sql.SysReader(query, P);
                    string ret = "";
                    if (R.HasRows)
                    {

                        R.Read();
                        if (R["VarValue"] != DBNull.Value) ret = ((string)R["VarValue"]);

                    }
                    R.Close();
                    Sql.SysDisconnect();
                    return ret;
                }
                catch(Exception e) {

                    throw new Exception("ERROR in ren_cms: " + e.Message + " in QUERY: " + query);

                }
            }

            /// <summary>
            /// Inserts or if the Profile Var allready exists updates the Profile Var
            /// </summary>
            public void Save()
            {
                /* Table Profile_Vars
                * Name,
                * ViewName,
                * LangLine,
                * Active,
                * ShowInProfile
                * ---------------------------------------------
                * Table Profile_User_Values
                * VarName,VarValue
                   */
                string[] queries = new string[]{
                "INSERT INTO "+ sqlprefix +"Profile_Vars (Name,LangLine,Active,ShowInProfile,ViewName) VALUES(@name,@lgn,@active,@show,@view)",
                "UPDATE "+ sqlprefix +"Profile_Vars SET LangLine=@lgn,Active=@active,ShowInProfile=@show,ViewName=@view WHERE Name=@name"

                };
                nSqlParameterCollection P = new nSqlParameterCollection();
                P.Add(new SqlParameter("@name", this.pname));
                P.Add(new SqlParameter("@lgn", this.plangline));
                P.Add(new SqlParameter("@active", this.pactive));
                P.Add(new SqlParameter("@show", this.pshowinprofile));
                P.Add(new SqlParameter("@view", this.pview));

                string bad = "";
                if (new Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper().SomethingIsEmpty(new object[]{
                this.pname,
                this.plangline,
                this.psection,

                this.pview,

                }, out bad))
                {
                    throw new Exception("Profile Var´s Name,LangLine,Section,PartialView cannot be null,String.empty or white spaces", new Exception("\"" + bad + "\""));

                }
                else
                {

                    SqlHelper Sql = new SqlHelper();
                    Sql.SysConnect();
                    int c = Sql.SysCount("Profile_Vars", "Name", this.pname);
                    string query = "";
                    if (c > 0) query = queries[1];
                    else query = queries[0];
                    Sql.SysNonQuery(query, P);
                    Sql.SysDisconnect();

                }
            }

            public void SetUserValue(string uservalue, string userpkid = "__CURRENTID__")
            {
                if (userpkid == "__CURRENTID__") userpkid = new nProvider.CurrentUser().nUser.ProviderUserKey.ToString();
                SqlHelper Sql = new SqlHelper();
                Sql.SysConnect();
                string[] queries = new string[]{
                "INSERT INTO "+ sqlprefix +"Profile_User_Values (VarValue,VarName,PKID) VALUES(@val,@name,@pid)",
                "DELETE "+ sqlprefix +"Profile_User_Values WHERE PKID=@pid AND VarName=@name"

                };
                nSqlParameterCollection P = new nSqlParameterCollection();
                P.Add(new SqlParameter("@pid", userpkid));
                P.Add(new SqlParameter("@name", this.pname));

                nSqlParameterCollection P2 = new nSqlParameterCollection();
                P2.Add(new SqlParameter("@pid", userpkid));
                P2.Add(new SqlParameter("@name", this.pname));

                Sql.SysNonQuery(queries[1], P2);
                P.Add("@val", uservalue);
                Sql.SysNonQuery(queries[0], P);
                Sql.SysDisconnect();
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}