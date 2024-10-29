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

        fridgeUI.SetActive(false);
        
        if (inKitchen)
        {
            kitchenView.SetActive(false);    
            diningRoomView.SetActive(true);   
        }
        else
        {
            kitchenView.SetActive(true);       
            diningRoomView.SetActive(false);   
        }

        inKitchen = !inKitchen;
    }
}
