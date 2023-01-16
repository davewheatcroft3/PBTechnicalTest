using System.ComponentModel.DataAnnotations.Schema;

namespace PBTechnicalAssignment.Data.Model
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}