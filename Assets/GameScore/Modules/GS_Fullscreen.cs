using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_Fullscreen : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSFullscreenOpen();
        public static void Open() => GSFullscreenOpen();



        [DllImport("__Internal")]
        private static extern void GSFullscreenClose();
        public static void Close() => GSFullscreenClose();



        [DllImport("__Internal")]
        private static extern void GSFullscreenToggle();
        public static void Toggle() => GSFullscreenToggle();



        public static event UnityAction OnFullscreenOpen;
        public static event UnityAction OnFullscreenClose;
        public static event UnityAction OnFullscreenChange;



        private void CallFullscreenOpen() => OnFullscreenOpen?.Invoke();
        private void CallFullscreenClose() => OnFullscreenClose?.Invoke();
        private void CallFullscreenChange() => OnFullscreenChange?.Invoke();

    }
}