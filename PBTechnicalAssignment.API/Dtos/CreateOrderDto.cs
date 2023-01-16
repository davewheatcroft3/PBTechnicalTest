namespace PBTechnicalAssignment.API.Dtos
{
    /// <summary>
    /// Create an order of which will then be delivered to pickup point.
    /// </summary>
    public class CreateOrderDto
    {
        /// <summary>
        /// Amounts of product types in the order.
        /// </summary>
        public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
