using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private int levelRequiredToUnlock;
    
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color unlockedColor = Color.white;
    

    private Button button;
    private Image buttonImage;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        int highestLevel = GameManager.instance.GetHighestLevelCompleted();

        if (highestLevel >= levelRequiredToUnlock)
        {
            button.interactable = true;
            buttonImage.color = unlockedColor;
            button.onClick.AddListener(LoadLevel);
        }
        else
        {
            button.interactable = false;
            buttonImage.color = lockedColor;
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
