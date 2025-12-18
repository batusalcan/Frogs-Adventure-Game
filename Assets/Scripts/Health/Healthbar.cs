using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        if (playerHealth == null)
        {
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
            }
            else
            {
                Debug.LogError("Healthbar: Sahnede 'Player' etiketli obje bulunamadı!");
            }
        }
        
        if (playerHealth != null)
        {
            totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }
}
