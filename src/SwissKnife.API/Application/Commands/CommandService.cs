using System;
using System.Threading.Tasks;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using SwissKnife.Infrastructure;

namespace SwissKnife.API.Application.Commands
{
    public class CommandService : ICommandService
    {
        private readonly AppDbContext _context;

        public CommandService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task SaveProduct(string name)
        {
            var product = new Product { Name = name};              //Id = Guid.NewGuid().ToString(),
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}