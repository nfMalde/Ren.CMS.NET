namespace Ren.CMS.Persistence.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.Persistence.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class tFileTypeMap : ClassMapping<tFileType>
    {
        #region Constructors

        public tFileTypeMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "FileTypes");
            Schema("dbo");
            Lazy(false);
            Id<int>(x => x.Id, map => {

                map.Generator(Generators.Identity);

            });
            //Property(x => x.Id, map => map.NotNullable(true));

            Property(x => x.Physical, map =>
            {
                map.NotNullable(true);

            });
            Property(x => x.ProfileId, map => map.NotNullable(false));
            Property(x => x.TypeName, map => {

                map.NotNullable(true);

          

            });
            
 
       

            ManyToOne(x => x.Profile, m =>
            {

                m.Column(c => c.Name("ProfileId"));
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
                m.Lazy(LazyRelation.NoLazy);

            });
            /*
            Set(x => x.AllowedMIMETypes, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("");
                });
                mapping.Inverse(true);
                mapping.Cascade(Cascade.None);

            },
            r => r.OneToMany());
             * */

            //OneToOne(x => x.Category, map =>
            //{
            //    map.PropertyReference(
            //    map.Column("CID");
            //    map.NotFound(NotFoundMode.Ignore);
            //    map.Cascade(Cascade.None);
            //});

            //ManyToOne(x => x.User, map =>
            //{
            //    map.PropertyRef("PKID");
            //    map.Column("CreatorPKID");

            //    map.Cascade(Cascade.None);
            //});
        }

        #endregion Constructors
    }
}