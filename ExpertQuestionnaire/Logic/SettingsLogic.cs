using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class SettingsLogic
    {
        static SettingsLogic()
        { _values = GetValues(); }

        public SettingsLogic()
        {
            SmtpURL = GetValue("SmtpURL".ToLower());
            EMail = GetValue("EMail".ToLower());
            Password = GetValue("Password".ToLower());
            SmtpPort = GetValue("SmtpPort".ToLower(), 25);
            EnableSSL = GetValue("EnableSSL".ToLower(), true);
            AllowReanswer = GetValue("AllowReanswer".ToLower(), false);
        }

        private static T GetValue<T>(string key, T defaultValue = default(T))
        {
            object value;

            _values.TryGetValue(key, out value);

            if (_values.TryGetValue(key, out value))
            {
                if (value.GetType() != typeof(T))
                {
                    value = (T)Convert.ChangeType(value, typeof(T));

                    _values[key] = value;
                }
            }
            else
            { _values[key] = value = defaultValue; }

            return (T)value;
        }

        private string GetValue(string key)
        { return GetValue(key, String.Empty); }

        private static Dictionary<string, object> GetValues()
        {
            var values = new Dictionary<string, object>();

            foreach (string keySetting in ConfigurationManager.AppSettings.Keys)
            { values.Add(keySetting.ToLower(), ConfigurationManager.AppSettings[keySetting]); }

            return values;
        }

        private static Dictionary<string, object> _values;

        public string SmtpURL
        { get; private set; }

        public string EMail
        { get; private set; }

        public string Password
        { get; private set; }

        public int SmtpPort
        { get; private set; }

        public bool EnableSSL
        { get; private set; }

        public bool AllowReanswer
        { get; private set; }
    }
}
