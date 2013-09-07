using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ProfileVarMap : ClassMapping<ProfileVar> {
        
        public ProfileVarMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Profile_Vars");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Name);
			Property(x => x.LangLine);
			Property(x => x.Section);
			Property(x => x.ViewName);
			Property(x => x.Active);
			Property(x => x.ShowInProfile);
        }
    }
}
