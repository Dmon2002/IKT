using System.Collections.Generic;
using UnityEngine;

public class VKConnector : MonoBehaviour
{
    public VkBridgeController bridge;
    public ScoreManager scoreManager;

    public void Awake()
    {
        bridge.VKWebAppInit();
    }

    public void AddPost()
    {
        bridge.Send("VKWebAppShowWallPostBox", new Dictionary<string, string> { { "message", "��������" }, { "attachments", "photo183896350_457241037" } }, ResultAddPost);
    }

    //public void ShowLeaderBoard()
    //{
    //    bridge.VKWebAppShowLeaderBoardBox((int)scoreManager.Score);
    //}

    public void ResultAddPost(string json)
    {
        Debug.Log(json);
    }
}
