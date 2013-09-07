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
    public class ContentTextMap : ClassMapping<Persistence.Domain.ContentText>
    {

        public ContentTextMap()
        {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix +"Content_Text");
			Schema("dbo");
			Lazy(false);
            Id<int>(x => x.Id, map => {

                map.Generator(Generators.Identity);

            
            
            });
            Property(x => x.ContentId, map => map.NotNullable(true));
            Property(x => x.LangCode, map => map.NotNullable(true));

			//Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.Title, map => map.NotNullable(true));
       
			 
			Property(x => x.Seoname);
 
 
			Property(x => x.MetaKeyWords);
			Property(x => x.MetaDescription);
			Property(x => x.PreviewText);
			 
            Property(x => x.LongText, x => x.Type(NHibernateUtil.StringClob));

            ManyToOne(x => x.Content, m => {





                m.Lazy(LazyRelation.NoLazy);
                m.Column("ContentId");
               
            
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
