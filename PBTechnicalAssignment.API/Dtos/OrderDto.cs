namespace PBTechnicalAssignment.API.Dtos
{
    /// <summary>
    /// An order placed via the PhotoBox API.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Amounts of product types in the order.
        /// </summary>
        public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        /// <summary>
        /// The minimum bin width required to fit all items in.
        /// </summary>
        public double MinimumBinWidth { get; set; }
    }
}
