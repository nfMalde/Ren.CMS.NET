using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.nhibernate.Base
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class PersistenceAssembly:Attribute
    {
        public PersistenceAssembly()
        { 
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class PersistenceMapping : Attribute
    {
        public PersistenceMapping()
        { }
    
    }
}
