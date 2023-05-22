using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] private Vector2Int _startingPosition;
    private GameObject _player;

    public ScoreManager scoreManager;

    private void Awake()
    {
        Time.timeScale = 1;
    }
}
