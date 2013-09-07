using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ren.CMS.CORE.Language.LanguageDefaults
{

    public static class LanguageDefaultsMessages
    {
        public static LanguageDefaultValues LANG_SHARED_MESSAGE_NO_PERMISSION = new LanguageDefaultValues("LANG_SHARED_MESSAGE_NO_PERMISSION") { 
                                                                            { "de-DE", "Sie haben hierfür leider keine Berechtigung." }, 
                                                                            { "en-US", "You don´t have the permission for this action." } };
        public static LanguageDefaultValues LANG_SHARED_MESSAGE_FORM_NOT_VALID = new LanguageDefaultValues("LANG_SHARED_MESSAGE_FORM_NOT_VALID") { 
                                                                            { "de-DE", "Ihre Angaben sind fehlerhaft und müssen korrigiert werden." }, 
                                                                            { "en-US", "The data you entered is not valid. Please check them again." } };


        public static LanguageDefaultValues LANG_SHARED_MESSAGE_FORM_CONTENT_SAVED = new LanguageDefaultValues("LANG_SHARED_MESSAGE_FORM_CONTENT_SAVED") { 
                                                                            { "de-DE", "Sie haben den Inhalt erfolgreich aktualisiert." }, 
                                                                            { "en-US", "Update of the content was successfull" } };

     
    
    }

    public static class LanguageDefaultsShared
    {
        public static LanguageDefaultValues LANG_SHARED_CLICK_HERE_FOR_INFO = new LanguageDefaultValues("LANG_SHARED_CLICK_HERE_FOR_INFO") { 
                                                                            { "de-DE", "Klicken Sie hier um die Hilfe anzuzeigen" }, 
                                                                            { "en-US", "Please click here to get help with this" } };

        public static LanguageDefaultValues LANG_SHARED_HIDE_HELP = new LanguageDefaultValues("LANG_SHARED_HIDE_HELP") { 
                                                                { "de-DE", "Hilfe ausblenden" }, 
                                                                { "en-US", "Hide help" } };

        public static LanguageDefaultValues LANG_SHARED_YES = new LanguageDefaultValues("LANG_SHARED_YES") { 
                                                            { "de-DE", "Ja" }, 
                                                            { "en-US", "Yes" } };

        public static LanguageDefaultValues LANG_SHARED_SUCCESS = new LanguageDefaultValues("LANG_SHARED_SUCCESS") { 
                                                                { "de-DE", "Erfolg:" }, 
                                                                { "en-US", "Success:" } };
        public static LanguageDefaultValues LANG_SHARED_ERROR = new LanguageDefaultValues("LANG_SHARED_ERROR") { 
                                                             { "de-DE", "Fehler:" }, 
                                                             { "en-US", "Error:" } };

        public static LanguageDefaultValues LANG_SHARED_NO = new LanguageDefaultValues("LANG_SHARED_NO") { 
                                                           { "de-DE", "Nein" }, 
                                                           { "en-US", "No" } };

        public static LanguageDefaultValues LANG_SHARED_ADD = new LanguageDefaultValues("LANG_SHARED_ADD") { 
                                                            { "de-DE", "Hinzuf&uuml;gen" }, 
                                                            { "en-US", "Add" } };

        public static LanguageDefaultValues LANG_SHARED_EDIT = new LanguageDefaultValues("LANG_SHARED_EDIT") { 
                                                            { "de-DE", "Bearbeiten" }, 
                                                            { "en-US", "Edit" } };

        public static LanguageDefaultValues LANG_SHARED_DELETE = new LanguageDefaultValues("LANG_SHARED_DELETE"){ 
                                                            { "de-DE", "L&ouml;schen"} , 
                                                            { "en-US","DELETE"} };

        public static LanguageDefaultValues LANG_SHARED_SAVE = new LanguageDefaultValues("LANG_SHARED_SAVE"){
                                                            {"de-DE","Speichern"},
                                                            {"en-US", "Save"}};
        public static LanguageDefaultValues LANG_SHARED_ABORT = new LanguageDefaultValues("LANG_SHARED_ABORT"){
                                                            {"de-DE", "Abbrechen"},
                                                            {"en-US", "Abort"}};

        public static LanguageDefaultValues LANG_CONTENTS = new LanguageDefaultValues("LANG_CONTENTS", "CONTENT_MANAGEMENT") { 
        
        
        {"de-DE", "Inhalte"},
        {"en-US", "Contents"}
        
        
        };
    

    }
}