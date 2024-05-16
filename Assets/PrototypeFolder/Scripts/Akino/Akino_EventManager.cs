using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_EventManager : MonoBehaviour
{
    public static Akino_EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action OnFridgeEnable;
    public event Action OnFridgeDisable;

    public void FridgeEnable()
    {
         OnFridgeEnable();
    }

    public void FridgeDisable()
    {
        OnFridgeDisable();
    }
}