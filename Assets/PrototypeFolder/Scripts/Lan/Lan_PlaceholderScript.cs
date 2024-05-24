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

    public List<Lan_CounterSlot> counterSlots = new List<Lan_CounterSlot>(); // List of CounterSlot scripts attached to each slot
    public bool[] slotAvailability; // Array to keep track of slot availability

    Akino_UIManager UIManager;

    private void Start()
    {
        UIManager = GetComponent<Akino_UIManager>();
        buttonIndex = ingredientsMenu.GetComponentsInChildren<Button>();
    }

    public void RegisterSlot(Lan_CounterSlot slot)
    {
        
        if (slotAvailability == null || slotAvailability.Length < counterSlots.Count)
        {
            slotAvailability = new bool[counterSlots.Count];
        }
        slotAvailability[slot.slotIndex] = true; // All slots are initially free
    }

    public void Input(string message)
    {
        switch (message)
        {
            case "Beef": InstantiateBeef(); break;
            case "Fish": InstantiateFish(); break;
            case "Lettuce": InstantiateLettuce(); break;
            case "Noodles": InstantiateNoodles(); break;
            case "Rice": InstantiateRice(); break;
            case "Carrot": InstantiateCarrot(); break;
        }
    }

    public void InstantiateBeef()
    {
        InstantiateIngredient(Beef);
    }

    public void InstantiateFish()
    {
        InstantiateIngredient(Fish);
    }

    public void InstantiateLettuce()
    {
        InstantiateIngredient(Lettuce);
    }

    public void InstantiateNoodles()
    {
        InstantiateIngredient(Noodles);
    }

    public void InstantiateRice()
    {
        InstantiateIngredient(Rice);
    }

    public void InstantiateCarrot()
    {
        InstantiateIngredient(Carrot);
    }

    private void InstantiateIngredient(GameObject ingredient)
    {
        int freeSlotIndex = GetFreeSlotIndex();
        if (freeSlotIndex == -1)
        {
            Debug.LogWarning("All counter slots are full. Cannot instantiate more ingredients.");
            return;
        }

        // Instantiate the ingredient at the position of the free slot
        Instantiate(ingredient, counterSlots[freeSlotIndex].transform.position, Quaternion.identity);
        slotAvailability[freeSlotIndex] = false; // Mark the slot as occupied
        Akino_EventManager.FridgeToggle();
    }

    private int GetFreeSlotIndex()
    {
        for (int i = 0; i < slotAvailability.Length; i++)
        {
            if (slotAvailability[i])
            {
                Debug.Log(i);
                return i;
            }
        }
        return -1; // No free slot available
    }

    public void SetSlotAvailability(int index, bool isAvailable)
    {
        if (index >= 0 && index < slotAvailability.Length)
        {
            slotAvailability[index] = isAvailable;
        }
    }
}
