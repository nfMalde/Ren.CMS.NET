using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Filemanagement
{
    public class nFileProfile
    {
        public string ProfileName { get; set; }

        public int Id { get; set; }

        public List<nFileSetting> Settings { get; set; }

    }

    public class nFileSetting
    {
        public int Id { get; set; }
        public int ValueId { get; set; }

        public string SettingName { get; set; }

        public string Value { get; set; }
    }

}
