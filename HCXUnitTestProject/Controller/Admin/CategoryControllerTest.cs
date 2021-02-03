using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HC.DataAccess.Data.Repository; 
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using Moq;
using HomeCook.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace HCXUnitTestProject.Controller
{
    public class CategoryControllerTest
    {
        [Fact]
        public void GetAll_ReturnJsonResult_WithAListOfCategory()
        {
          
            var mockUnit = new Mock<IUnitOfWork>();
            _ = mockUnit.Setup(unit => unit.Category.GetAll( null, null, null )).Returns(GetTestCategory()); 
            var controller = new CategoryController(mockUnit.Object);
            var  result = controller.GetAll();

            Assert.True(result.GetType() == typeof(JsonResult)); 
        }

        [Fact]
        public void Insert_ReturnIndexPage()
        {
            var category = new Category()
            {
                Name = "Services",
                DisplayOrder = 3,
                Description = "Test Inserted"
            };

            var mockUnit = new Mock<IUnitOfWork>();
            _ = mockUnit.Setup(unit => unit.Category.GetAll(null, null, null)).Returns(GetTestCategory());
            var controller = new CategoryController(mockUnit.Object);
            var result = (IActionResult) controller.Upsert(category);

            Assert.True(result.GetType() == typeof(RedirectToActionResult));
            RedirectToActionResult actionResult = (RedirectToActionResult)result;
            Assert.True(actionResult.ActionName == "Index"); 
            
        }

        private IEnumerable<Category> GetTestCategory()
        {
            return new List<Category>()
            {
                new Category() { 
                    Id = 1, 
                    DisplayOrder = 1, 
                    Name = "Dessert"
                }, 
                new Category(){ 
                    Id = 2, 
                    DisplayOrder = 2, 
                    Name = "Main Dishes"
                }
            }; 
        }
    }
}
