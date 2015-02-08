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
    public class SettingModelMap : ClassMapping<SettingModel>
    {
        #region Constructors

        public SettingModelMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"SettingModels");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity)); 
            Property(x => x.SettingName, map => map.NotNullable(true));
            Property(x => x.SettingLangLineLabel, map => map.NotNullable(true));
            Property(x => x.SettingLangLineDescr, map => map.NotNullable(true));
            Property(x => x.SettingDefaultValue);
            Property(x => x.SettingRelation, map => map.NotNullable(true));
            Property(x => x.SettingType, map => map.NotNullable(true));
            Property(x => x.ValueType, map => map.NotNullable(true));
            Property(x => x.Cid);
        }

        #endregion Constructors
    }
}