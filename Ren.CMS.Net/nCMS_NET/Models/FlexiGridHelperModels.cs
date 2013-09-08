namespace Ren.CMS.Models.FlexiGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Security;

    public class FlexiButtons
    {
        #region Fields

        public string jsOnPress = "function () { alert('Please change the \'jsOnPress\' Attribute in the FlexiButtons Model'); }";
        public bool Seperator = false;

        #endregion Fields

        #region Constructors

        public FlexiButtons()
        {
        }

        public FlexiButtons(string name, string onPress ="",  bool seperator = false, string bgClass = "")
        {
            this.Name = name;

            if (!String.IsNullOrEmpty(onPress))
                this.jsOnPress = onPress;
            this.Seperator = seperator;

            this.BGclass = bgClass;
        }

        #endregion Constructors

        #region Properties

        public string BGclass
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }

    public class FlexiColModel
    {
        #region Constructors

        public FlexiColModel()
        {
        }

        public FlexiColModel(string display, string name, int width = 0, int height = 0, bool sortable = true, string align = "left")
        {
            this.Display = display;

            this.Name = name;

            this.Width = width;

            this.Height = height;

            this.Sortable = sortable;

            this.Align = align;
        }

        #endregion Constructors

        #region Properties

        public string Align
        {
            get; set;
        }

        public string Display
        {
            get; set;
        }

        public int Height
        {
            get;set;
        }

        public string Name
        {
            get; set;
        }

        public bool Sortable
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        #endregion Properties
    }

    public class FlexiGridReturn : JsonResult
    {
        #region Fields

        private int Page = 1;
        private List<FlexiRow> rows = new List<FlexiRow>();
        private int TotalRows = 0;

        #endregion Fields

        #region Constructors

        public FlexiGridReturn(int page, int totalRows)
        {
            this.Page = page;
            this.TotalRows = totalRows;
        }

        public FlexiGridReturn(List<FlexiRow> Rows, int page, int totalRows)
        {
            this.rows.AddRange(Rows);
            this.Page = page;
            this.TotalRows = totalRows;
        }

        #endregion Constructors

        #region Properties

        public List<FlexiRow> GetRows
        {
            get { return this.rows; }
        }

        #endregion Properties

        #region Methods

        public void AddRow(FlexiRow Row)
        {
            this.rows.Add(Row);
        }

        public void AddRows(List<FlexiRow> Rows)
        {
            this.rows.AddRange(Rows);
        }

        public void Clear()
        {
            this.rows.Clear();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            List<object> DataRows = new List<object>();
            foreach (FlexiRow r in rows)
            {

                DataRows.Add(new
                {
                    id = r.id,
                    cell = r.cell
                });

            }

            this.Data = new { page = this.Page, total = this.TotalRows, rows = this.rows };
            base.ExecuteResult(context);
        }

        public void Remove(int index)
        {
            if(this.rows[index] != null)
            {
                this.rows.Remove(this.rows[index]);
            }
        }

        #endregion Methods
    }

    public class FlexiRow
    {
        #region Fields

        public Dictionary<string, object> cell = new Dictionary<string, object>();
        public object id = Guid.NewGuid();

        #endregion Fields
    }

    public class FlexiSearchItem
    {
        #region Constructors

        public FlexiSearchItem()
        {
        }

        public FlexiSearchItem(string display, string name, bool isDefault)
        {
            this.Display = display;
            this.Name = name;
            this.IsDefault = isDefault;
        }

        #endregion Constructors

        #region Properties

        public string Display
        {
            get; set;
        }

        public bool IsDefault
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }

    public class FlexiSettings
    {
        #region Fields

        public List<FlexiButtons> Buttons = new List<FlexiButtons>();
        public List<FlexiColModel> ColumnModel = new List<FlexiColModel>();
        public string DataType = "json";
        public string ElementID = "FLXGRID-"+ new CryptoServices().ConvertToSHA1( Guid.NewGuid().ToString());
        public List<FlexiSearchItem> SearchItems = new List<FlexiSearchItem>();

        #endregion Fields

        #region Properties

        public string Height
        {
            get; set;
        }

        public bool ShowTableToggleBtn
        {
            get; set;
        }

        public bool SingleSelect
        {
            get; set;
        }

        public string SortName
        {
            get; set;
        }

        public string SortOrder
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Url
        {
            get; set;
        }

        public bool UsePager
        {
            get; set;
        }

        public bool UseRP
        {
            get; set;
        }

        public string Width
        {
            get; set;
        }

        #endregion Properties
    }
}