namespace Ren.CMS.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Tool.hbm2ddl;

    using Ren.CMS.Persistence;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;

    public class ContentRepository : Base.BaseRepository<TContent>
    {
        #region Constructors

        public ContentRepository()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        public override TContent GetOne(NHibernate.Criterion.ICriterion expression)
        {
            var one = base.GetOne(expression);

            if (one != null)
            {

              //  one.User = new Base.BaseRepository<User>().GetOne(NHibernate.Criterion.Expression.Where<User>(u => u.Pkid == one.CreatorPKID)) ?? new User();
              //  one.Category = new Base.BaseRepository<Category>().GetOne(NHibernate.Criterion.Expression.Where<Category>(c => c.Pkid == one.Cid)) ?? new Category();

            }

            return one;
        }

        public override  void Add(TContent tContent)
        {


            this.addTContent(tContent);
        }

        public override void Update(TContent tContent)
        {
            base.Update(tContent);
            Base.BaseRepository<ContentText> TextRepo = new Base.BaseRepository<Domain.ContentText>();

            foreach (var text in tContent.Texts)
            {
                text.ContentId = tContent.Id;
                TextRepo.Update(text);
            }
           
        }


        public override void Delete(Domain.TContent entity)
        {
            Base.BaseRepository<ContentText> TextRepo = new Base.BaseRepository<Domain.ContentText>();

            foreach (var text in entity.Texts)
            {
                if(text.ContentId == entity.Id)
                    TextRepo.Delete(text);
            }

            base.Delete(entity);
        }

        public override  object AddAndGetId(TContent tContent)
        {


            return this.addTContent(tContent);
        }

        private object addTContent (TContent tContent)
        {

            object id = base.AddAndGetId(tContent);

            Base.BaseRepository<ContentText> TextRepo = new Base.BaseRepository<Domain.ContentText>();

            foreach (var text in tContent.Texts)
            {
                int ix = 0;
                if(int.TryParse(id.ToString(), out ix) )
                {
                    text.ContentId = ix;

                    TextRepo.Add(text);
                }
            }

            return id;
        }

        public List<TContent> Pagination(ref int totalRows, int pageSize,
            int pageIndex,
            string[] contentTypes,
            Category category = null,
            string orderBy = "cDate",
            string orderType = "DESC",
            ContentTag Tag = null,
            bool locked = false, string[] languages = null)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

               TContent AliasContent = new TContent();
               User AliasUser = new User();
                IEnumerable<ContentText> AliasText = new List<ContentText>();

               IEnumerable<ContentText> AliasText2 = new List<ContentText>();

                var res = session.QueryOver<TContent>(() => AliasContent)

                  .Where( NHibernate.Criterion.Expression.In("ContentType",contentTypes));

                 //By Category
               if (category != null)
               {
                   res = res.Where(e => e.Cid == category.Pkid);

               }
                 IQueryOver<TContent,ContentTags2Content> restags = null;
               if (Tag != null)
               {
                   ContentTags2ContentRepository c2t = new ContentTags2ContentRepository();

                   res = res.Where(NHibernate.Criterion.Expression.In("Id", c2t.GetContentIDsByTagId(Tag.Id)));

               }
                ContentText F = new ContentText();
               if (languages != null)
               {

                  // res = res.And(Restrictions.On<ContentText>(e => e.LangCode).IsIn(languages));
                  //Dirty
                   var Texts = new Base.BaseRepository<ContentText>().GetMany(NHibernate.Criterion.Expression.In(Projections.Property<ContentText>(e => e.LangCode), languages));
                   IEnumerable<int> ids = Texts.Select(e => e.ContentId);
                   int[] _ids = new int[ids.Count()];
                   int xy = 0;
                   foreach(int idx in ids)
                   {
                       _ids[xy] = idx;
                       xy++;
                   }
                       res = res.Where(NHibernate.Criterion.Expression.In(Projections.Property<TContent>(e => e.Id), _ids));

               }
               var propi = typeof(TContent).GetProperties().Where(e => e.Name.ToLower() == orderBy.ToLower());
               if (propi.Count() > 0)
               {
                   orderBy = propi.First().Name;
                   //Order
                   if (orderType.ToLower() == "desc")
                   {

                       res = res.OrderBy(NHibernate.Criterion.Projections.Property(orderBy)).Desc;

                   }
                   else
                   {

                       res = res.OrderBy(NHibernate.Criterion.Projections.Property(orderBy)).Asc;

                   }
               }

               var resultset = (pageSize != 0 ?
                    res.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize) : res);

               List<TContent> cc = new List<TContent>();
               foreach (var e in res.List<TContent>())
               {
               // e.User = new Base.BaseRepository<User>().GetOne(NHibernate.Criterion.Expression.Where<User>(u => u.Pkid == e.CreatorPKID)) ?? new User();
               // e.Category = new Base.BaseRepository<Category>().GetOne (NHibernate.Criterion.Expression.Where<Category>(c => c.Pkid == e.Cid)) ?? new Category();
                cc.Add(e);

               }

                var count = session.QueryOver<TContent>()
                            .Where(NHibernate.Criterion.Expression.In("ContentType", contentTypes))
                            .RowCount();

                totalRows = count;

                return cc.ToList();

            }
        }

        #endregion Methods
    }
}