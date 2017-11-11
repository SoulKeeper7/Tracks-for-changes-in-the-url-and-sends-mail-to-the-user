using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace URLTRACKER
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public static string lastmod = null;
        [WebMethod]
        public String  CheckModificationTime()
        {
            while (true)
            {
                callback();
                Console.WriteLine("*** calling MyMethod *** ");
                Thread.Sleep(60 * 1 * 1000);
            }
            //NewMethod1(timer);     
           // return lastmod;     



        }
        [WebMethod]
        public void CheckModificationTimeforhttp()
        {
            while (true)
            {
                bool forhttps = true;
                callback(forhttps);
                Console.WriteLine("*** calling MyMethod *** ");
                Thread.Sleep(60 * 1 * 1000);
            }
            //NewMethod1(timer);          



        }

        private void callback(bool forhttps = false)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            HttpWebRequest request = null;
            if (forhttps)
            {
                request = (HttpWebRequest)WebRequest.Create("https://www.dropbox.com/home");
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create("http://www.cota.com/COTA/media/COTAContent/OpenGTFSData.zip");
            }
            request.Method = "GET";

            // request.Credentials = new NetworkCredential("amitkagarwal@ufl.edu", "Amit200292@");  
            // System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
           
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String x = ((HttpWebResponse)response).LastModified.ToString();
            if (!string.IsNullOrEmpty(lastmod) && lastmod != x)
            {
                lastmod = x;
                Sendmail();
            }
            else if (string.IsNullOrEmpty(lastmod))
            {
                lastmod = x;
            }
        }

        private bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Sendmail()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("amit@zeddigital.net");
            mail.To.Add("agarwalamit71@yahoo.in");
            mail.To.Add("ruban@zeddigital.net");
            mail.From = new MailAddress("websitescolumbusohio@gmail.com");
            mail.Subject = "sub";
            mail.Body = "body";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 ("websitescolumbusohio@gmail.com", "polgara@qtr1"); // ***use valid credentials***
            smtp.Port = 587;
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}

