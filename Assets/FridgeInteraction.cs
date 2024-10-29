using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInteraction : MonoBehaviour
{
    public GameObject ingredientPanel;    
    public GameObject kitchenView;     

    void OnMouseDown()
    {
   
        if (kitchenView.activeSelf)
        {
            ingredientPanel.SetActive(!ingredientPanel.activeSelf);  
        }
    }
}
