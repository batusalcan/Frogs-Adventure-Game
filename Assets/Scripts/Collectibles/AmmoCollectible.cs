using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 1;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();

            if (playerAttack != null)
            {
                
                SoundManager.instance.PlaySound(collectSound);
                
                
                playerAttack.AddAmmo(ammoAmount);
                
                
                gameObject.SetActive(false); 
            }
        }
    }
}