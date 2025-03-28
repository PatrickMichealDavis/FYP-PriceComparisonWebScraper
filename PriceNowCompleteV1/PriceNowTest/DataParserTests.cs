using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Helpers;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Services;

namespace PriceNowTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnTrue_NameExactUnitExact()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 44mm x 4.8m" };
            var product2 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 44mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnFasle_NameDifferentUnitDifferent()
        {
            var product1 = new Product {Name = "treated rough white deal timber", Unit = "100mm x 50mm x 4.8m" };
            var product2 = new Product { Name = "gyproc metal stud s", Unit = "3600mm 70 50" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnFalse_NameDifferentUnitExact()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 50mm x 4.8m" };
            var product2 = new Product { Name = "gyproc metal stud s", Unit = "100mm x 50mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnFalse_NameExactUnitDifferent()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 50mm x 4.8m" };
            var product2 = new Product { Name = "treated rough white deal timber", Unit = "3600mm 70 50" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }


        [Test]
        public void CheckForCloseComparrison_ShouldReturnFalse_NameCloseUnitClose()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 50mm x 4.8m" };
            var product2 = new Product { Name = "rough white deal timber", Unit = "10mm x 50mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnFalse_NameExactUnitClose()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 50mm x 4.8m" };
            var product2 = new Product { Name = "treated rough white deal timber", Unit = "10mm x 50mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnTrue_NameCloseUnitExact()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 44mm x 4.8m" };
            var product2 = new Product { Name = "treated white deal timber", Unit = "100mm x 44mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForCloseComparrison_ShouldReturnFalse_NameContainsTreatedUnitExact()
        {
            var product1 = new Product { Name = "treated rough white deal timber", Unit = "100mm x 44mm x 4.8m" };
            var product2 = new Product { Name = "white deal rough timber", Unit = "100mm x 44mm x 4.8m" };

            var result = DataParser.CheckForCloseComparrison(product1, product2);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CheckForCloseComparrison_ShouldReturnTrue_BothProductTheSame()
        {
           
            string chadwicksSanitizedProductsFilePath = "chadwicksSanitizedProducts.json";
            string corkbpSanitizedProductsFilePath = "corkbpSanitizedProducts.json";
            string tjomahonySanitizedProductsFilePath = "tjomahonySanitizedProducts.json";

            var scrapedProducts = await FileHelper.GetJsonProducts(tjomahonySanitizedProductsFilePath);
             var existingProducts =  await FileHelper.GetJsonProducts(chadwicksSanitizedProductsFilePath);
           // var existingProducts = await FileHelper.GetJsonProducts(corkbpSanitizedProductsFilePath);



            foreach (var exProduct in existingProducts) 
            {
                foreach (var scProduct in scrapedProducts)
                {
                    if (DataParser.CheckForCloseComparrison(exProduct, scProduct))
                    {
                        exProduct.Prices = (ICollection<Price>)scProduct.Prices.First();
                        Console.WriteLine($"Match found: {exProduct.Name} ~ {scProduct.Name}");
                    }
                }
            }


            
            

        }

    }
}