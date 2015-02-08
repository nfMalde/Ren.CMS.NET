using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Backend
{
    public class BackendWidget
    {
        public string widgetName { get; set; }
        public int definedWidth { get; set; }
        public int definedHeight { get; set; }
        public string neededPermission { get; set; }
        public string widgetPartialView { get; set; }
        public string Icon { get; set; }
    }
}
