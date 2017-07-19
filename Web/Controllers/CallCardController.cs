using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Data.Domain;
using Data.Repositories.Abstract;
using Infrastructure;
using Web.Service.Abstract;
using Web.ViewModel;

namespace Web.Controllers
{
    public class CallCardController : Controller
    {
        IUserRepository _UserRepos;
        IPhoneUserRepository _PhoneUserRepos;
        public CallCardController(IUserRepository UserRepos, IPhoneUserRepository PhoneRepos)
        {
            _UserRepos = UserRepos;
            _PhoneUserRepos = PhoneRepos;
        }
        public ActionResult Index()
        {
            CallCardViewModel ccvm = new CallCardViewModel();

            var username = HttpContext.User.Identity.Name;
            ccvm.user = _UserRepos.GetUserByUsername(username);
            return View(ccvm);
        }
        
        
        [HttpPost]
        public ActionResult Calling(string text)
        {  
            CallCardViewModel ccvm = new CallCardViewModel();

            var username = HttpContext.User.Identity.Name;
            ccvm.user = _UserRepos.GetUserByUsername(username);

            var phoneuser = _PhoneUserRepos.GetPhoneUser(ccvm.user.UserId);
            //user = phoneuser.UserName;
            //password = phoneuser.Password;
            user = "admin";
            password = "Pbx2011";
            //int numbertocall = int.Parse(text);
            var num = text;
            if (num.Contains("("))
            {
                num = num.Remove(0, 1);
                num = num.Remove(3, 1);
                num = num.Remove(7, 1);
                num = num.Remove(3, 1);
            }
            //num = "1" + num;
            long phoneNumberDialing = Convert.ToInt64(num);

            string xml = "<request method= \"switchvox.users.call\"> <parameters> <account_id>" + /*phoneuser.AccountId*/ 1155 + "</account_id><dial_first>" + phoneuser.Extension + "</dial_first> <dial_second>" + phoneNumberDialing + "</dial_second> <variables> <variable>balance=300</variable> </variables> </parameters> </request> ";
            //string xml = "<request method= \"switchvox.users.call\"> <parameters> <account_id>1106</account_id><dial_first>201</dial_first> <dial_second>202</dial_second> <variables> <variable>balance=300</variable> </variables> </parameters> </request> ";
            // string xml = "<request method=\"switchvox.extensions.getInfo\"> <parameters> <extensions> <extension>201</extension> <extension>202</extension> </extensions> </parameters></request> ";
            //string xml = "<request method=\"switchvox.extensionGroups.getList\">	<parameters>		<sort_field>name</sort_field>		<sort_order>ASC</sort_order>	</parameters></request>";
            //string xml = "<request method=\"switchvox.extensions.featureCodes.callMonitoring.add\">  <parameters>    <feature_code>41</feature_code>        <groups>            <group>                <authorized>                    <members>                        <member>                     <type>extension_group</type>                         <id>1005</id>                        </member>        </members>                </authorized>                <targeted>                    <members>                        <member>                            <type>extension_group</type>                            <id>1004</id>                        </member>                    </members>                </targeted>            </group>        </groups>  </parameters></request>";

            //string xml = "<request method=\"switchvox.extensionGroups.add\"> <parameters>  <extension_group_name>Admin</extension_group_name>   <members> <member>       <type>account</type> <id>1106</id> </member> </members>  <vm_quota>100</vm_quota>  <description> Administrator</description>  <user_viewable>1</user_viewable>    </parameters></request>";
            string url = ConfigReader.VoipIP;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //string s = "id="+Server.UrlEncode(xml);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
            req.Method = "POST";
            req.ContentType = "text/xml;charset=utf-8";
            req.ContentLength = requestBytes.Length;
            req.Credentials = new NetworkCredential(user, password);
            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();

            sr.Close();
            res.Close();

            return Content("Calling "+ text );
        }

        [HttpPost]
        public ActionResult EndCall(string text)
        {
            CallCardViewModel ccvm = new CallCardViewModel();

            var username = HttpContext.User.Identity.Name;
            ccvm.user = _UserRepos.GetUserByUsername(username);

            var phoneuser = _PhoneUserRepos.GetPhoneUser(ccvm.user.UserId);
            user = "admin";
            password = "admin";

            string xml = "<request method=\"switchvox.currentCalls.getList\"> <parameters>  </parameters></request>";
            string url = ConfigReader.VoipIP;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //string s = "id="+Server.UrlEncode(xml);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
            req.Method = "POST";
            req.ContentType = "text/xml;charset=utf-8";
            req.ContentLength = requestBytes.Length;
            req.Credentials = new NetworkCredential(user, password);
            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();

            
            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.LoadXml(backstr); //* load the XML document from the specified file.

            //* Get elements.
            XmlNodeList test = xmlDoc.GetElementsByTagName("current_calls");

            var result = Json(test[0].InnerXml);
           

            ////* Display the results.
            Console.WriteLine("Address: " + test[0].InnerText);
           

            sr.Close();
            res.Close();

            return Content("Call Ended");
            //return Redirect("https://192.168.2.89/");
        }
        public string password { get; set; }



        public string user { get; set; }
    }
}
