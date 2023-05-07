using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask raycaseLayers;
    [SerializeField] private float explodeDelay;
    [SerializeField] private float explodeDamage;

    private Transform _player;
    
    private void Start()
    {
        _player = GameManager.Instance.Player.transform;
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        var room = LevelManager.Instance.GetNearestRoom(transform.position);
        var neighbours = LevelManager.Instance.getAllNeighbours(room.Coords);
        yield return new WaitForSeconds(explodeDelay);
        foreach (var alive in room.InRoom)
        {
            alive.ApplyDamage(explodeDamage);
        }
        foreach (var neighbour in neighbours)
        {
            if (neighbour is null)
            {
                continue;
            }
            neighbour.RevealFog(neighbour.transform.position - transform.position);
        }
        gameObject.SetActive(false);
    }

}
