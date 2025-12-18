using UnityEngine;
using System.Collections;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Banana (Shrink) Settings")]
    [SerializeField] private float shrinkDuration = 10f;
    [SerializeField] private float shrinkScale = 2.5f; 
    [SerializeField] private float normalScale = 5f; 
    private float transitionDuration = 0.5f;

    [Header("Strawberry (Invisibility) Settings")]
    [SerializeField] private float invisDuration = 10f;
    [SerializeField] private float invisAlpha = 0.5f; 
    private int defaultLayer;
    private int invisibleLayer;

    [Header("Kiwi (Magnet) Settings")]
    [SerializeField] private float magnetDuration = 10f; 
    [SerializeField] private GameObject magnetFieldObject;
    
    [Header("Melon (Heavy) Settings")]
    [SerializeField] private float heavyDuration = 10f;
    [SerializeField] private Color heavyColor = new Color(0f, 0.5f, 0f, 1f);
    
    [Header("UI Icons")]
    [SerializeField] private Sprite bananaIcon;
    [SerializeField] private Sprite strawberryIcon;
    [SerializeField] private Sprite kiwiIcon;
    [SerializeField] private Sprite melonIcon;
    
    
    public bool IsHeavy { get; private set; } 
    private Coroutine heavyRoutine;
    

    private SpriteRenderer spriteRend;
    
 
    private Coroutine shrinkRoutine;
    private Coroutine invisRoutine;
    private Coroutine magnetRoutine;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultLayer = gameObject.layer; 
        invisibleLayer = LayerMask.NameToLayer("Invisible"); 
    }

    // --- 1. BANANA (SHRINK) ---
    public void ActivateBanana()
    {
        if (shrinkRoutine != null) StopCoroutine(shrinkRoutine);
        shrinkRoutine = StartCoroutine(BananaRoutine());
    }

    private IEnumerator BananaRoutine()
    {
        
        if (PowerUpUI.instance != null) 
            PowerUpUI.instance.ActivateTimer(bananaIcon, shrinkDuration);
       
       // --- SHRINK ANIMATION ---
        
        float timer = 0f;
        Vector3 startScale = transform.localScale;
        
        float direction = Mathf.Sign(transform.localScale.x); 
        Vector3 targetSmallScale = new Vector3(direction * shrinkScale, shrinkScale, shrinkScale);

        while (timer < transitionDuration)
        {
            
            direction = Mathf.Sign(transform.localScale.x);
            
            targetSmallScale = new Vector3(direction * shrinkScale, shrinkScale, shrinkScale);
            
            startScale = new Vector3(direction * Mathf.Abs(startScale.x), startScale.y, startScale.z);

            
            transform.localScale = Vector3.Lerp(startScale, targetSmallScale, timer / transitionDuration);
            
            timer += Time.deltaTime;
            yield return null; 
        }
        
        transform.localScale = targetSmallScale;


        
        yield return new WaitForSeconds(shrinkDuration);


        // --- GROW ANIMATION ---
        
        timer = 0f;
        Vector3 currentSmallScale = transform.localScale;
        
        direction = Mathf.Sign(transform.localScale.x);
        Vector3 targetNormalScale = new Vector3(direction * normalScale, normalScale, normalScale);

        while (timer < transitionDuration)
        {
            
            direction = Mathf.Sign(transform.localScale.x);
            targetNormalScale = new Vector3(direction * normalScale, normalScale, normalScale);
            currentSmallScale = new Vector3(direction * Mathf.Abs(currentSmallScale.x), currentSmallScale.y, currentSmallScale.z);

            transform.localScale = Vector3.Lerp(currentSmallScale, targetNormalScale, timer / transitionDuration);
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetNormalScale;
        
        shrinkRoutine = null;
    }

    // --- 2. STRAWBERRY (INVISIBILITY) ---
    public void ActivateStrawberry()
    {
        if (invisRoutine != null) StopCoroutine(invisRoutine);
        invisRoutine = StartCoroutine(StrawberryRoutine());
    }

    private IEnumerator StrawberryRoutine()
    {
        
        if (PowerUpUI.instance != null) 
            PowerUpUI.instance.ActivateTimer(strawberryIcon, invisDuration);
        
        gameObject.layer = invisibleLayer; 
        
        
        Color c = spriteRend.color;
        c.a = invisAlpha;
        spriteRend.color = c;

        yield return new WaitForSeconds(invisDuration);

        
        gameObject.layer = defaultLayer;
        c.a = 1f;
        spriteRend.color = c;
        
        invisRoutine = null;
    }

    // --- 3. KIWI (MAGNET) ---
    public void ActivateKiwi()
    {
        
        if (magnetRoutine != null) StopCoroutine(magnetRoutine);
        magnetRoutine = StartCoroutine(KiwiRoutine());
    }

    private IEnumerator KiwiRoutine()
    {
        if (PowerUpUI.instance != null) 
            PowerUpUI.instance.ActivateTimer(kiwiIcon, magnetDuration);
        
        if (magnetFieldObject != null) magnetFieldObject.SetActive(true);

        yield return new WaitForSeconds(magnetDuration);

        if (magnetFieldObject != null) magnetFieldObject.SetActive(false);
        magnetRoutine = null;
    }
    
    // --- 4. MELON (HEAVY) ---
    
    public void ActivateMelon()
    {
        if (heavyRoutine != null) StopCoroutine(heavyRoutine);
        heavyRoutine = StartCoroutine(MelonRoutine());
    }

    private IEnumerator MelonRoutine()
    {
        if (PowerUpUI.instance != null) 
            PowerUpUI.instance.ActivateTimer(melonIcon, heavyDuration);
        
        IsHeavy = true; 
        
       
        Color originalColor = spriteRend.color;
        spriteRend.color = heavyColor;

        
        float originalGravity = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().gravityScale = originalGravity * 2f;

        yield return new WaitForSeconds(heavyDuration);

        
        IsHeavy = false;
        spriteRend.color = originalColor;
        GetComponent<Rigidbody2D>().gravityScale = originalGravity;

        heavyRoutine = null;
    }
}