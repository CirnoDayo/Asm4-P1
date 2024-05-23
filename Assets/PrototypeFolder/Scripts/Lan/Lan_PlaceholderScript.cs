using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lan_PlaceholderScript : MonoBehaviour
{
    public GameObject ingredientsMenu;
    public Button[] buttonIndex;
    
    public GameObject Beef;
    public GameObject Fish;
    public GameObject Lettuce;
    public GameObject Noodles;
    public GameObject Rice;
    public GameObject Carrot;

    Akino_UIManager UIManager;
    private void Start()
    {
        UIManager = GetComponent<Akino_UIManager>();
        buttonIndex = ingredientsMenu.GetComponentsInChildren<Button>();
    }

    public void Input(string message)
    {
        switch (message)
        {
            case "Beef": InstantiateBeef(); break;
            case "Fish": InstantiateFish(); break;
            case "Lettuce": InstantiateLettuce(); break;
            case "Noodles": InstantiateNoodles(); break;
            case "Rice": InstantiateNoodles(); break;
            case "Carrot": InstantiateNoodles(); break;

        }
    }

    public void InstantiateBeef()
    {
        Instantiate(Beef, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }
    
    public void InstantiateFish()
    {
        Instantiate(Fish, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }

    public void InstantiateLettuce()
    {
        Instantiate(Lettuce, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }

    public void InstantiateNoodles()
    {
        Instantiate(Noodles, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }
    public void InstantiateRice()
    {
        Instantiate(Rice, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }
    public void InstantiateCarrot()
    {
        Instantiate(Carrot, Vector2.zero, Quaternion.identity);
        Akino_EventManager.FridgeToggle();
    }
}
