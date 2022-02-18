using System;
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
        
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void InsertExplicitIdentityShouldFail(int id)
        {
            string expectedExceptionMessage =
                "No se puede insertar un valor explícito en la columna de identidad de la tabla 'EncapsulationTypes' cuando IDENTITY_INSERT es OFF.";
            Action action = () => InsertNewEncapsulationType(id, $"Name {id}", $"Body {id}");
            DbUpdateException originalException = Assert.Throws<DbUpdateException>(action);
            Assert.Equal(expectedExceptionMessage, originalException.InnerException!.Message);
        }
        
        [Theory]
        [InlineData(4, 1)]
        [InlineData(4, 2)]
        [InlineData(4, 3)]
        public void InsertProductIntoProductsListShouldOk(int etId, int productId)
        {
            var database = new ICDatabase();
            var encapsulationType = GetEncapsulationType(etId);
            var product = GetProduct(productId);
            var previousCount = encapsulationType.Products.Count();
            encapsulationType.Products.Add(product);
            database.Entry(encapsulationType).State = EntityState.Modified;
            database.Entry(product).State = EntityState.Modified;
            database.SaveChanges();
            var newCount = GetEncapsulationType(etId).Products.Count();
            
            Assert.True(previousCount < newCount);
        }
        
        [Theory]
        [InlineData(4, 5)]
        [InlineData(5, 5)]
        [InlineData(6, 5)]
        public void UpdateEncapsulationTypeOfExistingProductShouldOk(int productId, int etId)
        {
            var database = new ICDatabase();
            var product = GetProduct(productId);
            product.EncapsulationTypeId = etId;
            database.Entry(product).State = EntityState.Modified;
            database.SaveChanges();
            var updatedITId = GetProduct(productId).EncapsulationTypeId;
            Assert.True(etId == updatedITId);
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
        
        private void InsertNewEncapsulationType(int id, string name, string body)
        {
            using ICDatabase database = new();
            var encapsulationType = new EncapsulationType() {Id = id, Name = name, BodyWidth = body};
            database.EncapsulationTypes.Add(encapsulationType);
            database.SaveChanges();
        }
        
        private Product GetProduct(int id)
        {
            using ICDatabase database = new();
            return database.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}