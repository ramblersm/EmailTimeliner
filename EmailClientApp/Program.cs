using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;


namespace EmailClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exchange Web Services Codeblock

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            
       
            Console.WriteLine("Enter your email address");
            string uname = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string pwd = Console.ReadLine();
            Console.WriteLine("Enter the email address of recipient");
            string recp = Console.ReadLine();
            Console.WriteLine("Enter your message");
            string mailbody = Console.ReadLine();

            service.Credentials = new WebCredentials(uname,pwd);
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;
            service.AutodiscoverUrl(uname, RedirectionUrlValidationCallback);
            EmailMessage email = new EmailMessage(service);
            email.ToRecipients.Add(recp);
            email.Subject = "EmailTimeliner being developed!";
            email.Body = new MessageBody(mailbody);
            email.SendAndSaveCopy();

        }
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

    }
}
