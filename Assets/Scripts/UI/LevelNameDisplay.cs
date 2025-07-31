using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelNameDisplay : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float holdDuration = 1.5f;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        levelText.text = sceneName.Replace("Level", "LEVEL "); 
        levelText.enabled = true;
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 0); 

        StartCoroutine(ShowLevelName());
    }

    private IEnumerator ShowLevelName()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 1f);
        yield return new WaitForSeconds(holdDuration);

        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = 1f - (timer / fadeDuration);
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        levelText.enabled = false;
    }
}