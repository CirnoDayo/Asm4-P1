using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_UIManager : MonoBehaviour
{
    public GameObject ingredientsMenu;

    private void Start()
    {
        Akino_EventManager.instance.OnFridgeEnable += EnableIngredientsMenu;
        Akino_EventManager.instance.OnFridgeDisable += DisableIngredientsMenu;
    }

    public void EnableIngredientsMenu()
    {
        ingredientsMenu.SetActive(true);
        Akino_ControlsManager.inMenu = ingredientsMenu.activeSelf;
    }

    public void DisableIngredientsMenu()
    {
        
        ingredientsMenu.SetActive(false);
        Akino_ControlsManager.inMenu = ingredientsMenu.activeSelf;
    }
}
