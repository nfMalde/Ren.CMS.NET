using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.ThisApplication;
using System.Data.SqlClient;
using Ren.CMS.Models.FlexiGrid;
using Ren.CMS.Models.FormDialog;
using Ren.CMS.Models.Backend.Users;
using Ren.CMS.MemberShip;
using System.Web.Security;
using Ren.CMS.CORE.Security;
namespace Ren.CMS.Controllers
{
    public class BackendHandlerUsersController : Controller
    {
        [HttpPost]
        public FormDialogReturn Edit(EditUser MDL)
        {
            nProvider Provider = (nProvider)Membership.Provider;
            if (!ModelState.IsValid)
            {
                if (MDL.password != MDL.password_confirm)
                    return new FormDialogReturn(false, "Fehler: 'Passwort' und 'Passwort bestätigen' stimmen nicht überein.");


                return new FormDialogReturn(false, "Bitte korrigieren Sie ihre Angaben!");

            
            
            }

            MembershipUser User = Provider.GetUser(MDL.PKID, false);
            if (MDL.Username != User.UserName)
            {
                MembershipCreateStatus status = MembershipCreateStatus.UserRejected;
                Provider.UpdateUsername(User.ProviderUserKey, MDL.Username, out status);
                if (status != MembershipCreateStatus.Success)
                {  
                    return new FormDialogReturn(false, "Fehler: Entweder ist der Benutzername schon vergeben oder er wurde vom System abgelehnt (Error-Code: MembershipCreateStatus." + status.ToString() + ")");

                }

            }
            User.Email = MDL.email;
            User.ChangePasswordQuestionAndAnswer(User.GetPassword(), MDL.secretQuestion, MDL.secretAnswer);
            User.IsApproved = (MDL.user_status == "activated" ? true : false);
            User.Comment = MDL.user_comment;
            if (MDL.user_locked)
            {

                if(Provider.LockUser(User.UserName))
                    return new FormDialogReturn(false, "Cannot lock User. Please try again later");
            }
            else
            {

                if (!Provider.UnlockUser(User.UserName))
                    return new FormDialogReturn(false, "Cannot unlock User. Please try again later");

            }
            if (!String.IsNullOrEmpty(MDL.password))
            {
                if (!User.ChangePassword(User.GetPassword(), MDL.password))
                    return new FormDialogReturn(false, "Cannot change Password. Please try again later");

            }
            //Update Process
            Provider.UpdateUser(User,MDL.Pgroup);
            

            return new FormDialogReturn(true, "");


        }

        [HttpPost]
        public FormDialogReturn Create(CreateUser MDL) {

            CryptoServices Crypto = new CryptoServices();
            if(MDL.GeneratePWD)
            {
               Random PWhash = new Random(1000);
               int pwhashExt = DateTime.Now.Millisecond;

               string uncrypted = PWhash +"_"+ pwhashExt;

               string cryptedPW = Crypto.ConvertToSHA1(uncrypted);

               string newPWD = ( cryptedPW.Length > 8 ?
                                    cryptedPW.Substring(0,8) :
                                    cryptedPW);


               MDL.password = newPWD;
               MDL.password_confirm = newPWD;
            }


            if (!ModelState.IsValid)
            {
              
                  
            if(MDL.password != MDL.password_confirm)        
                return new FormDialogReturn(false, "Das Passwort stimmt nicht mit der Besätigung überein");

             
                
               
                return new FormDialogReturn(false, "Es wurde nicht alle Felder ausgefüllt oder sind fehlerhaft. Bitte überprüfen Sie ihre Angaben!");

            
            }
            MDL.email = (MDL.email == null ? String.Empty : MDL.email); 
            MemberShip.nProvider Prov = (MemberShip.nProvider)Membership.Provider;
            MembershipCreateStatus Status = new MembershipCreateStatus();
            MembershipUser User = Prov.CreateUser(
                                                    MDL.Username,
                                                    MDL.password,
                                                    MDL.email,
                                                    MDL.secretQuestion,
                                                    MDL.secretAnswer,
                                                    (MDL.user_status == "activated" ? true : false),
                                                    Guid.NewGuid(),
                                                    out Status, MDL.Pgroup);

           


             
            

                if(Status == MembershipCreateStatus.Success)
                {
                //Now lets try the lockout
                if (MDL.user_locked)
                {

                    Prov.LockUser(MDL.Username);
                
                }

                if (!String.IsNullOrEmpty(MDL.user_comment))
                {
                    User = Prov.GetUser(User.ProviderUserKey, false);

                    User.Comment = MDL.user_comment;

                    Prov.UpdateUser(User);
                
                }
                    return new FormDialogReturn(true, "Benutzer wurde erfolgreich erstellt");
                 
                }
                else if(Status == MembershipCreateStatus.DuplicateUserName)
                {

                return new FormDialogReturn(false, "Dieser Benutzername existert bereits");
                }
                else if (Status == MembershipCreateStatus.DuplicateEmail)
                {
                    return new FormDialogReturn(false, "Es existiert breits ein Benutzer mit dieser E-Mail Adresse.");
                }


                return new FormDialogReturn(false, "Unbekannter Fehler während der Benutzererstellung: <a href=\"http://msdn.microsoft.com/de-de/library/system.web.security.membershipcreatestatus.aspx\" target=\"_blank\">" + Status.ToString() +"</a>");
            
            




        }

        [HttpPost]
        public FlexiGridReturn UserList(Ren.CMS.Models.Core.FlexyGridPostParameters Flexi = null)
        {

            List<FlexiRow> UserRows = new List<FlexiRow>();
            if (!ModelState.IsValid)
            {
                Flexi.page = 1;
                Flexi.rp = 10;
                Flexi.sortname = "UNAME";
                Flexi.sortorder = "DESC";
            }
            else
            {
                switch (Flexi.sortname)
                {
                    case "UNAME":

                        Flexi.sortname = "u.Username";

                        break;
                    case "PGROUP":
                        Flexi.sortname = "pg.groupName";

                        break;
                    case "PKID":
                        Flexi.sortname = "u.PKID";
                        break;
                    default:
                        Flexi.sortname = "u.Username";
                        break;
                }

            }


            switch (Flexi.sortorder.ToUpper())
            { 
                case "DESC":
                case "ASC":

                    Flexi.sortorder = Flexi.sortorder.ToUpper();
                    break;

                default:
                    Flexi.sortorder = "DESC";
                    break;
            
            }

            List<object> _list = new List<object>();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;

            string query =

                ";with pagination as ( " +
                "SELECT dense_rank() over ( ORDER BY " + Flexi.sortname + " " + Flexi.sortorder + " ) as rowNo, u.PKID as PKID, u.Username as UNAME, u.IsLockedOut as LOCKEDOUT, pg.groupName as GROUPNAME FROM " + prefix + "Users u INNER JOIN " + prefix + "PermissionGroups pg ON (u.PermissionGroup = pg.groupName)" +
                ")" +
                " SELECT *,(SELECT COUNT(*) from pagination) as TotalRows FROM pagination WHERE rowNo BETWEEN  @i and @ii";

            //Calculate pages
            int pageSize = Flexi.rp;
            int pageStart = (Flexi.page - 1) * pageSize;
            int pageEnd = Flexi.page * pageSize;
            int totalRows = 0;



            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            SqlDataReader Users = SQL.SysReader(query, new nSqlParameterCollection(){ new SqlParameter("@i",pageStart) , new SqlParameter("@ii",pageEnd)});

            if (Users.HasRows)
            {
                while (Users.Read())
                {
                    totalRows = (int)Users["TotalRows"];

                    FlexiRow Frow = new FlexiRow();
                    Frow.cell.Add("UNAME", ((string)Users["UNAME"]));
                    Frow.cell.Add("PKID", ((object)Users["PKID"]).ToString());
                    Frow.cell.Add("PGROUP", ((string)Users["GROUPNAME"]));
                    Frow.cell.Add("LOCKED", ((string)Users["LOCKEDOUT"]));

                    Frow.id = ((object)Users["PKID"]).ToString();
                    UserRows.Add(Frow);
                    object row = new
                    {
                        id = ((object)Users["PKID"]).ToString(),
                        cell = new
                        {
                            UNAME = ((string)Users["UNAME"]),
                            PKID = ((object)Users["PKID"]).ToString(),
                            PGROUP = ((string)Users["GROUPNAME"]),
                            LOCKED = ((string)Users["LOCKEDOUT"]),


                        }
                    };
                }

            }
            Users.Close();
            SQL.SysDisconnect();
            
            //here we are setting up the return and with a little bit magic we return a valid json return for our flexi grid
            FlexiGridReturn Ret = new FlexiGridReturn(UserRows, Flexi.page, totalRows);


            return Ret;
        }

        [HttpPost]
        public JsonResult GetUserData(object PKID)
        {
            string pkid = PKID.ToString();
            nProvider Provider = (nProvider)Membership.Provider;

            MembershipUser User = Provider.GetUser(PKID, true);

            if(User == null) 
                return Json(new { success = false, error = "User not found"});
            Dictionary<string, object> allData = Provider.GetUserDataRow(PKID);

            EditUser Return = new EditUser();
            Return.PKID = User.ProviderUserKey;
            Return.email = User.Email;
            Return.GeneratePWD = false;
            Return.inform_user = true;
            Return.Pgroup = allData.Where(i => i.Key == "PermissionGroup").First().Value.ToString();
            Return.PKID = User.ProviderUserKey;
            Return.secretAnswer = allData.Where(i => i.Key == "PasswordAnswer").First().Value.ToString();
            Return.secretQuestion = User.PasswordQuestion;
            Return.user_status = (User.IsApproved ? "activated" : "deactivated");
            Return.user_locked = User.IsLockedOut;
            Return.Username = User.UserName;


            return Json(new { success = true, UserData = Return });


        }





    }
}
