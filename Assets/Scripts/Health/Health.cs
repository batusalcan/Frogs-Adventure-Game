using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
  [Header("Health")]
  [SerializeField] private float startingHealth;
  public float currentHealth {get; private set;}
  private Animator anim;
  private bool dead;
  
  [Header("iFrames")]
  [SerializeField] private float iFramesDuration;
  [SerializeField] private int numberOfFlashes;
  private SpriteRenderer spriteRend;
  
  [Header("Components")]
  [SerializeField]private Behaviour[] components;
  private bool invulnerable;
  
  [Header("Sounds")]
  [SerializeField]private AudioClip deathSound;
  [SerializeField]private AudioClip hurtSound;
  [SerializeField] private AudioClip spawnSound;
  
  private Rigidbody2D body;

  private void Awake()
  {
    float calculadetHealth = 1;

    if (GameManager.instance.IsSkillUnlocked("ExtraHealth_1"))
    {
      calculadetHealth += 1;
    }
    if (GameManager.instance.IsSkillUnlocked("ExtraHealth_2"))
    {
      calculadetHealth += 1;
    }

    startingHealth = calculadetHealth;
    currentHealth = startingHealth;
    
    anim = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();

    body = GetComponent<Rigidbody2D>();
  }
  
  private void Start()
  {
    if (spawnSound != null)
    {
      SoundManager.instance.PlaySound(spawnSound);
    }
  }

  public void TakeDamage(float _damage)
  {
    if (invulnerable)
    {
      return;
    }
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

    if (currentHealth > 0)
    {
      anim.SetTrigger("hurt");
      StartCoroutine(Invulnerability());
      SoundManager.instance.PlaySound(hurtSound);
    }
    else
    {
      if (!dead)
      {
        

        foreach (Behaviour component in components)
        {
          component.enabled = false;
        }
       
        anim.SetBool("grounded",true);
        anim.SetTrigger("die");
        
        dead = true;
        SoundManager.instance.PlaySound(deathSound);
        
        body.velocity = Vector2.zero; 
        body.bodyType = RigidbodyType2D.Static;
      }
    }
  }

  public void AddHealth(float _value)
  {
    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
  }

  private IEnumerator Invulnerability()
  {
    invulnerable = true;
    Physics2D.IgnoreLayerCollision(10,11,true);
    for (int i = 0; i < numberOfFlashes; i++)
    {
      spriteRend.color = new Color(1, 0, 0, 0.5f);
      yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      spriteRend.color = Color.white;
      yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    }
    Physics2D.IgnoreLayerCollision(10,11,false);
    invulnerable = false;
  }

  private void Deactivate()
  {
    gameObject.SetActive(false);
  }

  public void Respawn()
  {
    dead = false;
    AddHealth(startingHealth);
    anim.ResetTrigger("die");
    anim.Play("Spawn");
    
    if (spawnSound != null)
    {
      SoundManager.instance.PlaySound(spawnSound);
    }
    
    StartCoroutine(Invulnerability());
    
    foreach (Behaviour component in components)
    {
      component.enabled = true;
    }
    
    body.bodyType = RigidbodyType2D.Dynamic;
  }

 
}
