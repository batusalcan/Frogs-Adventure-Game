using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject breakEffect; // Kırılma efekti (parçacıklar)
    [SerializeField] private AudioClip breakSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerPowerUps powerUps = collision.gameObject.GetComponent<PlayerPowerUps>();

            
            if (powerUps != null && powerUps.IsHeavy)
            {
                if (collision.relativeVelocity.y <= -2f)
                {
                    
                    if (collision.transform.position.y > transform.position.y)
                    {
                        BreakTheObject();
                    }
                }
            }
        }
    }

    private void BreakTheObject()
    {
        if (breakSound != null) 
            SoundManager.instance.PlaySound(breakSound);

        if (breakEffect != null) 
            Instantiate(breakEffect, transform.position, Quaternion.identity);

        
        Destroy(gameObject);
    }
}