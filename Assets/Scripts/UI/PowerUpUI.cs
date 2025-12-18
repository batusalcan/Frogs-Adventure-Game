using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public static PowerUpUI instance;

    [Header("UI References")]
    [SerializeField] private GameObject container;   
    [SerializeField] private Image iconImage;       
    [SerializeField] private Image timerImage;       

    private float totalTime;
    private float timeLeft;
    private bool isTimerActive;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (isTimerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerImage.fillAmount = timeLeft / totalTime;
            }
            else
            {
                timeLeft = 0;
                isTimerActive = false;
                container.SetActive(false); 
            }
        }
    }

    
    public void ActivateTimer(Sprite icon, float duration)
    {
     
        iconImage.sprite = icon;
        
      
        totalTime = duration;
        timeLeft = duration;
        
      
        timerImage.fillAmount = 1f;
        
        isTimerActive = true;
        container.SetActive(true);
    }
}