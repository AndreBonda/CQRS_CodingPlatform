using CodingPlatform.Domain.Commands;
using CodingPlatform.Domain.Queries;
using CodingPlatform.Web.DTO.Challenges;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingPlatform.Web.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public UserController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost("user/sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto body)
    {
        await _mediator.Send(
            new CreateUserCmd(body.Email, body.Username, body.Password)
        );

        return Ok("User created");
    }

    [HttpPost("user/sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto body)
    {
        var jwt = await _mediator.Send(
            new GetJWT(body.Email, body.Password, _configuration.GetSection("JWT:Key").Value)
        );

        return Ok(jwt);
    }
}