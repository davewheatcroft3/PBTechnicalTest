using FluentResults;
using MediatR;
using PBTechnicalAssignment.Core.Model;

namespace PBTechnicalAssignment.Core.Commands
{
    public class CreateOrderCommand : IRequest<Result<Guid>>
    {
        public IEnumerable<OrderItem> OrderItems { get; init; }

        public CreateOrderCommand(IEnumerable<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }
    }
}
