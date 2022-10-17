using Libraries.Models;

namespace Help.Services;

public interface IHelpService
{
    Task<bool> UserHelp(TwilioResponse twilioResponse);
}