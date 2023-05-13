using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_Ads : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSAdsShowFullscreen();
        public static void ShowFullscreen()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
             GSAdsShowFullscreen();
#else
            Console.Log("SHOW FULL SCREEN AD");
#endif
        }



        [DllImport("__Internal")]
        private static extern void GSAdsShowRewarded(string idOrTag);
        public static void ShowRewarded(string idOrTag = "COINS")
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        GSAdsShowRewarded(idOrTag);
#else
            Console.Log("SHOW REWARD VIDEO AD  --- REWARD TAG IS: ", idOrTag);
            OnRewardedReward?.Invoke(idOrTag);
#endif
        }




        [DllImport("__Internal")]
        private static extern void GSAdsShowPreloader();
        public static void ShowPreloader()
        {

#if !UNITY_EDITOR && UNITY_WEBGL
            GSAdsShowPreloader();
#else
            Console.Log("SHOW PRELOADER AD");
#endif

        }



        [DllImport("__Internal")]
        private static extern void GSAdsShowSticky();
        public static void ShowSticky()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            GSAdsShowSticky();
#else
            Console.Log("SHOW STICKY BANNER AD");
#endif
        }




        [DllImport("__Internal")]
        private static extern void GSAdsCloseSticky();
        public static void CloseSticky() => GSAdsCloseSticky();



        [DllImport("__Internal")]
        private static extern void GSAdsRefreshSticky();
        public static void RefreshSticky() => GSAdsRefreshSticky();



        [DllImport("__Internal")]
        private static extern string GSAdsIsAdblockEnabled();
        public static bool IsAdblockEnabled() => GSAdsIsAdblockEnabled() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsStickyAvailable();
        public static bool IsStickyAvailable() => GSAdsIsStickyAvailable() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsFullscreenAvailable();
        public static bool IsFullscreenAvailable() => GSAdsIsFullscreenAvailable() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsRewardedAvailable();
        public static bool IsRewardedAvailable() => GSAdsIsRewardedAvailable() == "true";



        [DllImport("__Internal")]
        private static extern string GSAdsIsPreloaderAvailable();
        public static bool IsPreloaderAvailable() => GSAdsIsPreloaderAvailable() == "true";



        [DllImport("__Internal")]
        private static extern string GSAdsIsStickyPlaying();
        public static bool IsStickyPlaying() => GSAdsIsStickyPlaying() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsFullscreenPlaying();
        public static bool IsFullscreenPlaying() => GSAdsIsFullscreenPlaying() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsRewardedPlaying();
        public static bool IsRewardedPlaying() => GSAdsIsRewardedPlaying() == "true";


        [DllImport("__Internal")]
        private static extern string GSAdsIsPreloaderPlaying();
        public static bool IsPreloaderPlaying() => GSAdsIsPreloaderPlaying() == "true";


        public static event UnityAction OnAdsStart;
        public static event UnityAction<bool> OnAdsClose;
        public static event UnityAction OnFullscreenStart;
        public static event UnityAction<bool> OnFullscreenClose;
        public static event UnityAction OnPreloaderStart;
        public static event UnityAction<bool> OnPreloaderClose;
        public static event UnityAction OnRewardedStart;
        public static event UnityAction<bool> OnRewardedClose;
        public static event UnityAction<string> OnRewardedReward;
        public static event UnityAction OnStickyStart;
        public static event UnityAction OnStickyClose;
        public static event UnityAction OnStickyRefresh;
        public static event UnityAction OnStickyRender;

        private void CallAdsStart() => OnAdsStart?.Invoke();
        private void CallAdsClose(string success) => OnAdsClose?.Invoke(success == "true");

        private void CallAdsFullscreenStart() => OnFullscreenStart?.Invoke();
        private void CallAdsFullscreenClose(string success) => OnFullscreenClose?.Invoke(success == "true");

        private void CallAdsPreloaderStart() => OnPreloaderStart?.Invoke();
        private void CallAdsPreloaderClose(string success) => OnPreloaderClose?.Invoke(success == "true");

        private void CallAdsRewardedStart() => OnRewardedStart?.Invoke();
        private void CallAdsRewardedClose(string success) => OnRewardedClose?.Invoke(success == "true");
        private void CallAdsRewardedReward(string Tag) => OnRewardedReward?.Invoke(Tag);

        private void CallAdsStickyStart() => OnStickyStart?.Invoke();
        private void CallAdsStickyClose() => OnStickyClose?.Invoke();
        private void CallAdsStickyRefresh() => OnStickyRefresh?.Invoke();
        private void CallAdsStickyRender() => OnStickyRender?.Invoke();

    }
}