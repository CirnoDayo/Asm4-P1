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
    public List<Transform> spawningPlace;// Assign your customer prefab in the inspector


    public void InstantiateBeef()
    {
        Instantiate(Beef, spawningPlace[0].position, Quaternion.identity);
    }
    
    public void InstantiateFish()
    {
        Instantiate(Fish, spawningPlace[1].position, Quaternion.identity);
    }
    public void InstantiateCarrot()
    {
        Instantiate(Carrot, spawningPlace[2].position, Quaternion.identity);
    }
    public void InstantiateLettuce()
    {
        Instantiate(Lettuce, spawningPlace[3].position, Quaternion.identity);
    }
    public void InstantiateRice()
    {
        Instantiate(Rice, spawningPlace[4].position, Quaternion.identity);
    }
    public void InstantiateNoodles()
    {
        Instantiate(Noodles, spawningPlace[5].position, Quaternion.identity);
    }
}
