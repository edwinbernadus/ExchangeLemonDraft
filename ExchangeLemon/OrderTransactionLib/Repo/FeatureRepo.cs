namespace BlueLight.Main
{
    public class FeatureRepo
    {
#if DEBUG
        public static bool UseSemaphore = false;
#else
        public static bool UseSemaphore = false;
#endif

        public static bool UseTransaction = true;

        public static bool UseRequestWebLog = false;
    }
}