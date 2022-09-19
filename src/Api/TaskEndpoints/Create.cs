using ApplicationCore;
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
    public class Create : EndpointBaseAsync
        .WithRequest<CreateTaskCommand>
        .WithActionResult<TaskResult>
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
        [HttpPost("/api/tasks")]
        [SwaggerOperation(
            Summary = "Creates a new Task",
            Description = "Creates a new Task",
            OperationId = "Task.Create",
            Tags = new[] { "TaskEndpoint" })
        ]
        public override async Task<ActionResult<TaskResult>> HandleAsync(
            CreateTaskCommand request,
            CancellationToken cancellationToken = default)
        {
            var claimUserId = User.FindFirst("userId").Value;

            int userId;
            int.TryParse(claimUserId, out userId);

            var task = new TaskItem();
            _mapper.Map(request, task);
            task.OwnerId = userId;

            await _dbContext.Tasks.AddAsync(task, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<CreateTaskResult>(task);

            return Created("", result);
        }
    }
}