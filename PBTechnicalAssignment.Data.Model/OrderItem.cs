namespace PBTechnicalAssignment.Data.Model
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
    }
}