using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardInteraction : MonoBehaviour
{
    public GameObject cupboardPanel; //cupboard panel gameobject
    public GameObject kitchenView;

    void OnMouseDown()
    {
   
        if (kitchenView.activeSelf)
        {
            cupboardPanel.SetActive(!cupboardPanel.activeSelf); //if in kitchenview, toggle cupboard panel when clicked
        }
    }
}
