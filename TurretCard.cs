using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretCard : MonoBehaviour
{
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;
    
    public void SetupTurretButton(TurretShopTurrets turretShopTurrets)
    {
        turretImage.sprite = turretShopTurrets.TurretShopSprite;
        turretCost.text = turretShopTurrets.TurretShopCost.ToString();
    }
}
