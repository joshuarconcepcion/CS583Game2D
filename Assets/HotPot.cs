using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPot : MonoBehaviour
{
    public List<string> ingredientsInPot = new List<string>(); // Track ingredients added to the pot
    public GameObject currentIngredient;

    public void AddIngredient()
    {
        if (currentIngredient != null)
        {
            // Record the ingredient name
            Ingredient ingredient = currentIngredient.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                ingredientsInPot.Add(ingredient.ingredientName); // Add to list
            }

            // Destroy the ingredient in hand, similar to the trash can
            Destroy(currentIngredient);
            currentIngredient = null;
        }
    }
}
