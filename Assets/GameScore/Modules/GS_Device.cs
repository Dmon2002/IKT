using System.Runtime.InteropServices;
using UnityEngine;

namespace GameScore
{
    public class GS_Device : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSIsMobile();
        public static bool IsMobile() => GSIsMobile() == "true";
        public static bool IsDesktop() => GSIsMobile() == "false";
    }

}