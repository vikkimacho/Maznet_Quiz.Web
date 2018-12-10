using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.API.Controllers
{
    public class GoogleMailController : Controller
    {


        //using gmail scope
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "SendMail";

        public static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        public static void Main()
        {
            
        }
        // GET: GoogleMail
        public ActionResult SendGoogleMail(GoogleMail googleMail)
        {
            try
            {
                UserCredential credential;
                //read credentials file
                using (FileStream stream = new FileStream(@"~\packagejson\credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = @"~\packagejson\credentialsnew.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }

                string plainText = $"To: " + googleMail.ToMail + "  \r\n" +
                                   $"Subject: " + googleMail.Subject + " \r\n" +
                                   "Content-Type: text/html; charset=utf-8\r\n\r\n" +
                                   $"<h1>" + googleMail.Subject + "</h1>";

                //call gmail service
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                var newMsg = new Google.Apis.Gmail.v1.Data.Message();
                newMsg.Raw = Base64UrlEncode(plainText.ToString());
                service.Users.Messages.Send(newMsg, "me").Execute();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View();
        }
    }
}