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
    public class FileMap : ClassMapping<File>
    {
        #region Constructors

        public FileMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Files");
            Schema("dbo");
            Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.FilePath, map => map.NotNullable(true));
            Property(x => x.AliasName, map => map.NotNullable(true));
            Property(x => x.isActive, map => map.NotNullable(true));
            Property(x => x.Physical);
            Property(x => x.FileSize);

            ManyToOne(x => x.MainFile, map =>
            {
                map.Column("FileReference");
                map.Lazy(LazyRelation.NoLazy);
                map.Fetch(FetchKind.Select);
                map.Insert(true);
                map.Update(true);
                map.Cascade(Cascade.None);
            });
            Bag(x => x.ReferencedFiles, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("FileReference");
                });
                mapping.Inverse(true);
                mapping.Cascade(Cascade.All);

            },
                r => r.OneToMany());

        }

        #endregion Constructors
    }
}