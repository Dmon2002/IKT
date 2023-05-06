using System;
using UnityEngine;

public class EventManager : Manager<EventManager>
{

    public event Action<int> gameOver;
    public event Action<int> scoreChanged;
    public event Action gameRestarted;


    public void GameOver(int score)
    {
        gameOver?.Invoke(score);
    }

    public void ScoreChanged(int score)
    {
        scoreChanged?.Invoke(score);
    }

    public void GameRestarted()
    {
        gameRestarted?.Invoke();
    }

 
}
