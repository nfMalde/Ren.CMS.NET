namespace Ren.CMS.CORE.nhibernate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;
    using NHibernate.Criterion;

    using Ren.CMS.CORE.nhibernate.Domain;
    using Ren.CMS.CORE.nhibernate.Mapping;

    public class ContentTags2ContentRepository : Base.BaseRepository<ContentTags2Content>
    {
        #region Constructors

        public ContentTags2ContentRepository()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<ContentTags2Content> GetByTagId(int tagId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<ContentTags2Content>().Where(expression: Expression.Where<ContentTags2Content>(t => t.TagID == tagId)).List<ContentTags2Content>();
            }
        }

        public int[] GetContentIDsByTagId(int tagId)
        {
            var tag2c = this.GetByTagId(tagId).ToList();

            var ii = tag2c.Count();
            int[] i = new int[ii];
            for (int x = 0; x < tag2c.Count(); x++)
                i[x] = tag2c[x].Id;

            return i;
        }

        #endregion Methods
    }
}