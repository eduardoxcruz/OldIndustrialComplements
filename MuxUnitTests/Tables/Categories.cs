using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mux;
using Mux.Model;
using Xunit;

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