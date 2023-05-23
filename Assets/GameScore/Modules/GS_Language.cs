using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
namespace GameScore
{
    public class GS_Language : MonoBehaviour
    {

        [DllImport("__Internal")]
        private static extern string GSLanguage();
        /// <summary> Return  current language </summary>
        public static string Current() => GSLanguage();



        [DllImport("__Internal")]
        private static extern void GSChangeLanguage(string language);
        public static void Change(Language language) => GSChangeLanguage(language.ToString());



        public static event UnityAction<string> OnChangeLanguage;


        private void CallChangeLanguage(string language) => OnChangeLanguage?.Invoke(language);

    }
    public enum Language : byte
    {
        /* English */
        en,

        /* French */
        fr,

        /* Italian */
        it,

        /* German */
        de,

        /* Spanish */
        es,

        /* Chineese */
        zh,

        /* Portuguese */
        pt,

        /* Korean */
        ko,

        /* Japanese */
        ja,

        /* Russian */
        ru,
    }
}