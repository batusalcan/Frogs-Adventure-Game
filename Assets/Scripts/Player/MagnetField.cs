using UnityEngine;

public class MagnetField : MonoBehaviour
{
    [SerializeField] private float pullSpeed = 10f; 

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Cherry") || collision.GetComponent<CherryCollectible>() != null)
        {
           
            collision.transform.position = Vector3.MoveTowards(
                collision.transform.position, 
                transform.parent.position, 
                pullSpeed * Time.deltaTime
            );
        }
    }
}