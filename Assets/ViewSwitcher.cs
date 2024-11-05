using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public GameObject kitchenView;
    public GameObject diningRoomView;
    public GameObject fridgeUI;
    public GameObject finishOrderButton;

    public bool inKitchen = true;
    private OrderSpawner orderSpawner;
    private AudioSource audioSource; //sound effect when switch view button is clicked
    public AudioSource diningRoomAudioSource; //ambient chatting in dining room

    void Start()
    {
        orderSpawner = FindObjectOfType<OrderSpawner>();
        audioSource = GetComponent<AudioSource>(); 

        
        if (!inKitchen && diningRoomAudioSource != null) //if not in kitchen, play dining room audio
        {
            diningRoomAudioSource.Stop();
        }

        if (!inKitchen)
        {
            finishOrderButton.SetActive(false); //if not in kitchen, finish order button is not visible
            SetOrderVisibilityInDiningRoom(true); 
        }
        else
        {
            SetOrderVisibilityInDiningRoom(false); //i had trouble hiding the order in dining room when in kitchen, so i just make it invisible altogether if not in the dining room
        }
    }

    public void ToggleView()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        fridgeUI.SetActive(false); //hide fridge UI automatically when switching view

        if (inKitchen)
        {
            kitchenView.SetActive(false);
            diningRoomView.SetActive(true);
            finishOrderButton.SetActive(false);

            SetOrderVisibilityInDiningRoom(true); //show customer order in dining room

            if (diningRoomAudioSource != null && !diningRoomAudioSource.isPlaying)
            {
                diningRoomAudioSource.Play();
            }
        }
        else
        {
            kitchenView.SetActive(true);
            diningRoomView.SetActive(false);
            finishOrderButton.SetActive(true);

            SetOrderVisibilityInDiningRoom(false);

            if (diningRoomAudioSource != null && diningRoomAudioSource.isPlaying)
            {
                diningRoomAudioSource.Stop();
            }
        }

        inKitchen = !inKitchen;
    }

    private void SetOrderVisibilityInDiningRoom(bool visible) //show customer order in dining room
    {
        if (orderSpawner != null)
        {
            orderSpawner.SetOrderVisibility(visible);
        }
    }
}
