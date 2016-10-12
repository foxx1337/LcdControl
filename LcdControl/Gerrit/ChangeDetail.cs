using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LcdControl.Gerrit
{
    class ChangeDetail
    {
        public string Id { get; set; }
        public Labels Labels { get; set; }
    }
}
