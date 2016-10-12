using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LcdControl.Gerrit
{
    class Labels
    {
        [JsonProperty("Code-Review")]
        public CodeReview Review { get; set; }
    }
}
