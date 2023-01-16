namespace PBTechnicalAssignment.Core.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}