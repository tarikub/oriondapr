using Dapr.Client;
using Libraries.Interfaces;
using Libraries.Models;
using System.Text.Json;

namespace Media.Services;

public class SlideShowService
{
    const string FEED_DELIMITER = " ";
    private const string STATE_STORAGE_NAME = "orionstate";
    private readonly ILogger<SlideShowController> _logger;
    private readonly IStateManager<SlideShow> _slideShowStateManager;
    private readonly IStateManager<User> _userStateManager;

    public SlideShowService(ILogger<SlideShowController> logger, 
        IStateManager<SlideShow> slideShowStateManager,
        IStateManager<User> userStateManager,
        DaprClient daprClient)
    {
        _logger = logger;
        _slideShowStateManager = slideShowStateManager;
        _userStateManager = userStateManager;
    }

    public async Task<SlideShow> SlideShowFeed(Dictionary<string, string> input)
    {
        var phoneNumber = input.FirstOrDefault().Key;
        var pin = input.FirstOrDefault().Value;

        if (!String.IsNullOrEmpty(phoneNumber))
        {
            var slideShow = await _slideShowStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, $"{phoneNumber}{pin}");
            _logger.Log(LogLevel.Information, $"Feed requested for {phoneNumber} :: {pin}");
            return slideShow;
        }


        return new SlideShow();
    }

    public async Task<bool> UploadMedia(TwilioResponse twilioResponse)
    {
        var user = await _userStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, twilioResponse.From);


        if (user != null) // Verify user
        {
            var slideShow = await _slideShowStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}");

            if (slideShow != null && !string.IsNullOrEmpty(twilioResponse.Medias))
            {
                slideShow.Medias += $"{twilioResponse.Medias}{FEED_DELIMITER}";
                var strSlideShow = JsonSerializer.Serialize(slideShow);
                await _slideShowStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}", strSlideShow);
                _logger.Log(LogLevel.Information, $"Media updated for user {twilioResponse.From}");
                TwilioUtil.Notify(user.PhoneNumber, TwilioMessages.PhotoIsAddedToSlideShow, _logger);
            }
        }

        return true;
    }

    public async Task<bool> Reset(TwilioResponse twilioResponse)
    {
        var user = await _userStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, twilioResponse.From);

        if (user != null) // Verify user
        {
            var slideShow = await _slideShowStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}");

            if (slideShow != null)
            {

                slideShow.Medias = "";
                var strSlideShow = JsonSerializer.Serialize(slideShow);

                await _slideShowStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}", strSlideShow);
                _logger.Log(LogLevel.Information, $"Media resetted for user {twilioResponse.From}");
            }

            TwilioUtil.Notify(user.PhoneNumber, TwilioMessages.ResettedAccount, _logger);
        }
        
        return true;
    }

    public async Task<bool> Delete(TwilioResponse twilioResponse)
    {
        var user = await _userStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, twilioResponse.From);

        if (user != null) // Verify user
        {
            var slideShow = await _slideShowStateManager.GetDataFromStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}");

            if (slideShow != null && !string.IsNullOrEmpty(slideShow.Medias))
                {
                var images = slideShow.Medias.Split(FEED_DELIMITER).Where(media => !string.IsNullOrWhiteSpace(media));
                var checkidxToDelete = twilioResponse.Body.Split(FEED_DELIMITER).Where(userText => int.TryParse(userText, out var indexToDelete)).FirstOrDefault();

                if (int.TryParse(checkidxToDelete, out int numToDelete) && numToDelete > 0 && numToDelete <= images.Count())
                {
                    var idxToDelete = --numToDelete;
                    var updatedList = new List<string>(images);
                    var removedMsgURL = updatedList[idxToDelete];
                    updatedList.RemoveAt(idxToDelete);
                    slideShow.Medias = String.Join(FEED_DELIMITER, updatedList);

                    var strSlideShow = JsonSerializer.Serialize(slideShow);
                    await _slideShowStateManager.SaveDataToStateStore(STATE_STORAGE_NAME, $"{twilioResponse.From}{user.PIN}", strSlideShow);
                    

                    _logger.Log(LogLevel.Information, $"Media deleted for user {twilioResponse.From}");
                    TwilioUtil.Notify(user.PhoneNumber, TwilioMessages.PhotoDeleted, _logger);
                }
            }

            TwilioUtil.Notify(user.PhoneNumber, TwilioMessages.ResettedAccount, _logger);
        }

        return true;
    }
}