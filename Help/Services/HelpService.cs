using Dapr.Client;
using Libraries.Interfaces;
using Libraries.Models;

namespace Help.Services;

public class HelpService : IHelpService
{
    const string FEED_DELIMITER = " ";
    private const string STATE_STORAGE_NAME = "orionstate";
    private readonly ILogger<HelpController> _logger;

    public HelpService(ILogger<HelpController> logger,
        IStateManager<SlideShow> slideShowStateManager,
        IStateManager<User> userStateManager,
        DaprClient daprClient)
    {
        _logger = logger;
    }

    public async Task<bool> UserHelp(TwilioResponse twilioResponse)
    {
        if (!String.IsNullOrEmpty(twilioResponse.From))
        {
            TwilioUtil.Notify(twilioResponse.From, TwilioMessages.MoreText, _logger);
        }


        return await Task.FromResult(true);
    }
}