using UnityEngine;
using UnityEngine.UI;

public class PineappleUI : MonoBehaviour
{
    [SerializeField] private Text pineappleText; 

    private void OnEnable()
    {
        UpdatePineappleText();
    }


    public void UpdatePineappleText()
    {
        if (GameManager.instance != null)
        {
            
            int currentCount = GameManager.instance.GetPineappleCount();
            pineappleText.text = currentCount.ToString() + "/9";
        }
        else
        {
            pineappleText.text = "0/9";
        }
    }
}