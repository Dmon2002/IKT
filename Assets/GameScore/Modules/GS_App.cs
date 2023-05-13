using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using GS_Utilities;

namespace GameScore
{
    public class GS_App : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GSAppTitle();
        public static string Title() => GSAppTitle();



        [DllImport("__Internal")]
        private static extern string GSAppDescription();
        public static string Description() => GSAppDescription();



        [DllImport("__Internal")]
        private static extern string GSAppImage();
        public async static void GetImage(Image image)
        {
            try
            {
                await GS_Utility.DownloadImageAsync(GSAppImage(), image);
            }
            catch (Exception exception)
            {
                Debug.Log("Error");
                Debug.Log(exception.Message);
            }
        }

        public static string ImageUrl() => GSAppImage();



        [DllImport("__Internal")]
        private static extern string GSAppUrl();
        public static string Url() => GSAppUrl();
    }
}
