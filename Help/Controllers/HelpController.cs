using Libraries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Help.Services;

[ApiController]
[Route("[controller]")]
public class HelpController : ControllerBase
{
    private readonly IHelpService _helpService;
    public HelpController(IHelpService helpService)
    {
        _helpService = helpService;
    }

    [HttpPost("UserHelp")]
    public async Task<bool> UserHelp([FromBody] TwilioResponse twilioResponse)
    {
        return await _helpService.UserHelp(twilioResponse);
    }
}