using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_Socials : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSSocialsShare(string text, string url, string image);
        public static void Share(string text = "", string url = "", string image = "")
        {
            GSSocialsShare(text, url, image);
        }


        [DllImport("__Internal")]
        private static extern void GSSocialsPost(string text, string url, string image);
        public static void Post(string text = "", string url = "", string image = "")
        {
            GSSocialsPost(text, url, image);
        }



        [DllImport("__Internal")]
        private static extern void GSSocialsInvite(string text, string url, string image);
        public static void Invite(string text = "", string url = "", string image = "")
        {
            GSSocialsInvite(text, url, image);
        }


        [DllImport("__Internal")]
        private static extern void GSSocialsJoinCommunity();
        public static void JoinCommunity() => GSSocialsJoinCommunity();



        [DllImport("__Internal")]
        private static extern string GSSocialsCommunityLink();
        public static string CommunityLink() => GSSocialsCommunityLink();



        [DllImport("__Internal")]
        private static extern string GSSocialsIsSupportsNativeShare();
        public static bool IsSupportsNativeShare() => GSSocialsIsSupportsNativeShare() == "true";



        [DllImport("__Internal")]
        private static extern string GSSocialsIsSupportsNativePosts();
        public static bool IsSupportsNativePosts() => GSSocialsIsSupportsNativePosts() == "true";



        [DllImport("__Internal")]
        private static extern string GSSocialsIsSupportsNativeInvite();
        public static bool IsSupportsNativeInvite() => GSSocialsIsSupportsNativeInvite() == "true";



        [DllImport("__Internal")]
        private static extern string GSSocialsCanJoinCommunity();
        public static bool CanJoinCommunity() => GSSocialsCanJoinCommunity() == "true";



        [DllImport("__Internal")]
        private static extern string GSSocialsIsSupportsNativeCommunityJoin();
        public static bool IsSupportsNativeCommunityJoin() => GSSocialsIsSupportsNativeShare() == "true";


        public static event UnityAction<bool> OnShare;
        public static event UnityAction<bool> OnPost;
        public static event UnityAction<bool> OnInvite;
        public static event UnityAction<bool> OnJoinCommunity;


        private void CallSocialsShare(string success) => OnShare?.Invoke(success == "true");
        private void CallSocialsPost(string success) => OnPost?.Invoke(success == "true");
        private void CallSocialsInvite(string success) => OnInvite?.Invoke(success == "true");
        private void CallSocialsJoinCommunity(string success) => OnJoinCommunity?.Invoke(success == "true");
    }
}