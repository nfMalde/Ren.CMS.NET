using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nContentAttachmentTexts
    {
        private int _id = 0;
        private string _title = "";
        private string _description = "";
        private Guid Attachment = new Guid();
        private string _langCode = "";

        public nContentAttachmentTexts(Persistence.Domain.ContentAttachmentTexts Text)
        {
            this._id = Text.id;
            this._title = Text.Title;
            this._description = Text.Description;
            this.Attachment = Text.AttachmentId;
            this._langCode = Text.LangCode;

        }

        public nContentAttachmentTexts(string title, string description, Guid attachment, string LangCode)
        {
            //this._id = Text.id;
            this._title = title;
            this._description = description;
            this.Attachment = attachment;
            this._langCode = LangCode;
        }
        public nContentAttachmentTexts(string title, string description, string LangCode)
        {
            //this._id = Text.id;
            this._title = title;
            this._description = description;
            //this.Attachment = attachment;
            this._langCode = LangCode;
        }

        public string Title { get { return this._title; } set { this._title = value; } }
        public string Description { get { return this._description; } set { this._description = value;  } }
        public Guid AttachmentId { get { return this.Attachment; } set { this.Attachment = value; } }
        public int Id { get { return this._id; } set { this._id = value; } }
        public string LangCode { get { return this._langCode; } set { this._langCode = value; } }
    }
}
