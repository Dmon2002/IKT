using UnityEngine;

public class Teleportable : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = teleportTo.position;
        }
    }
}
