using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public GameObject kitchenView;   
    public GameObject diningRoomView;  
    public GameObject fridgeUI;    
    private bool inKitchen = true;     

    public void ToggleView()
    {
        fridgeUI.SetActive(false); // Hide the fridge UI when switching views

        if (inKitchen)
        {
            kitchenView.SetActive(false); // Hide the kitchen view
            diningRoomView.SetActive(true); // Show the dining room view
            kitchenView.transform.Find("trashCan").gameObject.SetActive(false); // Hide the trash can
        }
        else
        {
            kitchenView.SetActive(true); // Show the kitchen view
            diningRoomView.SetActive(false); // Hide the dining room view
            kitchenView.transform.Find("trashCan").gameObject.SetActive(true); // Show the trash can
        }

        inKitchen = !inKitchen; // Toggle the view state
    }
}
