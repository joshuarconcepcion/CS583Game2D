using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<string> orderList = new List<string>(); 
    public bool orderFulfilled = false; 

    public List<GameObject> brothPrefabs;
    public List<GameObject> ingredientPrefabs;
    public Transform[] ingredientSpawnPoints;
    public HotPotOrderManager hotPotOrderManager;

}
