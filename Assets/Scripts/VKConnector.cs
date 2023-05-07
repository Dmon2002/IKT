using System.Collections.Generic;
using UnityEngine;

public class VKConnector : MonoBehaviour
{
    public VkBridgeController bridge;

    public void Awake()
    {
        bridge.VKWebAppInit();
    }

    public void AddPost()
    {
        bridge.Send("VKWebAppShowWallPostBox", new Dictionary<string, string> { { "message", "Пожужжим" }, { "attachments", "photo183896350_457241037" } }, ResultAddPost);
    }

    public void ResultAddPost(string json)
    {
        Debug.Log(json);
    }
}
