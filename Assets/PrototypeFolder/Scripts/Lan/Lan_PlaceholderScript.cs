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

    Akino_UIManager UIManager;
    private void Start()
    {
        UIManager = GetComponent<Akino_UIManager>();
        buttonIndex = ingredientsMenu.GetComponentsInChildren<Button>();
    }

    public void Input(string message)
    {
        Akino_EventManager.instance.FridgeDisable();
        switch (message)
        {
            case "Beef": InstantiateBeef(); break;
            case "Fish": InstantiateFish(); break;
            case "Lettuce": InstantiateLettuce(); break;
            case "Noodles": InstantiateNoodles(); break;
        }
    }

    public void InstantiateBeef()
    {
        Instantiate(Beef, Vector2.zero, Quaternion.identity);

    }
    
    public void InstantiateFish()
    {
        Instantiate(Fish, Vector2.zero, Quaternion.identity);
    }

    public void InstantiateLettuce()
    {
        Instantiate(Lettuce, Vector2.zero, Quaternion.identity);
    }

    public void InstantiateNoodles()
    {
        Instantiate(Noodles, Vector2.zero, Quaternion.identity);
    }
}
