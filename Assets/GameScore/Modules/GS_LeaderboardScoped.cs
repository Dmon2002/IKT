using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_LeaderboardScoped : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSLeaderboardScopedOpen(
            string leaderboard_id = "0",
            string tag = "",
            string variant = "",
            // DESC | ASC
            string order = "DESC",
            string limit = "10",
            // level,exp,rank
            string includeFields = "",
            // level,rank
            string displayFields = "",
            // none | first | last
            string withMe = "none"
        );

        public static void Open(int leaderboard_id = 0, string variant = "some_variant", Order order = Order.DESC, int limit = 10, string includeFields = "", string displayFields = "", WithMe withMe = WithMe.first)
        {
            GSLeaderboardScopedOpen(leaderboard_id.ToString(), "", variant, order.ToString(), limit.ToString(), includeFields, displayFields, withMe.ToString());
        }

        public static void Open(string tag = "leaderboard_Tag", string variant = "some_variant", Order order = Order.DESC, int limit = 10, string includeFields = "", string displayFields = "", WithMe withMe = WithMe.first)
        {
            GSLeaderboardScopedOpen("0", tag, variant, order.ToString(), limit.ToString(), includeFields, displayFields, withMe.ToString());
        }



        [DllImport("__Internal")]
        private static extern void GSLeaderboardScopedFetch(
            string leaderboard_id = "0",
            string tag = "",
            string variant = "",
            // DESC | ASC
            string order = "DESC",
            string limit = "10",
            // level,exp,rank
            string includeFields = "",
            // none | first | last
            string withMe = "none"
        );

        public static void Fetch(int leaderboard_id = 0, string variant = "some_variant", Order order = Order.DESC, int limit = 10, string includeFields = "", WithMe withMe = WithMe.none)
        {
            GSLeaderboardScopedFetch(leaderboard_id.ToString(), "", variant, order.ToString(), limit.ToString(), includeFields, withMe.ToString());
        }

        public static void Fetch(string tag = "leaderboard_Tag", string variant = "some_variant", Order order = Order.DESC, int limit = 10, string includeFields = "", WithMe withMe = WithMe.none)
        {
            GSLeaderboardScopedFetch("0", tag, variant, order.ToString(), limit.ToString(), includeFields, withMe.ToString());
        }

        [DllImport("__Internal")]
        private static extern void GSLeaderboardScopedPublishRecord(
            string leaderboard_id = "0",
            string tag = "",
            string variant = "",
            bool Override = true,
            string key1 = "",
            string value1 = "0",
            string key2 = "",
            string value2 = "0",
            string key3 = "",
            string value3 = "0"
        );

        public static void PublishRecord(int leaderboard_id = 0, string variant = "some_variant", bool Override = true, string key1 = "", int record_value1 = 0, string key2 = "", int record_value2 = 0, string key3 = "", int record_value3 = 0)
        {
            GSLeaderboardScopedPublishRecord(leaderboard_id.ToString(), "", variant, Override, key1, record_value1.ToString(), key2, record_value2.ToString(), key3, record_value3.ToString());
        }

        public static void PublishRecord(string tag = "leaderboard_Tag", string variant = "some_variant", bool Override = true, string key1 = "", int record_value1 = 0, string key2 = "", int record_value2 = 0, string key3 = "", int record_value3 = 0)
        {
            GSLeaderboardScopedPublishRecord("0", tag, variant, Override, key1, record_value1.ToString(), key2, record_value2.ToString(), key3, record_value3.ToString());
        }



        [DllImport("__Internal")]
        private static extern void GSLeaderboardScopedFetchPlayerRating(
            string leaderboard_id = "0",
            string tag = "",
            string variant = "",
            string includeFields = "rank"
            );

        public static void FetchPlayerRating(int leaderboard_id = 0, string variant = "some_variant", string includeFields = "")
        {
            GSLeaderboardScopedFetchPlayerRating(leaderboard_id.ToString(), "", variant, includeFields);
        }

        public static void FetchPlayerRating(string tag = "leaderboard_Tag", string variant = "some_variant", string includeFields = "")
        {
            GSLeaderboardScopedFetchPlayerRating("0", tag, variant, includeFields);
        }



        public static event UnityAction<string, GS_ModelArray> OnFetch;
        public static event UnityAction<string, string, GS_ModelArray> OnFetch_TAG_VARIANT;
        public static event UnityAction OnFetchError;

        public static event UnityAction<string, int> OnFetchPlayerRating;
        public static event UnityAction<string, string, int> OnFetchPlayerRating_TAG_VARIANT;
        public static event UnityAction OnFetchPlayerRatingError;

        public static event UnityAction OnOpen;
        public static event UnityAction OnPublishRecordComplete;
        public static event UnityAction OnPublishRecordError;


        private string _leaderboardFetchTag;
        private string _leaderboardFetchVariant;

        private string _leaderboardPlayerRatingFetchTag;
        private string _leaderboardPlayerRatingFetchVariant;


        private void CallLeaderboardScopedOpen() => OnOpen?.Invoke();
        private void CallLeaderboardScopedFetchTag(string lastTag) => _leaderboardFetchTag = lastTag;
        private void CallLeaderboardScopedFetchVariant(string lastVariant) => _leaderboardFetchVariant = lastVariant;

        private void CallLeaderboardScopedFetchError() => OnFetchError?.Invoke();


        private void CallLeaderboardScopedFetch(string data)
        {
            GS_ModelArray modelArray = new GS_ModelArray();
            modelArray.Data(data);
            OnFetch?.Invoke(_leaderboardFetchTag, modelArray);
            OnFetch_TAG_VARIANT?.Invoke(_leaderboardFetchTag, _leaderboardFetchVariant, modelArray);
        }


        private void CallLeaderboardScopedPublishRecordComplete() => OnPublishRecordComplete?.Invoke();
        private void CallLeaderboardScopedPublishRecordError() => OnPublishRecordError?.Invoke();


        private void CallLeaderboardScopedFetchPlayerTag(string lastTag) => _leaderboardPlayerRatingFetchTag = lastTag;
        private void CallLeaderboardScopedFetchPlayerVariant(string lastVariant) => _leaderboardPlayerRatingFetchVariant = lastVariant;


        private void CallLeaderboardScopedFetchPlayer(int playerPosition)
        {
            OnFetchPlayerRating?.Invoke(_leaderboardPlayerRatingFetchTag, playerPosition);
            OnFetchPlayerRating_TAG_VARIANT?.Invoke(_leaderboardPlayerRatingFetchTag, _leaderboardPlayerRatingFetchVariant, playerPosition);
        }


        private void CallLeaderboardScopedFetchPlayerRatingError() => OnFetchPlayerRatingError?.Invoke();

    }
}