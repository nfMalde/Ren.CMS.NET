namespace Ren.CMS.Persistence.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Assembly)]
    public class PersistenceAssembly : Attribute
    {
        #region Constructors

        public PersistenceAssembly()
        {
        }

        #endregion Constructors
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistenceMapping : Attribute
    {
        #region Constructors

        public PersistenceMapping()
        {
        }

        #endregion Constructors
    }
}