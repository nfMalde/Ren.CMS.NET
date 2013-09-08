namespace Ren.CMS.Models.FormDialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Security;

    public class FormDialogDataStore
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public List<FormDialogDataStoreRow> Contents = new List<FormDialogDataStoreRow>();

        #endregion Fields
    }

    public class FormDialogDataStoreRow
    {
        #region Constructors

        public FormDialogDataStoreRow()
        {
        }

        public FormDialogDataStoreRow(string label, object value)
        {
            this.Label = label;
            this.Value = value;
        }

        #endregion Constructors

        #region Properties

        public string Label
        {
            get;set;
        }

        public object Value
        {
            get;set;
        }

        #endregion Properties
    }

    public class FormDialogElement
    {
        #region Fields

        public FormDialogDataStore DataStore = new FormDialogDataStore();

        #endregion Fields

        #region Constructors

        public FormDialogElement()
        {
        }

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

        #endregion Constructors

        #region Properties

        public string CustomRenderer
        {
            get; set;
        }

        public string CustomTemplate
        {
            get; set;
        }

        public string ID
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public bool Required
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string Validation
        {
            get; set;
        }

        public object Value
        {
            get; set;
        }

        #endregion Properties
    }

    public static class FormDialogElementType
    {
        #region Fields

        public const string Checkbox = "checkbox";
        public const string Combobox = "combobox";
        public const string Hidden = "hidden";
        public const string Password = "password";
        public const string Radiobutton = "radiobutton";
        public const string RichTextbox = "richttextbox";
        public const string Textbox = "textbox";

        #endregion Fields

        #region Methods

        public static string Custom(string type)
        {
            return type;
        }

        #endregion Methods
    }

    public class FormDialogReturn : JsonResult
    {
        #region Fields

        private string _message = String.Empty;
        private bool _success = false;

        #endregion Fields

        #region Constructors

        public FormDialogReturn()
        {
        }

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="success">Will return to the Formdialog ajax callback: true = Successfull, false=Display ErrorMessage</param>
        /// <param name="message">Message to return</param>
        public FormDialogReturn(bool success, string message)
        {
            this._message = message;
            this._success = success;
        }

        #endregion Constructors

        #region Properties

        public string Message
        {
            get { return this._message; } set { this._message = value; }
        }

        public bool Success
        {
            get { return this._success; } set { this._success = value; }
        }

        #endregion Properties

        #region Methods

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            this.Data = new { success = this._success, message = this._message };

            base.ExecuteResult(controllerContext);
        }

        #endregion Methods
    }

    public class FormDialogSettings
    {
        #region Fields

        public string ElementID = "FRMDLG-"+ new CryptoServices().ConvertToSHA1( Guid.NewGuid().ToString());
        public List<FormDialogElement> Elements = new List<FormDialogElement>();

        #endregion Fields

        #region Properties

        public string AbortText
        {
            get; set;
        }

        public bool AutoOpen
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public FormMethod Method
        {
            get; set;
        }

        public bool Modal
        {
            get; set;
        }

        public string OnSuccess
        {
            get; set;
        }

        public string SaveText
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string URL
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        #endregion Properties
    }
}