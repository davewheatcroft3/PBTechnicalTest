using FluentResults;
using MediatR;
using PBTechnicalAssignment.Core.Model;

namespace PBTechnicalAssignment.Core.Commands
{
    public class GetOrderQuery : IRequest<Result<Order>>
    {
        public string Id { get; init; }

        public GetOrderQuery(string id)
        {
            Id = id;
        }
    }
}
