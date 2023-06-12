using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //[SerializeField] private LayerMask raycaseLayers;
    //[SerializeField] private float explodeDelay;
    //[SerializeField] private float explodeDamage;
    //[SerializeField] private GameObject explosionPrefab;

    //private Transform _player;
    
    //private void Start()
    //{
    //    _player = LevelManager.Instance.Player.transform;
    //    StartCoroutine(Explode());
    //}

    //public IEnumerator Explode()
    //{
    //    var room = LevelManager.Instance.GetNearestRoom(transform.position);
    //    var neighbours = LevelManager.Instance.GetAllNeighbours(room.Coords);
    //    yield return new WaitForSeconds(explodeDelay);
    //    AudioManager.instance.PlayBombExplosion();
    //    SpawnExplosion(room);
    //    var buffer = new List<Entity>(room.InRoom);
    //    foreach (var alive in buffer)
    //    {
    //        alive.ApplyDamage(explodeDamage);
    //    }
    //    foreach (var neighbour in neighbours)
    //    {
    //        if (neighbour is null)
    //        {
    //            continue;
    //        }
    //        if (neighbour.FogRevealed)
    //        {
    //            SpawnExplosion(neighbour);
    //            buffer = new List<Entity>(neighbour.InRoom);
    //            foreach (var alive in buffer)
    //            {
    //                alive.ApplyDamage(explodeDamage);
    //            }
    //        }
    //        else
    //        {
    //            neighbour.RevealFog(neighbour.transform.position - transform.position);
    //        }
    //    }
    //    Destroy(gameObject);
    //}

    //private void SpawnExplosion(Room room)
    //{

    //    Instantiate(explosionPrefab, room.transform.position, Quaternion.identity);
    //}
}
