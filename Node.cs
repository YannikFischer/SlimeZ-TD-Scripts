using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;

    public TankData TankData { get; set; }

    public void SetTurret(TankData tankData)
    {
        TankData = tankData;
    }

    public bool IsEmpty()
    {
        return TankData == null;
    }

    public void selectTurret()
    {
        OnNodeSelected?.Invoke(this);
    }
}
