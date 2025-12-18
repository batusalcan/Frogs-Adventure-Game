using UnityEngine;
using System.Collections; // Coroutine için gerekli

public class FruitCollectible : MonoBehaviour
{
    public enum FruitType { Banana, Strawberry, Kiwi, Melon }

    [SerializeField] private FruitType fruitType;
    [SerializeField] private AudioClip collectSound;
    
    
    private Animator animator;
    private bool isCollected = false;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isCollected) return;

        if (collision.CompareTag("Player"))
        {
            PlayerPowerUps powerUps = collision.GetComponent<PlayerPowerUps>();

            if (powerUps != null)
            {
                isCollected = true; 

                
                if (collectSound != null) SoundManager.instance.PlaySound(collectSound);

                
                switch (fruitType)
                {
                    case FruitType.Banana:
                        powerUps.ActivateBanana();
                        break;
                    case FruitType.Strawberry:
                        powerUps.ActivateStrawberry();
                        break;
                    case FruitType.Kiwi:
                        powerUps.ActivateKiwi();
                        break;
                    case FruitType.Melon:
                        powerUps.ActivateMelon();
                        break;
                }

                
                if (animator != null)
                {
                    animator.SetTrigger("Collected"); 
                }

                
                StartCoroutine(WaitForAnimationAndDisable());
            }
        }
    }

    private IEnumerator WaitForAnimationAndDisable()
    {
        
        yield return null; 

        
        if (animator != null)
        {
            
            float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength);
        }

        
        gameObject.SetActive(false);
    }
}