using System;
using System.Threading.Tasks;
using SwissKnife.API.Data.Entities;
using SwissKnife.API.Persistence;

namespace SwissKnife.API.Commands
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
            var product = new Product {Id = Guid.NewGuid().ToString(), Name = name};
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}