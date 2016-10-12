using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace LcdControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string SETTINGS_FILE = "LcdControl.json";

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeConsole();

        private Settings settings;
        private JsonSerializer serializer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            serializer = new JsonSerializer();
            AllocConsole();
            ReadSettings();
            Console.WriteLine("We have just started, looking good!");

            ProcessGerritChanges();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            WriteSettings();
        }

        private void ReadSettings()
        {
            StreamReader file = null;
            try
            {
                file = File.OpenText(SETTINGS_FILE);
                settings = (Settings)serializer.Deserialize(file, typeof(Settings));
            }
            catch (FileNotFoundException)
            {
                settings = new Settings();
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }

            Console.WriteLine("Using {0}:{1}@{2}", settings.GerritUser, settings.GerritPassword, settings.GerritUrl);
            Console.WriteLine("Dirty = {0}", settings.Dirty);
        }

        private void WriteSettings()
        {
            if (settings.Dirty)
            {
                using (StreamWriter file = File.CreateText(SETTINGS_FILE))
                {
                    serializer.Serialize(file, settings);
                }
                settings.Dirty = false;
            }
        }

        private void ProcessGerritChanges()
        {
            string response = FromGerrit(GetTextAt("/a/changes/?q=status:open+owner:self"));
            var changes = JsonConvert.DeserializeObject<List<Gerrit.Change>>(response);
            Console.WriteLine("got {0} changes", changes.Count);
            foreach (var change in changes)
            {
                Console.WriteLine("got change {0}", change);
                if (change.Id.Equals("resilience%2Flogging~master~I6eab9c98ba10bebeaf9ef36634dc73abcfe486ca"))
                {
                    Console.WriteLine("gaddit!");
                    SingleGerritChange("resilience%2Flogging~master~I6eab9c98ba10bebeaf9ef36634dc73abcfe486ca");
                }
            }
            
        }

        private void SingleGerritChange(string id)
        {
            Uri uri = new Uri(settings.GerritUrl + "/a/changes/" + id + "/detail");
            Console.WriteLine("attempting at " + uri.AbsoluteUri);
            string response = FromGerrit(GetTextAt("/a/changes/" + id + "/detail/"));
            var change = JsonConvert.DeserializeObject<Gerrit.ChangeDetail>(response);
            Console.WriteLine("fetched a change from gerrit and it's " + change.Id);
            foreach (var rating in change.Labels.Review.All)
            {
                Console.WriteLine("and a rating of value " + rating.Value);
            }
        }

        private string FromGerrit(string input)
        {
            return input.Substring(5);
        }

        private string GetTextAt(string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(settings.GerritUrl + method);
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(
                new Uri(settings.GerritUrl),
                "Digest",
                new NetworkCredential(settings.GerritUser, settings.GerritPassword)
            );
            request.PreAuthenticate = true;
            request.Credentials = credentialCache;

            WebResponse response = request.GetResponse();
            return GetResponseText(response);
        }

        private string GetResponseText(WebResponse response)
        {
            using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
            {
                return responseStream.ReadToEnd();
            }

        }
    }
}
