using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("Skill Info")]
    [SerializeField] private string skillID;
    [SerializeField] private int cherryCost;
    [SerializeField] private int levelRequired;

    [Header("UI References")]
    [SerializeField] private Text statusText; 
    [SerializeField] private GameObject costArea; 
    [SerializeField] private Text priceText; 
    
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip purchaseSound;

    [Header("Colors")]
    [SerializeField] private Color lockedColor = Color.gray;     
    [SerializeField] private Color availableColor = Color.white; 
    [SerializeField] private Color purchasedColor = new Color(0f, 1f, 0f,1f); 

    private Button button;
    private Image buttonImage;

    void OnEnable()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
            buttonImage = GetComponent<Image>();
            button.onClick.AddListener(TryUnlockSkill);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GameManager.instance == null) return;

     
        if (GameManager.instance.IsSkillUnlocked(skillID))
        {
            button.interactable = false;
            buttonImage.color = purchasedColor; 
            
            costArea.SetActive(false); 
            statusText.gameObject.SetActive(true);
            statusText.text = "UNLOCKED";
        }
        else
        {
           
            if (GameManager.instance.GetHighestLevelCompleted() < levelRequired)
            {
                button.interactable = false;
                buttonImage.color = lockedColor; 

                costArea.SetActive(false); 
                statusText.gameObject.SetActive(true);
                statusText.text = "Req. Level " + levelRequired;
            }
            
            else
            {
                
                buttonImage.color = availableColor; 
                statusText.gameObject.SetActive(false); 
                costArea.SetActive(true); 
                
                priceText.text = cherryCost.ToString();

               
                if (GameManager.instance.GetTotalCherries() < cherryCost)
                {
                    button.interactable = false; 
                    
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
    }

    void TryUnlockSkill()
    {
        if (GameManager.instance.GetTotalCherries() >= cherryCost &&
            GameManager.instance.GetHighestLevelCompleted() >= levelRequired &&
            !GameManager.instance.IsSkillUnlocked(skillID))
        {
            GameManager.instance.SpendCherries(cherryCost);
            GameManager.instance.UnlockSkill(skillID);
            
            if (audioSource != null && purchaseSound != null)
            {
                audioSource.PlayOneShot(purchaseSound);
            }
            
            UpdateUI();
            
            TotalCherryUI[] allCounters = FindObjectsOfType<TotalCherryUI>();
            
            foreach (TotalCherryUI counter in allCounters)
            {
                counter.UpdateCherryText();
            }

            TotalCherryUI cherryCounter = FindObjectOfType<TotalCherryUI>();
            if (cherryCounter != null)
            {
                cherryCounter.UpdateCherryText();
            }
        }
    }
}