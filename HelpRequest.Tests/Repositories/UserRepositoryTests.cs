using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using HelpRequest.Core.Domain;
using HelpRequest.Core.Mappings;
using HelpRequest.Tests.Core;
using HelpRequest.Tests.Core.Extensions;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace HelpRequest.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		User
    /// LookupFieldName:	LoginId
    /// </summary>
    [TestClass]
    public class UserRepositoryTests : AbstractRepositoryTests<User, int, UserMap>
    {
        /// <summary>
        /// Gets or sets the User repository.
        /// </summary>
        /// <value>The User repository.</value>
        public IRepository<User> UserRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryTests"/> class.
        /// </summary>
        public UserRepositoryTests()
        {
            UserRepository = new Repository<User>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override User GetValid(int? counter)
        {
            return CreateValidEntities.User(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<User> GetQuery(int numberAtEnd)
        {
            return UserRepository.Queryable.Where(a => a.LoginId.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(User entity, int counter)
        {
            Assert.AreEqual("LoginId" + counter, entity.LoginId);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(User entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.LoginId);
                    break;
                case ARTAction.Restore:
                    entity.LoginId = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.LoginId;
                    entity.LoginId = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            UserRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            UserRepository.DbContext.CommitTransaction();
        }

        [TestMethod]
        public override void CanUpdateEntity()
        {
            CanUpdateEntity(false);
        }

        [TestMethod]
        [ExpectedException(typeof(NHibernate.HibernateException))]
        public override void CanDeleteEntity()
        {
            try
            {
                base.CanDeleteEntity();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("Attempted to delete an object of immutable class: [HelpRequest.Core.Domain.User]", ex.Message);
                throw;
            }

        }

        #endregion Init and Overrides	

        #region Fluent Mapping Tests
        [TestMethod]
        public void TestCanCorrectlyMapAttachment()
        {
            #region Arrange
            var id = UserRepository.Queryable.Max(x => x.Id) + 1;
            var session = NHibernateSessionManager.Instance.GetSession();
            #endregion Arrange

            #region Act/Assert
            new PersistenceSpecification<User>(session)
                .CheckProperty(c => c.Id, id)
                .CheckProperty(c => c.Email, "test@test.com")
                .CheckProperty(c => c.EmployeeId, "test")
                .CheckProperty(c => c.FirstName, "FirstName")
                .CheckProperty(c => c.LastName, "LastName")
                .CheckProperty(c => c.LoginId, "LoginId")
                .CheckProperty(c => c.Phone, "234-2345")
                .CheckProperty(c => c.Sid, "sid")
                .CheckProperty(c => c.UserKey, "userKey")
                .VerifyTheMappings();
            #endregion Act/Assert
        }
        #endregion Fluent Mapping Tests
        
        #region LoginId Tests
        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests
        [TestMethod]
        public void TestLoginIdWithNullValueSaves()
        {
            #region Arrange
            var user = GetValid(9);
            user.LoginId = "x";
            #endregion Arrange

            #region Act
            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the login id with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestLoginIdWithEmptyStringSaves()
        {
            #region Arrange
            var user = GetValid(9);
            user.LoginId = string.Empty;
            #endregion Arrange

            #region Act
            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            #endregion Assert
        }
        /// <summary>
        /// Tests the login id with spaces onlyr saves.
        /// </summary>
        [TestMethod]
        public void TestLoginIdWithSpacesOnlyrSaves()
        {
            #region Arrange
            var user = GetValid(9);
            user.LoginId = " ";
            #endregion Arrange

            #region Act
            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the LoginId with one character saves.
        /// </summary>
        [TestMethod]
        public void TestLoginIdWithOneCharacterSaves()
        {
            #region Arrange
            var user = GetValid(9);
            user.LoginId = "x";
            #endregion Arrange

            #region Act
            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            #endregion Assert
        }

        /// <summary>
        /// Tests the LoginId with long value saves.
        /// </summary>
        [TestMethod]
        public void TestLoginIdWithLongValueSaves()
        {
            #region Arrange
            var user = GetValid(9);
            user.LoginId = "x".RepeatTimes(999);
            #endregion Arrange

            #region Act
            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.AreEqual(999, user.LoginId.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            #endregion Assert
        }

        #endregion Valid Tests
        #endregion LoginId Tests

        #region Email Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the Email with null value saves.
        /// </summary>
        [TestMethod]
        public void TestEmailWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.Email = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Email with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestEmailWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Email = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Email with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestEmailWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Email = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Email with one character saves.
        /// </summary>
        [TestMethod]
        public void TestEmailWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Email = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Email with long value saves.
        /// </summary>
        [TestMethod]
        public void TestEmailWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Email = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.Email.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion Email Tests

        #region Phone Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the Phone with null value saves.
        /// </summary>
        [TestMethod]
        public void TestPhoneWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.Phone = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Phone with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestPhoneWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Phone = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Phone with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestPhoneWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Phone = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Phone with one character saves.
        /// </summary>
        [TestMethod]
        public void TestPhoneWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Phone = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Phone with long value saves.
        /// </summary>
        [TestMethod]
        public void TestPhoneWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Phone = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.Phone.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion Phone Tests

        #region FirstName Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the FirstName with null value saves.
        /// </summary>
        [TestMethod]
        public void TestFirstNameWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.FirstName = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the FirstName with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestFirstNameWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.FirstName = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the FirstName with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestFirstNameWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.FirstName = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the FirstName with one character saves.
        /// </summary>
        [TestMethod]
        public void TestFirstNameWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.FirstName = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the FirstName with long value saves.
        /// </summary>
        [TestMethod]
        public void TestFirstNameWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.FirstName = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.FirstName.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion FirstName Tests

        #region LastName Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the LastName with null value saves.
        /// </summary>
        [TestMethod]
        public void TestLastNameWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.LastName = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the LastName with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestLastNameWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.LastName = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the LastName with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestLastNameWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.LastName = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the LastName with one character saves.
        /// </summary>
        [TestMethod]
        public void TestLastNameWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.LastName = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the LastName with long value saves.
        /// </summary>
        [TestMethod]
        public void TestLastNameWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.LastName = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.LastName.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion LastName Tests

        #region EmployeeId Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the EmployeeId with null value saves.
        /// </summary>
        [TestMethod]
        public void TestEmployeeIdWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.EmployeeId = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the EmployeeId with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestEmployeeIdWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.EmployeeId = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the EmployeeId with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestEmployeeIdWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.EmployeeId = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the EmployeeId with one character saves.
        /// </summary>
        [TestMethod]
        public void TestEmployeeIdWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.EmployeeId = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the EmployeeId with long value saves.
        /// </summary>
        [TestMethod]
        public void TestEmployeeIdWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.EmployeeId = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.EmployeeId.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion EmployeeId Tests

        #region UserKey Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the UserKey with null value saves.
        /// </summary>
        [TestMethod]
        public void TestUserKeyWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.UserKey = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the UserKey with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestUserKeyWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.UserKey = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the UserKey with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestUserKeyWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.UserKey = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the UserKey with one character saves.
        /// </summary>
        [TestMethod]
        public void TestUserKeyWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.UserKey = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the UserKey with long value saves.
        /// </summary>
        [TestMethod]
        public void TestUserKeyWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.UserKey = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.UserKey.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion UserKey Tests

        #region Sid Tests

        #region Invalid Tests

        #endregion Invalid Tests

        #region Valid Tests

        /// <summary>
        /// Tests the Sid with null value saves.
        /// </summary>
        [TestMethod]
        public void TestSidWithNullValueSaves()
        {
            #region Arrange

            User user = GetValid(9);
            user.Sid = null;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Sid with empty string saves.
        /// </summary>
        [TestMethod]
        public void TestSidWithEmptyStringSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Sid = string.Empty;

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Sid with spaces only saves.
        /// </summary>
        [TestMethod]
        public void TestSidWithSpacesOnlySaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Sid = " ";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Sid with one character saves.
        /// </summary>
        [TestMethod]
        public void TestSidWithOneCharacterSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Sid = "x";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        /// <summary>
        /// Tests the Sid with long value saves.
        /// </summary>
        [TestMethod]
        public void TestSidWithLongValueSaves()
        {
            #region Arrange

            var user = GetValid(9);
            user.Sid = "x".RepeatTimes(999);

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.AreEqual(999, user.Sid.Length);
            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());

            #endregion Assert
        }

        #endregion Valid Tests

        #endregion Sid Tests

        #region FullName Tests

        /// <summary>
        /// Tests the full name joins first and last.
        /// </summary>
        [TestMethod]
        public void TestFullNameJoinsFirstAndLast()
        {
            #region Arrange

            var user = GetValid(9);
            user.FirstName = "FName";
            user.LastName = "LName";

            #endregion Arrange

            #region Act

            UserRepository.DbContext.BeginTransaction();
            UserRepository.EnsurePersistent(user);
            UserRepository.DbContext.CommitTransaction();

            #endregion Act

            #region Assert

            Assert.IsFalse(user.IsTransient());
            Assert.IsTrue(user.IsValid());
            Assert.AreEqual("FName LName", user.FullName);

            #endregion Assert
        }
        #endregion FullName Tests

        
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
            expectedFields.Add(new NameAndType("Email", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("EmployeeId", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("FirstName", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("FullName", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Id", "System.Int32", new List<string>
            {
                "[Newtonsoft.Json.JsonPropertyAttribute()]", 
                "[System.Xml.Serialization.XmlIgnoreAttribute()]"
            }));
            expectedFields.Add(new NameAndType("LastName", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("LoginId", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Phone", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Sid", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("UserKey", "System.String", new List<string>()));
            #endregion Arrange

            AttributeAndFieldValidation.ValidateFieldsAndAttributes(expectedFields, typeof(User));

        }

        #endregion Reflection of Database.	
		
		
    }
}