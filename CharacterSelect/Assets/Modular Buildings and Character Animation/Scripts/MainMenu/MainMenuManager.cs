using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<CharItem> listOfCharItems;

    private void Start()
    {
        foreach (CharItem item in listOfCharItems)
        {
            item.Enlarge(0.2f);
        }
    }
}
