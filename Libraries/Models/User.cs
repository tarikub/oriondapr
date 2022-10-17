namespace Libraries.Models;

public class User : BaseData
{
    public string Id { set; get; } = string.Empty;
    public UserStatus Status { set; get; } = UserStatus.Active;

    public string PhoneNumber { get; set; } = string.Empty;

    public string PIN { get; set; } = string.Empty;

    public string RokuUID { get; set; } = string.Empty;

    public User()
    {
    }
}
