using UnityEngine;

public class FogAnimationEvent : MonoBehaviour
{
    [SerializeField] private Room room;

    public void OnAnimationEnd()
    {
        room.OnFogAnimationEnd();
    }
}
