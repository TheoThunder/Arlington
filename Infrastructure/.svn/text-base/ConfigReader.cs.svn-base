using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public static class ConfigReader
    {
        /// <summary>
        /// if not present in webconfig static
        /// </summary>
        public static string RepositoryMode
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["RepositoryMode"] != null)
                    return (System.Configuration.ConfigurationManager.AppSettings["RepositoryMode"]);
                else
                    return "Static";
                //throw new Exception("Database Connection String Missing from AppSettings");
            }
        }

        public static string UploadPath
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"] != null)
                    return (System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"]);
                else
                    throw new Exception("Upload path missing from AppSettings");
            }
        }

        public static string VoipIP
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["VoipIp"] != null)
                    return (System.Configuration.ConfigurationManager.AppSettings["VoipIp"]);
                else
                    throw new Exception("Voip ip address missing from AppSettings");
            }
        }


        public static string ConnectionString
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PostgresConnString"] != null)
                    return (System.Configuration.ConfigurationManager.AppSettings["PostgresConnString"]);
                else
                    throw new Exception("Database Connection String Missing from AppSettings");
            }

        }


        /// <summary>
        /// Reads AppConfig setting "PhoneSystemOnOff" for bool value
        /// </summary>
        public static bool PhoneSystemOnOff
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PhoneSystemOnOff"] != null)
                    return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["PhoneSystemOnOff"]);
                else
                    return false;
            }
        }
    }
}