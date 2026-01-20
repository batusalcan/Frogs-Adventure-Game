using UnityEngine;
using System.Collections.Generic;

public class DemoModeController : MonoBehaviour
{
    // YEDEKLEME DEĞİŞKENLERİ
    private int backupCherries;
    private int backupLevel;
    private Dictionary<string, int> backupSkills = new Dictionary<string, int>();

    // AÇILACAK SKILLERİN LİSTESİ (GameManager'daki isimlerle AYNI olmalı)
    private string[] allSkillKeys = new string[] 
    { 
        "WallJump", 
        "DoubleJump", 
        "Dash", 
        "Shuriken_Finite", 
        "Shuriken_Infinite", 
        "ExtraHealth_1", 
        "ExtraHealth_2" 
    };

    void Awake()
    {
        // 1. MEVCUT GERÇEK DURUMU YEDEKLE
        BackupSaveData();

        // 2. HİLELERİ AKTİF ET (Her şeyi aç)
        ActivateDemoMode();
    }

    void OnDestroy()
    {
        // 3. SAHNEDEN ÇIKARKEN ESKİ HALİNE DÖNDÜR
        RestoreSaveData();
        
        // 4. KİRLENMİŞ GAMEMANAGER'I YOK ET
        // Bunu yapıyoruz ki menüye dönünce temiz verilerle yeni bir GameManager oluşsun.
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
    }

    private void BackupSaveData()
    {
        // Parayı yedekle
        backupCherries = PlayerPrefs.GetInt("TotalCherries", 0);
        // Leveli yedekle
        backupLevel = PlayerPrefs.GetInt("HighestLevelCompleted", 0);

        // Skilleri yedekle
        foreach (string skill in allSkillKeys)
        {
            backupSkills[skill] = PlayerPrefs.GetInt(skill, 0);
        }
        
        Debug.Log("DEMO: Gerçek veriler yedeklendi.");
    }

    private void ActivateDemoMode()
    {
        if (GameManager.instance == null) return;

        // 9999 Para ekle (Mevcut paranın üzerine ekler ama sorun değil, restore edeceğiz)
        GameManager.instance.AddCherries(9999);

        // Tüm skilleri aç
        foreach (string skill in allSkillKeys)
        {
            GameManager.instance.UnlockSkill(skill);
        }

        Debug.Log("DEMO MODU AKTİF: Her şey açıldı!");
    }

    private void RestoreSaveData()
    {
        // Parayı eski haline getir
        PlayerPrefs.SetInt("TotalCherries", backupCherries);
        
        // Leveli eski haline getir
        PlayerPrefs.SetInt("HighestLevelCompleted", backupLevel);

        // Skilleri eski haline getir
        foreach (string skill in allSkillKeys)
        {
            // Eğer skill yedekte 1 ise 1, 0 ise 0 olarak kaydet
            PlayerPrefs.SetInt(skill, backupSkills[skill]);
        }

        // Değişiklikleri diske yaz
        PlayerPrefs.Save();
        
        Debug.Log("DEMO BİTTİ: Veriler eski haline döndürüldü.");
    }
}