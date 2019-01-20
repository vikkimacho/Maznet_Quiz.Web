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
using Newtonsoft.Json;
using System.Web.Http;
using System.Configuration;

namespace Quiz.Web.API.Controllers
{
    public class GoogleMailController : ApiController
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
        [System.Web.Http.HttpPost]
        public string SendGoogleMail(GoogleMail googleMail)
        {
            string result = "FAILED";
            try
            {
                string credentialsPath = ConfigurationManager.AppSettings["GSuiteCredntial"];
                string credentialsPathNew = ConfigurationManager.AppSettings["GSuiteCredentialNew"];

                UserCredential credential;
                //read credentials file
                using (FileStream stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                {
                    string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = credentialsPathNew;
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }

                string plainText = $"To: " + googleMail.ToMail + "  \r\n" +
                                   $"Subject: " + googleMail.Subject + " \r\n" +
                                   "Content-Type: text/html; charset=utf-8\r\n\r\n" +
                                   $"<h1>" + googleMail.Body + "</h1>";

                //call gmail service
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                var newMsg = new Google.Apis.Gmail.v1.Data.Message();
                newMsg.Raw = Base64UrlEncode(plainText.ToString());
                service.Users.Messages.Send(newMsg, "me").Execute();
                result = "SUCCESS";

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}