using UnityEngine;
using UnityEngine.UI; 


[RequireComponent(typeof(Text))] 
public class TotalCherryUI : MonoBehaviour
{
    private Text cherryText; 

    void Awake()
    {
        
        cherryText = GetComponent<Text>();
    }

 
    void OnEnable()
    {
        UpdateCherryText();
    }


    public void UpdateCherryText()
    {

        if (GameManager.instance != null)
        {
          
            cherryText.text = GameManager.instance.GetTotalCherries().ToString();
        }
        else
        {
            
            cherryText.text = "0"; 
        }
    }
}