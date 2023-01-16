using AutoMapper;
using MediatR;
using PBTechnicalAssignment.API.Dtos;
using PBTechnicalAssignment.Core.Commands;
using PBTechnicalAssignment.Core.Errors;
using PBTechnicalAssignment.Core.Model;
using PBTechnicalAssignment.Core.Services;
using System.Net;

namespace PBTechnicalAssignment.API.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/orders/{id}", async (
                string id,
                IMediator mediator,
                IMapper mapper,
                BinWidthCalculationService service) =>
                {
                    var result = await mediator.Send(new GetOrderQuery(id));

                    if (result.IsSuccess)
                    {
                        var dto = mapper.Map<OrderDto>(result.Value);
                        dto.MinimumBinWidth = service.CalculateMinimiumBinWidth(result.Value);

                        return Results.Ok(dto);
                    }
                    else if (result.HasError<EntityNotFoundError>())
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        // TODO: log error
                        return Results.BadRequest();
                    }
                })
                .Produces((int)HttpStatusCode.OK, typeof(OrderDto))
                .Produces((int)HttpStatusCode.NotFound)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapPost("/orders", async (CreateOrderDto dto, IMediator mediator, IMapper mapper) =>
            {
                IEnumerable<OrderItem> items;
                try
                {
                    items = dto.Items
                        .Select(x => mapper.Map<OrderItem>(x))
                        .ToList();
                }
                catch (Exception)
                {
                    // TODO: Log mapping exception...
                    return Results.BadRequest("Ensure you pass a valid ProductType: see API specification");
                }

                var command = new CreateOrderCommand(items);

                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Created("/orders", result.Value.ToString());
                }
                else
                {
                    // TODO: log error, return friendly message, etc
                    var error = result.Errors.FirstOrDefault();
                    return Results.BadRequest(error?.Message);
                }
            })
            .Produces((int)HttpStatusCode.Created, typeof(string))
            .Produces((int)HttpStatusCode.BadRequest);
        }
    }
}