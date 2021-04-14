using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Shop Settings")]
public class TurretShopTurrets : ScriptableObject
{
    public GameObject TurretPrefab;
    public int TurretShopCost;
    public Sprite TurretShopSprite;
}
