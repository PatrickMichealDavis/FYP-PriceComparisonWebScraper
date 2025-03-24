using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Helpers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceNowTest
{
    public class PerformanceTests
    {
        [Test]
        public async Task CompareProcessProductsAndProcessProductsV2()
        {


            var existingProducts = TestDataGenerator.GenerateProducts(5000, "timber");

            var scrapedProducts = TestDataGenerator.GenerateProducts(2000, "timber");

            var mockProductRepository = new MockProductRepository(existingProducts);

            var productService = new ProductService(mockProductRepository);

            var sw = Stopwatch.StartNew();
            await productService.ProcessProducts(scrapedProducts);
            sw.Stop();
            var oldTime = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Old approach took {oldTime} ms");


            existingProducts = TestDataGenerator.GenerateProducts(5000, "timber");

            mockProductRepository = new MockProductRepository(existingProducts);

            productService = new ProductService(mockProductRepository);

            sw.Restart();
            await productService.ProcessProductsV2(scrapedProducts);
            sw.Stop();
            var newTime = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"New approach took {newTime} ms");

            Assert.That(newTime, Is.LessThan(oldTime));
        }

        [Test]
        public async Task CompareProcessProductsAndProcessProductsV2LiveData()
        {
            string chadwicksSanitizedProductsFilePath = "chadwicksSanitizedProducts.json";
            string corkbpSanitizedProductsFilePath = "corkbpSanitizedProducts.json";
            string tjomahonySanitizedProductsFilePath = "tjomahonySanitizedProducts.json";


            var existingProducts = await FileHelper.GetJsonProducts(chadwicksSanitizedProductsFilePath);

            var scrapedProducts = await FileHelper.GetJsonProducts(corkbpSanitizedProductsFilePath);

            var mockProductRepository = new MockProductRepository(existingProducts);

            var productService = new ProductService(mockProductRepository);

            var sw = Stopwatch.StartNew();
            await productService.ProcessProducts(scrapedProducts);
            sw.Stop();
            var oldTime = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Old approach took {oldTime} ms");


            existingProducts = await DataParser.GetJsonProducts(chadwicksSanitizedProductsFilePath);

            mockProductRepository = new MockProductRepository(existingProducts);

            productService = new ProductService(mockProductRepository);

            sw.Restart();
            await productService.ProcessProductsV2(scrapedProducts);
            sw.Stop();
            var newTime = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"New approach took {newTime} ms");

            Assert.That(newTime, Is.LessThan(oldTime));
        }
    }
}
