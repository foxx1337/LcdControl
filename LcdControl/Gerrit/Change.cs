using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LcdControl.Gerrit
{
    class Change
    {
        public string Id { get; set; }
        public string Subject { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}
