using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text text;
    private float score;
    private void Start()
    {
        /*
        if (! PlayerPrefs.HasKey("Score") )
        {
            PlayerPrefs.SetInt("Score" ,0);
        }
        */
        score = 0;
        text.text = score.ToString();
    }

    public void PlusScore()
    {
        score ++;
        text.text = score.ToString();
    }
}
