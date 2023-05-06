using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [System.Serializable]
    private class SpawnPoint
    {
        public Transform point;
        public GameObject prefab;

        public GameObject Spawn()
        {
            return Instantiate(prefab, point.position, Quaternion.identity);
        }
    }

    private Vector2Int _coords;

    [SerializeField] List<SpawnPoint> spawnables;

    private bool spawned;

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }

    public void Spawn()
    {
        if (spawned)
            return;
        spawned = true;
        spawnables.ForEach(x => x.Spawn());
    }
}
