using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_UIManager : MonoBehaviour
{
    public GameObject ingredientsMenu;

    private void Start()
    {
        Akino_EventManager.instance.OnFridgeToggle += ToggleIngredientsMenu;
    }

    public void ToggleIngredientsMenu()
    {
        ingredientsMenu.SetActive(!ingredientsMenu.activeSelf);
        Akino_ControlsManager.inMenu = ingredientsMenu.activeSelf;
    }
}
