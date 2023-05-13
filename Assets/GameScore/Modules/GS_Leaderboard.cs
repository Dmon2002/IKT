using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
namespace GameScore
{
    public class GS_Leaderboard : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSLeaderboardOpen(
                string orderBy = "score",
                // DESC | ASC
                string order = "DESC",
                string limit = "10",
                // none | first | last
                string withMe = "none",
                // level,exp,rank
                string includeFields = "",
                // level,rank
                string displayFields = ""
              );
        public static void Open(string orderBy = "score", Order order = Order.DESC, int limit = 10, WithMe withMe = WithMe.none, string includeFields = "", string displayFields = "")
        {
            GSLeaderboardOpen(orderBy, order.ToString(), limit.ToString(), withMe.ToString(), includeFields, displayFields);
        }



        [DllImport("__Internal")]
        private static extern void GSLeaderboardFetch(
            string tag = "",
            string orderBy = "score",
            // DESC | ASC
            string order = "DESC",
            string limit = "10",
            // none | first | last
            string withMe = "none",
            // level,exp,rank
            string includeFields = ""
        );
        public static void Fetch(string tag = "", string orderBy = "score", Order order = Order.DESC, int limit = 10, WithMe withMe = WithMe.none, string includeFields = "")
        {
            GSLeaderboardFetch(tag, orderBy, order.ToString(), limit.ToString(), withMe.ToString(), includeFields);
        }



        [DllImport("__Internal")]
        private static extern void GSLeaderboardFetchPlayerRating(
            string tag = "",
            string orderBy = "score",
            // DESC | ASC
            string order = "DESC"
        );
        public static void FetchPlayerRating(string tag = "", string orderBy = "score", Order order = Order.DESC)
        {
            GSLeaderboardFetchPlayerRating(tag, orderBy, order.ToString());
        }

        public static event UnityAction<string, GS_ModelArray> OnLeaderboardFetch;
        public static event UnityAction OnLeaderboardFetchError;

        public static event UnityAction<string, int> OnLeaderboardFetchPlayer;
        public static event UnityAction OnLeaderboardFetchPlayerError;

        public static event UnityAction OnLeaderboardOpen;


        private string _leaderboardFetchTag;
        private string _leaderboardPlayerFetchTag;


        private void CallLeaderboardOpen() => OnLeaderboardOpen?.Invoke();


        private void CallLeaderboardFetch(string data)
        {
            GS_ModelArray modelArray = new GS_ModelArray();
            modelArray.Data(data);
            OnLeaderboardFetch?.Invoke(_leaderboardFetchTag, modelArray);

        }
        private void CallLeaderboardFetchTag(string lastTag) => _leaderboardFetchTag = lastTag;
        private void CallLeaderboardFetchError() => OnLeaderboardFetchError?.Invoke();



        private void CallLeaderboardFetchPlayerTag(string lastTag) => _leaderboardPlayerFetchTag = lastTag;

        private void CallLeaderboardFetchPlayer(int playerPosition) => OnLeaderboardFetchPlayer?.Invoke(_leaderboardPlayerFetchTag, playerPosition);

        private void CallLeaderboardFetchPlayerError() => OnLeaderboardFetchPlayerError?.Invoke();
    }

    public enum Order : byte
    {
        DESC,
        ASC
    }

    public enum WithMe : byte
    {
        none,
        first,
        last
    }
}
