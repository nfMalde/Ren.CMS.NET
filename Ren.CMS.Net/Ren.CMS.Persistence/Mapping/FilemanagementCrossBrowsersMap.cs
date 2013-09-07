using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;
using Ren.CMS.Persistence.Domain;

namespace Ren.CMS.Persistence.Mapping
{
    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class FilemanagementCrossBrowsersMap:ClassMapping<FilemanagementCrossBrowsers>
    {
        public FilemanagementCrossBrowsersMap()
        {
            Lazy(true);
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Filemanagement_Crossbrowsers");
            Schema("dbo");
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
            Property<string>(x => x.browserFullName, map => {
                map.NotNullable(true);
 
            
            });

            Property<string>(x => x.browserID, map =>
            {
                map.NotNullable(true);
            });


            Property<string>(x => x.FileFormat, map =>  {
                map.NotNullable(true);
                map.Length(5);
           
            
            });


            Property<string>(x => x.FileType, map => 
            {
                map.NotNullable(true);
             
            
            });
        
        
        }
    }
}
