using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;

namespace Ren.CMS.CORE.nhibernate.Base
{
    public class BaseRepository<TEntity>
 where TEntity : class, new()
    {

        public BaseRepository()
        {


        }

        //Fügt einen Eintrag in der Tabelle hinzu
        public void Add(TEntity newEntity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //Speichert aktuellen Eintrag in der Session
                    session.Save(newEntity);
                    //Commit - Besätigen. Hier wird nun das SQL genriert
                    transaction.Commit();
                }
            }
        }

        public object AddAndGetId(TEntity newEntity)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //Speichert aktuellen Eintrag in der Session
                   object id = session.Save(newEntity);
                    //Commit - Besätigen. Hier wird nun das SQL genriert
                    transaction.Commit();
                    return id;
                }
            }
        
        }
        public virtual TEntity GetOne(NHibernate.Criterion.ICriterion expression)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //Hier holen wir einen Eintrag aus der Tabelle.
                var result = session.QueryOver<TEntity>().Where(expression).Take(1).SingleOrDefault();
                return result ?? null;
            }
        }

        public IEnumerable<TEntity> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            //Mit dieser Base funktion holen wir mehrere anhand einer WHERE Clausel und ORDER BY Clausel
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.QueryOver<TEntity>();
                if (where != null)
                {

                    result = result.Where(where);


                }
                
                if (orderBy != null)
                {
                    if (Asc)
                        return result.OrderBy(orderBy).Asc.List<TEntity>();
                    else
                        return result.OrderBy(orderBy).Desc.List<TEntity>();
                }

                return result.List<TEntity>() ?? new List<TEntity>();
                //Wir geben eine Liste an Entities zurück.
                 


            }

        }





        public void Update(TEntity entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }

            //Diese Base Funktion ist zum aktualisieren von Einträgen.
        }

        public void Delete(TEntity entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }

            //Und diese zum löschen.
        }

    }
}
