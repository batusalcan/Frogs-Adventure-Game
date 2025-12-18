using UnityEngine;
using UnityEngine.SceneManagement; 

public class PineappleCollectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioClip collectSound;
    
   
    [Tooltip("Define a unique number for all pineapples in this level (1, 2, 3...)")]
    [SerializeField] private string pineappleID; 

    private bool isCollected = false;
    private string saveKey; 

    private void Start()
    {
        
        saveKey = "Collected_" + SceneManager.GetActiveScene().name + "_" + pineappleID;

       
        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;

        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            
            if(collectSound != null) SoundManager.instance.PlaySound(collectSound);
            
            
            GameManager.instance.AddPineapple();

            
            PlayerPrefs.SetInt(saveKey, 1);
            PlayerPrefs.Save();

            
            PineappleUI ui = FindObjectOfType<PineappleUI>();
            if (ui != null)
            {
                ui.UpdatePineappleText();
            }

            GetComponent<Animator>()?.SetTrigger("Collected");
            Destroy(gameObject, 0.5f); 
        }
    }
}