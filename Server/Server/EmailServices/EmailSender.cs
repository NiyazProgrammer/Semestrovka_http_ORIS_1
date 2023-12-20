using System.Net;
using System.Net.Mail;

namespace Server;

public class EmailSender : IEmailSender
{
    private static AppSettings _config = ConfigManager.GetConfig();
    public void SendEmail(string email, string password)
    {
        // отправитель - устанавливаем адрес и отображаемое в письме имя
        MailAddress from = new MailAddress($"master.niyaz@yandex.ru", $"{_config.FromName}");
        // кому отправляем
        MailAddress to = new MailAddress(email);
        // создаем объект сообщения
        MailMessage m = new MailMessage(from, to);
        // тема письма
        m.Subject = "Успешный вход в Fandom Watch Dogs 2";
        // текст письма
        m.Body = $"<h2>Здравствуйте!</h2><br><p>Ваш логин: {email}</p><p>Ваш пароль: {password}</p>";
        // письмо представляет код
        m.IsBodyHtml = true;
        // адрес smtp-сервера и порт, с которого будем отправлять письмо
        SmtpClient smtp = new SmtpClient($"smtp.yandex.ru", 25);
        // логин и пароль
        smtp.Credentials = new NetworkCredential($"master.niyaz@yandex.ru", $"Niyaz_-04");
        smtp.EnableSsl = true;
        smtp.Send(m);
    }
}