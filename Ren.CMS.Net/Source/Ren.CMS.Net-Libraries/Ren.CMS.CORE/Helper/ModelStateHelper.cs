using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
 

namespace Ren.CMS.CORE.Helper.ModelStateHelper
{
    public static class ModelStateHelper
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0);
            }
            return null;
        }
    }
}
