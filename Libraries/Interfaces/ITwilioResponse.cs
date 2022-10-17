using Libraries.Models;

namespace Libraries.Interfaces;

public interface ITwilioResponse
{
    MessageProcessingType ProcessingType();
}
