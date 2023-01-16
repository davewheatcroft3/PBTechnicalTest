using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PBTechnicalAssignment.Core.Errors;
using PBTechnicalAssignment.Core.Model;
using PBTechnicalAssignment.Data.Access.Context;

namespace PBTechnicalAssignment.Core.Commands
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<Order>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOrderQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(request.Id, out Guid parsedId))
            {
                var order = await _dbContext.Orders
                  .AsNoTracking()
                  .Include(x => x.OrderItems)
                  .FirstOrDefaultAsync(x => x.Id == parsedId);

                if (order == null)
                {
                    return Result.Fail(new EntityNotFoundError());
                }

                return Result.Ok(new Order()
                {
                    Id = order.Id,
                    Items = order.OrderItems.Select(x => new OrderItem()
                    {
                        ProductType = x.ProductType,
                        Quantity = x.Quantity
                    }).ToList()
                });
            }
            else
            {
                // TODO: log this or different result, for now report back as cant find since
                // calling user shouldnt decipher between invalid id from parsing or not in db
                return Result.Fail(new EntityNotFoundError());
            }
        }
    }
}
