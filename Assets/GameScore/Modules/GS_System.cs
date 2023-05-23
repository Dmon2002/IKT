using System.Runtime.InteropServices;
using UnityEngine;
namespace GameScore
{
    public class GS_System : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSIsDev();
        public static bool IsDev() => GSIsDev() == "true";

        [DllImport("__Internal")]
        private static extern string GSIsAllowedOrigin();
        public static bool IsAllowedOrigin() => GSIsAllowedOrigin() == "true";
    }

}