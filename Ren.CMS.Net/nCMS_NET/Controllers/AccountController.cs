using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Ren.CMS.Models;
using Ren.CMS.CORE.Extras;
using Ren.CMS.CORE.Settings;
using Ren.CMS.CORE.Permissions;
using Ren.CMS.MemberShip;
using Ren.CMS.CORE.FileManagement;
namespace Ren.CMS.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult SaveSettings() { 
        
           //Save Settings
            UserSettings USR = new UserSettings(new MemberShip.nProvider.CurrentUser().nUser);
            
            nValueType VT = new nValueType();
            List<bool> OKs = new List<bool>();
            foreach (nSetting Sett in USR.listSettings(false)) {


                if (nPermissions.hasPermission(Sett.PermissionFrontend))
                {

                string fieldName = "setting_" + Sett.Name;
                if (Request.Form[fieldName] != null) {

                    if (Sett.ValueType == nValueType.ValueArray)
                    {

                        Sett.Value = Request.Form.GetValues(fieldName);


                    }
                    else {

                        Sett.Value = Request.Form[fieldName].ToString();
                    
                    }

                  bool ok =  USR.setValue(Sett);
                  OKs.Add(ok);
                }
            
                
            
            }
            
            
            }

            int x = 0; 
            foreach (bool isOk in OKs) {


                if (!isOk) x++;
            
            }

            return RedirectToAction("Index/Success_"+x, "Account");
        
        }

        public ActionResult ChangeProfile() {

            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language("__USER__","PAGE_TITLES");
            MemberShip.ProfileManagement PM = new ProfileManagement();
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation(Lang.getLine("LANG_PAGE_TITLE_CHANGE_PROFILE"), "/Account/ChangeProfile", true);
            Bar.Render();

            GlobalSettings GS = new GlobalSettings();

            EditProfileModel MDL = new EditProfileModel();

            MDL.title = Lang.getLine("LANG_PAGE_TITLE_CHANGE_PROFILE");
            MDL.basicInfo = PM.getCollection("basic", true);
            MDL.social = PM.getCollection("social", true);
            MDL.ProfileImage = PM.GetProfileVarByName("ProfileImage");
         
            MDL.User = new nProvider.CurrentUser().nUser;
            nSetting ProfTypes = GS.getSetting("ACCOUNT_PIX_FILETYPES");
            if (ProfTypes.Value == "") ProfTypes.Value = new string[0];
            MDL.ProfilePictureFileTypes = (string[])ProfTypes.Value;
            if(String.IsNullOrEmpty(MDL.ProfileImage.getUserValue())){


             MDL.picname = "/UserAvatar/"+ MDL.User.UserName + "-Picture.jpg";
    
    
    
            }
            else{
            
            MDL.picname = MDL.ProfileImage.getUserValue();
            
            }
            MDL.nMaxFileSize = GS.getSetting("ACCOUNT_MAX_PROFILEPIC_FILE_SIZE");
            MDL.nMinWidth = GS.getSetting("ACCOUNT_PIC_MIN_WIDTH");
            MDL.nMinHeight = GS.getSetting("ACCOUNT_PIC_MIN_HEIGHT");
            MDL.nMaxWidth = GS.getSetting("ACCOUNT_PIC_MAX_WIDTH");
            MDL.nMaxHeight = GS.getSetting("ACCOUNT_PIC_MAX_HEIGHT");

            

            return View(MDL);
        }

        public ActionResult Settings()
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Einstellungen", "/Account/Settings", true);
            Bar.Render();

            return View();
        
        }
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Login", "/Account/LogOn", true);
            Bar.Render();
            return View();
        }


        [HttpPost]
        public WrappedJsonResult AjaxSaveAvatar()
        {
             string fieldName = "avatar";
             string[] dirs = { "/Storage/User", "/Storage/User/Avatars" };

             foreach (string dir in dirs)
             { 
                if(!System.IO.Directory.Exists(Server.MapPath(dir)))
                {
                
                    System.IO.Directory.CreateDirectory(Server.MapPath(dir));
                }
             
             
             }
         
            MembershipUser U = new MemberShip.nProvider.CurrentUser().nUser;

            HttpPostedFileBase avatar = Request.Files[fieldName];
            GlobalSettings GS = new GlobalSettings();
            ChangeAvatar MD = new ChangeAvatar();

            MD.nMaxFileSize = GS.getSetting("ACCOUNT_MAX_PROFILEPIC_FILE_SIZE");

            MD.ProfilePictureFileTypes = GS.getSetting("ACCOUNT_PIX_FILETYPES").toStringArray();
            int fileS =  GS.getSetting("ACCOUNT_MAX_PROFILEPIC_FILE_SIZE").toInt();

            int minWidth = GS.getSetting("ACCOUNT_PIC_MIN_WIDTH").toInt();
            int minHeight = GS.getSetting("ACCOUNT_PIC_MIN_HEIGHT").toInt();
            int maxWidth = GS.getSetting("ACCOUNT_PIC_MAX_WIDTH").toInt();
            int maxHeight = GS.getSetting("ACCOUNT_PIC_MAX_HEIGHT").toInt();


             //Check Size

            if (avatar.ContentType.StartsWith("image"))
            {

                bool minsizeOK = true;
                System.Drawing.Bitmap IMG = new System.Drawing.Bitmap(avatar.InputStream);
                if (minWidth > 0)
                {

                    if (IMG.Width < minWidth) minsizeOK = false;



                }
                if (minHeight > 0)
                {


                    if (IMG.Height < minHeight) minsizeOK = false;



                }
                if (!minsizeOK)
                {
                    
                    return new WrappedJsonResult
                    {
                        Data = new
                        {
                            IsValid = false,
                            Message = "Das Bild entsprecht nicht den vorgegebenen Mindestmaßen: " + minHeight + "px x " + minWidth + "px",
                            ImagePath = string.Empty
                        }   
                    };

                }

                bool maxsizeok = true;
                if (maxWidth > 0)
                {

                    if (IMG.Width > maxWidth) maxsizeok = false;



                }
                if (maxHeight > 0)
                {


                    if (IMG.Height > maxHeight)
                    {

                        maxsizeok = false;


                    }

                }
                if (!maxsizeok)
                    return new WrappedJsonResult
                    {
                        Data = new
                        {
                            IsValid = false,
                            Message = "Das Bild entsprecht nicht den vorgegebenen Maximalmaßen: " + maxHeight + "px x " + maxWidth + "px",
                            ImagePath = string.Empty
                        }
                    };


            }
            else {



                return new WrappedJsonResult
                {
                    Data = new
                    {
                        IsValid = false,
                        Message = "Die angeforderte Datei ist keine gültige Bild Datei.",
                        ImagePath = string.Empty
                    }
                };
            
            }

            if (fileS > 0) {

                int uploadedSize = avatar.ContentLength;

                if (uploadedSize > fileS) {

                 
                    return new WrappedJsonResult
                    {
                        Data = new
                        {
                            IsValid = false,
                            Message = "Diese Datei ist zu Groß, das Maximum liegt bei"+ MD.ParsedMaxFileSize +".",
                            ImagePath = string.Empty
                        }
                    };
                
                
                }
            
            
            
            }

           

            if (Request.Files[fieldName] == null || Request.Files[fieldName].ContentLength == 0)
            {
                return new WrappedJsonResult
                {
                    Data = new
                    {
                        IsValid = false,
                        Message = "No file was uploaded.",
                        ImagePath = string.Empty
                    }
                };
            }

           
            FileManagement FM = new FileManagement();

            string[] exts = new string[] {
            
            ".jpg",".jpeg", ".png", ".bmp", ".gif"
            
            };

            if (Request.Files[fieldName] == null || Request.Files[fieldName].ContentLength == 0)
            {
                return new WrappedJsonResult
                {
                    Data = new
                    {
                        IsValid = false,
                        Message = "No file was uploaded.",
                        ImagePath = string.Empty
                    }
                };
            }

            nFile NewF = new nFile();
            FileManagement.nFileProfiles Prof = new FileManagement.nFileProfiles("UserAvatar");
           
            NewF.aliasName = U.UserName +"-Picture"+ System.IO.Path.GetExtension(avatar.FileName);
          
            NewF.filepath = "/Storage/User/Avatars";
            NewF.id = 0;
            NewF.isActive = true;
            NewF.mimetype = avatar.ContentType;
            NewF.ProfileID = Prof.ID;
            FM.RegisterFile(NewF, fieldName, true, MD.ProfilePictureFileTypes);


            NewF = FM.getFile(NewF.aliasName);
            HttpContext.Server.MapPath(NewF.filepath);
            ProfileManagement PM = new ProfileManagement();
            ProfileManagement.GenericProfileModel PMo = PM.GetProfileVarByName("ProfileImage");
            PMo.SetUserValue("/UserAvatar/" + NewF.aliasName);
            return new WrappedJsonResult
            {
                Data = new
                {
                    IsValid = true,
                    Message = string.Empty,
                    ImagePath = Url.Content("/UserAvatar/" + NewF.aliasName)
                }
            };
        
        }


        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Login", "/Account/LogOn", true);
            Bar.Render();
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Der angegebene Benutzername oder das angegebene Kennwort ist ungültig.");
                }
            }

            // Wurde dieser Punkt erreicht, ist ein Fehler aufgetreten; Formular erneut anzeigen.
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Kontro erstellen", "/Account/Register", true);
            Bar.Render();
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Benutzerkonto erstellen", "/Account/Register", true);
            Bar.Render();
            if (ModelState.IsValid)
            {
                // Versuch, den Benutzer zu registrieren
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // Wurde dieser Punkt erreicht, ist ein Fehler aufgetreten; Formular erneut anzeigen.
            return View(model);
        }


        [Authorize]

        public ActionResult Index(string id="") {
            if (id.StartsWith("Success_")) {
                string secc = "Success_";
                CORE.Language.Language LNG = new CORE.Language.Language("__USER__", "ACCOUNT");
                ViewBag.IsSuccess = true;
               
                int xe = 0;
                string errors = id.Substring(secc.Length);
                int.TryParse(errors, out xe);

                if (xe > 0)
                {
                    ViewBag.SuccessMessageText = LNG.getLine("LANG_ACCOUNT_SUCCESS_TEXT_ERROR").Replace("%%NUMBER%%", errors);
                    ViewBag.SuccessMessageTitle = LNG.getLine("LANG_ACCOUNT_SUCCESS_TITLE_ERROR");


                }
                else
                {
                    ViewBag.SuccessMessageText = LNG.getLine("LANG_ACCOUNT_SUCCESS_TEXT");
                    ViewBag.SuccessMessageTitle = LNG.getLine("LANG_ACCOUNT_SUCCESS_TITLE");
                }

                ViewBag.Errors = xe;
            }
            //Hauptseite der Accountverwaltung anzeigen....
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", true);
 
            Bar.Render();



            return View();
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Password ändern", "/Account/Register", true);
            Bar.Render();
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Password ändern", "/Account/Register", true);
            Bar.Render();
            if (ModelState.IsValid)
            {

                // In bestimmten Fehlerszenarien löst ChangePassword eine Ausnahme aus,
                // anstatt \"false\" zurückzugeben.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Das aktuelle Kennwort ist nicht korrekt, oder das Kennwort ist ungültig.");
                }
            }

            // Wurde dieser Punkt erreicht, ist ein Fehler aufgetreten; Formular erneut anzeigen.
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            LocationBar Bar = new LocationBar(this.ControllerContext);

            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Benutzerkonto", "/Account/Index", false);
            Bar.AddLocation("Password ändern", "/Account/Register", true);
            Bar.Render();
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Eine vollständige Liste mit Statuscodes finden Sie unter http://go.microsoft.com/fwlink/?LinkID=177550
            //.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Der Benutzername ist bereits vorhanden. Geben Sie einen anderen Benutzernamen ein.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Für diese E-Mail-Adresse ist bereits ein Benutzername vorhanden. Geben Sie eine andere E-Mail-Adresse ein.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Das angegebene Kennwort ist ungültig. Geben Sie einen gültigen Kennwortwert ein.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Die angegebene E-Mail-Adresse ist ungültig. Überprüfen Sie den Wert, und wiederholen Sie den Vorgang.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Die angegebene Kennwortabrufantwort ist ungültig. Überprüfen Sie den Wert, und wiederholen Sie den Vorgang.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Die angegebene Kennwortabruffrage ist ungültig. Überprüfen Sie den Wert, und wiederholen Sie den Vorgang.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Der angegebene Benutzername ist ungültig. Überprüfen Sie den Wert, und wiederholen Sie den Vorgang.";

                case MembershipCreateStatus.ProviderError:
                    return "Vom Authentifizierungsanbieter wurde ein Fehler zurückgegeben. Überprüfen Sie die Eingabe, und wiederholen Sie den Vorgang. Sollte das Problem weiterhin bestehen, wenden Sie sich an den zuständigen Systemadministrator.";

                case MembershipCreateStatus.UserRejected:
                    return "Die Benutzererstellungsanforderung wurde abgebrochen. Überprüfen Sie die Eingabe, und wiederholen Sie den Vorgang. Sollte das Problem weiterhin bestehen, wenden Sie sich an den zuständigen Systemadministrator.";

                default:
                    return "Unbekannter Fehler. Überprüfen Sie die Eingabe, und wiederholen Sie den Vorgang. Sollte das Problem weiterhin bestehen, wenden Sie sich an den zuständigen Systemadministrator.";
            }
        }
        #endregion
    }
}
