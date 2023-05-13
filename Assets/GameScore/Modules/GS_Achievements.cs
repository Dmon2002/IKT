using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

using GS_Utilities;

namespace GameScore
{
    public class GS_Achievements : MonoBehaviour
    {
        private List<AchievementsFetch> _achievements;
        private List<AchievementsFetchGroups> _achievementsGroups;
        private List<AchievementsFetchPlayer> _achievementsPlayer;


        [DllImport("__Internal")]
        private static extern void GSAchievementsOpen();
        public static void Open() => GSAchievementsOpen();



        [DllImport("__Internal")]
        private static extern void GSAchievementsFetch();
        public static void Fetch() => GSAchievementsFetch();



        [DllImport("__Internal")]
        private static extern void GSAchievementsUnlock(string idOrTag);
        public static void Unlock(string idOrTag) => GSAchievementsUnlock(idOrTag);



        public static event UnityAction OnAchievementsOpen;
        public static event UnityAction OnAchievementsClose;

        public static event UnityAction<List<AchievementsFetch>> OnAchievementsFetch;
        public static event UnityAction<List<AchievementsFetchGroups>> OnAchievementsFetchGroups;
        public static event UnityAction<List<AchievementsFetchPlayer>> OnAchievementsFetchPlayer;

        public static event UnityAction OnAchievementsFetchError;
        public static event UnityAction<string> OnAchievementsUnlock;
        public static event UnityAction OnAchievementsUnlockError;


        private void CallAchievementsFetch(string achievementsData)
        {
            _achievements = GS_JSON.GetArrayList<AchievementsFetch>(achievementsData);
            OnAchievementsFetch?.Invoke(_achievements);
        }

        private void CallAchievementsFetchGroups(string achievementsGroupsData)
        {
            _achievementsGroups = GS_JSON.GetArrayList<AchievementsFetchGroups>(achievementsGroupsData);
            OnAchievementsFetchGroups?.Invoke(_achievementsGroups);
        }

        private void CallPlayerAchievementsFetch(string AchievementsDataPlayer)
        {
            _achievementsPlayer = GS_JSON.GetArrayList<AchievementsFetchPlayer>(AchievementsDataPlayer);
            OnAchievementsFetchPlayer?.Invoke(_achievementsPlayer);
        }


        private void CallAchievementsOpen() => OnAchievementsOpen?.Invoke();
        private void CallAchievementsClose() => OnAchievementsClose?.Invoke();

        private void CallAchievementsFetchError() => OnAchievementsFetchError?.Invoke();
        private void CallAchievementsUnlock(string idOrTag) => OnAchievementsUnlock?.Invoke(idOrTag);
        private void CallAchievementsUnlockError() => OnAchievementsUnlockError?.Invoke();

    }

    [System.Serializable]
    public class AchievementsFetch
    {
        public int id;
        public string icon;
        public string iconSmall;
        public string tag;
        public string rare;
        public string name;
        public string description;
    }

    [System.Serializable]
    public class AchievementsFetchGroups
    {
        public int id;
        public string tag;
        public string name;
        public string description;
        public string achievements;
    }

    [System.Serializable]
    public class AchievementsFetchPlayer
    {
        public int playerId;
        public int achievementId;
        public string createdAt;
    }
}
