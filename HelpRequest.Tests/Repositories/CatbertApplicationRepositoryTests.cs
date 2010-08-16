using System.Collections.Generic;
using System.Linq;
using HelpRequest.Core.Domain;
using HelpRequest.Tests.Core;
using HelpRequest.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace HelpRequest.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		CatbertApplication
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class CatbertApplicationRepositoryTests : AbstractRepositoryTests<CatbertApplication, int>
    {
        /// <summary>
        /// Gets or sets the CatbertApplication repository.
        /// </summary>
        /// <value>The CatbertApplication repository.</value>
        public IRepository<CatbertApplication> CatbertApplicationRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="CatbertApplicationRepositoryTests"/> class.
        /// </summary>
        public CatbertApplicationRepositoryTests()
        {
            CatbertApplicationRepository = new Repository<CatbertApplication>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override CatbertApplication GetValid(int? counter)
        {
            return CreateValidEntities.CatbertApplication(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<CatbertApplication> GetQuery(int numberAtEnd)
        {
            return CatbertApplicationRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(CatbertApplication entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(CatbertApplication entity, ARTAction action)
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
            CatbertApplicationRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            CatbertApplicationRepository.DbContext.CommitTransaction();
        }

        [TestMethod]
        public override void CanUpdateEntity()
        {
            CanUpdateEntity(false);
        }

        #endregion Init and Overrides	
        
        //Fields don't need to be tested.
        
        
        
        
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
            expectedFields.Add(new NameAndType("Abbr", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Id", "System.Int32", new List<string>
            {
                "[Newtonsoft.Json.JsonPropertyAttribute()]", 
                "[System.Xml.Serialization.XmlIgnoreAttribute()]"
            }));
            expectedFields.Add(new NameAndType("Location", "System.String", new List<string>()));
            expectedFields.Add(new NameAndType("Name", "System.String", new List<string>()));
            #endregion Arrange

            AttributeAndFieldValidation.ValidateFieldsAndAttributes(expectedFields, typeof(CatbertApplication));

        }

        #endregion Reflection of Database.	
		
		
    }
}