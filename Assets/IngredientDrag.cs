using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDrag : MonoBehaviour
{
    public GameObject fridgeUI;
    public GameObject cupboardUI;
    public TrashCan trashCan;
    public HotPot hotPot;
    public GameObject ingredientPrefab;
    public Canvas canvas;
    private bool isDragging = false;
    private RectTransform rectTransform;
    private Image image;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); //get the recttransform
        image = GetComponent<Image>(); //get the image (had to add this because the image was not showing up when dragging)
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            StartDragging(); //picks up the ingredient
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Make the ingredient follow the mouse
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out worldPoint);
            rectTransform.localPosition = worldPoint;

            // Set the current ingredient for both the trash can and hotpot
            trashCan.currentIngredient = gameObject;
            hotPot.currentIngredient = gameObject;
        }
    }

    void StartDragging()
    {
        if (fridgeUI != null && fridgeUI.activeSelf) 
        {
            fridgeUI.SetActive(false); //auto closes the fridge when you pick up an ingredient
        }
        if (cupboardUI != null && cupboardUI.activeSelf)
        {
            cupboardUI.SetActive(false); //auto closes the cupboard when you pick up a broth
        }

        //instantiates a new prefab of the ingredient so that you're not taking the one from the fridge (fridge resources should be unlimited in case you discard someting on accident)
        GameObject newIngredient = Instantiate(ingredientPrefab, canvas.transform); 
        IngredientDrag ingredientDragScript = newIngredient.GetComponent<IngredientDrag>(); 
        ingredientDragScript.fridgeUI = fridgeUI; 
        ingredientDragScript.cupboardUI = cupboardUI;
        ingredientDragScript.trashCan = trashCan;
        ingredientDragScript.hotPot = hotPot;
        ingredientDragScript.canvas = canvas;
        ingredientDragScript.isDragging = true;
        //newly instantiated prefab wasnt working so i had to look this up idk why i had to do this manually but it works now

        RectTransform newRectTransform = newIngredient.GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out worldPoint);
        newRectTransform.localPosition = worldPoint;
        newRectTransform.SetAsLastSibling();

        Image newIngredientImage = newIngredient.GetComponent<Image>();
        newIngredientImage.raycastTarget = false;
    }

    public void Discard()
    {
        Destroy(gameObject); 
        trashCan.currentIngredient = null; 
        hotPot.currentIngredient = null;  
    }
}

//note for tomorrow pls delete: newingredient prefab not working properly bc unity has a stick up it's ass