using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Testing;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Testing.Extensions;

namespace HelpRequest.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		Application
    /// LookupFieldName:	Abbr
    /// </summary>
    [TestClass]
    public class ApplicationRepositoryTests : AbstractRepositoryTests<Application, int, ApplicationMap>
    {
        /// <summary>
        /// Gets or sets the Application repository.
        /// </summary>
        /// <value>The Application repository.</value>
        public IRepository<Application> ApplicationRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRepositoryTests"/> class.
        /// </summary>
        public ApplicationRepositoryTests()
        {
            ApplicationRepository = new Repository<Application>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Application GetValid(int? counter)
        {
            return CreateValidEntities.Application(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Application> GetQuery(int numberAtEnd)
        {
            return ApplicationRepository.Queryable.Where(a => a.Abbr.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Application entity, int counter)
        {
            Assert.AreEqual("Abbr" + counter, entity.Abbr);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Application entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.Abbr);
                    break;
                case ARTAction.Restore:
                    entity.Abbr = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.Abbr;
                    entity.Abbr = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            ApplicationRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            ApplicationRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides	

        #region Abbr Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the Abbr with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAbbrWithNullValueDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.Abbr = null;
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Abbr: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the Abbr with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAbbrWithEmptyStringDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.Abbr = string.Empty;
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Abbr: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the Abbr with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAbbrWithSpacesOnlyDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.Abbr = " ";
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Abbr: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the Abbr with too long value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAbbrWithTooLongValueDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.Abbr = "x".RepeatTimes((50 + 1));
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                Assert.AreEqual(50 + 1, application.Abbr.Length);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Abbr: length must be between 0 and 50");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }
        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the Abbr with one character saves.
        /// </summary>
        [TestMethod]
        public void TestAbbrWithOneCharacterSaves()
        {
            #region Arrange
            var application = GetValid(9);
            application.Abbr = "x";
            #endregion Arrange

            #region Act
            ApplicationRepository.DbContext.BeginTransaction();
            ApplicationRepository.EnsurePersistent(application);
            ApplicationRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(application.IsTransient());
            Assert.IsTrue(application.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the Abbr with long value saves.
        /// </summary>
        [TestMethod]
        public void TestAbbrWithLongValueSaves()
        {
            #region Arrange
            var application = GetValid(9);
            application.Abbr = "x".RepeatTimes(50);
            #endregion Arrange

            #region Act
            ApplicationRepository.DbContext.BeginTransaction();
            ApplicationRepository.EnsurePersistent(application);
            ApplicationRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(50, application.Abbr.Length);
            Assert.IsFalse(application.IsTransient());
            Assert.IsTrue(application.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion Abbr Tests

        #region ApplicationName Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the ApplicationName with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestApplicationNameWithNullValueDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.ApplicationName = null;
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("ApplicationName: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the ApplicationName with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestApplicationNameWithEmptyStringDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.ApplicationName = string.Empty;
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("ApplicationName: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the ApplicationName with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestApplicationNameWithSpacesOnlyDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.ApplicationName = " ";
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("ApplicationName: may not be null or empty");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the ApplicationName with too long value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestApplicationNameWithTooLongValueDoesNotSave()
        {
            Application application = null;
            try
            {
                #region Arrange
                application = GetValid(9);
                application.ApplicationName = "x".RepeatTimes((50 + 1));
                #endregion Arrange

                #region Act
                ApplicationRepository.DbContext.BeginTransaction();
                ApplicationRepository.EnsurePersistent(application);
                ApplicationRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(application);
                Assert.AreEqual(50 + 1, application.ApplicationName.Length);
                var results = application.ValidationResults().AsMessageList();
                results.AssertErrorsAre("ApplicationName: length must be between 0 and 50");
                Assert.IsTrue(application.IsTransient());
                Assert.IsFalse(application.IsValid());
                throw;
            }
        }
        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the ApplicationName with one character saves.
        /// </summary>
        [TestMethod]
        public void TestApplicationNameWithOneCharacterSaves()
        {
            #region Arrange
            var application = GetValid(9);
            application.ApplicationName = "x";
            #endregion Arrange

            #region Act
            ApplicationRepository.DbContext.BeginTransaction();
            ApplicationRepository.EnsurePersistent(application);
            ApplicationRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(application.IsTransient());
            Assert.IsTrue(application.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the ApplicationName with long value saves.
        /// </summary>
        [TestMethod]
        public void TestApplicationNameWithLongValueSaves()
        {
            #region Arrange
            var application = GetValid(9);
            application.ApplicationName = "x".RepeatTimes(50);
            #endregion Arrange

            #region Act
            ApplicationRepository.DbContext.BeginTransaction();
            ApplicationRepository.EnsurePersistent(application);
            ApplicationRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(50, application.ApplicationName.Length);
            Assert.IsFalse(application.IsTransient());
            Assert.IsTrue(application.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion ApplicationName Tests

        #region FluentMapping Tests

        [TestMethod]
        public void TestCanCorrectlyMapApplication()
        {
            #region Arrange
            var id = ApplicationRepository.Queryable.Max(x => x.Id) + 1;
            var session = NHibernateSessionManager.Instance.GetSession();
            #endregion Arrange

            #region Act/Assert
            new PersistenceSpecification<Application>(session)
                .CheckProperty(c => c.Id, id)
                .CheckProperty(c => c.Abbr, "Abbr")
                .CheckProperty(c => c.ApplicationName, "ApplicationName")
                .VerifyTheMappings();
            #endregion Act/Assert
        }

        #endregion FluentMapping Tests
        
        #region Reflection of Database.

        /// <summary>
        /// Tests all fields in the database have been tested.
        /// If this fails and no other tests, it means that a field has been added which has not been tested above.
        /// </summary>
        [TestMethod]
        public void TestAllFieldsInTheDatabaseHaveBeenTested()
        {
            #region Arrange
            var expectedFields = new List<NameAndType>();
            expectedFields.Add(new NameAndType("Abbr", "System.String", new List<string>
            {
                 "[NHibernate.Validator.Constraints.LengthAttribute((Int32)50)]", 
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute()]"
            }));
            expectedFields.Add(new NameAndType("ApplicationName", "System.String", new List<string>
            {
                 "[NHibernate.Validator.Constraints.LengthAttribute((Int32)50)]", 
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute()]"
            }));
            expectedFields.Add(new NameAndType("Id", "System.Int32", new List<string>
            {
                "[Newtonsoft.Json.JsonPropertyAttribute()]", 
                "[System.Xml.Serialization.XmlIgnoreAttribute()]"
            }));
            #endregion Arrange

            AttributeAndFieldValidation.ValidateFieldsAndAttributes(expectedFields, typeof(Application));

        }

        #endregion Reflection of Database.	
		
		
    }
}