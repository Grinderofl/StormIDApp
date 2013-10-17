using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Domain.Configuration
{
    public static class Settings
    {
        private const string KeyName = "allowedFileTypes";
        public static ObservableCollection<FileTypes> AllowedFileTypes;

        static Settings()
        {
            AllowedFileTypes = new ObservableCollection<FileTypes>();
            AllowedFileTypes.CollectionChanged += (sender, args) => SaveSettingsToConfig();
        }

        public static void LoadSettingsFromConfig()
        {
            var config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            var value = config.AppSettings.Settings[KeyName];
            if (value == null) return;

            foreach (var item in value.Value.Split('|'))
            {
                var type = (FileTypes) Enum.Parse(typeof (FileTypes), item);
                if (Enum.IsDefined(typeof (FileTypes), type))
                    AllowedFileTypes.Add(type);
            }
        }

        private static void SaveSettingsToConfig()
        {
            var config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            if(!config.AppSettings.Settings.AllKeys.Contains(KeyName))
                config.AppSettings.Settings.Add(KeyName, "");
            config.AppSettings.Settings[KeyName].Value = string.Join("|", AllowedFileTypes.ToList());
            config.Save();
        }
    }
}
