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
        public static void RunProductNameSplitTests()
        {
            var testCases = new List<string>
            {
                "white deal rough timber - 150mm x 44mm x 3.6m ( 6 x 2 inches approx)",
                "2.4m 100 x 44 imported rough white deal",
                "4.2m 50 x 36 imported white deal rough sr82",
                "7.2m 225 x 44 truss timber c24 rough white deal c24",
                "4.8m 100 x 50 cls white deal",
                "50mm x 22mm rough timber (2 x 1)",
                "150mm x 44mm rough timber c16 en14081 (6 x 2)"
            };

            foreach (var test in testCases)
            {
                var result = DataParser.SplitProductNameAndUnit(test);
                Console.WriteLine("Original:  " + test);
                Console.WriteLine("Name:      " + result.Item1);
                Console.WriteLine("Unit:      " + result.Item2);
                Console.WriteLine(new string('-', 60));
            }
        }
    }
}