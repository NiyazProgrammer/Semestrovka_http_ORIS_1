﻿using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
             // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("master.niyaz@yandex.ru", "Niyaz");
            // кому отправляем
            MailAddress to = new MailAddress("niazr90@mail.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("master.niyaz@yandex.ru", "Niyaz_-04");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
        }
    }
}