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
    public class LinkMap : ClassMapping<Link>
    {
        #region Constructors

        public LinkMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Links");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.LinkType, map => map.NotNullable(true));
            Property(x => x.LinkController);
            Property(x => x.LinkAction);
            Property(x => x.LinkHref);
            Property(x => x.LinkText, map => map.NotNullable(true));
            Property(x => x.LinkRelationship);
            Property(x => x.LinkIsActive);
            Property(x => x.SublinkController);
            Property(x => x.SublinkAction);
            Property(x => x.SublinkFrom);
            Property(x => x.NormalStateClass);
            Property(x => x.HoverStateClass);
        }

        #endregion Constructors
    }
}