using System.Linq;
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
            InsertNewCategoryIntoTableOnlyWithName(categoryName);
            int newCount = database.Categories.Count();
            Assert.True(previousCount < newCount);
        } 
        
        void InsertNewCategoryIntoTableOnlyWithName(string name)
        {
            using ICDatabase database = new ICDatabase();
            var newCategory = new Category() { Name = name };
            database.Categories.Add(newCategory);
            database.SaveChanges();
        }
    }
}