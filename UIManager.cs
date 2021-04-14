﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject TurretShop;

    private Node _currentNodeSelected;

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;

        if (_currentNodeSelected.IsEmpty())
        {
            TurretShop.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
