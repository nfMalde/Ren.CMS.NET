namespace Ren.CMS.CORE.nhibernate.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.CORE.nhibernate.Domain;

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class LanguageMap : ClassMapping<tbLanguage>
    {
        #region Constructors

        public LanguageMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Language");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Name, map => map.NotNullable(true));
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Content, map => map.NotNullable(true));
            Property(x => x.Package, map => map.NotNullable(true));
            Property(x => x.Code, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}