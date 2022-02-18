using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteEncapsulationTypeIdShouldOk(int id)
        {
            using ICDatabase database = new();
            var encapsulation = GetEncapsulationType(id);
            int previousCount = GetDataCountFromTable();
            database.EncapsulationTypes.Remove(encapsulation);
            database.SaveChanges();
            int newCount = GetDataCountFromTable();
            Assert.True(newCount < previousCount);
        }
        
        private int GetDataCountFromTable()
        {
            using ICDatabase database = new();
            return database.EncapsulationTypes.Count();
        }
        
        private EncapsulationType GetEncapsulationType(int id)
        {
            using ICDatabase database = new();
            return database.EncapsulationTypes
                .Include(e => e.Products)
                .FirstOrDefault(e => e.Id == id);
        }

    }
}