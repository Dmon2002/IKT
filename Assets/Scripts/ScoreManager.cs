using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public TMPro.TMP_Text otherText;

    private float score;

    public float Score => score;

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
        otherText.text = score.ToString();
    }

    public void PlusScore()
    {
        score ++;
        text.text = score.ToString();
        otherText.text = score.ToString();

    }
}
