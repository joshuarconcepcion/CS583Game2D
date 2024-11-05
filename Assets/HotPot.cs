using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPot : MonoBehaviour
{
    public List<string> ingredientsInPot = new List<string>(); //list of ingredients in pot; will use to compare to customer order
    public GameObject currentIngredient;

    private AudioSource ingredientAddAudioSource; //drop ingredient in pot sound

    void Start()
    {

        ingredientAddAudioSource = GetComponent<AudioSource>();
    }

    public void AddIngredient()
    {
        if (currentIngredient != null)
        {
            Ingredient fridgeIngredient = currentIngredient.GetComponent<Ingredient>();
            if (fridgeIngredient != null)
            {
                ingredientsInPot.Add(fridgeIngredient.ingredientName); //adds ingredient from fridge to list
            }
            else
            {
                CupboardIngredient cupboardIngredient = currentIngredient.GetComponent<CupboardIngredient>();
                if (cupboardIngredient != null)
                {
                    ingredientsInPot.Add(cupboardIngredient.ingredientName); //adds broth to hot pot list
                }
            }

            Destroy(currentIngredient); //removes ingredient/broth in hand from scene (visually puts in pot)
            currentIngredient = null;

            if (ingredientAddAudioSource != null)
            {
                ingredientAddAudioSource.Play();
            }
        }
    }
}
