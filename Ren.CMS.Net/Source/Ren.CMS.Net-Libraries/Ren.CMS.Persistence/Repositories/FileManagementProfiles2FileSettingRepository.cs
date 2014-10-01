using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileManagementProfiles2FileSettingRepository : Base.BaseRepository<Domain.FileManagementProfiles2FileSetting>
    {
        private FileSettingRepository SettingRepository = null;
        private FileSettingValueRepository ValueRepository = null;

        public FileManagementProfiles2FileSettingRepository() : base()
        {
            this.SettingRepository = new FileSettingRepository();
        }

        public int InserOrUpdateSetting ( Domain.FileManagementFileSetting Setting)
        {
            Domain.FileManagementFileSetting _c = this.SettingRepository.GetOne(NHibernate.Criterion.Expression.Where<Domain.FileManagementFileSetting>(e => e.Id == Setting.Id || e.SettingName == Setting.SettingName));
            if(_c != null)
            {
                this.SettingRepository.Update(Setting);
                return Setting.Id;
            }
            else
            {
                return (int) this.SettingRepository.AddAndGetId(Setting);
            }
        }
        
        public override void Add(Domain.FileManagementProfiles2FileSetting newEntity)
        {
            if (newEntity.Setting != null)
                newEntity.SettingID = this.InserOrUpdateSetting(newEntity.Setting);

            base.Add(newEntity);
            
        }

        public override object AddAndGetId(Domain.FileManagementProfiles2FileSetting newEntity)
        {
            if (newEntity.Setting != null)
                newEntity.SettingID = this.InserOrUpdateSetting(newEntity.Setting);

            return base.AddAndGetId(newEntity);
        }


        public override void Delete(Domain.FileManagementProfiles2FileSetting entity)
        {
            if(entity.Setting.Value != null && entity.Setting.Value.Any(e => e.ProfileID == entity.ProfileID))
            {
                foreach(Domain.FileSettingValue val in entity.Setting.Value.Where(e => e.ProfileID == entity.ProfileID))
                {
                    this.ValueRepository.Delete(val);
                }
            }

            base.Delete(entity);
        }

        public override IEnumerable<Domain.FileManagementProfiles2FileSetting> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            List<Domain.FileManagementProfiles2FileSetting> Rows = base.GetMany(where, orderBy, Asc).ToList();
            List<Domain.FileManagementProfiles2FileSetting> _return = new List<Domain.FileManagementProfiles2FileSetting>();

            foreach(Domain.FileManagementProfiles2FileSetting v in Rows)
            {
                Domain.FileManagementFileSetting Setting = this.SettingRepository.GetOne(NHibernate.Criterion.Expression.Where<Domain.FileManagementFileSetting>(e => e.Id == v.SettingID));
                if (Setting == null)
                {
                    this.Delete(v);
                    continue;
                }

                v.Setting = Setting;
                _return.Add(v);
            }

            return _return;
        }

        public override Domain.FileManagementProfiles2FileSetting GetOne(NHibernate.Criterion.ICriterion expression)
        {
            Domain.FileManagementProfiles2FileSetting Row = base.GetOne(expression);
            Row.Setting = this.SettingRepository.GetOne(NHibernate.Criterion.Expression.Where<Domain.FileManagementFileSetting>(e => e.Id == Row.SettingID));
            if(Row.Setting != null)
            {
                if (Row.Setting.Value.Any(e => e.ProfileID == Row.ProfileID))
                    Row.Setting.Value = Row.Setting.Value.Where(e => e.ProfileID == Row.ProfileID).ToList();
                else
                    Row.Setting.Value = null;
            }

            return Row;
        }

        public override void Update(Domain.FileManagementProfiles2FileSetting entity)
        {
            this.InserOrUpdateSetting(entity.Setting);
            base.Update(entity);
        }
    }
}
