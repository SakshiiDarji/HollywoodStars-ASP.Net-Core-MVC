using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using HollywoodStars.Data;
using Microsoft.EntityFrameworkCore;
using HollywoodStars.Models;

namespace HollywoodStars.Tests
{
    [TestClass]
    public class HollywoodStarsCompanyTests
    {
        private  HollywoodStarsContext _context = null!;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<HollywoodStarsContext>()
                .UseInMemoryDatabase(databaseName: $"HollywoodStarsTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new HollywoodStarsContext(options);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task Insert_ShouldAddCompany()
        {
            // Arrange
            var company = new Company()
            {
                Name = "Test Company",
                Type = "Test Type"
            };

            //Act
            await CompaniesData.Insert(company, _context);

            //Assert
            var insertedCompany = await _context.Companies.FirstOrDefaultAsync(t => t.Name == company.Name);
            Assert.IsNotNull(insertedCompany);
            Assert.AreEqual(company.Name, insertedCompany.Name);
            Assert.AreEqual(company.Type, insertedCompany.Type);
       
        }

        //[TestMethod]
        //public async Task Update_ShouldModifyExistingCompany()
        //{
        //    // Arrange
        //    var company = new Company
        //    {
        //        Name = "Update Company",
        //        Type = "Update Type"
        //    };
        //    await CompaniesData.Insert(company, _context);

        //    // Act
        //    company.Name = "Update Company";
        //    company.Type = "Update Type";
        //    await CompaniesData.Update(company, _context);

        //    // Assert
        //    var updatedCompany = await _context.Companies.FindAsync(company.CompanyId);
        //    Assert.IsNotNull(updatedCompany, "Company was not found after update.");
        //    Assert.AreEqual(company.Name, updatedCompany.Name);
        //    Assert.AreEqual(company.Type, updatedCompany.Type);
           
        //}

        [TestMethod]
        public async Task Delete_ShouldRemoveCompany()
        {
            // Arrange
            var company = new Company
            {
                Name = "Delete Company",
                Type = "Delete Type"
            };
            await CompaniesData.Insert(company, _context);

            // Act
            await CompaniesData.Delete(company, _context);

            // Assert
            var deletedcompany = await _context.Companies.FindAsync(company.CompanyId);
            Assert.IsNull(deletedcompany, "Company was not deleted.");
        }


    }
}
