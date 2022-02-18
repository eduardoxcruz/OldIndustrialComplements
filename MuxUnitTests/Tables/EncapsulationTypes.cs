using System.Linq;
using Mux;
using Mux.Model;
using Xunit;

namespace MuxUnitTests.Tables
{
    public class EncapsulationTypes
    {
        [Theory]
        [InlineData("TO-220", "3.9mm")]
        [InlineData("SOIC-14", "7mm")]
        [InlineData("DIP-6", "4mm")]
        public void AddNewEncapsulationTypesShouldOk(string name, string bodyWidth)
        {
            using ICDatabase database = new();
            EncapsulationType newEncapsulationType = new() { Name = name, BodyWidth = bodyWidth};
            int previousCount = GetDataCountFromTable();
            database.EncapsulationTypes.Add(newEncapsulationType);
            database.SaveChanges();
            int newCount = GetDataCountFromTable();
            Assert.True(previousCount < newCount);
        }
        
        private int GetDataCountFromTable()
        {
            using ICDatabase database = new();
            return database.EncapsulationTypes.Count();
        }
    }
}