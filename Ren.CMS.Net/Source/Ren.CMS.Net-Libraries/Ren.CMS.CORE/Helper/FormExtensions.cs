using Ren.CMS.CORE.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.CORE.Helper
{
    public static class FormExtensions
    {
         

        public static FormGatherer FormGatherer(this HttpRequestBase request)
        {
           
            return new FormGatherer(request);
        }
    }
}
