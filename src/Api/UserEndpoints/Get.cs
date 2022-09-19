﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Api.UserEndpoints
{
    public class Get : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<UserResult>
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
        [HttpGet("/api/users/{id}")]
        [SwaggerOperation(
            Summary = "Get User",
            Description = "Get User",
            OperationId = "User.Get",
            Tags = new[] { "UserEndpoint" })
        ]
        public override async Task<ActionResult<UserResult>> HandleAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var task = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var result = _mapper.Map<UserResult>(task);

            return Ok(result);
        }
    }
}