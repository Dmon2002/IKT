using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomBound : MonoBehaviour
{
    [SerializeField] private Room room;

    public Vector2Int Coord => room.Coords;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            alive.AddRoom(Coord);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            alive.AddRoom(Coord);
        }
    }
}
