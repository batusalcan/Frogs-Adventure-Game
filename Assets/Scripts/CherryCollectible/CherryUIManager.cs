using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CherryUIManager : MonoBehaviour
{
    public static CherryUIManager instance;

    [SerializeField] private Text cherryText;
    private int cherryCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cherryCount = 0; // Sahne geçişinde sıfırla
        AttachToCanvas();
        UpdateUI();
    }

    private void AttachToCanvas()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null && transform.parent != canvas.transform)
        {
            transform.SetParent(canvas.transform, false);
        }

        if (cherryText == null)
        {
            Transform textObj = transform.Find("CherryText");
            if (textObj != null)
                cherryText = textObj.GetComponent<Text>();
        }
    }

    public void IncreaseCherryCount()
    {
        cherryCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (cherryText != null)
        {
            cherryText.text = cherryCount.ToString();
        }
        else
        {
            Debug.LogWarning("CherryText referansı yok, UI güncellenemedi.");
        }
    }
}