using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using GS_Utilities;

namespace GameScore
{
    public class GS_Player : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern int GSPlayerGetID();
        public static int GetID() => GSPlayerGetID();



        [DllImport("__Internal")]
        private static extern float GSPlayerGetScore();
        public static float GetScore() => GSPlayerGetScore();



        [DllImport("__Internal")]
        private static extern string GSPlayerGetName();
        public static string GetName() => GSPlayerGetName();



        [DllImport("__Internal")]
        private static extern string GSPlayerGetAvatar();
        public static string GetAvatarUrl() => GSPlayerGetAvatar();

        public async static void GetAvatar(Image image)
        {
            try
            {
                await GS_Utility.DownloadImageAsync(GSPlayerGetAvatar(), image);
            }
            catch (Exception exception)
            {
                Debug.Log("Error");
                Debug.Log(exception.Message);
            }
        }



        [DllImport("__Internal")]
        private static extern string GSPlayerGetFieldName(string key);
        public static string GetFieldName(string key) => GSPlayerGetFieldName(key);



        [DllImport("__Internal")]
        private static extern string GSPlayerGetFieldVariantName(string key, string value);
        public static string GetFieldVariantName(string key, string value) => GSPlayerGetFieldVariantName(key, value);



        [DllImport("__Internal")]
        private static extern string GSPlayerGetFieldVariantAt(string key, string index);
        public static string GetFieldVariantAt(string key, int index) => GSPlayerGetFieldVariantAt(key, index.ToString());



        [DllImport("__Internal")]
        private static extern string GSPlayerGetFieldVariantIndex(string key, string value);
        public static string GetFieldVariantIndex(string key, string value) => GSPlayerGetFieldVariantIndex(key, value);



        [DllImport("__Internal")]
        private static extern void GSPlayerSetName(string name);
        public static void SetName(string name) => GSPlayerSetName(name);



        [DllImport("__Internal")]
        private static extern void GSPlayerSetAvatar(string src);
        public static void SetAvatar(string src) => GSPlayerSetAvatar(src);



        [DllImport("__Internal")]
        private static extern void GSPlayerSetScore(float score);
        public static void SetScore(float score) => GSPlayerSetScore(score);
        public static void SetScore(int score) => GSPlayerSetScore(score);



        [DllImport("__Internal")]
        private static extern void GSPlayerAddScore(float score);
        public static void AddScore(float score) => GSPlayerAddScore(score);
        public static void AddScore(int score) => GSPlayerAddScore(score);



        [DllImport("__Internal")]
        private static extern int GSPlayerGetNumberInt(string key);
        public static int GetInt(string key) => GSPlayerGetNumberInt(key);



        [DllImport("__Internal")]
        private static extern float GSPlayerGetNumberFloat(string key);
        public static float GetFloat(string key) => GSPlayerGetNumberFloat(key);



        [DllImport("__Internal")]
        private static extern string GSPlayerGetString(string key);
        public static string GetString(string key) => GSPlayerGetString(key);



        [DllImport("__Internal")]
        private static extern string GSPlayerGetBool(string key);
        public static bool GetBool(string key) => GSPlayerGetBool(key) == "true";



        [DllImport("__Internal")]
        private static extern void GSPlayerSet(string key, string value);
        public static void Set(string key, string value) => GSPlayerSet(key, value);
        public static void Set(string key, int value) => GSPlayerSet(key, value.ToString());
        public static void Set(string key, bool value) => GSPlayerSet(key, value.ToString());
        public static void Set(string key, float value) => GSPlayerSet(key, value.ToString());



        [DllImport("__Internal")]
        private static extern void GSPlayerSetFlag(string key, bool value);
        public static void SetFlag(string key, bool value) => GSPlayerSetFlag(key, value);



        [DllImport("__Internal")]
        private static extern void GSPlayerAdd(string key, string value);
        public static void Add(string key, float value) => GSPlayerAdd(key, value.ToString());
        public static void Add(string key, int value) => GSPlayerAdd(key, value.ToString());



        [DllImport("__Internal")]
        private static extern void GSPlayerToggle(string key);
        public static void Toggle(string key) => GSPlayerToggle(key);



        [DllImport("__Internal")]
        private static extern void GSPlayerReset();
        public static void ResetPlayer() => GSPlayerReset();



        [DllImport("__Internal")]
        private static extern void GSPlayerRemove();
        public static void Remove() => GSPlayerRemove();



        [DllImport("__Internal")]
        private static extern void GSPlayerSync(bool forceOverride = false);
        public static void Sync(bool forceOverride = false) => GSPlayerSync(forceOverride);



        [DllImport("__Internal")]
        private static extern void GSPlayerLoad();
        public static void Load() => GSPlayerLoad();



        [DllImport("__Internal")]
        private static extern void GSPlayerLogin();
        public static void Login() => GSPlayerLogin();



        [DllImport("__Internal")]
        private static extern void GSPlayerFetchFields();
        public static void FetchFields() => GSPlayerFetchFields();



        [DllImport("__Internal")]
        private static extern string GSPlayerHas(string key);
        public static bool Has(string key) => GSPlayerHas(key) == "true";



        [DllImport("__Internal")]
        private static extern string GSPlayerIsLoggedIn();
        public static bool IsLoggedIn() => GSPlayerIsLoggedIn() == "true";



        [DllImport("__Internal")]
        private static extern string GSPlayerHasAnyCredentials();
        public static bool HasAnyCredentials() => GSPlayerHasAnyCredentials() == "true";



        [DllImport("__Internal")]
        private static extern string GSPlayerIsStub();
        public static bool IsStub() => GSPlayerIsStub() == "true";



        public static event UnityAction OnPlayerChange;
        public static event UnityAction OnPlayerSyncComplete;
        public static event UnityAction OnPlayerSyncError;
        public static event UnityAction OnPlayerLoadComplete;
        public static event UnityAction OnPlayerLoadError;
        public static event UnityAction OnPlayerLoginComplete;
        public static event UnityAction OnPlayerLoginError;
        public static event UnityAction<List<PlayerFetchFieldsData>> OnPlayerFetchFieldsComplete;
        public static event UnityAction OnPlayerFetchFieldsError;
        public static event UnityAction OnPlayerReady;

        private void CallPlayerReady() => OnPlayerReady?.Invoke();
        private void CallPlayerChange() => OnPlayerChange?.Invoke();

        private void CallPlayerSyncComplete() => OnPlayerSyncComplete?.Invoke();
        private void CallPlayerSyncError() => OnPlayerSyncError?.Invoke();

        private void CallPlayerLoadComplete() => OnPlayerLoadComplete?.Invoke();
        private void CallPlayerLoadError() => OnPlayerLoadError?.Invoke();

        private void CallPlayerLoginComplete() => OnPlayerLoginComplete?.Invoke();
        private void CallPlayerLoginError() => OnPlayerLoginError?.Invoke();



        private List<PlayerFetchFieldsData> _playerFetchFields;
        private void CallPlayerFetchFieldsComplete(string data)
        {
            _playerFetchFields = GS_JSON.GetArrayList<PlayerFetchFieldsData>(data);
            OnPlayerFetchFieldsComplete?.Invoke(_playerFetchFields);
        }
        private void CallPlayerFetchFieldsError() => OnPlayerFetchFieldsError?.Invoke();
    }

    [System.Serializable]
    public class PlayerFetchFieldsData
    {
        public string name;
        public string key;
        public string type;
        public string defaultValue; // string | bool | number
        public bool important;
        public Variants[] variants;

    }
    [System.Serializable]
    public class Variants
    {
        public string value; // string | number
        public string name;
    }
}

