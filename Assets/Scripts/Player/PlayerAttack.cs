using UnityEngine;
using UnityEngine.UI; 

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Audio")]
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip emptyFireSound;
    
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 5;
    private int currentAmmo;

    [Header("UI References")]
    
    [SerializeField] private Text ammoText; 
    [SerializeField] private GameObject ammoUIContainer; 

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentAmmo = maxAmmo;
    }

    private void Start()
    {
       
        UpdateAmmoUI();
    }

    private void Update()
    {
        
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
         
            if (GameManager.instance.IsSkillUnlocked("Shuriken_Infinite"))
            {
                Attack(); 
            }
          
            else if (GameManager.instance.IsSkillUnlocked("Shuriken_Finite"))
            {
                if (currentAmmo > 0)
                {
                    Attack();
                    currentAmmo--; 
                    UpdateAmmoUI();
                }
                else
                {
                    if (emptyFireSound != null)
                    {
                        SoundManager.instance.PlaySound(emptyFireSound);
                    }
                    cooldownTimer = 0;
                }
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo; 
        UpdateAmmoUI();
    }

  
    public void RefillAmmoFull()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText == null) return;

     
        if (GameManager.instance.IsSkillUnlocked("Shuriken_Infinite"))
        {
            ammoText.text = "∞"; 
            ammoText.color = Color.yellow;
        }
        
        else if (!GameManager.instance.IsSkillUnlocked("Shuriken_Finite"))
        {
            if(ammoUIContainer != null) ammoUIContainer.SetActive(false);
        }
        
        else
        {
            if(ammoUIContainer != null) ammoUIContainer.SetActive(true);
            ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        }
    }
}