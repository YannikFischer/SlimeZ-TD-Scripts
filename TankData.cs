using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankLevel
{
    public int cost;
    public GameObject display;
    public GameObject displaySkin;
    public GameObject bullet;
    public float fireRate;
}

public class TankData : MonoBehaviour
{
    public List<TankLevel> level;
    public TankLevel currentLevel;

    private void OnEnable()
    {
        currentLevel = level[0];
        SetTankLevel(currentLevel);
    }

    public TankLevel GetTankLevel()
    {
        return currentLevel;
    } 

    public void SetTankLevel(TankLevel tankLevel)
    {
        currentLevel = tankLevel;

        int currentLevelIndex = level.IndexOf(currentLevel);

        //Das Level dass wir wissen wollen, nehmen wir aus der Liste "level" heraus die wie oben erstellt haben
        GameObject levelDisplay = level[currentLevelIndex].display;

        //diese Schleife Aktiviert bzw deaktiviert Tanks, das kontrollieren wir mit der Liste 
        for (int i = 0; i < level.Count; i++)
        {
            if (levelDisplay != null)
            {
                if (i == currentLevelIndex)
                {
                    //Aktiviert Tanks, sodass zb. Level 3 sichtbar wird
                    level[i].display.SetActive(true);
                }
                else
                {
                    //Deaktiviert Tanks, also zb. deaktiviert 2 wenn man auf Level 3 upgraded
                    level[i].display.SetActive(false);
                }
            }
        }

    }

    public void IncreaseLevel()     //erhöht Level des Tanks
    {
        int currentLevelIndex = level.IndexOf(currentLevel);
        if(currentLevelIndex < level.Count - 1)
        {
            currentLevel = level[currentLevelIndex + 1];
            SetTankLevel(currentLevel);
        }
    }

    public TankLevel GetNextTankLevel()     //checkt ob das jetzige Level schon das Max Level ist oder ob er nochmal upgraden kann
    {
        int currentLevelIndex = level.IndexOf(currentLevel);
        int maxLevelIndex = level.Count - 1;

        if (currentLevelIndex < maxLevelIndex)
        {
            return level[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void OnSkinSelection(TankLevel _tankLevel)
    {
        _tankLevel.display = _tankLevel.displaySkin;
    }
}
