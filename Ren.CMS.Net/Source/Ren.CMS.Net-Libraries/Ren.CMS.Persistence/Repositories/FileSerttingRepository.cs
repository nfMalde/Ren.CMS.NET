using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileSettingRepository:Base.BaseRepository<Domain.FileManagementFileSetting>
    {
        /// <summary>
        /// Repository for Setting Values
        /// </summary>
        private FileSettingValueRepository ValueRepository = null;

        /// <summary>
        /// Contructor
        /// </summary>
        public FileSettingRepository() : base()
        {
            this.ValueRepository = new FileSettingValueRepository();
        }

        /// <summary>
        /// Inserts or Updates an Value
        /// </summary>
        /// <param name="Val">The Value to be inserted or updated</param>
        public void InsertOrUpdateValue(Domain.FileSettingValue Val)
        {
            var c = this.ValueRepository.GetOne(NHibernate.Criterion.Expression.Where<Domain.FileSettingValue>(e => e.Id == Val.Id));
            if (c != null)
                this.ValueRepository.Update(Val);
            else
                this.ValueRepository.Add(Val);
        }

        /// <summary>
        /// Gets the Values for the Setting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<Domain.FileSettingValue> GetValues(Domain.FileManagementFileSetting entity)
        {
            return this.ValueRepository.GetMany(NHibernate.Criterion.Expression.Where<Domain.FileSettingValue>(e => e.SettingID == entity.Id)).ToList();
        }

       
        public override void Add(Domain.FileManagementFileSetting newEntity)
        {
            base.Add(newEntity);
            if(newEntity.Value != null)
            {
                foreach( Domain.FileSettingValue Val in newEntity.Value)
                {
                    this.InsertOrUpdateValue(Val);

                }

            }
        }

        public override object AddAndGetId(Domain.FileManagementFileSetting newEntity)
        {
             object id =  base.AddAndGetId(newEntity);
             if (newEntity.Value != null)
             {
                 foreach (Domain.FileSettingValue Val in newEntity.Value)
                 {
                     this.InsertOrUpdateValue(Val);

                 }

             }

             return id;
        }


        public override void Delete(Domain.FileManagementFileSetting entity)
        {
            base.Delete(entity);
            if(entity.Value != null)
            {
                foreach (Domain.FileSettingValue Val in entity.Value)
                    this.ValueRepository.Delete(Val);

            }
        }

        public override Domain.FileManagementFileSetting GetOne(NHibernate.Criterion.ICriterion expression)
        {
            Domain.FileManagementFileSetting _return = base.GetOne(expression);
            if(_return != null)
            {
                _return.Value = this.GetValues(_return);
            }
            return _return;
        }

        public override IEnumerable<Domain.FileManagementFileSetting> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            List<Domain.FileManagementFileSetting> Rows = base.GetMany(where, orderBy, Asc).ToList();

           List<Domain.FileManagementFileSetting> _return = new List<Domain.FileManagementFileSetting>();
           foreach(Domain.FileManagementFileSetting row in Rows)
           {
               row.Value = this.GetValues(row);
               _return.Add(row);

           }

           return _return;
        }


        public override void Update(Domain.FileManagementFileSetting entity)
        {
            base.Update(entity);
            if(entity.Value != null)
            {
                foreach(Domain.FileSettingValue Val in entity.Value)
                {
                    this.InsertOrUpdateValue(Val);
                }
            }
        }
        
    }
}
