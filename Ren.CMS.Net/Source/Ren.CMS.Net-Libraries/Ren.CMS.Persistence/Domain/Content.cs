namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    //Hierbei handelt es sich um eine Entity
    //Diese Klasse repräsentiert die Komplette Tabellenstruktur e.g. genau eine Zeile einer Tabelle. In unserem Fall "Content".
    public class TContent
    {
        #region Properties

        public virtual Category Category
        {
            get; set;
        }

        public virtual System.Nullable<System.DateTime> CDate
        {
            get; set;
        }

        public virtual System.Guid? Cid
        {
            get; set;
        }

        public virtual System.Nullable<int> ContentRef
        {
            get; set;
        }

        public virtual string ContentType
        {
            get; set;
        }

        public virtual System.Guid? CreatorPKID
        {
            get; set;
        }

        public virtual string CreatorSpecialName
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual bool Locked
        {
            get; set;
        }

        public virtual System.Nullable<int> RatingGroupID
        {
            get; set;
        }

        public virtual IList<ContentText> Texts { get; set; }

        public virtual IList<ContentAttachment> Attachments { get; set; }


        public virtual User User
        {
            get; set;
        }

        #endregion Properties
    }
}