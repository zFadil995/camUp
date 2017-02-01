using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CamUp
{
    public static class Settings
    {
        private const string folderPath = "accountID";
        private static readonly string folderDefaultPath = "";


        public static string FolderPath
        {
            get { return AppSettings.GetValueOrDefault<string>(folderPath, folderDefaultPath); }
            set { AppSettings.AddOrUpdateValue<string>(folderPath, value); }
        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
    }
}
