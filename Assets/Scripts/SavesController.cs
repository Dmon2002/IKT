using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class SavesController : MonoBehaviour
{
    // В первую очередь подпишитесь на событие GP_SDK.OnReady;
    [SerializeField]
    private bool IsYandexBuild = true;
    private void OnEnable()
    {
        if (IsYandexBuild)
        {
            YandexGame.GetDataEvent += OnSDKReady;
        }
        else
        {
            GameScore.GS_Player.OnPlayerReady += OnSDKReady;
            GameScore.GS_Ads.OnRewardedClose += EndAds;
        }
    }

    private void OnDisable()
    {
        if (IsYandexBuild)
        {
            YandexGame.GetDataEvent -= OnSDKReady;
        }
        else
        {
            GameScore.GS_Player.OnPlayerReady -= OnSDKReady;
        }
            
    }

    private void OnSDKReady()
    {
        if (IsYandexBuild)
        {

        }
        else
        {
        }

    }

    public void ChangeLanguage(string value)
    {
        if (IsYandexBuild)
        {

        }
        else
        {

        }

    }

    public int GetInt(string value)
    {
        if (IsYandexBuild)
        {
            if(value== "score")
            {
                return YandexGame.savesData.score;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (GameScore.GS_Player.Has(value))
            {
                return GameScore.GS_Player.GetInt(value);
            }
            else
            {
                return 0;
            }
        }

    }
    public void SetInt(string value, int valueInt)
    {
        if (IsYandexBuild)
        {
            if (value == "score")
            {
                YandexGame.savesData.score = valueInt;
            }
        }
        else
        {
            GameScore.GS_Player.Set(value, valueInt);
        }
        
    }

    public void ShowAdd()
    {
        if (IsYandexBuild)
        {
            YandexGame.FullscreenShow();
        }
        else
        {
            if (GameScore.GS_Ads.IsRewardedAvailable())
            {
                GameScore.GS_Ads.ShowRewarded("0");
            }
        }

    }
    private void EndAds(bool value)
    {
        print(value);
    }

    public void EndLevel()
    {

    }
    public void EndLocation()
    {
        ShowAdd();
    }

    public void LoseLevel()
    {
        ShowAdd();
        SaveProgress();
    }

    public void BuyHero(int id)
    {
        if (IsYandexBuild)
        {
            YandexGame.savesData.hero[id] = true;
        }
        else
        {
            GameScore.GS_Player.Set("hero" + id, true);           
        }
        SaveProgress();
    }

    private void SaveProgress()
    {
        if (IsYandexBuild)
        {
            YandexGame.SaveProgress();
        }
        else
        {
            GameScore.GS_Player.Sync();
        }
        
    }
}
