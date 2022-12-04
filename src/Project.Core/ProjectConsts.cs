using Project.Debugging;

namespace Project
{
    public class ProjectConsts
    {
        public const string LocalizationSourceName = "Project";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "17ed68dd01bd426d84cbfebed8c2564a";
    }
}
