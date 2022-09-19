﻿using ApplicationCore;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.TaskEndpoints
{
    public class List : EndpointBaseAsync
        .WithRequest<TaskListRequest>
        .WithActionResult<IList<TaskListResult>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public List(
            AppDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("/api/tasks")]
        [SwaggerOperation(
            Summary = "List all Tasks",
            Description = "List all Tasks",
            OperationId = "Task.List",
            Tags = new[] { "TaskEndpoint" })
        ]
        public override async Task<ActionResult<IList<TaskListResult>>> HandleAsync(
            [FromQuery] TaskListRequest request,
            CancellationToken cancellationToken = default)
        {
            var claimUserId = User.FindFirst("userId").Value;

            int userId;
            int.TryParse(claimUserId, out userId);

            if (request.PerPage == 0)
            {
                request.PerPage = 10;
            }
            if (request.Page == 0)
            {
                request.Page = 1;
            }

            IQueryable<TaskItem> query = _dbContext.Tasks;

            if (userId > 0)
            {
                query = query.Where(x => x.OwnerId.Equals(userId));
            }

            if (!string.IsNullOrEmpty(request.ContainsTitle))
            {
                query = query.Where(x => x.Title.Contains(request.ContainsTitle));
            }

            if (!string.IsNullOrEmpty(request.EqualType))
            {
                query = query.Where(x => x.Type.Equals(request.EqualType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.EqualStatus))
            {
                query = query.Where(x => x.Type.Equals(request.EqualStatus, StringComparison.OrdinalIgnoreCase));
            }

            if (request.EqualPriority.HasValue)
            {
                query = query.Where(x => x.Priority.Equals(request.EqualPriority));
            }

            if (request.FromDeadlineOn.HasValue)
            {
                query = query.Where(x => x.DeadlineOn >= request.FromDeadlineOn);
            }

            if (request.ToDeadlineOn.HasValue)
            {
                query = query.Where(x => x.DeadlineOn <= request.ToDeadlineOn);
            }

            var count = query.Count();

            var items = (await query
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ToListAsync(cancellationToken))
                .Select(x => _mapper.Map<TaskItemDto>(x));

            var result = new TaskListResult();
            result.Items.AddRange(items);
            result.Count = count;

            return Ok(result);
        }
    }
}
