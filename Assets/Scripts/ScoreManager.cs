using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public TMPro.TMP_Text otherText;
    public TMPro.TMP_Text maxText;

    private int score;

    public float Score => score;

    [SerializeField]
    private SavesController savesController;

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
        maxText.text = savesController.Get("score").ToString();
    }

    public void PlusScore()
    {
        print("PlusScore");
        score ++;
        text.text = score.ToString();
        otherText.text = score.ToString();
        if (score > savesController.Get("score"))
        {
            savesController.Set("score", score);
            
        }
        maxText.text = savesController.Get("score").ToString();
        print(savesController.Get("score"));
    }
}
