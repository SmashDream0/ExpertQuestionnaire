using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExpertQuestionnaire.Logic
{
    public class EmailSendLogic
    {
        public EmailSendLogic(string smtpUrl, int smtpPort, bool useSSL, string emailLogin, string emailPassword)
        {
            _smtpUrl = smtpUrl;
            _smtpPort = smtpPort;
            _useSSL = useSSL;
            _emailLogin = emailLogin;
            _emailPassword = emailPassword;
        }

        private readonly string _smtpUrl;
        private readonly int _smtpPort;
        private readonly string _emailLogin;
        private readonly string _emailPassword;
        private readonly bool _useSSL;

        public void Send(string fileName, string email)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            { Send(fs, Path.GetFileName(fileName), email); }
        }
        public void Send(Stream stream, string name, string email)
        {
            try
            {
                var m = new MailMessage(_emailLogin, email)
                {
                    Subject = "Автоматическая отправка",
                    Body = "К письму приложен файл",
                    IsBodyHtml = true
                };

                var attachment = new Attachment(stream, name);
                m.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient(_smtpUrl, _smtpPort);

                smtp.Credentials = new NetworkCredential(_emailLogin, _emailPassword);
                smtp.EnableSsl = _useSSL;
                smtp.Send(m);

                MessageBox.Show("Отправка прошла успешно");
            }
            catch (Exception ex)
            {
                const string errorFileName = "errorFile.txt";

                var errorText = GetErrorMesages(ex);

                using (var sw = new StreamWriter(".\\" + errorFileName))
                { sw.Write(errorText); }

                MessageBox.Show($"При формировании либо отправке письма возникла ошибка(ки):{Environment.NewLine}{ex.Message}{Environment.NewLine}Подробности смотрите в файле\"{errorFileName}\"");
            }
        }

        private static string GetErrorMesages(Exception ex)
        {
            var sb = new StringBuilder();

            var number = 0;

            while (ex != null)
            {
                sb.AppendLine($"{++number}) {ex.Message};");
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}