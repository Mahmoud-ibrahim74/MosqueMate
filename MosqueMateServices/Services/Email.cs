using System.Text.RegularExpressions;

namespace MosqueMateServices.Services
{
    public class EMail
    {
     
        //public static void Send(string subject,string body, string toEmail, string contactName)
        //{
        //    string host = "smtp.gmail.com";
        //    int port = 587;
        //    var siteMail = "mosquemates@gmail.com";
        //    string password = "vmcz vrbg zdkd jvti";

        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Mosque mate Support", siteMail));
        //    message.To.Add(new MailboxAddress(contactName, toEmail));
        //    message.Subject = subject;
        //    #region build template
        //    StringBuilder EmailBody = new StringBuilder();
        //    EmailBody.Append(
        //        "<!DOCTYPE html>" +
        //            "<html lang='en'>" +
        //                "<head>" +
        //                    "<meta charset='UTF-8'>" +
        //                    "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
        //                    "<title>Procoor Mate</title>" +
        //                    "<link rel='preconnect' href='https://fonts.googleapis.com'>" +
        //                    "<link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>" +
        //                    "<link href='https://fonts.googleapis.com/css2?family=Mulish:ital,wght@0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;0,1000;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900;1,1000&display=swap' rel='stylesheet'>" +
        //                    "<style>" +
        //                        "body {margin: 0; padding: 0; min-width: 100%; width: 100% !important; height: 100% !important; background: #f7f9fa; font-family: 'Mulish', sans-serif;} " +
        //                        "    a {color: #294CFF;text-decoration: none;}" +
        //                        "td {display: block; margin-bottom: 8px;}    " +
        //                        "span {text-align: center !important;}     " +
        //                    "</style>" +
        //                "</head>" +
        //                "<body bgcolor='#f9f9f9' topmargin='0' rightmargin='0' bottommargin='0' leftmargin='0' marginwidth='0' marginheight='0' width='100%'>" +
        //                    "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed;background-color:#f7f9fa;' bgcolor='#f7f9fa' cellpadding='0' cellspacing='0'>" +
        //                        "<tbody cellpadding='' cellspacing='0'>" +
        //                         "<tr>" +
        //                            "<td style='padding-right:10px;padding-left:10px;' align='center' valign='top'>" +
        //                                "<table border='0' cellpadding='0' cellspacing='0' width=100% class='wrapperBody' style='max-width:700px ; padding-top: 20px;'>" +
        //                                    " <tbody>" +
        //                                        "<tr>" +
        //                                            "<td align='center' valign='top'>" +
        //                                                "<table border='0' cellpadding='0' cellspacing='0' width=100% class=tableCard style='background-color:#fff;padding: 20px;border-radius: 10px;'>" +
        //                                                    "<tbody>" +
        //                                                        "<tr>" +
        //                                                            "<td style='padding-bottom: 20px;' align='start' valign='middle' class='emailLogo'>" +
        //                                                                "<a href=https://www.Procoor Mate.com/ style='text-decoration:none' target='_blank'>" +
        //                                                                "<img src='https://www.Procoor Mate.com/assets/images/img_Email/logo-bright.png' alt='logo'>" +
        //                                                                "</a>" +
        //                                                            "</td>" +
        //                                                        "</tr>" +
        //                                                        "<tr class='text' style='font-size: 19px;font-weight: 400;padding:20px 10px; border-radius: 10px;;margin-bottom:0px;display: block;color: #525C75; text-transform: none; text-align: start;padding: 0;margin: 0;'>" +
        //                                                            "<td valign='top' style='padding-bottom: 16px;'>" +
        //                                                                "<h4 style='margin: 0;font-size: 19px; font-weight: 900;text-align: start;color: #1c1e26;'>" +
        //                                                                "Hi " + "<span style='font-size: 19px;font-weight: 900;'>" + ' ' + contactName + ',' + "</span>" +
        //                                                                "</h4>" +
        //                                                            "</td>" +
        //                                                            "<td valign='top' style='padding-bottom: 8px;'>" +
        //                                                                "<h4 style='margin: 0;font-size:20px; font-weight: 700;text-align: start;'>Welcome to Mosque Mate." + "</h4>" +
        //                                                            "</td>" +
        //                                                            "<td valign='top' style='padding-bottom:16px;'>"+body+"</td>" +
        //                                                            "<td valign='top' style='margin: auto;margin-bottom:16px;text-align: center;display: flex'>" +
        //                                                                "<a href='https://www.Procoor Mate.com/' " +
        //                                                                    "style='margin: 0;font-size: 18px; font-weight: 700;text-align: start;color: #fff;background-color: #294CFF;text-decoration: none;padding: 10px 18px;height: 100%;display: block;width:auto;margin: auto;border-radius: 10px;'>" +
        //                                                                     "Visit My Website" +
        //                                                                "</a>" +
        //                                                            "</td>" +
        //                                                        "</tr>" +
        //                                                        "<tr>" +
        //                                                            "<td align='start' valign='middle' class='social' style='display: flex;width: 100%;flex-wrap: wrap;align-items: center; justify-content: center;'>" +
        //                                                                "<a href='https://www.Procoor Mate.com/assets/images/img_Email/fcbk.png' style='text-decoration:none;width: 40px;' target='_blank'>" +
        //                                                                    "<img src='fcbk.svg' alt='' style='width: 100%;'>" +
        //                                                                "</a>" +
        //                                                                "<a href='https://www.Procoor Mate.com/assets/images/img_Email/lkd-in.png' style='text-decoration:none;width: 40px' target='_blank'>" +
        //                                                                    "<img src='lkd-in.svg' alt='' style='width: 100%;'>" +
        //                                                                "</a>" +
        //                                                                "<a href='https://www.Procoor Mate.com/assets/images/img_Email/twtr.png' style='text-decoration:none;width: 40px' target='_blank'>" +
        //                                                                    "<img src='twtr.svg' alt='' style='width: 100%;'>" +
        //                                                                "</a>" +
        //                                                            "</td>" +
        //                                                        "</tr>" +
        //                                                    "</tbody>" +
        //                                                "</table>" +
        //                                            "</td>" +
        //                                        "</tr>" +
        //                                    "</tbody>" +
        //                                "</table>" +
        //                            "</td>" +
        //                           "</tr>" +
        //                        "</tbody>" +
        //                    "</table>" +
        //                "</body>" +
        //        "</html>");

        //    #endregion


        //    message.Body = new TextPart("html")
        //    {
        //        Text = EmailBody.ToString()
        //    };

        //    try
        //    {
        //        using (var client = new MailKit.Net.Smtp.SmtpClient())
        //        {
        //            client.Connect(host, port, SecureSocketOptions.StartTls);
        //            client.Authenticate(siteMail, password);
        //             client.Send(message);
        //            client.Disconnect(true);
        //            MessageBox.Show("Email Sent Succesfully","Done",MessageBoxButtons.OK, MessageBoxIcon.Information);    
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("************************************ Send Email" + ex.Message + "*********************************");
        //    }
        //}
       public static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for a simple email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Use Regex.IsMatch to check if the email string matches the pattern
            return Regex.IsMatch(email, pattern);
        }
    }
}


