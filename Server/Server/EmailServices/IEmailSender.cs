namespace Server;

public interface IEmailSender
{
    void SendEmail(string emailFromUser, string passwordFromUser);
}