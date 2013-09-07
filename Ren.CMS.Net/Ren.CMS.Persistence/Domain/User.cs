using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class User {
        public virtual System.Guid Pkid { get; set; }
        public virtual string Username { get; set; }
        public virtual string Loginname { get; set; }
        public virtual string ApplicationName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Comment { get; set; }
        public virtual string Password { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual string PasswordAnswer { get; set; }
        public virtual string IsApproved { get; set; }
        public virtual System.Nullable<System.DateTime> LastActivityDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastLoginDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public virtual System.Nullable<System.DateTime> CreationDate { get; set; }
        public virtual string IsOnLine { get; set; }
        public virtual string IsLockedOut { get; set; }
        public virtual System.Nullable<System.DateTime> LastLockedOutDate { get; set; }
        public virtual System.Nullable<int> FailedPasswordAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public virtual System.Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public virtual string IsSubscriber { get; set; }
        public virtual string CustomerID { get; set; }
        public virtual string PermissionGroup { get; set; }
    }
}
