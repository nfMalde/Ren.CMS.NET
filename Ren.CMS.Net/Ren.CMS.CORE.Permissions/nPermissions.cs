using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Ren.CMS.CORE.Permissions
{


    public class nPermissions
    {

        private MembershipUser myuser = new MemberShip.nProvider.CurrentUser().nUser;





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
        public bool hasPermission(string permissionKey)
        {

            bool ret = false;


            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();

            Sql.SysConnect();

            if (this.myuser != null)
            {
                //Get Permissiongroup
                string query = "SELECT PermissionGroup FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Users WHERE PKID=@p";
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@p", myuser.ProviderUserKey.ToString()) };
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
                new SqlParameter("@usr", this.myuser.ProviderUserKey)
                
                
                
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

                ret = this.hasPermission("FULL_ACCESS");
            }

            return ret;







        }







    }




}
