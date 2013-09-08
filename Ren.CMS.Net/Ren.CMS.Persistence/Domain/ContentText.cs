namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.nhibernate.Domain;

    public class ContentText
    {
        #region Properties

        public virtual TContent Content
        {
            get; set;
        }

        public virtual int ContentId
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string LangCode
        {
            get; set;
        }

        public virtual string LongText
        {
            get; set;
        }

        public virtual string MetaDescription
        {
            get; set;
        }

        public virtual string MetaKeyWords
        {
            get; set;
        }

        public virtual string PreviewText
        {
            get; set;
        }

        public virtual string Seoname
        {
            get; set;
        }

        public virtual string Title
        {
            get; set;
        }

        #endregion Properties
    }
}