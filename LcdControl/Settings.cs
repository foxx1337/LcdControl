using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LcdControl
{
    class Settings
    {
        private int queryInterval;
        private string gerritUrl;
        private string gerritUser;
        private string gerritPassword;

        public Settings()
        {
            Dirty = false;
        }

        [JsonConstructor]
        public Settings(
            int queryIntervalSeconds,
            string gerritUrl,
            string gerritUser,
            string gerritPassword
            )
        {
            this.queryInterval = queryIntervalSeconds;
            this.gerritUrl = gerritUrl;
            this.gerritUser = gerritUser;
            this.gerritPassword = gerritPassword;

            Dirty = false;
        }

        public int QueryIntervalSeconds
        {
            get
            {
                return queryInterval;
            }
            set
            {
                Dirty = true;
                queryInterval = value;
            }
        }

        public string GerritUrl
        {
            get
            {
                return gerritUrl;
            }
            set
            {
                Dirty = true;
                gerritUrl = value;
            }
        }

        public string GerritUser
        {
            get
            {
                return gerritUser;
            }
            set
            {
                Dirty = true;
                gerritUser = value;
            }
        }

        public string GerritPassword
        {
            get
            {
                return gerritPassword;
            }
            set
            {
                Dirty = true;
                gerritPassword = value;
            }
        }

        [JsonIgnore]
        public bool Dirty { get; set; }
    }
}
