using PBTechnicalAssignment.Core.Model;

namespace PBTechnicalAssignment.Core.Services
{
    public class BinWidthCalculationService
    {
        private Dictionary<Data.Model.ProductType, double> _productTypeWidths = new()
        {
            { Data.Model.ProductType.PHOTOBOOK, 19 },
            { Data.Model.ProductType.CALENDAR, 10 },
            { Data.Model.ProductType.CANVAS, 16 },
            { Data.Model.ProductType.CARDS, 4.7 },
            { Data.Model.ProductType.MUG, 94 }
        };

        public double CalculateMinimiumBinWidth(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Total required bin width is summed width of each item type
            // bearing in mind the amount of each item type
            var itemWidths = order.Items
                .Select(CalculateBinWidth)
                .ToList();

            return itemWidths.Sum();
        }

        private double CalculateBinWidth(OrderItem orderItem)
        {
            // Width increased with each item, except mugs which can stack up to 4 in one 'slot'
            var multiplier = orderItem.Quantity;
            if (orderItem.ProductType == Data.Model.ProductType.MUG)
            {
                multiplier = (int)Math.Ceiling((double)orderItem.Quantity / 4);
            }

            var itemWidth = _productTypeWidths[orderItem.ProductType];
            return itemWidth * multiplier;
        }
    }
}