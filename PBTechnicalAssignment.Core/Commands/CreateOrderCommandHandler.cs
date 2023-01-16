using FluentResults;
using MediatR;
using PBTechnicalAssignment.Data.Access.Context;
using PBTechnicalAssignment.Data.Model;

namespace PBTechnicalAssignment.Core.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateOrderCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order();
            _dbContext.Orders.Add(order);

            foreach (var item in request.OrderItems)
            {
                _dbContext.OrderItems.Add(new OrderItem()
                {
                    Order = order,
                    ProductType = item.ProductType,
                    Quantity = item.Quantity
                });
            }

            try
            {
                await _dbContext.SaveChangesAsync();

                return Result.Ok(order.Id);
            }
            catch(Exception ex)
            {
                return Result.Fail(new ExceptionalError(ex));
            }
        }
    }
}
