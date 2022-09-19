using Ardalis.ApiEndpoints;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Api.TaskEndpoints
{
    public class Get : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<TaskResult>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public Get(
            AppDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("/api/tasks/{id}")]
        [SwaggerOperation(
            Summary = "Get Task",
            Description = "Get Task",
            OperationId = "Task.Get",
            Tags = new[] { "TaskEndpoint" })
        ]
        public override async Task<ActionResult<TaskResult>> HandleAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var result = _mapper.Map<TaskResult>(task);

            return Ok(result);
        }
    }
}