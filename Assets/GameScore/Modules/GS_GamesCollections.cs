using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_GamesCollections : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSGamesCollectionsOpen(string idOrTag);
        public static void Open(string idOrTag) => GSGamesCollectionsOpen(idOrTag);



        [DllImport("__Internal")]
        private static extern void GSGamesCollectionsFetch(string idOrTag);
        public static void Fetch(string idOrTag) => GSGamesCollectionsFetch(idOrTag);



        public static event UnityAction OnGamesCollectionsOpen;
        public static event UnityAction OnGamesCollectionsClose;

        public static event UnityAction<string, GamesCollectionsFetchData> OnGamesCollectionsFetch;
        public static event UnityAction OnGamesCollectionsFetchError;


        private GamesCollectionsFetchData GamesCollectionsFecthData;
        private string _gamesCollectionsFetchTag;


        private void CallGamesCollectionsOpen() => OnGamesCollectionsOpen?.Invoke();
        private void CallGamesCollectionsClose() => OnGamesCollectionsClose?.Invoke();


        private void CallGamesCollectionsFetch(string data)
        {
            GamesCollectionsFecthData = JsonUtility.FromJson<GamesCollectionsFetchData>(data);

            OnGamesCollectionsFetch?.Invoke(_gamesCollectionsFetchTag, GamesCollectionsFecthData);
        }


        private void CallGamesCollectionsFetchTag(string idOrTag) => _gamesCollectionsFetchTag = idOrTag;
        private void CallGamesCollectionsFetchError() => OnGamesCollectionsFetchError?.Invoke();
    }


    [System.Serializable]
    public struct GamesCollectionsFetchData
    {
        public string __typename;
        public int id;
        public string tag;
        public string name;
        public string description;
        public Games[] games;
    }

    [System.Serializable]
    public struct Games
    {
        public int id;
        public string url;
        public string name;
        public string description;
        public string icon;
    }

}