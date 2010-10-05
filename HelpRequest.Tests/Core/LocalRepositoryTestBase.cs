using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using HelpRequest.Core.Mappings;
using NHibernate.Cfg;
using NHibernate.Stat;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Data.NHibernate.Fluent;
using UCDArch.Testing;

namespace HelpRequest.Tests.Core
{
    public abstract class LocalRepositoryTestBase
    {
        public IRepository Repository { get; set; }

        public IStatistics FactoryStatistics
        {
            get { return NHibernateSessionManager.Instance.FactoryStatistics; }
        }

        protected LocalRepositoryTestBase()
        {
            Repository = new Repository();
           // NHibernateSessionConfiguration.Mappings.UseFluentMappings(typeof(HelpTopicMap).Assembly); //???
            CreateDB();
        }

        public void CreateDB()
        {
           
            //Configuration config = new Configuration().Configure();
            Configuration config = Fluently.Configure()
                .Mappings(x => x.FluentMappings.AddFromAssembly(typeof(HelpTopicMap).Assembly)
                .Conventions.AddAssembly(typeof(PrimaryKeyConvention).Assembly))
                .BuildConfiguration();
            new NHibernate.Tool.hbm2ddl.SchemaExport(config).Execute(false, true, false, NHibernateSessionManager.Instance.GetSession().Connection, Console.Out);

            InitServiceLocator();

            LoadData();
        }

        protected virtual void LoadData()
        {

        }

        protected virtual void InitServiceLocator()
        {
            var container = ServiceLocatorInitializer.Init();

            RegisterAdditionalServices(container);
        }

        protected virtual void RegisterAdditionalServices(IWindsorContainer container)
        {

        }
    }
}
