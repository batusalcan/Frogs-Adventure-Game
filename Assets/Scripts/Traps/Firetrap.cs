using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
  [SerializeField] private float damage;
  
  [Header("Firetrap Timers")]
  [SerializeField] private float activationDelay;
  [SerializeField] private float activeTime;
  private Animator anim;
  private SpriteRenderer spriteRend;
  private CapsuleCollider2D fireCollider;
  
  [Header("SFX")] 
  [SerializeField] private AudioClip firetrapSound;

  
  private bool triggered;
  private bool active;
  private Health playerHealth;

  private void Awake()
  {
    anim = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();
    fireCollider = GetComponent<CapsuleCollider2D>();
    fireCollider.enabled = false;
  }

  private void Update()
  {
    if (playerHealth != null && active)
    {
      playerHealth.TakeDamage(damage);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      playerHealth = collision.GetComponent<Health>();
      if (!triggered)
      {
        StartCoroutine(ActivateFiretrap());
      }

    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      playerHealth = null;
    }
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    if (fireCollider.enabled && collision.CompareTag("Player"))
    {
      collision.GetComponent<Health>().TakeDamage(damage);
    }
  }

  private IEnumerator ActivateFiretrap()
  {
    //turn sprite red to notify the player and trigger the trap
    triggered = true;
    spriteRend.color = Color.red;
    
    //wait for delay, activate trap, turn on anim, return color back to normal
    yield return new WaitForSeconds(activationDelay);
    SoundManager.instance.PlaySound(firetrapSound);
    spriteRend.color = Color.white;
    active = true;
    anim.SetBool("activated",true);
    fireCollider.enabled = true;
    
    //wait untill x seconts, deactivate trap and reset all variables
    yield return new WaitForSeconds(activeTime);
    active = false;
    triggered = false;
    anim.SetBool("activated",false);
    fireCollider.enabled = false;
  }
}
