using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    public List<GameObject> brothPrefabs; //list of broth prefabs
    public Transform brothSpawnPoint; //a single broth spawn point (orders should only use one broth)
    private Vector3 defaultScale = new Vector3(0.008f, 0.008f, 0.5f); //ingredients kept showing up super big so i set their scale manually

    public List<GameObject> ingredientPrefabs; //list of ingredient prefabs
    public List<Transform> ingredientSpawnPoints; //list of spawns for ingredients (multiple ingredients)

    private List<GameObject> currentIngredients = new List<GameObject>(); 
    public List<string> orderList = new List<string>(); //list attached to customer order
    private GameObject brothInstance; //instance of the broth prefab

    void Start()
    {
        GenerateAndDisplayOrder(startHidden: true); //had to add this otherwise order would show even in the kitchen in the beginning
    }

    public void GenerateAndDisplayOrder(bool startHidden = false)
    {
        ClearCurrentOrderDisplay(); //clears all order prefabs
        orderList.Clear(); //clears the order list

        if (brothPrefabs.Count > 0)
        {
            int brothIndex = Random.Range(0, brothPrefabs.Count); //random broth out of the 4 available
            GameObject selectedBroth = brothPrefabs[brothIndex];
            brothInstance = Instantiate(selectedBroth, brothSpawnPoint.position, Quaternion.identity, brothSpawnPoint); //instantiate the broth prefab
            brothInstance.transform.localScale = defaultScale; //manually set scale
            currentIngredients.Add(brothInstance); //tracking for when i need to clear the order

            CupboardIngredient brothIngredient = brothInstance.GetComponent<CupboardIngredient>(); 
            if (brothIngredient != null)
            {
                orderList.Add(brothIngredient.ingredientName); //get broth name and add to order list
            }
        }

        List<GameObject> shuffledIngredients = new List<GameObject>(ingredientPrefabs); 
        shuffledIngredients = ShuffleList(shuffledIngredients); //shuffles the ingredient list

        int ingredientsToAdd = Random.Range(6, Mathf.Min(12, shuffledIngredients.Count) + 1); //i wanted at 6 min and 12 max ingredients, but only 1 max of each
        for (int i = 0; i < ingredientsToAdd && i < ingredientSpawnPoints.Count; i++)
        {
            GameObject ingredient = shuffledIngredients[i];
            Transform spawnPoint = ingredientSpawnPoints[i]; //use spawn points from root to tail (before i added this, there were a bunch of empty spaces in between each ingredient)
            GameObject ingredientInstance = Instantiate(ingredient, spawnPoint.position, Quaternion.identity, spawnPoint); //instantiate the ingredient prefab
            ingredientInstance.transform.localScale = defaultScale; //manually set scale
            currentIngredients.Add(ingredientInstance); //tracking for when i need to clear the order


            Ingredient fridgeIngredient = ingredientInstance.GetComponent<Ingredient>();
            if (fridgeIngredient != null)
            {
                orderList.Add(fridgeIngredient.ingredientName); //get ingredient name and add to order list
            }
        }

        SetOrderVisibility(!startHidden); //added this because the order would show even in the kitchen in the beginning

    }
    //helper method to shuffle ingredient list
    private List<GameObject> ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }


    public void ClearCurrentOrderDisplay()
    {
        foreach (var item in currentIngredients)
        {
            Destroy(item); //clears all prefabs in the customer order
        }
        currentIngredients.Clear();
        brothInstance = null;
    }

    //used to hide the order when the player is in the kitchen 
    public void SetOrderVisibility(bool isVisible)
    {
        if (brothInstance != null)
        {
            brothInstance.SetActive(isVisible);
        }

        foreach (GameObject ingredient in currentIngredients)
        {
            ingredient.SetActive(isVisible);
        }
    }
}
