using System.Runtime.InteropServices;
using UnityEngine;

namespace GameScore
{
    public class GS_Platform : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSPlatformType();
        public static string Type() => GSPlatformType();



        [DllImport("__Internal")]
        private static extern string GSPlatformHasIntegratedAuth();
        public static bool HasIntegratedAuth() => GSPlatformHasIntegratedAuth() == "true";



        [DllImport("__Internal")]
        private static extern string GSPlatformIsExternalLinksAllowed();
        public static bool IsExternalLinksAllowed() => GSPlatformIsExternalLinksAllowed() == "true";
    }
}

