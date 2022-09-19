using Ardalis.ApiEndpoints;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Api.TaskEndpoints
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateTaskCommand>
        .WithActionResult<UpdatedTaskResult>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public Update(
            AppDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut("/api/tasks")]
        [SwaggerOperation(
            Summary = "Updates an existing Task",
            Description = "Updates an existing Task",
            OperationId = "Task.Create",
            Tags = new[] { "TaskEndpoint" })
        ]
        public override async Task<ActionResult<UpdatedTaskResult>> HandleAsync(
            UpdateTaskCommand request,
            CancellationToken cancellationToken = default)
        {
            var task = await _dbContext.Tasks.FindAsync(request.Id);
            _mapper.Map(request, task);

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<UpdatedTaskResult>(task);

            return Ok(result);
        }
    }
}