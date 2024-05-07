using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_PlaceholderScript : MonoBehaviour
{
    public GameObject Beef;
    public GameObject Fish;
    public GameObject Carrot;
    public GameObject Lettuce;
    public GameObject Rice;
    public GameObject Noodles;
    public Transform spawningPlace;// Assign your customer prefab in the inspector


    public void InstantiateBeef()
    {
        Instantiate(Beef, spawningPlace.position, Quaternion.identity);
    }
    
    public void InstantiateFish()
    {
        Instantiate(Fish, spawningPlace.position, Quaternion.identity);
    }
    public void InstantiateCarrot()
    {
        Instantiate(Carrot, spawningPlace.position, Quaternion.identity);
    }
    public void InstantiateLettuce()
    {
        Instantiate(Lettuce, spawningPlace.position, Quaternion.identity);
    }
    public void InstantiateRice()
    {
        Instantiate(Rice, spawningPlace.position, Quaternion.identity);
    }
    public void InstantiateNoodles()
    {
        Instantiate(Noodles, spawningPlace.position, Quaternion.identity);
    }
}
