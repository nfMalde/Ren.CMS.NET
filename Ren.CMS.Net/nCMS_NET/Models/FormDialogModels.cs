using Ren.CMS.CORE.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Ren.CMS.Models.FormDialog
{
    public  class FormDialogReturn : JsonResult
    {
        private string _message = String.Empty;
        private bool _success = false;
        public FormDialogReturn() { }
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="success">Will return to the Formdialog ajax callback: true = Successfull, false=Display ErrorMessage</param>
        /// <param name="message">Message to return</param>
        public  FormDialogReturn(bool success, string message) 
        {

            this._message = message;
            this._success = success;
             
            
        }
        public bool Success { get { return this._success; } set { this._success = value; } }
        public string Message { get { return this._message; } set { this._message = value; } }


        public override void ExecuteResult(ControllerContext controllerContext)
        {
        
        
            this.Data = new { success = this._success, message = this._message };

            base.ExecuteResult(controllerContext);
        
        
        }
       


    

    }

    public class FormDialogElement
    {
        public FormDialogElement() { }
        public FormDialogElement(string type, string id, string name, string label, object value, IEnumerable<FormDialogDataStoreRow> dataStore = null, string customRenderer = null, string customTemplate = null, string validators = null, bool required= false)
        {
            this.Type = type;
            this.Label = label;
            this.Value = value;
            this.ID = id;
            this.Name = name;
            if (dataStore != null)
                this.DataStore.Contents.AddRange(dataStore);
            
            this.CustomRenderer = customRenderer;
            this.CustomTemplate = customTemplate;
            this.Validation = validators;
            this.Required = required;
        }


        public string Type { get; set; }
        public string Label { get; set; }
        public object Value { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string CustomRenderer { get; set; }
        public string CustomTemplate { get; set; }
        public FormDialogDataStore DataStore = new FormDialogDataStore();
        public bool Required { get; set; }
        public string Validation { get; set; } 
    }
    public class FormDialogDataStoreRow
    {
        public FormDialogDataStoreRow()
        { }

        public FormDialogDataStoreRow(string label, object value)
        {
            this.Label = label;
            this.Value = value;
        
        }

        public string Label { get;set;}
        public object Value { get;set;}
    
    }
    public class FormDialogDataStore 
    {
        /// <summary>
        /// 
        /// </summary>
        public List<FormDialogDataStoreRow> Contents = new List<FormDialogDataStoreRow>();
    
    }
    public class FormDialogSettings
    {
        public string ElementID = "FRMDLG-"+ new CryptoServices().ConvertToSHA1( Guid.NewGuid().ToString());
        public bool AutoOpen { get; set; }
        public FormMethod Method { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public bool Modal { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<FormDialogElement> Elements = new List<FormDialogElement>();
        public string OnSuccess { get; set; }
        public string SaveText { get; set; }
        public string AbortText { get; set; }
    }

    public static class FormDialogElementType
    {
        public const string Textbox = "textbox";
        public const string Radiobutton = "radiobutton";
        public const string Combobox = "combobox";
        public const string Checkbox = "checkbox";
        public const string Password = "password";
        public const string Hidden = "hidden";
        public static string Custom(string type) { return type; }
        public const string RichTextbox = "richttextbox";
    
    }
}