using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileManagementProfileRepository : Base.BaseRepository<Domain.FileManagementProfile>
    {

        private FileManagementProfiles2FileSettingRepository SettingRepo = null;

        public FileManagementProfileRepository()
            : base()
        {
            this.SettingRepo = new FileManagementProfiles2FileSettingRepository();

        }

    
        private void InsertOrUpdate(Domain.FileManagementProfiles2FileSetting setting)
        {
            Domain.FileManagementProfiles2FileSetting c = this.SettingRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.FileManagementProfiles2FileSetting>(e => e.Id == setting.Id && e.ProfileID == setting.ProfileID));
            if(c != null)
            {
                this.SettingRepo.Update(setting);
            }
            else
            {
                this.SettingRepo.Add(setting);
            }
        }

        private int __add(Domain.FileManagementProfile newEntity)
        {

            int i = (int)base.AddAndGetId(newEntity);
            if (newEntity.Settings != null)
            {
                foreach (Domain.FileManagementProfiles2FileSetting s in newEntity.Settings)
                {
                    s.ProfileID = i;
                    List<Domain.FileSettingValue> Vals = new List<Domain.FileSettingValue>();

                    if (s.Setting.Value != null)
                    {
                        List<Domain.FileSettingValue> newVals = new List<Domain.FileSettingValue>();
                        foreach (Domain.FileSettingValue V in s.Setting.Value)
                        {
                            V.ProfileID = i;
                            newVals.Add(V);
                        }
                        s.Setting.Value.Clear();
                        s.Setting.Value.AddRange(newVals);

                    }

                    this.InsertOrUpdate(s);
                }
            }

            return i;
        }

        public override void Add(Domain.FileManagementProfile newEntity)
        {
            this.__add(newEntity); 
        }


        public override object AddAndGetId(Domain.FileManagementProfile newEntity)
        {
            return this.__add(newEntity); 
        }
       
    }
}
