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

    private void OnEnable()
    {
        room.FogRevealStart += Spawn;
    }

    private void OnDisable()
    {
        room.FogRevealStart -= Spawn;
    }

    public void Spawn()
    {
        List<GameObject> entities = new ();
        if (spawned)
            return;
        spawned = true;
        spawnables.ForEach(x => x.Spawn());
    }
}
