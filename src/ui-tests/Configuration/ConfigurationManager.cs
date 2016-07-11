using System;
using System.Diagnostics.Contracts;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UITests.Configuration
{
    public sealed class ConfigurationManager
    {
        private const String CONFIGURATION_FILE = "application.yml";

        private static ConfigurationManager instance = null;
        private static object syncRoot = new Object();

        private Configuration configuration;

        public static ConfigurationManager Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationManager();
                    }
                    return instance;
                }
            }
        }

        private ConfigurationManager()
        {
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            try
            {
                using (TextReader streamReader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIGURATION_FILE)))
                {
                    configuration = deserializer.Deserialize<Configuration>(streamReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String SiteUrl
        {
            get
            {
                return getFromEnvOrDefault("SiteUrl", (i) => i, configuration.SiteUrl);
            }
        }

        public bool UseWebDriverRC
        {
            get
            {
                return getFromEnvOrDefault("UseWebDriverRC", bool.Parse, configuration.WebDriverConfig.IsRemote);
            }
        }

        public String WebDriverHubUrl
        {
            get
            {
                return getFromEnvOrDefault("WebDriverHubUrl", (i) => i, configuration.WebDriverConfig.HubUrl);
            }
        }

        public String ScreenshotsPath
        {
            get
            {
                return getFromEnvOrDefault("ScreenshotsPath", (i) => i, configuration.ScreenshotsPath);
            }
        }

        public int WebDriverImplicitlyWaitTime
        {
            get
            {
                return getFromEnvOrDefault("ImplicitlyWaitTime", int.Parse, configuration.WebDriverConfig.ImplicitlyWaitTime);
            }
        }

        private T getFromEnvOrDefault<T>(String name, Func<String, T> converter, T defaultValue)
        {
            String value = Environment.GetEnvironmentVariable(name);
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return converter.Invoke(value);
        }
    }
}

