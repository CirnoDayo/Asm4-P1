using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_FoodItemScript : MonoBehaviour
{
    private void Awake()
    {
        Akino_EventManager.instance.FridgeToggle();
    }
}
