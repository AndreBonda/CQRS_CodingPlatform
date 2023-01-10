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
        var challenge = await _mediator.Send(
            new GetChallengeById(id)
        );

        return Ok(challenge);
    }

    [HttpGet("challenge-admin/{id}")]
    public async Task<IActionResult> GetByAdmin(Guid id)
    {
        var challenge = await _mediator.Send(
            new GetChallengeByAdmin(id, GetCurrentUserId())
        );

        return Ok(challenge);
    }

    [HttpPost("challenge")]
    public async Task<IActionResult> Post(CreateChallengeDto body)
    {
        var command = new CreateChallengeCmd(Guid.NewGuid(), GetCurrentUserId(), body.Title, body.Description, body.EndDate, body.Tips);
        await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }

    [HttpPatch("challenge/{id}")]
    public async Task<IActionResult> Patch(Guid id, UpdateChallengeDto body)
    {
        await _mediator.Send(
            new UpdateChallengeCmd(id, GetCurrentUserId(), body.Title, body.Description, body.EndDate, body.Tips)
        );

        return Ok();
    }
}