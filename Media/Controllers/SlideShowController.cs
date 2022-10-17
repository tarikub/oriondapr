using Libraries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Media.Services;

[ApiController]
[Route("[controller]")]
public class SlideShowController : ControllerBase
{
    private readonly ISlideShowService _slideShowService;
    public SlideShowController(ISlideShowService slideShowService)
    {
        _slideShowService = slideShowService;
    }

    [HttpGet("SlideShowFeed")]
    public async Task<SlideShow> SlideShowFeed([FromQuery] string phoneNumber, [FromQuery] string pin)
    {
        return await _slideShowService.SlideShowFeed(phoneNumber, pin);
    }

    [HttpPost("UploadMedia")]
    public async Task<bool> UploadMedia(TwilioResponse twilioResponse)
    {
        return await _slideShowService.UploadMedia(twilioResponse);
    }

    [HttpPost("Reset")]
    public async Task<bool> Reset(TwilioResponse twilioResponse)
    {
        return await _slideShowService.Reset(twilioResponse);
    }

    [HttpPost("Delete")]
    public async Task<bool> Delete(TwilioResponse twilioResponse)
    {
        return await _slideShowService.Delete(twilioResponse);
    }
}