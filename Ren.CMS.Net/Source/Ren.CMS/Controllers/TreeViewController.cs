namespace Ren.CMS.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class TreeViewController : Controller
    {
        #region Methods

        public static List<TreeViewLocation> GetLocations()
        {
            var locations = new List<TreeViewLocation>
                                {
                                    new TreeViewLocation
                                        {
                                            Name = "United States",
                                            ChildLocations =
                                                {
                                                    new TreeViewLocation
                                                        {
                                                            Name = "Chicago",
                                                            ChildLocations =
                                                                {
                                                                    new TreeViewLocation {Name = "Rack 1"},
                                                                    new TreeViewLocation {Name = "Rack 2"},
                                                                    new TreeViewLocation {Name = "Rack 3"},
                                                                }
                                                        },
                                                    new TreeViewLocation {Name = "Dallas"}
                                                }
                                        },
                                    new TreeViewLocation
                                        {
                                            Name = "Canada",
                                            ChildLocations =
                                                {
                                                    new TreeViewLocation {Name = "Ontario"},
                                                    new TreeViewLocation {Name = "Windsor"}
                                                }
                                        }
                                };
            return locations;
        }

        public ActionResult Index()
        {
            var locations = GetLocations();

            return View(locations);
        }

        #endregion Methods
    }

    public class TreeViewLocation
    {
        #region Constructors

        public TreeViewLocation()
        {
            ChildLocations = new HashSet<TreeViewLocation>();
        }

        #endregion Constructors

        #region Properties

        public ICollection<TreeViewLocation> ChildLocations
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}