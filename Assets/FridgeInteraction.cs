using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInteraction : MonoBehaviour
{
    public GameObject ingredientPanel; 
    public GameObject kitchenView;
    private AudioSource audioSource; //fridge door opening sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void OnMouseDown()
    {
   
        if (kitchenView.activeSelf)
        {
            ingredientPanel.SetActive(!ingredientPanel.activeSelf);  //auto closes the fridge UI when switching view

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
