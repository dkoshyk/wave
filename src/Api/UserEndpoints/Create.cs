using ApplicationCore;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Api.UserEndpoints
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateUserCommand>
        .WithActionResult<UserResult>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public Create(
            AppDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("/api/users")]
        [SwaggerOperation(
            Summary = "Creates a new User",
            Description = "Creates a new User",
            OperationId = "User.Create",
            Tags = new[] { "UserEndpoint" })
        ]
        public override async Task<ActionResult<UserResult>> HandleAsync(
            CreateUserCommand request,
            CancellationToken cancellationToken = default)
        {
            var user = new User();
            _mapper.Map(request, user);

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<CreateUserResult>(user);

            return Ok(result);
        }
    }
}