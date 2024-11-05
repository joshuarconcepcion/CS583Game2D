using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs; //list for customer prefabs
    public Transform[] spawnPoints; //array for spawn points
    public int maxCustomers = 1; //i have this set to 1 but changed it in game because an new order would show up, but not a new customer prefab (idk if i still need this here probs not tho)
    private List<GameObject> activeCustomers = new List<GameObject>();
    public OrderSpawner orderSpawner; 

    void Start()
    {
        SpawnCustomer(); //spawn customer at start
    }

    public void SpawnCustomer()
    {
        if (activeCustomers.Count >= maxCustomers) return;

        int randomIndex = Random.Range(0, customerPrefabs.Count); //picks random customer prefab
        GameObject customerPrefab = customerPrefabs[randomIndex];
        Transform spawnPoint = spawnPoints[randomIndex]; //each spawnpoint has the same index as associated prefab (some prefabs were taller/different size so each had to have their own spawn)

        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity, spawnPoint); //instantiate the prefab

        activeCustomers.Add(newCustomer);

        Customer customerScript = newCustomer.GetComponent<Customer>();
        orderSpawner.GenerateAndDisplayOrder();
    }

    /*public void RemoveCustomer(GameObject customer) 
    {
        activeCustomers.Remove(customer);
        Destroy(customer);

        StartCoroutine(SpawnNewCustomerWithDelay());
    } */ // isnt working rn; putting it in hotpotordermanager instead to just destroy the customer prefab, will revisit if that doesnt work


}
