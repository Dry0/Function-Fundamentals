using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    void Start()
    {
        TestAllSystems();
    }

    public int CalculateExperience(int baseXp, float difficultyMultiplier, int bonusXp = 0)
   {
       // Bereken de totale XP
       int totalXp = (int)(baseXp * difficultyMultiplier) + bonusXp;

       // Geef het resultaat terug
       return totalXp;
   }

   public (int level, float percentageToNextLevel) UpdateLevel(int currentExperience, int experiencePerLevel)
   {
       // Bereken het huidige level 
       int level = currentExperience / experiencePerLevel;
       
       // Bereken het percentage naar het volgende level
       int experienceIntoCurrentLevel = currentExperience % experiencePerLevel;
       float percentageToNextLevel = (float)experienceIntoCurrentLevel / experiencePerLevel * 100;
       
       // Geef beide waarden terug als een tuple
       return (level, percentageToNextLevel);
   }

   public float CalculateStats(int baseStrength, int level, float growthFactor, bool includeEquipment = false, int equipmentBonus = 0)
   {
       // Bereken de basiswaarde
       float finalValue = baseStrength + (level * growthFactor);

       // Voeg de equipment bonus toe als includeEquipment true is
       if (includeEquipment)
       {
           finalValue += equipmentBonus;
       }
       
       // Rond het resultaat af op 1 decimaal
       return (float)Math.Round(finalValue, 1);
   }

   public void TestAllSystems()
   {
       Debug.Log("==== Start Testing All Systems ====");
       
       // Test 1: Experience Berekening
       Debug.Log("== Test 1: CalculateExperience ==");
       Debug.Log("Scenario 1: Base XP 100 , Difficulty 1.5, No Bonus");
       int xp1 = CalculateExperience(100, 1.5f);
       Debug.Log("Result: " + xp1); // verwacht 150 

       Debug.Log("Scenario 2: Base XP 200, Difficulty 2, Bonus 50");
       int xp2 = CalculateExperience(200, 2.0f, 50);
       Debug.Log("Result: " + xp2); // Verwacht: 450
       
       // Test 2: Level Progress Tracking
       Debug.Log("== Test 2: UpdateLevel ==");
       Debug.Log("Scenario 1: Experience 450, Per level 100");
       var level1 = UpdateLevel(450, 100);
       Debug.Log($"Result: Level {level1.level}, Percentage {level1.percentageToNextLevel}%"); // Verwacht: Level 4, 50%
       
       Debug.Log("Scenario 2: Experience 1234, Per Level 250");
       var level2 = UpdateLevel(1234, 250);
       Debug.Log($"Result: Level {level2.level}, Percentage {level2.percentageToNextLevel}%"); // Verwacht: Level 4, 93.6%
       
       // Test 3: Statistiek Berekening
       Debug.Log("== Test 3: CalculateStats ==");
       Debug.Log("Scenario 1: Base 50, Level 10, Growth 2.5, No Equipment");
       float stat1 = CalculateStats(50, 10, 2.5f );
       Debug.Log("Result: " + stat1); // Verwacht: 75.0
       
       Debug.Log("Scenario 2: Base 50, Level 10, Growth 2.5, With Equipment 15");
       float stat2 = CalculateStats(50, 10, 2.5f, true, 15);
       Debug.Log("Result: " + stat2); // Verwacht: 90.0

       Debug.Log("==== End Testing All Systems ====");
   }
   
}
