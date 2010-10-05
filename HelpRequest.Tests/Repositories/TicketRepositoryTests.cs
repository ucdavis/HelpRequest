using System;
using System.Collections.Generic;
using System.Linq;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Mappings;
using HelpRequest.Tests.Core;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Testing.Extensions;

namespace HelpRequest.Tests.Repositories
{
    /// <summary>
    /// Note, this table is not in the database, it is a container only
    /// Entity Name:		Ticket
    /// LookupFieldName:	FromEmail
    /// </summary>
    [TestClass]
    public class TicketRepositoryTests : AbstractRepositoryTests<Ticket, int, TicketMap>
    {
        /// <summary>
        /// Gets or sets the Ticket repository.
        /// </summary>
        /// <value>The Ticket repository.</value>
        public IRepository<Ticket> TicketRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketRepositoryTests"/> class.
        /// </summary>
        public TicketRepositoryTests()
        {
            TicketRepository = new Repository<Ticket>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Ticket GetValid(int? counter)
        {
            return CreateValidEntities.Ticket(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Ticket> GetQuery(int numberAtEnd)
        {
            return TicketRepository.Queryable.Where(a => a.FromEmail.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Ticket entity, int counter)
        {
            Assert.AreEqual("FromEmail" + counter, entity.FromEmail);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Ticket entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.FromEmail);
                    break;
                case ARTAction.Restore:
                    entity.FromEmail = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.FromEmail;
                    entity.FromEmail = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            TicketRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            TicketRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides	

        #region User Tests
        #region Invalid Tests

        [TestMethod]
        [ExpectedException(typeof(NHibernate.TransientObjectException))]
        public void TestUserWithNewValueDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.User = new User();

                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception ex)
            {
                #region Assert
                Assert.IsNotNull(ticket);
                Assert.IsNotNull(ex);
                Assert.AreEqual("object references an unsaved transient instance - save the transient instance before flushing. Type: HelpRequest.Core.Domain.User, Entity: HelpRequest.Core.Domain.User", ex.Message);
                #endregion Assert

                throw;
            }
        }

        #endregion Invalid Tests

        #region Valid Tests

        [TestMethod]
        public void TestWhenUserIsNullSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.User = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNull(ticket.User);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        [TestMethod]
        public void TestWhenUserIsNotNullSaves()
        {
            #region Arrange
            LoadUsers(1);
            var ticket = GetValid(9);
            ticket.User = Repository.OfType<User>().GetNullableById(1);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNotNull(ticket.User);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        #endregion Valid Tests

        #region CRUD Tests

        [TestMethod]
        public void TestDeleteQuestionSetDoesNotCascadeToUser()
        {
            #region Arrange
            LoadUsers(3);
            var ticket = GetValid(9);
            ticket.User = Repository.OfType<User>().GetNullableById(1);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            Assert.AreEqual(3, Repository.OfType<User>().GetAll().Count);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.Remove(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(3, Repository.OfType<User>().GetAll().Count);
            #endregion Assert
        }
        #endregion CRUD Tests
        #endregion User Tests
        
        #region FromEmail Tests
 

        #region Valid Tests
        /// <summary>
        /// Tests from email with null value saves.
        /// </summary>
        [TestMethod]
        public void TestFromEmailWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.FromEmail = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests from email with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestFromEmailWithEmptyStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.FromEmail = string.Empty;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests from email with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestFromEmailWithSpacesOnlySaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.FromEmail = " ";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the FromEmail with one character saves.
        /// </summary>
        [TestMethod]
        public void TestFromEmailWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.FromEmail = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the FromEmail with long value saves.
        /// </summary>
        [TestMethod]
        public void TestFromEmailWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.FromEmail = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.FromEmail.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion FromEmail Tests

        #region UrgencyLevel Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the UrgencyLevel with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUrgencyLevelWithNullValueDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.UrgencyLevel = null;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("UrgencyLevel: You must select an Urgency Level");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the UrgencyLevel with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUrgencyLevelWithEmptyStringDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.UrgencyLevel = string.Empty;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("UrgencyLevel: You must select an Urgency Level");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the UrgencyLevel with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUrgencyLevelWithSpacesOnlyDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.UrgencyLevel = " ";
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("UrgencyLevel: You must select an Urgency Level");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the UrgencyLevel with one character saves.
        /// </summary>
        [TestMethod]
        public void TestUrgencyLevelWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.UrgencyLevel = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the UrgencyLevel with long value saves.
        /// </summary>
        [TestMethod]
        public void TestUrgencyLevelWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.UrgencyLevel = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.UrgencyLevel.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion UrgencyLevel Tests

        #region SupportDepartment Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the SupportDepartment with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSupportDepartmentWithNullValueDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.SupportDepartment = null;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("SupportDepartment: You must select a Support Department");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the SupportDepartment with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSupportDepartmentWithEmptyStringDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.SupportDepartment = string.Empty;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("SupportDepartment: You must select a Support Department");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the SupportDepartment with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSupportDepartmentWithSpacesOnlyDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.SupportDepartment = " ";
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("SupportDepartment: You must select a Support Department");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the SupportDepartment with one character saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartment = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the SupportDepartment with long value saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartment = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.SupportDepartment.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion SupportDepartment Tests

        #region SupportDepartmentOther Tests  

        #region Valid Tests
        [TestMethod]
        public void TestSupportDepartmentOtherWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartmentOther = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the support department other with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentOtherWithEmptyStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartmentOther = string.Empty;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the support department other with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentOtherWithSpacesOnlySaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartmentOther = " ";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the SupportDepartmentOther with one character saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentOtherWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartmentOther = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the SupportDepartmentOther with long value saves.
        /// </summary>
        [TestMethod]
        public void TestSupportDepartmentOtherWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.SupportDepartmentOther = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.SupportDepartmentOther.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion SupportDepartmentOther Tests

        #region ForApplication Tests

        #region Valid Tests
        /// <summary>
        /// Tests for application with null value saves.
        /// </summary>
        [TestMethod]
        public void TestForApplicationWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForApplication = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForApplication with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestForApplicationWithEmptyStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForApplication = string.Empty;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForApplication with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestForApplicationWithSpacesOnlySaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForApplication = " ";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForApplication with one character saves.
        /// </summary>
        [TestMethod]
        public void TestForApplicationWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForApplication = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the ForApplication with long value saves.
        /// </summary>
        [TestMethod]
        public void TestForApplicationWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForApplication = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.ForApplication.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion ForApplication Tests

        #region ForWebSite Tests

        #region Valid Tests
        /// <summary>
        /// Tests ForWebSite with null value saves.
        /// </summary>
        [TestMethod]
        public void TestForWebSiteWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForWebSite = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForWebSite with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestForWebSiteWithEmptyStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForWebSite = string.Empty;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForWebSite with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestForWebSiteWithSpacesOnlySaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForWebSite = " ";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the ForWebSite with one character saves.
        /// </summary>
        [TestMethod]
        public void TestForWebSiteWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForWebSite = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the ForWebSite with long value saves.
        /// </summary>
        [TestMethod]
        public void TestForWebSiteWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.ForWebSite = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.ForWebSite.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion ForWebSite Tests

        #region Subject Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the Subject with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSubjectWithNullValueDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.Subject = null;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Subject: You must enter the Subject.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the Subject with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSubjectWithEmptyStringDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.Subject = string.Empty;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Subject: You must enter the Subject.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the Subject with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSubjectWithSpacesOnlyDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.Subject = " ";
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Subject: You must enter the Subject.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the Subject with one character saves.
        /// </summary>
        [TestMethod]
        public void TestSubjectWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Subject = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the Subject with long value saves.
        /// </summary>
        [TestMethod]
        public void TestSubjectWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Subject = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.Subject.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion Subject Tests

        #region MessageBody Tests
        #region Invalid Tests

        /// <summary>
        /// Tests the MessageBody with null value does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestMessageBodyWithNullValueDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.MessageBody = null;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("MessageBody: You must enter the Message Body.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the MessageBody with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestMessageBodyWithEmptyStringDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.MessageBody = string.Empty;
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("MessageBody: You must enter the Message Body.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        /// <summary>
        /// Tests the MessageBody with spaces only does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestMessageBodyWithSpacesOnlyDoesNotSave()
        {
            Ticket ticket = null;
            try
            {
                #region Arrange
                ticket = GetValid(9);
                ticket.MessageBody = " ";
                #endregion Arrange

                #region Act
                TicketRepository.DbContext.BeginTransaction();
                TicketRepository.EnsurePersistent(ticket);
                TicketRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                Assert.IsNotNull(ticket);
                var results = ticket.ValidationResults().AsMessageList();
                results.AssertErrorsAre("MessageBody: You must enter the Message Body.");
                Assert.IsTrue(ticket.IsTransient());
                Assert.IsFalse(ticket.IsValid());
                throw;
            }
        }

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the MessageBody with one character saves.
        /// </summary>
        [TestMethod]
        public void TestMessageBodyWithOneCharacterSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.MessageBody = "x";
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the MessageBody with long value saves.
        /// </summary>
        [TestMethod]
        public void TestMessageBodyWithLongValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.MessageBody = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, ticket.MessageBody.Length);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion MessageBody Tests

        #region Availability Tests
        /// <summary>
        /// Tests the availability with null value saves.
        /// </summary>
        [TestMethod]
        public void TestAvailabilityWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Availability = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the availability with empty list saves.
        /// </summary>
        [TestMethod]
        public void TestAvailabilityWithEmptyListSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Availability = new List<string>();
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the availability with one string saves.
        /// </summary>
        [TestMethod]
        public void TestAvailabilityWithOneStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Availability.Add("Test");
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.Availability.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the availability with two string saves.
        /// </summary>
        [TestMethod]
        public void TestAvailabilityWithTwoStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Availability.Add("Test1");
            ticket.Availability.Add("Test2");
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.Availability.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Availability Tests

        #region EmailCCs Tests
        /// <summary>
        /// Tests the EmailCCs with null value saves.
        /// </summary>
        [TestMethod]
        public void TestEmailCCsWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.EmailCCs = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the EmailCCs with empty list saves.
        /// </summary>
        [TestMethod]
        public void TestEmailCCsWithEmptyListSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.EmailCCs = new List<string>();
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the EmailCCs with one string saves.
        /// </summary>
        [TestMethod]
        public void TestEmailCCsWithOneStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.EmailCCs.Add("Test");
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.EmailCCs.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the EmailCCs with two string saves.
        /// </summary>
        [TestMethod]
        public void TestEmailCCsWithTwoStringSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.EmailCCs.Add("Test1");
            ticket.EmailCCs.Add("Test2");
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.EmailCCs.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion EmailCCs Tests

        #region EmailCCs Tests
        /// <summary>
        /// Tests the EmailCCs with null value saves.
        /// </summary>
        [TestMethod]
        public void TestAttachmentsWithNullValueSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Attachments = null;
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the Attachments with empty list saves.
        /// </summary>
        [TestMethod]
        public void TestAttachmentsWithEmptyListSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Attachments = new List<Attachment>();
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the Attachments with one Attachment saves.
        /// </summary>
        [TestMethod]
        public void TestAttachmentsWithOneAttachmentSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Attachments.Add(new Attachment("test", "test2"));
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(1, ticket.Attachments.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the Attachments with two Attachments saves.
        /// </summary>
        [TestMethod]
        public void TestAttachmentsWithTwoAttachmentsSaves()
        {
            #region Arrange
            var ticket = GetValid(9);
            ticket.Attachments.Add(new Attachment("test1", "FileName1"));
            ticket.Attachments.Add(new Attachment("test2", "FileName2"));
            #endregion Arrange

            #region Act
            TicketRepository.DbContext.BeginTransaction();
            TicketRepository.EnsurePersistent(ticket);
            TicketRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(2, ticket.Attachments.Count);
            Assert.IsFalse(ticket.IsTransient());
            Assert.IsTrue(ticket.IsValid());
            #endregion Assert
        }

        #endregion Attachments Tests

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
            expectedFields.Add(new NameAndType("Attachments", "System.Collections.Generic.ICollection`1[HelpRequest.Core.Domain.Attachment]", new List<string>()));
            expectedFields.Add(new NameAndType("Availability", "System.Collections.Generic.List`1[System.String]", new List<string>()));
            expectedFields.Add(new NameAndType("EmailCCs", "System.Collections.Generic.List`1[System.String]", new List<string>()));
            
            expectedFields.Add(new NameAndType("ForApplication", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("ForWebSite", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("FromEmail", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Id", "System.Int32", new List<string>
            {
                "[Newtonsoft.Json.JsonPropertyAttribute()]", 
                "[System.Xml.Serialization.XmlIgnoreAttribute()]"
            }));
            expectedFields.Add(new NameAndType("MessageBody", "System.String", new List<string>
            {
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute(Message = \"You must enter the Message Body.\")]"
            }));
            expectedFields.Add(new NameAndType("Subject", "System.String", new List<string>
            {
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute(Message = \"You must enter the Subject.\")]"
            }));
            expectedFields.Add(new NameAndType("SupportDepartment", "System.String", new List<string>
            {
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute(Message = \"You must select a Support Department\")]"
            }));
            expectedFields.Add(new NameAndType("SupportDepartmentOther", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("UrgencyLevel", "System.String", new List<string>
            { 
                 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute(Message = \"You must select an Urgency Level\")]"
            }));
            expectedFields.Add(new NameAndType("User", "HelpRequest.Core.Domain.User", new List<string>()));
            #endregion Arrange

            AttributeAndFieldValidation.ValidateFieldsAndAttributes(expectedFields, typeof(Ticket));

        }

        #endregion Reflection of Database.	
		
		
    }
}