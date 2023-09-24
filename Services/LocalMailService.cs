namespace CityInfoAPI.Services;

public class LocalMailService : IMailService
{
    private string _mailTo = "zoeyee@d8e.com";
    private string _mailFrom = "frank@d8e.com";

    public void Send(string subject, string message)
    {
        Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " + 
                          $"with {nameof(LocalMailService)}");
        Console.WriteLine($"subject: {subject}");
        Console.WriteLine($"message: {message}");
    }
}