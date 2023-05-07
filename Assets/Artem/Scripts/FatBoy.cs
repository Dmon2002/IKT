using System.Collections;
using UnityEngine;

public class FatBoy : Enemy
{
    [SerializeField] private float tolerance;
    [SerializeField] private float delayBetweenRooms;

    public override void Agre()
    {
        StartCoroutine(Move());
    }

    private Room GetNextRoom()
    {
        return LevelManager.Instance.GetNearestHideRoom(transform.position);
    }

    private IEnumerator Move()
    {
        var room = GetNextRoom();
        while (room != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, room.transform.position, MoveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, room.transform.position) <= tolerance)
            {
                room = GetNextRoom();
                yield return new WaitForSeconds(delayBetweenRooms);
            }
            yield return null;
        }
    }
}
