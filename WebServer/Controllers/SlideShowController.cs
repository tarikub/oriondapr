using Dapr.Client;
using Libraries.Models;
using Media.Services;
using Microsoft.AspNetCore.Mvc;

namespace Webserver.Controller;

[ApiController]
[Route("api/[controller]")]
public class SlideShowController : ControllerBase
{
    private readonly DaprClient _daprClient;
    public SlideShowController(ISlideShowService slideShowService, DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpGet("SlideShowFeed")]
    public async Task<SlideShow> SlideShowFeed([FromQuery] string phoneNumber, [FromQuery] string pin)
    {
        var input = new Dictionary<string, string>();
        input.Add(phoneNumber, pin);
        return await _daprClient.InvokeMethodAsync<Dictionary<string, string>, SlideShow>("media", $"slideshow/{ nameof(SlideShowFeed)}", input);
    }

    [HttpPost("UploadMedia")]
    public async Task<bool> UploadMedia([FromBody] TwilioResponse twilioResponse)
    {
        await _daprClient.InvokeMethodAsync<TwilioResponse>("media", $"slideshow/{nameof(UploadMedia)}", twilioResponse);
        return await Task.FromResult(true);
    }

    [HttpPost("Reset")]
    public async Task<bool> Reset([FromBody] TwilioResponse twilioResponse)
    {
        await _daprClient.InvokeMethodAsync<TwilioResponse>("media", $"slideshow/{nameof(Reset)}", twilioResponse);
        return await Task.FromResult(true);
    }

    [HttpPost("Delete")]
    public async Task<bool> Delete([FromBody] TwilioResponse twilioResponse)
    {
        await _daprClient.InvokeMethodAsync<TwilioResponse>("media", $"slideshow/{nameof(Delete)}", twilioResponse);
        return await Task.FromResult(true);
    }
}