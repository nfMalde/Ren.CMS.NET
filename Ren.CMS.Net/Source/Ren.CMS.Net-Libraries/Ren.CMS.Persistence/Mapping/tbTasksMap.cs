using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.Persistence.Domain;


namespace Ren.CMS.Persistence.Mapping {

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class TbTasksMap : ClassMapping<TbTasks> {

        public TbTasksMap()
        {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Tasks");
			Schema("dbo");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Taskid, map => map.NotNullable(true));
			Property(x => x.Running, map => map.NotNullable(true));
			Property(x => x.Taskname, map => map.NotNullable(true));
			Property(x => x.Currentaction);
			Property(x => x.Moduledbtable);
			Property(x => x.Moduledbidentifier);
			Property(x => x.Moduledbidvalue);
			Property(x => x.Percentage);
        }
    }
}
