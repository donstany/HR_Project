using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models
{
    public class ShowLogModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ObjectKey { get; set; }
        public string ButtonLabel { get; set; }
        public string Title { get; set; }
    }
}
