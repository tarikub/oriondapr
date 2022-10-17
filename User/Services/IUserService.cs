using Libraries.Models;

namespace Media.Services;

public interface IUserService
{
    Task<Libraries.Models.User> Get(string userId);
    Task<bool> Register(TwilioResponse twilioResponse);
    Task<bool> Save(Libraries.Models.User user);
    Task<bool> UserVerify(TwilioResponse twilioResponse);
}