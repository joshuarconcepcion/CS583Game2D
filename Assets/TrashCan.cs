using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashCan : MonoBehaviour
{
    public GameObject currentIngredient;


    public void DiscardIngredient()
    {
        if (currentIngredient != null)
        {
            Destroy(currentIngredient); //discard ingredient
            currentIngredient = null; //set current ingredient to null
        }
    }

}
