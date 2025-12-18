using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f; 
    [SerializeField] private float destroyDelay = 2f; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip crackSound;

    private bool isFalling = false;

    private void Start()
    {
       
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
          
            if (collision.relativeVelocity.y <= 0f)
            {
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        
        if(crackSound != null) SoundManager.instance.PlaySound(crackSound);

       
        float timer = 0f;
        Vector3 originalPos = transform.position;
        
        while(timer < fallDelay)
        {
            
            float xOffset = Random.Range(-0.05f, 0.05f);
            transform.position = new Vector3(originalPos.x + xOffset, originalPos.y, originalPos.z);
            
            timer += Time.deltaTime;
            yield return null;
        }

        
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f; 

        
        GetComponent<Collider2D>().isTrigger = true;

        
        Destroy(gameObject, destroyDelay);
    }
}