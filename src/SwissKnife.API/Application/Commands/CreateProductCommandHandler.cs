using MediatR;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SwissKnife.API.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand message, CancellationToken cancellationToken)
        {
            var product = new Product() { Name = message.Name, Description = message.Description };
            _productRepository.Add(product);

            return await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
