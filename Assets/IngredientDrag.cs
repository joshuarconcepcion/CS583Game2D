using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDrag : MonoBehaviour
{
    public GameObject fridgeUI;
    public TrashCan trashCan;
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
        StartDragging();
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
        }
    }

    void StartDragging()
    {
        fridgeUI.SetActive(false);
        GameObject newIngredient = Instantiate(ingredientPrefab, canvas.transform);
        IngredientDrag ingredientDragScript = newIngredient.GetComponent<IngredientDrag>();
        ingredientDragScript.fridgeUI = fridgeUI;
        ingredientDragScript.trashCan = trashCan;
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

    public void StopDragging()
    {
        isDragging = false;
        trashCan.currentIngredient = null;
    }
}