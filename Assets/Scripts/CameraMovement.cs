using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 0.1f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = LevelManager.Instance.Player.transform;
    }

    private void Update()
    {
        Move(Time.deltaTime);
    }

    private void Move(float time)
    {

        if (playerTransform == null)
            return;

        Vector3 targetPosition = transform.position;

        if (playerTransform.position.y < targetPosition.y)
            return;

        targetPosition.y = Mathf.Lerp(transform.position.y, playerTransform.position.y, lerpSpeed * time);
        transform.position = targetPosition;
    }
}
