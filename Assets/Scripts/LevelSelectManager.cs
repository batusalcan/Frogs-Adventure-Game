using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private RectTransform worldPanelsContainer; 
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private float slideDuration = 0.5f; 

    private int currentWorldIndex = 0;
    private int totalWorlds = 3; 
    private float panelWidth;
    private bool isSliding = false; 

    void Start()
    {
        
        if (worldPanelsContainer.childCount > 0)
        {
            
            panelWidth = Screen.width; 
        }
        else
        {
            Debug.LogError("WorldPanelsContainer içinde panel yok!");
            panelWidth = 1920f; 
        }


       
        leftArrowButton.onClick.AddListener(GoPreviousWorld);
        rightArrowButton.onClick.AddListener(GoNextWorld);

        
        UpdateArrowVisibility();
    }

    public void GoNextWorld()
    {
        if (isSliding || currentWorldIndex >= totalWorlds - 1) return; 
        currentWorldIndex++;
        StartCoroutine(SlideToTarget(-currentWorldIndex * panelWidth));
    }

    public void GoPreviousWorld()
    {
        if (isSliding || currentWorldIndex <= 0) return; 
        currentWorldIndex--;
        StartCoroutine(SlideToTarget(-currentWorldIndex * panelWidth));
    }


    private IEnumerator SlideToTarget(float targetX)
    {
        isSliding = true;
        UpdateArrowVisibility(); 

        Vector2 startPos = worldPanelsContainer.anchoredPosition;
        Vector2 targetPos = new Vector2(targetX, worldPanelsContainer.anchoredPosition.y);
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / slideDuration);
            
            t = Mathf.SmoothStep(0f, 1f, t);

            worldPanelsContainer.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null; 
        }

        
        worldPanelsContainer.anchoredPosition = targetPos;
        isSliding = false;
        UpdateArrowVisibility(); 
    }


 
    private void UpdateArrowVisibility()
    {
        
        if (isSliding)
        {
             leftArrowButton.interactable = false;
             rightArrowButton.interactable = false;
        }
        else
        {
             leftArrowButton.interactable = (currentWorldIndex > 0); 
             rightArrowButton.interactable = (currentWorldIndex < totalWorlds - 1); 
        }
    }

    
}