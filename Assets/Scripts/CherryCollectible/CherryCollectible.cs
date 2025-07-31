using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip collectedSound;

    private Animator animator;
    private bool isCollected = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            isCollected = true;

            
            SoundManager.instance.PlaySound(collectedSound);

           
            animator.SetTrigger("Collected");

            
            CherryUIManager.instance.IncreaseCherryCount();

            
            StartCoroutine(WaitForAnimationAndDisable());
        }
    }

    private IEnumerator WaitForAnimationAndDisable()
    {
       
        yield return null;

        
        float animLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        
        yield return new WaitForSeconds(animLength);

        
        gameObject.SetActive(false);
    }
}