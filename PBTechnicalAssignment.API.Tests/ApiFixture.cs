using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PBTechnicalAssignment.API.Dtos;
using PBTechnicalAssignment.Data.Access.Context;

namespace PBTechnicalAssignment.API.Tests
{
    public class ApiFixture : WebApplicationFactory<CreateOrderDto>
    {
        private string _testName;
        private Guid _createdTestOrderId = Guid.NewGuid();

        public Guid CreatedTestOrderId => _createdTestOrderId;

        public ApiFixture(string testName)
        {
            _testName = testName;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Overwrite default db DI with in memory for tests
                // In our case for this tech test its in memory anyway - but
                // we need to ensure our tests run in seperate contexts via naming
                // uniquely per test
                services.AddScoped<ApplicationDbContext>(sp => CreateDbContext());
            });

            return base.CreateHost(builder);
        }

        private void AddTestOrder(ApplicationDbContext dbContext)
        {
            var order = new Data.Model.Order()
            {
                Id = _createdTestOrderId
            };
            dbContext.Orders.Add(order);

            var orderItem1 = new Data.Model.OrderItem()
            {
                Order = order,
                ProductType = Data.Model.ProductType.PHOTOBOOK,
                Quantity = 1
            };

            var orderItem2 = new Data.Model.OrderItem()
            {
                Order = order,
                ProductType = Data.Model.ProductType.CANVAS,
                Quantity = 2
            };

            dbContext.OrderItems.Add(orderItem1);
            dbContext.OrderItems.Add(orderItem2);

            dbContext.SaveChanges();
        }

        private ApplicationDbContext CreateDbContext()
        {
            // Because in memory and tests parallelizable, need to ensure dbs unique accross tests
            // so use passed in test name
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"IntegrationTests_{_testName}")
                .Options;

            var context = new ApplicationDbContext(contextOptions);

            if (context.Database.EnsureCreated())
            {
                // Preload with a test order for use in test(s)
                AddTestOrder(context);
            }

            return context;
        }
    }
}
