using Mux;
using Mux.Model;

namespace MuxUnitTests.Tables
{
    public class Categories
    {
        void InsertNewCategoryIntoTableOnlyWithName(string name)
        {
            using ICDatabase database = new ICDatabase();
            var newCategory = new Category() { Name = name };
            database.Categories.Add(newCategory);
            database.SaveChanges();
        }
    }
}