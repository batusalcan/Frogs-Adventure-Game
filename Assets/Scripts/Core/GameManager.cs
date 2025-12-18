
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance { get; private set; }
   
   private const string CHERRY_KEY = "TotalCherries";
   private const string LEVEL_KEY = "HighestLevelCompleted";
   public int totalCherries { get; private set; }
   public int currentLevelCherries { get; private set; }
   
   private int highestLevelCompleted;
   private Dictionary<string,bool> unlockedSkills = new Dictionary<string, bool>();
   
   private const string PINEAPPLE_KEY = "TotalPineapples";
   public int totalPineapples { get; private set; }

   private void Awake()
   {
      
#if UNITY_EDITOR
      // Eğer oyunu Unity Editör'de "Play" tuşuna basarak başlatırsak,
      // testlerin temiz olması için tüm kayıtlı verileri SİL.
      PlayerPrefs.DeleteAll();
      Debug.Log("EDITÖR TEST MODU: Tüm PlayerPrefs verileri silindi.");
#endif
      
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else if (instance != null && instance != this)
      {
         Destroy(gameObject);
         return;
      }
      
      totalCherries = PlayerPrefs.GetInt(CHERRY_KEY, 0);
      highestLevelCompleted = PlayerPrefs.GetInt(LEVEL_KEY, 0);
      
      unlockedSkills["WallJump"] = PlayerPrefs.GetInt("WallJump", 0)==1;
      unlockedSkills["DoubleJump"] = PlayerPrefs.GetInt("DoubleJump", 0)==1;
      unlockedSkills["Dash"] = PlayerPrefs.GetInt("Dash", 0)==1;
      unlockedSkills["Shuriken_Finite"] = PlayerPrefs.GetInt("Shuriken_Finite", 0)==1;
      unlockedSkills["Shuriken_Infinite"] = PlayerPrefs.GetInt("Shuriken_Infinite", 0)==1;
      unlockedSkills["ExtraHealth_1"] = PlayerPrefs.GetInt("ExtraHealth_1", 0) == 1;
      unlockedSkills["ExtraHealth_2"] = PlayerPrefs.GetInt("ExtraHealth_2", 0) == 1;
      
      totalPineapples = PlayerPrefs.GetInt(PINEAPPLE_KEY, 0);
      
   }
   
   private void OnEnable()
   {
      SceneManager.sceneLoaded += OnSceneLoaded;
   }

   private void OnDisable()
   {
      SceneManager.sceneLoaded -= OnSceneLoaded;
   }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      currentLevelCherries = 0;
   }
   
   public void AddPineapple()
   {
      totalPineapples++;
      PlayerPrefs.SetInt(PINEAPPLE_KEY, totalPineapples);
      PlayerPrefs.Save();
      Debug.Log("Pineapple collected Total: " + totalPineapples);
   }

   public int GetPineappleCount()
   {
      return totalPineapples;
   }
   

   public int GetTotalCherries()
   {
      return totalCherries;
   }

   public void AddCherries(int amount)
   {
      if (amount < 0)
         return;

      totalCherries += amount;
      PlayerPrefs.SetInt(CHERRY_KEY, totalCherries);
      PlayerPrefs.Save();
   }
   
   public void AddCherryTemporarily()
   {
      currentLevelCherries++;
   }
   
   public void SaveLevelCherriesToTotal()
   {
      totalCherries += currentLevelCherries;
      currentLevelCherries = 0;
      PlayerPrefs.SetInt(CHERRY_KEY, totalCherries);
      PlayerPrefs.Save();
   }

   public bool SpendCherries(int amountToSpend)
   {
      if (amountToSpend >= totalCherries)
      {
         Debug.Log("Not enough cherries");
         return false;
         
      }
      
      totalCherries -= amountToSpend;
      PlayerPrefs.SetInt(CHERRY_KEY, totalCherries);
      PlayerPrefs.Save();
      return true;
   }

   public void CompleteLevel(int levelIndex)
   {
      if (levelIndex > highestLevelCompleted)
      {
         highestLevelCompleted = levelIndex;
         PlayerPrefs.SetInt(LEVEL_KEY, highestLevelCompleted);
         PlayerPrefs.Save();
      }
   }

   public int GetHighestLevelCompleted()
   {
      return highestLevelCompleted;
   }

   public void UnlockSkill(string skillID)
   {
      if (!unlockedSkills.ContainsKey(skillID))
      {
         Debug.LogWarning("Skill " + skillID + " does not exist");
         return;
      }
      
      unlockedSkills[skillID] = true;
      PlayerPrefs.SetInt(skillID, 1);
      PlayerPrefs.Save();
   }

   public bool IsSkillUnlocked(string skillID)
   {
      if (!unlockedSkills.ContainsKey(skillID))
      {
         Debug.LogWarning("Skill " + skillID + " does not exist");
         return false;
      }

      return unlockedSkills[skillID];
   }
}
