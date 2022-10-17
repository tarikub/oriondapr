using Dapr.Client;
using Libraries.Interfaces;
using Libraries.Models;
using System.Text.Json;

namespace Media.Services;

public class UserService : IUserService
{
    private const string STATE_STORAGE_NAME = "orionstate";
    private readonly ILogger<UserController> _logger;
    private readonly IStateManager<Libraries.Models.User> _userStateManager;
    private readonly IStateManager<SlideShow> _slideShowManager;

    public UserService(ILogger<UserController> logger, IStateManager<Libraries.Models.User> userStateManager,
        IStateManager<SlideShow> slideShowManager,
        DaprClient daprClient)
    {
        _logger = logger;
        _slideShowManager = slideShowManager;
        _userStateManager = userStateManager;
    }

    public async Task<Libraries.Models.User> Get(string userId)
    {
        var user = await _userStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, userId);

        return user;
    }

    public async Task<bool> Save(Libraries.Models.User user)
    {
        var strUser = JsonSerializer.Serialize(user);
        await _userStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, user.Id, strUser);

        return true;
    }

    public async Task<bool> UserVerify(TwilioResponse twilioResponse)
    {
        var newPin = Verification.NewPIN();
        var user = await Get(twilioResponse.From);
        var pin = twilioResponse.Body;
        var verified = false;

        if (user != null) // Register user
        {
            if (string.Equals(user.PIN, pin, StringComparison.OrdinalIgnoreCase))
            {
                verified = true;


                // Check if number has been used before
                var slideShow = await _slideShowManager.GetDataFromStateStore(STATE_STORAGE_NAME, $"{user.PhoneNumber}{user.PIN}");

                if (slideShow == null)
                {
                    slideShow = new SlideShow
                    {
                        PIN = user.PIN,
                        Status = UserStatus.Active,
                        Id = $"{user.PhoneNumber}{user.PIN}",
                        Phone = user.PhoneNumber
                    };
                    var strSlideshow = JsonSerializer.Serialize(slideShow);
                    await _userStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, user.Id, strSlideshow);
                }

                TwilioUtil.Notify(user.Id, String.Format(TwilioMessages.AccountVerified, user.PIN), _logger);
            }
            else
            {
                TwilioUtil.Notify(user.Id, TwilioMessages.InvalidPin, _logger);
            }
        }
        return verified;
    }

    public async Task<bool> Register(TwilioResponse twilioResponse)
    {
        var newPin = Verification.NewPIN();
        var user = await Get(twilioResponse.From);

        if (user == null) // Register user
        {
            user = new Libraries.Models.User()
            {
                Timestamp = DateTime.UtcNow,
                PIN = newPin,
                PhoneNumber = twilioResponse.From
            };

            await Save(user);
            _logger.Log(LogLevel.Information, $"User {user.PhoneNumber} is added to storage");
        }
        else
        {
            user.PIN = newPin;
            var slideShow = new SlideShow
            {
                PIN = user.PIN,
                Status = UserStatus.Active,
                Phone = user.PhoneNumber
            };
            var strSlideshow = JsonSerializer.Serialize(slideShow);
            await _userStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, user.Id, strSlideshow);
            await Save(user);
        }


        TwilioUtil.Notify(user.Id, String.Format(TwilioMessages.VerifyPhone, newPin), _logger);
        return true;
    }
}