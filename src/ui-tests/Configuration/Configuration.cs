using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace UITests.Configuration
{
    public class Configuration
    {
        [YamlMember(Alias="site_url")]
        public String SiteUrl { get; set; }

        [YamlMember(Alias = "screenshots_path")]
        public String ScreenshotsPath { get; set; }

        [YamlMember(typeof(WebDriverConfig), Alias="web_driver")]
        public WebDriverConfig WebDriverConfig { get; set; }

        //[YamlMember(Alias="browsers")]
        //public List<dynamic> Browsers { get; set; }
    }

    public class WebDriverConfig
    {
        [YamlMember(Alias="remote")]
        public bool IsRemote { get; set; }

        [YamlMember(Alias="implicitly_wait_time")]
        public int ImplicitlyWaitTime { get; set; }

        [YamlMember(Alias="hub_url")]
        public string HubUrl { get; set; }
    }
}
