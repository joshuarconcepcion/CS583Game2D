using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDrag : MonoBehaviour
{
    public GameObject fridgeUI;
    public TrashCan trashCan;
    public HotPot hotPot; // Reference to the HotPot script
    public GameObject ingredientPrefab;
    public Canvas canvas;
    private bool isDragging = false;
    private RectTransform rectTransform;
    private Image image;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            StartDragging();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out worldPoint);
            rectTransform.localPosition = worldPoint;
            trashCan.currentIngredient = gameObject;
            hotPot.currentIngredient = gameObject; // Also set the current ingredient for the hot pot
        }
    }

    void StartDragging()
    {
        fridgeUI.SetActive(false);
        GameObject newIngredient = Instantiate(ingredientPrefab, canvas.transform);
        IngredientDrag ingredientDragScript = newIngredient.GetComponent<IngredientDrag>();
        ingredientDragScript.fridgeUI = fridgeUI;
        ingredientDragScript.trashCan = trashCan;
        ingredientDragScript.hotPot = hotPot; // Assign the hot pot reference
        ingredientDragScript.canvas = canvas;
        ingredientDragScript.isDragging = true;

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
        Destroy(gameObject); // Destroy the ingredient in hand
        trashCan.currentIngredient = null; // Clear the reference in TrashCan
        hotPot.currentIngredient = null; // Clear the reference in HotPot
    }
}