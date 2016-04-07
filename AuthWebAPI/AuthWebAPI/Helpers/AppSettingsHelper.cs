using System;
using System.Configuration;

namespace AuthWebAPI.Helpers
{
    public class AppSettingsHelper
    {
        public static TResult GetValue<TResult>(string key, Func<TResult> defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var keyValue = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrWhiteSpace(key))
                {
                    try
                    {
                        return (TResult)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(TResult));
                    }
                    catch (Exception e) { }
                }
            }

            if (defaultValue != null)
            {
                try
                {
                    return defaultValue.Invoke();
                }
                catch (Exception e) { }
            }

            return default(TResult);
        }
    }
}