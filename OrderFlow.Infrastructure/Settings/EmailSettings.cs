namespace OrderFlow.Infrastructure.Settings;

public class EmailSettings(
    string smtpServer,
    int port,
    string password,
    string from
)
{
    public string SmtpServer { get; set; } = smtpServer;

    public int Port { get; set; } = port;

    public string Password { get; set; } = password;

    public string From { get; set; } = from;
}