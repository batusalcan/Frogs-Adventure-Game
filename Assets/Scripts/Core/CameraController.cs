using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 0.15f;

    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(targetPos.x, targetPos.y, transform.position.z),
            ref velocity,
            speed
        );
    }

    public void MoveToNewRoom(Transform newTarget)
    {
        targetPos = newTarget.position;
        Debug.Log("Camera target: " + targetPos);
    }
}