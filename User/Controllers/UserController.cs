using Libraries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Media.Services;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("GetUser")]
        public async Task<Libraries.Models.User> Get(string userId)
    {
            return await _userService.Get(userId);
    }

    [HttpPost("SaveUser")]
    public async Task<bool> Save(Libraries.Models.User user)
    {
        return await _userService.Save(user);
    }

    [HttpPost("UserVerify")]
    public async Task<bool> UserVerify(TwilioResponse twilioResponse)
    {
        return await _userService.UserVerify(twilioResponse);
    }

    [HttpPost("Register")]
    public async Task<bool> Register(TwilioResponse twilioResponse)
    {
        return await _userService.Register(twilioResponse);
    }
}