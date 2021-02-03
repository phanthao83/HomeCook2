using HC.DataAccess;
using HC.DataAccess.Data.Repository;
using HC.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Xunit;
using HCXUnitTestProject.Utility; 

namespace HCXUnitTestProject.Respository
{
   public class CategoryRespositoryTest
    {

        [Fact]
        public void Add_SuccessfulWithFullInformation()
        {
            
            //Using dataset to insert 
            var dbCategory = new Mock<DbSet<Category>>();
        //    Moq.Language.Flow.IReturnsResult<DbSet<Category>> returnsResult = dbCategory.Setup(x => x.Add(It.IsAny<Category>())).Returns(category);
            Mock<ApplicationDbContext> mockDbContext = new Mock<ApplicationDbContext>( );
            mockDbContext.Setup(x => x.Set<Category>()).Returns(dbCategory.Object); 
            CategoryRespository categoryRespository = new CategoryRespository(mockDbContext.Object);
            var category = new Category()
            {
                Id = 1,
                DisplayOrder = 1,
                Name = "Category 1",
                Description = "Initialized db"
            };
            categoryRespository.Add(category);


            // Both must return the same value 
            mockDbContext.Verify(x => x.Set<Category>());
            dbCategory.Verify(x => x.Add(It.Is<Category>(y => y == category))); 

        }

        [Fact]
        public void GetAll_SuccessfulWithFullInformation()
        {

            //Using dataset to insert 
            //    Moq.Language.Flow.IReturnsResult<DbSet<Category>> returnsResult = dbCategory.Setup(x => x.Add(It.IsAny<Category>())).Returns(category);
            Mock<DbSet<Category>> mockDbSetCategory = CreateCategoryDbSet(); 

            Mock<ApplicationDbContext> mockDbContext = new Mock<ApplicationDbContext>();
            mockDbContext.Setup(x => x.Category).Returns(mockDbSetCategory.Object); 
          //  mockDbContext.Setup(x =>  x.Set<Category>()).Returns(mockDbSetCategory.Object);
            
            CategoryRespository categoryRespository = new CategoryRespository(mockDbContext.Object);
           
            var lstCategory =  categoryRespository.GetAll();


            // Both must return the same value 
            Assert.True(lstCategory.ToList().Count == 2); 

        }

        private Mock<DbSet<Category>> CreateCategoryDbSet()
        {
            var mockDbSetCategory = new Mock<DbSet<Category>>();
            var categoryLst = new List<Category>() { 
                new Category(){ Id = 1, Name = "Category1", Description = "Initlized Data" , DisplayOrder = 1},
                new Category(){ Id = 2, Name = "Category2", Description = "Initlized Data" , DisplayOrder = 2}
            };
            var queryableCateogryLst = categoryLst.AsQueryable();

            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(queryableCateogryLst.Expression);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(queryableCateogryLst.ElementType);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(queryableCateogryLst.GetEnumerator);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Category>(queryableCateogryLst.Provider));
            mockDbSetCategory.As<IDbAsyncEnumerable<Category>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Category>(queryableCateogryLst.GetEnumerator()));
            mockDbSetCategory.Setup(m => m.Add(It.IsAny<Category>())).Callback((Category cat) => categoryLst.Add(cat));
            mockDbSetCategory.Setup(m => m.Remove(It.IsAny<Category>())).Callback( (Category cat) => categoryLst.Remove(cat)); 
            return mockDbSetCategory; 


        }

      
        


    }
}
