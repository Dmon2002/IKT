using System.Runtime.InteropServices;
using UnityEngine;

namespace GameScore
{
    public class GS_Analytics : MonoBehaviour
    {

        [DllImport("__Internal")]
        private static extern void GSAnalyticsHit(string url);
        public static void Hit(string url) => GSAnalyticsHit(url);



        [DllImport("__Internal")]
        private static extern void GSAnalyticsGoal(string eventName, string value);
        public static void Goal(string eventName, string value) => GSAnalyticsGoal(eventName, value);
        public static void Goal(string eventName, int value) => GSAnalyticsGoal(eventName, value.ToString());
    }
}