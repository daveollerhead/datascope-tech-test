using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DatascopeTest.Commands;
using DatascopeTest.Queries;
using MediatR;
using Newtonsoft.Json;

namespace DatascopeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetPagedGamesQuery query)
        {
            var games = await _mediator.Send(query);

            var pagination = new
            {
                hasNextPage = games.HasNextPage,
                hasPreviousPage = games.HasPreviousPage,
                totalPages = games.TotalPages,
                totalCount = games.TotalCount,
                page = query.Page,
                pageSize = query.PageSize
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return Ok(games);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetGameQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand data)
        {
            var game = await _mediator.Send(data);
            return Created(game.Id.ToString(), game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGameCommand data)
        {
            data.Id = id;
            await _mediator.Send(data);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new DeleteGameCommand(id));
            return NoContent();
        }
    }
}
