namespace Ren.CMS.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Mapping.ByCode;

    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;

    public class NHibernateHelper
    {
        #region Fields

        public static ModelMapper Mapper = new ModelMapper();

        private static Configuration configuration = new Configuration();
        private static List<Type> mappingHolder = new List<Type>();
        private static ISession _currentSession = null;
        private static ISessionFactory _sessionFactory;

        #endregion Fields

        #region Properties

        private static ISessionFactory SessionFactory
        {
            get
            {

                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (object attribute in assembly.GetCustomAttributes(true))
                        {
                            if (attribute is Ren.CMS.Persistence.Base.PersistenceAssembly)
                                configuration.AddAssembly(assembly);
                            try
                            {
                                //Adding the mappings:
                                foreach (Type t in assembly.GetTypes())
                                {
                                    if (mappingHolder.Where(x => x == t).Count() == 0 && t.GetCustomAttributes().Where(e => e is Ren.CMS.Persistence.Base.PersistenceMapping).Count() > 0)
                                    {
                                        Mapper.AddMapping(t);

                                        mappingHolder.Add(t);
                                    }
                                }
                            }
                            catch
                            {
                            }

                        }
                    }

                    var hmp = Mapper.CompileMappingForEachExplicitlyAddedEntity();

                    hmp.ToList().ForEach(h => configuration.AddMapping(h));

                    configuration.BuildMappings();
                    _sessionFactory = configuration.BuildSessionFactory();

                }
                return _sessionFactory;
            }
        }

        #endregion Properties

        #region Methods

        public static Configuration GetConfiguration()
        {
            return configuration;
        }

        public static ISession OpenSession()
        {
            if(_currentSession == null || (!_currentSession.IsConnected || !_currentSession.IsOpen))
            _currentSession = SessionFactory.OpenSession();

            return SessionFactory.OpenSession();
        }

        private static void ConfigureAssemblies()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (object attribute in assembly.GetCustomAttributes(true))
                {
                    if (attribute is Ren.CMS.Persistence.Base.PersistenceAssembly)
                        configuration.AddAssembly(assembly);
                }
            }
        }

        #endregion Methods
    }
}