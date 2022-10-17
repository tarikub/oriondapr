using Dapr.Client;
using Libraries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Webserver.Controller;

[ApiController]
[Route("[controller]")]
public class HelpController : ControllerBase
{
    private readonly DaprClient _daprClient;
    public HelpController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }
    [HttpPost("UserHelp")]
    public async Task<bool> UserHelp([FromBody] TwilioResponse twilioResponse)
    {
        await _daprClient.InvokeMethodAsync<TwilioResponse>("help", $"help/{nameof(UserHelp)}", twilioResponse);
        return await Task.FromResult(true);
    }
}