using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_Game : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSIsPaused();
        public static bool IsPaused() => GSIsPaused() == "true";

        [DllImport("__Internal")]
        private static extern void GSPause();
        public static void Pause() => GSPause();

        [DllImport("__Internal")]
        private static extern void GSResume();
        public static void Resume() => GSResume();


        public static event UnityAction OnPause;
        public static event UnityAction OnResume;


        private void CallOnPause() => OnPause?.Invoke();
        private void CallOnResume() => OnResume?.Invoke();
    }

}