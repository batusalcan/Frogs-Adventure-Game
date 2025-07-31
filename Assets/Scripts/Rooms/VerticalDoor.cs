using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    [SerializeField] private Transform lowerRoom;
    [SerializeField] private Transform actualUpperRoom;
    [SerializeField] private Transform cameraTargetUpperRoom;
    [SerializeField] private Transform cameraTargetLowerRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.attachedRigidbody;

            if (rb != null && rb.velocity.y > 0f)
            {
                cam.MoveToNewRoom(cameraTargetUpperRoom);
                actualUpperRoom.GetComponent<Room>().ActivateRoom(true);
                lowerRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                cam.MoveToNewRoom(cameraTargetLowerRoom);
                lowerRoom.GetComponent<Room>().ActivateRoom(true);
                actualUpperRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}