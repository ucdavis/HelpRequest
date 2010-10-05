using System;
using System.Collections.Generic;
using System.Configuration;
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
	/// Note, this table does *not* exist in the database and is just used as a container
	/// Entity Name:		Attachment
	/// LookupFieldName:	Name
	/// </summary>
	[TestClass]
	public class AttachmentRepositoryTests : AbstractRepositoryTests<Attachment, int, AttachmentMap>
	{
		/// <summary>
		/// Gets or sets the Attachment repository.
		/// </summary>
		/// <value>The Attachment repository.</value>
		public IRepository<Attachment> AttachmentRepository { get; set; }
		
		#region Init and Overrides


		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRepositoryTests"/> class.
		/// </summary>
		public AttachmentRepositoryTests()
		{
			AttachmentRepository = new Repository<Attachment>();
		}

		/// <summary>
		/// Gets the valid entity of type T
		/// </summary>
		/// <param name="counter">The counter.</param>
		/// <returns>A valid entity of type T</returns>
		protected override Attachment GetValid(int? counter)
		{
			return CreateValidEntities.Attachment(counter);
		}

		/// <summary>
		/// A Query which will return a single record
		/// </summary>
		/// <param name="numberAtEnd"></param>
		/// <returns></returns>
		protected override IQueryable<Attachment> GetQuery(int numberAtEnd)
		{
			return AttachmentRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
		}

		/// <summary>
		/// A way to compare the entities that were read.
		/// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="counter"></param>
		protected override void FoundEntityComparison(Attachment entity, int counter)
		{
			Assert.AreEqual("Name" + counter, entity.Name);
		}

		/// <summary>
		/// Updates , compares, restores.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="action">The action.</param>
		protected override void UpdateUtility(Attachment entity, ARTAction action)
		{
			const string updateValue = "Updated";
			switch (action)
			{
				case ARTAction.Compare:
					Assert.AreEqual(updateValue, entity.Name);
					break;
				case ARTAction.Restore:
					entity.Name = RestoreValue;
					break;
				case ARTAction.Update:
					RestoreValue = entity.Name;
					entity.Name = updateValue;
					break;
			}
		}

		/// <summary>
		/// Loads the data.
		/// </summary>
		protected override void LoadData()
		{
			AttachmentRepository.DbContext.BeginTransaction();
			LoadRecords(5);
			AttachmentRepository.DbContext.CommitTransaction();
		}

		#endregion Init and Overrides	
		
		#region Name Tests
		#region Invalid Tests

		/// <summary>
		/// Tests the Name with null value does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestNameWithNullValueDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.Name = null;
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("Name: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the Name with empty string does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestNameWithEmptyStringDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.Name = string.Empty;
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("Name: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the Name with spaces only does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestNameWithSpacesOnlyDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.Name = " ";
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("Name: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the Name with too long value does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestNameWithTooLongValueDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.Name = "x".RepeatTimes((100 + 1));
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				Assert.AreEqual(100 + 1, attachment.Name.Length);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("Name: length must be between 0 and 100");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}
		#endregion Invalid Tests

		#region Valid Tests

		/// <summary>
		/// Tests the Name with one character saves.
		/// </summary>
		[TestMethod]
		public void TestNameWithOneCharacterSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.Name = "x";
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the Name with long value saves.
		/// </summary>
		[TestMethod]
		public void TestNameWithLongValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.Name = "x".RepeatTimes(100);
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.AreEqual(100, attachment.Name.Length);
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		#endregion Valid Tests
		#endregion Name Tests

		#region DateCreated Tests

		/// <summary>
		/// Tests the DateCreated with past date will save.
		/// </summary>
		[TestMethod]
		public void TestDateCreatedWithPastDateWillSave()
		{
			#region Arrange
			var compareDate = DateTime.Now.AddDays(-10);
			Attachment record = GetValid(99);
			record.DateCreated = compareDate;
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(record);
			AttachmentRepository.DbContext.CommitChanges();
			#endregion Act

			#region Assert
			Assert.IsFalse(record.IsTransient());
			Assert.IsTrue(record.IsValid());
			Assert.AreEqual(compareDate, record.DateCreated);
			#endregion Assert		
		}

		/// <summary>
		/// Tests the DateCreated with current date date will save.
		/// </summary>
		[TestMethod]
		public void TestDateCreatedWithCurrentDateDateWillSave()
		{
			#region Arrange
			var compareDate = DateTime.Now;
			var record = GetValid(99);
			record.DateCreated = compareDate;
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(record);
			AttachmentRepository.DbContext.CommitChanges();
			#endregion Act

			#region Assert
			Assert.IsFalse(record.IsTransient());
			Assert.IsTrue(record.IsValid());
			Assert.AreEqual(compareDate, record.DateCreated);
			#endregion Assert
		}

		/// <summary>
		/// Tests the DateCreated with future date date will save.
		/// </summary>
		[TestMethod]
		public void TestDateCreatedWithFutureDateDateWillSave()
		{
			#region Arrange
			var compareDate = DateTime.Now.AddDays(15);
			var record = GetValid(99);
			record.DateCreated = compareDate;
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(record);
			AttachmentRepository.DbContext.CommitChanges();
			#endregion Act

			#region Assert
			Assert.IsFalse(record.IsTransient());
			Assert.IsTrue(record.IsValid());
			Assert.AreEqual(compareDate, record.DateCreated);
			#endregion Assert
		}
		#endregion DateCreated Tests
		
		#region FileName Tests
		#region Invalid Tests

		/// <summary>
		/// Tests the FileName with null value does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestFileNameWithNullValueDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.FileName = null;
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("FileName: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the FileName with empty string does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestFileNameWithEmptyStringDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.FileName = string.Empty;
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("FileName: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the FileName with spaces only does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestFileNameWithSpacesOnlyDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.FileName = " ";
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("FileName: may not be null or empty");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}

		/// <summary>
		/// Tests the FileName with too long value does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestFileNameWithTooLongValueDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.FileName = "x".RepeatTimes((100 + 1));
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				Assert.AreEqual(100 + 1, attachment.FileName.Length);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("FileName: length must be between 0 and 100");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}
		#endregion Invalid Tests

		#region Valid Tests

		/// <summary>
		/// Tests the FileName with one character saves.
		/// </summary>
		[TestMethod]
		public void TestFileNameWithOneCharacterSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.FileName = "x";
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the FileName with long value saves.
		/// </summary>
		[TestMethod]
		public void TestFileNameWithLongValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.FileName = "x".RepeatTimes(100);
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.AreEqual(100, attachment.FileName.Length);
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		#endregion Valid Tests
		#endregion FileName Tests

		#region Contents Tests
		#region Invalid Tests

		/// <summary>
		/// Tests the contents with null value does not save.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestContentsWithNullValueDoesNotSave()
		{
			Attachment attachment = null;
			try
			{
				#region Arrange
				attachment = GetValid(9);
				attachment.Contents = null;
				#endregion Arrange

				#region Act
				AttachmentRepository.DbContext.BeginTransaction();
				AttachmentRepository.EnsurePersistent(attachment);
				AttachmentRepository.DbContext.CommitTransaction();
				#endregion Act
			}
			catch (Exception)
			{
				Assert.IsNotNull(attachment);
				Assert.IsNull(attachment.Contents);
				var results = attachment.ValidationResults().AsMessageList();
				results.AssertErrorsAre("Contents: may not be null");
				Assert.IsTrue(attachment.IsTransient());
				Assert.IsFalse(attachment.IsValid());
				throw;
			}
		}
		#endregion Invalid Tests
		#region Valid Tests
		/// <summary>
		/// Tests the contents with new value saves.
		/// </summary>
		[TestMethod]
		public void TestContentsWithNewValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.Contents = new byte[0];
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.AreEqual("", attachment.Contents.ByteArrayToString());
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the contents with new value saves.
		/// </summary>
		[TestMethod]
		public void TestContentsWithNonNullValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.Contents = new byte[0];
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.AreEqual("", attachment.Contents.ByteArrayToString());
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}
		#endregion Valid Tests
		#endregion Contents Tests
		
		#region ContentType Tests

		#region Valid Tests

		[TestMethod]
		public void TestContentTypeWithNullValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.ContentType = null;
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}
		/// <summary>
		/// Tests the content type with empty string saves.
		/// </summary>
		[TestMethod]
		public void TestContentTypeWithEmptyStringSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.ContentType = string.Empty;
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the content type with space only saves.
		/// </summary>
		[TestMethod]
		public void TestContentTypeWithSpaceOnlySaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.ContentType = " ";
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the ContentType with one character saves.
		/// </summary>
		[TestMethod]
		public void TestContentTypeWithOneCharacterSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.ContentType = "x";
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		/// <summary>
		/// Tests the ContentType with long value saves.
		/// </summary>
		[TestMethod]
		public void TestContentTypeWithLongValueSaves()
		{
			#region Arrange
			var attachment = GetValid(9);
			attachment.ContentType = "x".RepeatTimes(999);
			#endregion Arrange

			#region Act
			AttachmentRepository.DbContext.BeginTransaction();
			AttachmentRepository.EnsurePersistent(attachment);
			AttachmentRepository.DbContext.CommitTransaction();
			#endregion Act

			#region Assert
			Assert.AreEqual(999, attachment.ContentType.Length);
			Assert.IsFalse(attachment.IsTransient());
			Assert.IsTrue(attachment.IsValid());
			#endregion Assert
		}

		#endregion Valid Tests
		#endregion ContentType Tests

		#region Constructor Tests

		/// <summary>
		/// Tests the constructor sets expected values.
		/// </summary>
		[TestMethod]
		public void TestConstructorSetsExpectedValues()
		{
			#region Arrange
			var attachment = new Attachment("Name-Test", "FileName-Test");
			#endregion Arrange

			#region Assert
			Assert.AreEqual("Name-Test", attachment.Name);
			Assert.AreEqual("FileName-Test", attachment.FileName);
			Assert.AreEqual(DateTime.Now.Date, attachment.DateCreated.Date);
			#endregion Assert		
		}
		#endregion Constructor Tests

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
			expectedFields.Add(new NameAndType("Contents", "System.Byte[]", new List<string>{
				 "[NHibernate.Validator.Constraints.NotNullAttribute()]"
			}));
			expectedFields.Add(new NameAndType("ContentType", "System.String", new List<string>()));
			expectedFields.Add(new NameAndType("DateCreated", "System.DateTime", new List<string>()));
			expectedFields.Add(new NameAndType("FileName", "System.String", new List<string>
			{
				 "[NHibernate.Validator.Constraints.LengthAttribute((Int32)100)]", 
				 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute()]"
			}));
			expectedFields.Add(new NameAndType("Id", "System.Int32", new List<string>
			{
				"[Newtonsoft.Json.JsonPropertyAttribute()]", 
				"[System.Xml.Serialization.XmlIgnoreAttribute()]"
			}));
			expectedFields.Add(new NameAndType("Name", "System.String", new List<string>
			{
				 "[NHibernate.Validator.Constraints.LengthAttribute((Int32)100)]", 
				 "[UCDArch.Core.NHibernateValidator.Extensions.RequiredAttribute()]"
			}));
			#endregion Arrange

			AttributeAndFieldValidation.ValidateFieldsAndAttributes(expectedFields, typeof(Attachment));

		}

		#endregion Reflection of Database.	
		
		
	}
}