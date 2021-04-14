using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlacement : MonoBehaviour
{
    public GameObject tankPrefab;
    public GameObject tankPrefabSkin;
    private GameObject tank;

    public static TankPlacement tankPlacement;

    //OnMouseUp machts dass sobald man drückt und wieder loslässt passiert was
    private void OnMouseUp()
    {
        tankPlacement = this;
        //OnSkinSelection();

        //checkt ob er genug coins hat um den Level 1 Tank zu placen
        if (CanPlaceTank())
        {
            //OnSkinSelection();
            //GameManager.singleton.OnSkinSelect();

            int cost = tankPrefab.GetComponent<TankData>().level[0].cost;

            //hat man genug Coins?
            if (GameManager.singleton.GetCoins() >= cost)
            {
                tank = Instantiate(tankPrefab, transform.position, Quaternion.identity);

                //setzt die Coins auf die vorherigen Coins minus die Tank Kosten.
                GameManager.singleton.SetCoins(GameManager.singleton.GetCoins() - tank.GetComponent<TankData>().currentLevel.cost);
            }     
        }
        //checkt ob er genug coins hat um den Level 1 Tank zu placen
        else if (CanUpgradeTank())
        {
            int cost = tank.GetComponent<TankData>().GetNextTankLevel().cost;

            //hat man genug Coins?
            if (GameManager.singleton.GetCoins() >= cost)
            {
                tank.GetComponent<TankData>().IncreaseLevel();

                //setzt die Coins auf die vorherigen Coins minus die Tank Kosten.
                GameManager.singleton.SetCoins(GameManager.singleton.GetCoins() - tank.GetComponent<TankData>().currentLevel.cost);
            }
        }
    }
    
    //checkt ob man einen Tank platzieren kann, falls es schon einen gibt kann kein zweiter erstellt werden.
    private bool CanPlaceTank() 
    {
        if (tank == null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool CanUpgradeTank()       //kann er upgraden?
    {
        if (tank != null)
        {
            TankData data = tank.GetComponent<TankData>();
            TankLevel nextLevel = data.GetNextTankLevel();

            if (nextLevel != null)
            {
                return true;
            }
        }

        return false;
    }

    
    public void OnSkinSelection()
    {
        tankPrefab = tankPrefabSkin;
    }
    
}
