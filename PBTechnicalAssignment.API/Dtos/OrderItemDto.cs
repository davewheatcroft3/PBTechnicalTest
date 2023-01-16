namespace PBTechnicalAssignment.API.Dtos
{
    /// <summary>
    /// An amount of a given product type.
    /// </summary>
    public class OrderItemDto
    {
        /// <summary>
        /// One of: photoBook, calendar, canvas, cards, mug
        /// </summary>
        public string ProductType { get; set; } = null!;

        /// <summary>
        /// Amount of the product to order.
        /// </summary>
        public int Quantity { get; set; }
    }
}
