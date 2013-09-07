using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ren.CMS.CORE.nhibernate.Domain;
using Ren.CMS.CORE.nhibernate.Mapping;

namespace Ren.CMS.CORE.nhibernate.Repositories
{
    public class ContentTagRepository: Base.BaseRepository<ContentTag>
    {
        public ContentTagRepository()
            : base()
        { 
            
        
        
        }


        public ContentTag GetTagByName(string Tagname)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {

                var t = session.QueryOver<ContentTag>().Where(NHibernate.Criterion.Expression.Where<ContentTag>(e => e.TagName == Tagname)).SingleOrDefault();
                return t;
            
            
            }
        

        
        
        
        }




    }
}
