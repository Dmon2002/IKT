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

    [SerializeField] private Room room;
    [SerializeField] List<SpawnPoint> spawnables;

    private bool spawned;

    public void Spawn()
    {
        var objs = new List<GameObject>();
        if (spawned)
            return;
        spawned = true;
        spawnables.ForEach(x => x.Spawn());
        objs.ForEach(x =>
        {
            if (TryGetComponent<AliveObject>(out var alive))
            {
                room.AddAliveToRoom(alive);
            }
        });
    }
}
