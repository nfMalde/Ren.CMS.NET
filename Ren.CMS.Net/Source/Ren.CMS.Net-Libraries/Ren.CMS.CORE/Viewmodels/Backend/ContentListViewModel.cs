using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Viewmodels.Backend
{
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence;
    using Ren.CMS.Persistence.Base;


    public static class ContentListViewModel
    {
        public static List<ContentType> ContentTypeList()
        {
            BaseRepository<ContentType> repo = new BaseRepository<ContentType>();
            List<ContentType> _list = new List<ContentType>();

            foreach (var ct in repo.GetMany())
                _list.Add(ct);

            return _list;
        }
    }
}
