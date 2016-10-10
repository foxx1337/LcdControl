using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;

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
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            WriteSettings();
        }

        private void ReadSettings()
        {
            using (StreamReader file = File.OpenText(SETTINGS_FILE))
            {
                settings = (Settings)serializer.Deserialize(file, typeof(Settings));
                
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
    }
}
