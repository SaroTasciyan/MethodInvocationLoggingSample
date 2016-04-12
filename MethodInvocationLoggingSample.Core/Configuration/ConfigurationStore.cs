using System;
using System.Linq;
using System.Configuration;
using System.Globalization;

namespace MethodInvocationLoggingSample.Core.Configuration
{
    public class ConfigurationStore : IConfigurationStore
    {
        public string GetConnectionString(string key)
        {
            if (key == null)
            { throw new ArgumentNullException(nameof(key)); }

            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public T GetCasting<T>(string key)
        {
            if (key == null)
            { throw new ArgumentNullException(nameof(key)); }

            return (T)(object)ConfigurationManager.AppSettings[key];
        }

        public T Get<T>(string key) where T : IConvertible
        {
            if (key == null)
            { throw new ArgumentNullException(nameof(key)); }

            string value = ConfigurationManager.AppSettings[key];

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        public bool TryGetCasting<T>(string key, out T value)
        {
            if (key == null)
            { throw new ArgumentNullException(nameof(key)); }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                string appSettingsValue = ConfigurationManager.AppSettings[key];
                if (appSettingsValue is T)
                {
                    value = (T)(object)appSettingsValue;
                    return true;
                }
            }

            value = default(T);
            return false;
        }

        public bool TryGet<T>(string key, out T value) where T : IConvertible
        {
            if (key == null)
            { throw new ArgumentNullException(nameof(key)); }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                string appSettingsValue = ConfigurationManager.AppSettings[key];

                value = (T)Convert.ChangeType(appSettingsValue, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }

            value = default(T);
            return false;
        }
    }
}