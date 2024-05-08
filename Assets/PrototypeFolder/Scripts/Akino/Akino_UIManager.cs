using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_UIManager : MonoBehaviour
{
    public GameObject IngredientsMenu;

    private void Start()
    {
        Akino_EventManager.instance.OnFridgeInteract += ToggleIngredients;
    }

    private void ToggleIngredients()
    {
        IngredientsMenu.SetActive(!IngredientsMenu.activeSelf);
    }
}
