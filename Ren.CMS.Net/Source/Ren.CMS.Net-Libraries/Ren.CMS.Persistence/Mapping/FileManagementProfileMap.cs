namespace Ren.CMS.Persistence.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.Persistence.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class FileManagementProfileMap : ClassMapping<FileManagementProfile>
    {
        #region Constructors

        public FileManagementProfileMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FileManagementProfiles");
            Schema("dbo");
            Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.ProfileName, map => map.NotNullable(true));
            Bag(x => x.Settings, c => { 
                c.Inverse(true); 
                
                c.Key(e => e.Column("ProfileID")); }, 
                r => { r.OneToMany(); });
        }

        #endregion Constructors
    }
}