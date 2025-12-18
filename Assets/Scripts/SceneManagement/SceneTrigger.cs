using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionDelay = 1f;
    [SerializeField] private AudioClip transitionSound;
    [SerializeField] private int levelToCompleteIndex;

    private AudioSource audioSource;
    private bool isLoading = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLoading = true;

            
            if (GameManager.instance != null)
            {
                GameManager.instance.SaveLevelCherriesToTotal();
            }

            
            if (levelToCompleteIndex > 0 && GameManager.instance != null)
            {
                GameManager.instance.CompleteLevel(levelToCompleteIndex);
            }
            
           
            Health playerHealth = other.GetComponent<Health>();
            if(playerHealth != null && playerHealth.currentHealth <= 0)
            {
                return; 
            }
           
            
            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("die"); 

            
            if (transitionSound != null && audioSource != null)
                audioSource.PlayOneShot(transitionSound);

            
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(sceneToLoad);
    }
}