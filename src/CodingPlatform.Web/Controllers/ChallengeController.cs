using CodingPlatform.Domain.Commands;
using CodingPlatform.Domain.Queries;
using CodingPlatform.Web.DTO.Challenges;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingPlatform.Web.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ChallengeController : CustomBaseController
{
    private readonly IMediator _mediator;

    public ChallengeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("challenge/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetChallengeById(id);
        var challenge = await _mediator.Send(query);
        return Ok(challenge);
    }

    [HttpPost("challenge")]
    public async Task<IActionResult> Post(CreateChallengeDto dto)
    {
        var command = new CreateChallengeCmd(Guid.NewGuid(), GetCurrentUserId(), dto.Title, dto.Description, dto.DurationInHours, dto.Tips);
        await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id.ToString() });
    }
}