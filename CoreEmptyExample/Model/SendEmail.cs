using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Model
{
    public class SendEmail
    {
        public List<string> SendTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
