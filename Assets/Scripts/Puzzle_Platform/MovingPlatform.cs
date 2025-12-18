using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA; 
    [SerializeField] private Transform pointB; 
    [SerializeField] private float speed = 2f;

    private Vector3 targetPos;

    private void Start()
    {
        transform.position = pointA.position; 
        targetPos = pointB.position;
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

       
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
           
            if (targetPos == pointA.position)
                targetPos = pointB.position;
            else
                targetPos = pointA.position;
        }
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            collision.transform.SetParent(null);
            
            
        }
    }
    
   
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.5f);
            Gizmos.DrawWireSphere(pointB.position, 0.5f);
        }
    }
}