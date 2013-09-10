namespace Ren.CMS.CORE.Extras
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class Extras
    {
        #region Constructors

        public Extras()
        {
        }

        #endregion Constructors

        #region Methods

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

        public bool ViewExists(string name, ControllerContext ControllerContextVar)
        {
            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContextVar, name, null);
            return (result.View != null);
        }

        #endregion Methods
    }

    public class LocationBar
    {
        #region Fields

        private ControllerContext context = null;
        private string _linkTemplate_partialview = "location_bar_link";
        private string _locationBarTemplate_partialview = "location_bar";
        private List<LocationModel> _Locations = new List<LocationModel>();

        #endregion Fields

        #region Constructors

        public LocationBar(ControllerContext controllerContext)
        {
            this.context = controllerContext;
        }

        #endregion Constructors

        #region Methods

        public void AddLocation(LocationModel Location)
        {
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
        public void Render()
        {
            this.context.Controller.ViewData["LocationBarCollection"] = this._Locations;
        }

        #endregion Methods
    }

    public class LocationModel
    {
        #region Properties

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

        public string Name
        {
            get;
            set;
        }

        #endregion Properties
    }
}