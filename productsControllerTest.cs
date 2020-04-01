using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UnitTestAssignment1.Controllers.productsController;
using Assignment1.Data;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;



namespace UnitTestAssignment1
{
    [TestClass]
    public class productsControllerTest
    {
        productsController productsController;
        List<Product> products;

        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            products = new List<Product>();

            Product mockProduct = new Product
            {
                productId = 55,
                productName = "A product"
            };

            products.Add(new Product
            {
                productId = 20,
                productName = "Some Model",
                productType = 38,
                color = "Red",
                price = mockProduct
            });

            products.Add(new Product
            {
                productId = 200,
                productName = "Some Model",
                color = 770,
                price = "Blue",
                productType = mockProduct
            });

            products.Add(new Product
            {
                productId = 300,
                productName = "Some Model",
                productType = mockProduct,
                color = "Green",
                Price = 789
            });

            foreach (var k in products)
            {
                // add each product to in-memory db
                _context.Equipments.Add(k);
            }
            _context.SaveChanges();

            productsController = new ProductsController(_context);
        }

        [TestMethod]
        public void IndexLoadsCorrectView()
        {
            // act
            var result = productsController.Index().Result;
            var viewResult = (ViewResult)result;

            // ASSERT
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsProduct()
        {
            // act
            var result = productsController.Index().Result;

            // get the view result
            var viewResult = (ViewResult)result;

            // assert - convert result to list of products & compare to mock product list
            CollectionAssert.AreEqual(products.OrderBy(p => p.Model).ToList(), (List<Product>)viewResult.Model);
        }

        [TestMethod]
        public void DetailsMissingId()
        {
            // act
            var result = productsController.Details(null).Result;

            // assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            var result = productsController.Details(8879).Result;

            // assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsValidIdLoadsProduct()
        {
            // act
            var result = productsController.Details(200).Result;
            var viewResult = (ViewResult)result;

            // assert
            Assert.AreEqual(products[1], viewResult.Model);
        }


    }
}