using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace harmonypatch
{
    public class Config : IConfig
    {
        [Description("is plugin enabled?")]
        
        public bool IsEnabled { get; set; } = true;
        
        [Description("is the debug enabled?")]
        
        public bool Debug { get; set; } = false;
        
        [Description("the webhook url")]
        public string webhook { get; set; } = "0";
        
        [Description("should ignore dnt? (THIS MAY DELIST YOUR SERVER)")]
        
        public bool ignorednt { get; set; } = false;
    }
}