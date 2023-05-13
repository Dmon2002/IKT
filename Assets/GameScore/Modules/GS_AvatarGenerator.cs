using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_AvatarGenerator : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSAvatarGenerator();
        /// <summary> Return  current avatar generator </summary>
        public static string Current() => GSAvatarGenerator();



        [DllImport("__Internal")]
        private static extern void GSChangeAvatarGenerator(string generator);
        public static void Change(GeneratorType generator) => GSChangeAvatarGenerator(generator.ToString());



        public static event UnityAction<string> OnChangeAvatarGenerator;

        private void CallChangeAvatarGenerator(string generator) => OnChangeAvatarGenerator?.Invoke(generator);

    }
    public enum GeneratorType : byte
    {
        dicebear_retro,
        dicebear_identicon,
        dicebear_human,
        dicebear_micah,
        dicebear_bottts,
        icotar,
        robohash_robots,
        robohash_cats,
    }
}