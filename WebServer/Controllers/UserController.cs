using Dapr.Client;
using Libraries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Webserver.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DaprClient _daprClient;
    public UserController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpPost("GetUser")]
    public async Task<Libraries.Models.User> Get([FromQuery] string userId)
    {
        return await _daprClient.InvokeMethodAsync<string, Libraries.Models.User>("user", $"user/{nameof(Get)}", userId);
    }

    [HttpPost("SaveUser")]
    public async Task<bool> Save([FromBody] User user)
    {
        return await _daprClient.InvokeMethodAsync<Libraries.Models.User, bool>("user", $"user/{nameof(Save)}", user);

    }

    [HttpPost("UserVerify")]
    public async Task<bool> UserVerify([FromBody] TwilioResponse twilioResponse)
    {
        return await _daprClient.InvokeMethodAsync<TwilioResponse, bool>("user", $"user/{nameof(UserVerify)}", twilioResponse);
    }

    [HttpPost("Register")]
    public async Task<bool> Register([FromBody] TwilioResponse twilioResponse)
    {
        return await _daprClient.InvokeMethodAsync<TwilioResponse, bool>("user", $"user/{nameof(Register)}", twilioResponse);
    }
}