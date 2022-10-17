namespace Libraries.Models;

public class SlideShow 
{

    public UserStatus Status { set; get; } = UserStatus.Active;

    public string PIN { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty; 

    public string Medias { get; set; } = string.Empty; 

    public SlideShow()
    {
    }
}
