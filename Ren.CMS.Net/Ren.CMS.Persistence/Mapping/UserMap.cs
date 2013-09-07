using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class UserMap : ClassMapping<User> {
        
        public UserMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Users");
			Schema("dbo");
			Lazy(true);
            Id<Guid>(x => x.Pkid, map => {

                map.Generator(Generators.Guid);
           
            
            });
			//Property(x => x.Pkid, map => map.NotNullable(true));
			Property(x => x.Username, map => map.NotNullable(true));
			Property(x => x.Loginname, map => map.NotNullable(true));
			Property(x => x.ApplicationName, map => map.NotNullable(true));
			Property(x => x.Email, map => map.NotNullable(true));
			Property(x => x.Comment);
			Property(x => x.Password, map => map.NotNullable(true));
			Property(x => x.PasswordQuestion);
			Property(x => x.PasswordAnswer);
			Property(x => x.IsApproved);
			Property(x => x.LastActivityDate);
			Property(x => x.LastLoginDate);
			Property(x => x.LastPasswordChangedDate);
			Property(x => x.CreationDate);
			Property(x => x.IsOnLine);
			Property(x => x.IsLockedOut);
			Property(x => x.LastLockedOutDate);
			Property(x => x.FailedPasswordAttemptCount);
			Property(x => x.FailedPasswordAttemptWindowStart);
			Property(x => x.FailedPasswordAnswerAttemptCount);
			Property(x => x.FailedPasswordAnswerAttemptWindowStart);
			Property(x => x.IsSubscriber);
			Property(x => x.CustomerID);
			Property(x => x.PermissionGroup);
        }
    }
}
