using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_EventManager
{
    public static event Action OnFridgeToggle;

    public static void FridgeToggle()
    {
         OnFridgeToggle?.Invoke();
    }

}