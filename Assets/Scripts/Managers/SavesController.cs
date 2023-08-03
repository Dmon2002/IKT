using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class SavesController : MonoBehaviour
{
    // В первую очередь подпишитесь на событие GP_SDK.OnReady;
    [SerializeField]
    private bool IsYandexBuild = true;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

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

    public void OnSDKReady()
    {
        if (IsYandexBuild)
        {

        }
        else
        {

        }

    }

    public bool ChangeLanguage(string value)
    {
        if (IsYandexBuild)
        {
            if (value == "ru" || value == "en")
            {
                YandexGame.savesData.language = value;
                SaveProgress(); return true;
            }
        }
        else
        {
            if (value == "en")
            {
                GameScore.GS_Language.Change(GameScore.Language.en);
                SaveProgress(); return true;
            }
            else if(value == "ru")
            {
                GameScore.GS_Language.Change(GameScore.Language.ru);
                SaveProgress(); return true;
            }
        }
        return false;
    }

    public string GetCurrentLanguage()
    {
        if (IsYandexBuild)
        {
            return YandexGame.savesData.language;
        }
        else
        {
            return GameScore.GS_Language.Current();
        }
    }

    public bool TryBuyUpdate(int id)
    {
        if (IsYandexBuild)
        {
            if (id == 0)
            {
                YandexGame.savesData.lvlStartHealth++;
                return true;
            }
            else if (id == 1)
            {
                YandexGame.savesData.lvlSpeedExp++;
                return true;
            }
            else if (id == 2)
            {
                YandexGame.savesData.lvlPlusChest++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (id == 0)
            {
                GameScore.GS_Player.Set("update0", GameScore.GS_Player.GetInt("update0")+1);
                return true;
            }
            else if (id == 1)
            {
                GameScore.GS_Player.Set("update1", GameScore.GS_Player.GetInt("update1") + 1);
                return true;
            }
            else if (id == 2)
            {
                GameScore.GS_Player.Set("update2", GameScore.GS_Player.GetInt("update2") + 1);
                YandexGame.savesData.lvlPlusChest++;
                return true;
            }
            else
            {
                return false;
            }
        }
        SaveProgress();
    }

    public int Get(string value)
    {
        if (IsYandexBuild)
        {
            if(value== "score")
            {
                return YandexGame.savesData.score;
            }
            else if (value == "gold")
            {
                return YandexGame.savesData.gold;
            }
            else if (value == "hero0")
            {
                return 1;
            }
            else if (value == "hero1")
            {
                if (YandexGame.savesData.hero[1])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (value == "hero2")
            {
                if (YandexGame.savesData.hero[2])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (value == "hero3")
            {
                if (YandexGame.savesData.hero[3])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (value == "update2")
            {
                return YandexGame.savesData.lvlPlusChest;
            }
            else if (value == "update1")
            {
                return YandexGame.savesData.lvlSpeedExp;
            }
            else if (value == "update0")
            {
                return YandexGame.savesData.lvlStartHealth;
            }
            else if (value == "music")
            {
                return YandexGame.savesData.music;
            }
            else if (value == "sound")
            {
                return YandexGame.savesData.sound;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (GameScore.GS_Player.Has(value) && !(value == "hero0" || value == "hero1" || value == "hero2" || value == "hero3"))
            {
                return GameScore.GS_Player.GetInt(value);
            }
            else if (GameScore.GS_Player.Has(value) &&( value=="hero0" || value == "hero1" || value == "hero2" || value == "hero3"))
            {
                if(GameScore.GS_Player.GetBool(value))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

    }

    public void SetSound(int value)
    {
        if (IsYandexBuild)
        {
            YandexGame.savesData.sound = value;
        }
        else
        {
            GameScore.GS_Player.Set("sound", value);
        }
        SaveProgress();
    }
    public void SetMusic(int value)
    {
        if (IsYandexBuild)
        {
            YandexGame.savesData.music = value;
        }
        else
        {
            GameScore.GS_Player.Set("music", value);
        }
        SaveProgress();
    }
    public void SetScore(int valueInt)
    {
        if (IsYandexBuild)
        {
            YandexGame.savesData.score = valueInt;
            
        }
        else
        {
            GameScore.GS_Player.Set("score", valueInt);
        }
        SaveProgress();
    }

    public void SetGold(int value)
    {
        if (IsYandexBuild)
        {
            YandexGame.savesData.gold = value;

        }
        else
        {
            GameScore.GS_Player.Set("gold", value);
        }
        SaveProgress();
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
        SaveProgress();
    }
    public void EndLocation()
    {
        ShowAdd();
        if (IsYandexBuild)
        {
            YandexGame.savesData.location++;
        }
        else
        {
            GameScore.GS_Player.Set("location", GameScore.GS_Player.GetInt("location")+1);
        }
        SaveProgress();
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
