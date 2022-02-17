using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mux;
using Mux.Model;
using Xunit;
using Xunit.Sdk;

namespace MuxUnitTests.Tables
{
    public class Categories
    {
        [Theory]
        [InlineData("Leds")]
        [InlineData("Semiconductores")]
        [InlineData("Transistores")]
        public void InsertNewCategoryWithoutProductResultOk(string categoryName)
        {
            ICDatabase database = new();
            int previousCount = database.Categories.Count();
            InsertNewCategoryIntoTable(categoryName);
            int newCount = database.Categories.Count();
            Assert.True(previousCount < newCount);
        }

        void InsertNewCategoryIntoTable(string name)
        {
            using ICDatabase database = new ICDatabase();
            var newCategory = new Category() { Name = name };
            database.Categories.Add(newCategory);
            database.SaveChanges();
        }

        private Category GetCategory(int id)
        {
            using ICDatabase database = new();
            var category = database.Categories
                .Include(category => category.Products)
                .FirstOrDefault(category => category.Id == id);

            if (category == null)
            {
                throw new NotNullException();
            }

            return category;
        }

        private Product GetProduct(int id)
        {
            using ICDatabase database = new();
            var product = database.Products.FirstOrDefault(category => category.Id == id);

            if (product == null)
            {
                throw new NotNullException();
            }

            return product;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void InsertExplicitIdentityShouldThrowException(int id)
        {
            string expectedExceptionMessage =
                "No se puede insertar un valor explícito en la columna de identidad de la tabla 'Categories' cuando IDENTITY_INSERT es OFF.";
            Action action = () => InsertNewCategoryIntoTable(id, $"Tabla {id}");
            DbUpdateException originalException = Assert.Throws<DbUpdateException>(action);
            Assert.Equal(expectedExceptionMessage, originalException.InnerException!.Message);
        }
        
        void InsertNewCategoryIntoTable(int id, string name)
        {
            using ICDatabase database = new ICDatabase();
            var newCategory = new Category() { Id = id, Name = name };
            database.Categories.Add(newCategory);
            database.SaveChanges();
        }
    }
}