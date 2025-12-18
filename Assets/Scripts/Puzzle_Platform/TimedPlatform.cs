using UnityEngine;
using System.Collections;

public class TimedPlatform : MonoBehaviour
{
    [Header("Timings")]
    [SerializeField] private float activeTime = 2f;   
    [SerializeField] private float inactiveTime = 2f; 
    [SerializeField] private float startDelay = 0f;   

    [Header("Sounds")]
    [SerializeField] private AudioClip appearSound;    
    [SerializeField] private AudioClip disappearSound; 

    private SpriteRenderer spriteRend;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(PlatformCycle());
    }

    private IEnumerator PlatformCycle()
    {
       
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            
            if (appearSound != null) 
                SoundManager.instance.PlaySound(appearSound);

            EnablePlatform(true);
            yield return new WaitForSeconds(activeTime);


            
            
           
            if (disappearSound != null) 
                SoundManager.instance.PlaySound(disappearSound);

            EnablePlatform(false);
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void EnablePlatform(bool status)
    {
        if (spriteRend != null)
        {
           
            Color c = spriteRend.color;
            c.a = status ? 1f : 0.2f; 
            spriteRend.color = c;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = status;
        }
    }
}