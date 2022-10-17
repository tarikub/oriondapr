using Libraries.Models;

namespace Media.Services;

public interface ISlideShowService
{
    Task<bool> Delete(TwilioResponse twilioResponse);
    Task<bool> Reset(TwilioResponse twilioResponse);
    Task<SlideShow> SlideShowFeed(string phoneNumber, string pin);
    Task<bool> UploadMedia(TwilioResponse twilioResponse);
}