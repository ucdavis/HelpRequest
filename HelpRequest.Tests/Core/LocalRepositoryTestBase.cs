using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using HelpRequest.Core.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Stat;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Data.NHibernate.Fluent;
using UCDArch.Testing;

namespace HelpRequest.Tests.Core
{
    /// <summary>
    /// The TMap is the Mapping file for Fluent Mapping
    /// </summary>
    /// <typeparam name="TMap">The type of the map.</typeparam>
    public abstract class LocalRepositoryTestBase<TMap>
    {
        public IRepository Repository { get; set; }
        private Assembly MappingAssembly { get; set; }

        public IStatistics FactoryStatistics
        {
            get { return NHibernateSessionManager.Instance.FactoryStatistics; }
        }

        protected LocalRepositoryTestBase()
        {
            MappingAssembly = typeof(TMap).Assembly;
            Repository = new Repository();
            NHibernateSessionConfiguration.Mappings.UseFluentMappings(MappingAssembly); //Needed for the persister
        }

        [TestInitialize]
        public void CreateDB()
        {
           
            //Configuration config = new Configuration().Configure();
            Configuration config = Fluently.Configure()
                .Mappings(x => x.FluentMappings.AddFromAssembly(MappingAssembly)
                .Conventions.AddAssembly(typeof(PrimaryKeyConvention).Assembly))
                .BuildConfiguration();
            new NHibernate.Tool.hbm2ddl.SchemaExport(config).Execute(false, true, false, NHibernateSessionManager.Instance.GetSession().Connection, null);

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
