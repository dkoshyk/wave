using Ardalis.ApiEndpoints;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Api.UserEndpoints
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithoutResult
    {
        private readonly AppDbContext _dbContext;

        public Delete(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpDelete("/api/users/{id}")]
        [SwaggerOperation(
            Summary = "Deletes a User",
            Description = "Deletes a User",
            OperationId = "USer.Delete",
            Tags = new[] { "UserEndpoint" })
        ]
        public override async Task<ActionResult> HandleAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound(id);
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}