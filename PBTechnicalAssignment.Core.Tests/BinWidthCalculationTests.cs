using NUnit.Framework;
using PBTechnicalAssignment.Core.Model;
using PBTechnicalAssignment.Core.Services;
using ProductType = PBTechnicalAssignment.Data.Model.ProductType;

namespace PBTechnicalAssignment.Core.Tests
{
    [TestFixture]
    public class BinWidthCalculationTests
    {
        private readonly BinWidthCalculationService _service;

        private const double WIDTH_PHOTOBOOK = 19;
        private const double WIDTH_CALENDAR = 10;
        private const double WIDTH_CANVAS = 16;
        private const double WIDTH_CARDS = 4.7;
        private const double WIDTH_MUG = 94;

        public BinWidthCalculationTests()
        {
            _service = new BinWidthCalculationService();
        }

        [Test]
        public void OrderWithNoItems_ZeroWidth()
        {
            // Arrange
            var order = new Order();

            // Act
            var minWidth = _service.CalculateMinimiumBinWidth(order);

            // Assert
            Assert.AreEqual(0, minWidth);
        }

        [Test]
        [TestCase(ProductType.PHOTOBOOK, WIDTH_PHOTOBOOK)]
        [TestCase(ProductType.CALENDAR, WIDTH_CALENDAR)]
        [TestCase(ProductType.CANVAS, WIDTH_CANVAS)]
        [TestCase(ProductType.CARDS, WIDTH_CARDS)]
        [TestCase(ProductType.MUG, WIDTH_MUG)]
        public void OrderWithOneItem_WidthOfProductType(ProductType productType, double expectedWidth)
        {
            // Arrange
            var order = new Order()
            {
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductType = productType, Quantity = 1 }
                }
            };

            // Act
            var minWidth = _service.CalculateMinimiumBinWidth(order);

            // Assert
            Assert.AreEqual(expectedWidth, minWidth);
        }

        [Test]
        public void OrderWithOneItem_MultipliesWithQuantity()
        {
            // Arrange
            var quantity = 3;
            var expectedWidth = WIDTH_PHOTOBOOK * quantity;

            var order = new Order()
            {
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductType = ProductType.PHOTOBOOK, Quantity = quantity }
                }
            };

            // Act
            var minWidth = _service.CalculateMinimiumBinWidth(order);

            // Assert
            Assert.AreEqual(expectedWidth, minWidth);
        }

        [Test]
        [TestCase(1, WIDTH_MUG)]
        [TestCase(2, WIDTH_MUG)]
        [TestCase(3, WIDTH_MUG)]
        [TestCase(4, WIDTH_MUG)]
        [TestCase(5, 2 * WIDTH_MUG)]
        [TestCase(6, 2 * WIDTH_MUG)]
        [TestCase(7, 2 * WIDTH_MUG)]
        [TestCase(8, 2 * WIDTH_MUG)]
        [TestCase(9, 3 * WIDTH_MUG)]
        public void OrderWithOneItem_MultipliesWithQuantity_MugsSpecialCase(int quantity, double expectedWidth)
        {
            // Arrange
            var order = new Order()
            {
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductType = ProductType.MUG, Quantity = quantity }
                }
            };

            // Act
            var minWidth = _service.CalculateMinimiumBinWidth(order);

            // Assert
            Assert.AreEqual(expectedWidth, minWidth);
        }

        [Test]
        public void OrderWithMultipleItems_Sums()
        {
            // Arrange
            var order = new Order()
            {
                Items = new List<OrderItem>()
                {
                    new OrderItem() { ProductType = ProductType.PHOTOBOOK, Quantity = 1 },
                    new OrderItem() { ProductType = ProductType.CALENDAR, Quantity = 2 },
                    new OrderItem() { ProductType = ProductType.CANVAS, Quantity = 3 },
                    new OrderItem() { ProductType = ProductType.CARDS, Quantity = 4 }
                }
            };

            var expectedWidth =
                WIDTH_PHOTOBOOK +
                WIDTH_CALENDAR * 2 +
                WIDTH_CANVAS * 3 +
                WIDTH_CARDS * 4;

            // Act
            var minWidth = _service.CalculateMinimiumBinWidth(order);

            // Assert
            Assert.AreEqual(expectedWidth, minWidth);
        }
    }
}