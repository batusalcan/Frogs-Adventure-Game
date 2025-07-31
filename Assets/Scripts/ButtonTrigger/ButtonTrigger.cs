using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorBlocker;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private Animator leverAnimator;
    [SerializeField] private Text doorMessageText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float holdDuration = 1.5f;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.CompareTag("Player"))
        {
            isActivated = true;

            // Lever animasyonu tetikle
            if (leverAnimator != null)
                leverAnimator.SetTrigger("Activate");

            // Kapıyı kaldır
            if (doorBlocker != null)
                doorBlocker.SetActive(false);

            // Ses efekti çal
            if (openSound != null && SoundManager.instance != null)
                SoundManager.instance.PlaySound(openSound);

            // Ekran mesajı göster
            if (doorMessageText != null)
                StartCoroutine(ShowDoorMessage());
        }
    }

    private IEnumerator ShowDoorMessage()
    {
        doorMessageText.text = "The door is open now";
        doorMessageText.enabled = true;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            doorMessageText.color = new Color(doorMessageText.color.r, doorMessageText.color.g, doorMessageText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        doorMessageText.color = new Color(doorMessageText.color.r, doorMessageText.color.g, doorMessageText.color.b, 1f);

        yield return new WaitForSeconds(holdDuration);

        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = 1f - (timer / fadeDuration);
            doorMessageText.color = new Color(doorMessageText.color.r, doorMessageText.color.g, doorMessageText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        doorMessageText.enabled = false;
    }
}
