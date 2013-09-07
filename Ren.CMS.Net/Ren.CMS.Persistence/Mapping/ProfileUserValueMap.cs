using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ProfileUserValueMap : ClassMapping<ProfileUserValue> {
        
        public ProfileUserValueMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Profile_User_Values");
			Schema("dbo");
			Lazy(true);
			Property(x => x.VarName);
			Property(x => x.VarValue);
			Property(x => x.Pkid);
        }
    }
}
