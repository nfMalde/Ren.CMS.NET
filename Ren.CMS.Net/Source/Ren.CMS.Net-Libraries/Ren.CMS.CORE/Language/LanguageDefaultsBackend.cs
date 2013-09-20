namespace Ren.CMS.CORE.Language.LanguageDefaults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class LanguageDefaultsContent
    {
        #region Fields

        public static LanguageDefaultValues LANG_CONTENTS = new LanguageDefaultValues("LANG_CONTENTS", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Inhalte"},
        {"en-US", "Contents"}

        };
        public static LanguageDefaultValues LANG_CONTENT_BACKEND_FILESTAB_TITLE = new LanguageDefaultValues("LANG_CONTENT_BACKEND_FILESTAB_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Dateien" },
        {"en-US", "Files"}

        };
        public static LanguageDefaultValues LANG_CONTENT_BACKEND_FINISHTAB_TITLE = new LanguageDefaultValues("LANG_CONTENT_BACKEND_FINISHTAB_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Fertigstellen" },
        {"en-US", "Finish"}

        };

        //Tabs
        public static LanguageDefaultValues LANG_CONTENT_BACKEND_MAINTAB_TITLE = new LanguageDefaultValues("LANG_CONTENT_BACKEND_MAINTAB_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Hauptdaten" },
        {"en-US", "General data"}

        };
        public static LanguageDefaultValues LANG_CONTENT_BACKEND_SEOTAB_TITLE = new LanguageDefaultValues("LANG_CONTENT_BACKEND_SEOTAB_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Suchmaschienenoptmierung" },
        {"en-US", "SOE Data"}

        };
        public static LanguageDefaultValues LANG_CONTENT_BACKEND_TAGTAB_TITLE = new LanguageDefaultValues("LANG_CONTENT_BACKEND_TAGTAB_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Tags" },
        {"en-US", "Tags"}

        };
        public static LanguageDefaultValues LANG_CONTENT_CATEGORY = new LanguageDefaultValues("LANG_CONTENT_CATEGORY", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Kategorie"},
        {"en-US", "Category"}

        };
        public static LanguageDefaultValues LANG_CONTENT_CREATION_DATE = new LanguageDefaultValues("LANG_CONTENT_CREATION_DATE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Erstelldatum"},
        {"en-US", "Creation date"}

        };
        public static LanguageDefaultValues LANG_CONTENT_CREATOR = new LanguageDefaultValues("LANG_CONTENT_CREATOR", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Erstellt von"},
        {"en-US", "Created by"}

        };
        public static LanguageDefaultValues LANG_CONTENT_DIALOG_DELETE_TEXT = new LanguageDefaultValues("LANG_CONTENT_DIALOG_DELETE_TEXT", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Möchten Sie diesen Inhalt wirklich löschen?"},
        {"en-US", "Are you sure, that you want to delete this content?"}

        };
        public static LanguageDefaultValues LANG_CONTENT_TITLE = new LanguageDefaultValues("LANG_CONTENT_TITLE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Titel"},
        {"en-US", "Title"}

        };
        public static LanguageDefaultValues LANG_CONTENT_TYPE = new LanguageDefaultValues("LANG_CONTENT_TYPE", "CONTENT_MANAGEMENT") { 

        {"de-DE", "Inhaltstyp"},
        {"en-US", "Content type"}

        };

        #endregion Fields
    }

    public static class LanguageDefaultsManageUsers
    {
        #region Fields
       
        public static LanguageDefaultValues LANG_FM_DIALOG_COMMENT = new LanguageDefaultValues("LANG_FM_DIALOG_COMMENT", "USERS_BACKEND"){
                                                                    {"de-DE", "Internes Kommentar"},
                                                                    {"en-US", "Internal comment"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_COMMENT_NO_COMMENT = new LanguageDefaultValues("LANG_FM_DIALOG_COMMENT_NO_COMMENT", "USERS_BACKEND"){
                                                                                {"de-DE", "Kein Kommentar"},
                                                                                {"en-US", "No comment"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_CREATE = new LanguageDefaultValues("LANG_FM_DIALOG_CREATE", "USERS_BACKEND"){
                                                                        {"de-DE", "Erstellen"},
                                                                        {"en-US", "Create"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_CREATE_TITLE = new LanguageDefaultValues("LANG_FM_DIALOG_CREATE_TITLE", "USERS_BACKEND") { 
                                                                       {"de-DE", "Benutzer anlegen"},
                                                                       {"en-US", "Create a new User"} };
        public static LanguageDefaultValues LANG_FM_DIALOG_EDIT_TITLE = new LanguageDefaultValues("LANG_FM_DIALOG_CREATE_TITLE", "USERS_BACKEND") {
                                                                        {"de-DE", "Benutzer anpassen"},
                                                                          {"en-US", "Modify User"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_EMAIL = new LanguageDefaultValues("LANG_FM_DIALOG_EMAIL", "USERS_BACKEND"){
                                                                        {"de-DE", "E-Mail Adresse"},
                                                                        {"en-US", "eMail address"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_GENERATE_PW = new LanguageDefaultValues("LANG_FM_DIALOG_GENERATE_PW", "USERS_BACKEND"){
                                                                        {"de-DE", "Passwort generieren"},
                                                                        {"en-US", "Generate password"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_GROUP = new LanguageDefaultValues("LANG_FM_DIALOG_GROUP", "USERS_BACKEND"){
                                                                        {"de-DE", "Rechtegruppe"},
                                                                        {"en-US", "Permissiongroup"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_INFORM_USER = new LanguageDefaultValues("LANG_FM_DIALOG_INFORM_USER", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzer benachrichtigen"},
                                                                        {"en-US", "Inform user"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_INFORM_USER_NO = new LanguageDefaultValues("LANG_FM_DIALOG_INFORM_USER_NO", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzer nicht benachrichtigen"},
                                                                        {"en-US", "Dont inform user"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_INFORM_USER_YES = new LanguageDefaultValues("LANG_FM_DIALOG_INFORM_USER_YES", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzer benachrichtigen"},
                                                                        {"en-US", "Inform user"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_LOCK = new LanguageDefaultValues("LANG_FM_DIALOG_LOCK", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzer sperren"},
                                                                        {"en-US", "Lock user"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_PASSWORD = new LanguageDefaultValues("LANG_FM_DIALOG_PASSWORD", "USERS_BACKEND"){
                                                                        {"de-DE", "Passwort"},
                                                                        {"en-US", "Password"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_PASSWORD_CONFIRM = new LanguageDefaultValues("LANG_FM_DIALOG_PASSWORD_CONFIRM", "USERS_BACKEND"){
                                                                        {"de-DE", "Passwort best&auml;tigen"},
                                                                        {"en-US", "Confirm password"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_SECRET_QUESTION = new LanguageDefaultValues("LANG_FM_DIALOG_SECRET_QUESTION", "USERS_BACKEND"){
                                                                        {"de-DE", "Geheime Frage"},
                                                                        {"en-US", "Secret question"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_SECRET_ANSWER = new LanguageDefaultValues("LANG_FM_DIALOG_SECRET_QUESTION", "USERS_BACKEND"){
                                                                        {"de-DE", "Antwort"},
                                                                        {"en-US", "Answer"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_STATUS = new LanguageDefaultValues("LANG_FM_DIALOG_STATUS", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzerstatus"},
                                                                        {"en-US", "User status"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_STATUS_ACTIVATED = new LanguageDefaultValues("LANG_FM_DIALOG_STATUS_ACTIVATED", "USERS_BACKEND"){
                                                                        {"de-DE", "Aktiviert"},
                                                                        {"en-US", "Activated"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_STATUS_DEACTIVATED = new LanguageDefaultValues("LANG_FM_DIALOG_STATUS_DEACTIVATED", "USERS_BACKEND"){
                                                                        {"de-DE", "Deaktiviert"},
                                                                        {"en-US", "Deactivated"}};
        public static LanguageDefaultValues LANG_FM_DIALOG_USERNAME = new LanguageDefaultValues("LANG_FM_DIALOG_CREATE_TITLE", "USERS_BACKEND"){
                                                                        {"de-DE", "Benutzername"},
                                                                        {"en-US", "Username"}};
        public static LanguageDefaultValues LANG_MAILS_USERDATA_CHANGED_BODY = new LanguageDefaultValues("LANG_MAILS_USERDATA_CHANGED_BODY", "EMAIL_TEXTS"){
                                                                                {"de-DE", "Hallo {username}, <br/> Ihre Daten wurde von einem Administrator geändert. Anbei finden Sie Ihre neuen Daten:<br>"+
                                                                                 "<p><b>Benutzername:</b>&nbsp;{newusername}<br>"+
                                                                                 "<b>Passwort:</b>&nbsp;{password}</p>"+
                                                                                 "<p>Mit freundlichen Gr&uuml;ßen,<br> Ihr {sitename} Team</p>"},
                                                                                {"en-US", "Hello {username}, <br/> Your data has been change by one of our administrators. You find your new data below:<br>"+
                                                                                 "<p><b>Username:</b>&nbsp;{newusername}<br>"+
                                                                                 "<b>Password:</b>&nbsp;{password}</p>"+
                                                                                 "<p>Best regards,<br> Your {sitename} team</p>" }};
        public static LanguageDefaultValues LANG_MAILS_USERDATA_CHANGED_SUBJECT = new LanguageDefaultValues("LANG_MAILS_USERDATA_CHANGED_SUBJECT", "EMAIL_TEXTS"){
                                                                                {"de-DE", "Ihre Daten wurden geändert" },
                                                                                {"en-US", "Your data has been changed" }};
        public static LanguageDefaultValues LANG_M_USERS_GRID_BTN_ADD = new LanguageDefaultValues("LANG_M_USERS_GRID_BTN_ADD", "USERS_BACKEND"){
                                                                {"de-DE", "Benutzer erstellen"},
                                                                {"en-US", "Create User"}};
        public static LanguageDefaultValues LANG_M_USERS_GRID_BTN_PERMISSIONS = new LanguageDefaultValues("LANG_M_USERS_GRID_BTN_PERMISSIONS", "USERS_BACKEND"){
                                                                {"de-DE", "Individuelle Benutzerrechte"},
                                                                {"en-US", "Customized User Permissions"}};
        public static LanguageDefaultValues LANG_M_USERS_GRID_TITLE = new LanguageDefaultValues("LANG_M_USERS_GRID_TITLE", "USERS_BACKEND"){
                                                                {"de-DE", "Benutzerverwaltung"},
                                                                {"en-US", "User Management"}};
        public static LanguageDefaultValues LANG_M_USERS_LOCKED = new LanguageDefaultValues("LANG_M_USERS_LOCKED", "USERS_BACKEND"){
                                                                {"de-DE", "Gesperrt?"},
                                                                {"en-US", "Locked out?"}};
        public static LanguageDefaultValues LANG_M_USERS_PGROUP = new LanguageDefaultValues("LANG_M_USERS_PGROUP", "USERS_BACKEND"){
                                                                {"de-DE", "Benutzergruppe"},
                                                                {"en-US", "User group"}};
        public static LanguageDefaultValues LANG_M_USERS_PKID = new LanguageDefaultValues("LANG_M_USERS_PKID", "USERS_BACKEND"){
                                                            { "de-DE", "Eind. ID" },
                                                            { "en-US", "Unique ID" }};
        public static LanguageDefaultValues LANG_M_USERS_UNAME = new LanguageDefaultValues("LANG_M_USERS_UNAME", "USERS_BACKEND"){
                                                            { "de-DE", "Benutzername"},
                                                            { "en-US", "Username"}};

        #endregion Fields
    }
}