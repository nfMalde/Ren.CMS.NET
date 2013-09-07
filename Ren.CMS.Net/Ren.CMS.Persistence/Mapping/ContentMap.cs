using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;
using NHibernate;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ContentMap : ClassMapping<TContent>
    {
        
        public ContentMap() {
			Table("nfcms_Content");
			Schema("dbo");
			Lazy(false);
            Id<int>(x => x.Id, map => {

                map.Generator(Generators.Identity);

            
            
            });
			//Property(x => x.Id, map => map.NotNullable(true));
			 
            Property(x => x.Cid, map =>
            {
                map.NotNullable(true);


            });
			Property(x => x.CreatorPKID, map => map.NotNullable(true));
			Property(x => x.Locked, map => {


                map.NotNullable(true);
           

                map.Type(NHibernateUtil.Boolean);
            
            
            
            
            }); 
			Property(x => x.RatingGroupID);
			Property(x => x.CDate);
 
 
			Property(x => x.ContentType, map => map.NotNullable(true));
 
			Property(x => x.ContentRef);
			Property(x => x.CreatorSpecialName);

 



            Set(x => x.Texts, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("ContentId");
                });
                mapping.Inverse(true);
            },
       r => r.OneToMany());

            ManyToOne(x => x.User, m =>
            {

                m.Column(c => c.Name("CreatorPKID"));
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
                m.Lazy(LazyRelation.NoLazy);


            });

            ManyToOne(x => x.Category, m =>
            {

                m.Column(c => c.Name("CID"));
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
                m.Lazy(LazyRelation.NoLazy);


            });

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
    }
}
