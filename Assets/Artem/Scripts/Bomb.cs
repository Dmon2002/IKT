using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask raycaseLayers;

    private Transform _player;
    
    private void Start()
    {
        _player = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        TryAgre();
    }

    private void TryAgre()
    {
        if (Physics2D.Raycast(transform.position, transform.position - _player.position, float.PositiveInfinity, raycaseLayers))
            return;
        Agre();
    }

    public void Agre()
    {
        var room = LevelManager.Instance.GetNearestRoom(transform.position);
        
    }
}
