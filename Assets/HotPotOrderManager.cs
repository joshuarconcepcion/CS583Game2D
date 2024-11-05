using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotOrderManager : MonoBehaviour
{
    public GameObject completedHotPotPrefab;
    public Transform completedHotPotSpawnPoint;
    public GameObject sendOrderButtonPrefab;
    public Transform sendOrderButtonSpawnPoint;
    public HotPot hotPot;
    public CustomerSpawner customerSpawner;

    private CompletedHotPot completedHotPotScript;
    private GameObject sendOrderButtonInstance;

    private AudioSource finishOrderAudioSource; 
    private AudioSource sendOrderAudioSource; 

    void Start()
    {

        AudioSource[] audioSources = GetComponents<AudioSource>();
        finishOrderAudioSource = audioSources[0]; //for finish order button
        sendOrderAudioSource = audioSources[1]; //for send order button
    }

    public void FinishOrder()
    {
        if (finishOrderAudioSource != null)
        {
            finishOrderAudioSource.Play(); //play kitchen bell ding
        }

        if (completedHotPotPrefab != null && completedHotPotSpawnPoint != null && hotPot != null)
        {
            GameObject completedHotPotInstance = Instantiate(completedHotPotPrefab, completedHotPotSpawnPoint.position, Quaternion.identity, completedHotPotSpawnPoint); //instantiate completed hotpot prefab
            completedHotPotInstance.transform.localScale = Vector3.one;
            completedHotPotInstance.transform.localPosition = Vector3.zero;

            completedHotPotScript = completedHotPotInstance.GetComponent<CompletedHotPot>(); 
            if (completedHotPotScript != null)
            {
                completedHotPotScript.SetOrderList(new List<string>(hotPot.ingredientsInPot)); //copies over the list from the hotpot to the completed hotpot
                hotPot.ingredientsInPot.Clear(); //clears the original hotpot list for the next order
            }

            if (sendOrderButtonPrefab != null && sendOrderButtonSpawnPoint != null)
            {
                sendOrderButtonInstance = Instantiate(sendOrderButtonPrefab, sendOrderButtonSpawnPoint.position, Quaternion.identity, sendOrderButtonSpawnPoint);
                sendOrderButtonInstance.GetComponent<SendOrderButton>().Initialize(this, customerSpawner); //initialize the send order button prefab
            }
        }
    }

    public void SendOrder()
    {
        if (sendOrderAudioSource != null)
        {
            sendOrderAudioSource.Play();
        }

        Customer currentCustomer = FindObjectOfType<Customer>(); //find the current customer
        if (completedHotPotScript != null && currentCustomer != null)
        {
            int score = CalculateScore(completedHotPotScript.orderList, currentCustomer.orderList); //calculate score based on correct ingredients
            ScoreManager.instance.AddScore(score);

            Destroy(currentCustomer.gameObject); //once you send the order, customer leaves (destroy customer prefab)
            Destroy(completedHotPotScript.gameObject); //same for the completed hotpot prefab. clear it for the next order
            Destroy(sendOrderButtonInstance); //send order button should only appear once you finish order, so need to destroy it before starting next order

            OrderSpawner orderSpawner = FindObjectOfType<OrderSpawner>();
            if (orderSpawner != null)
            {
                orderSpawner.ClearCurrentOrderDisplay(); //clears the order display so i can generate new order
            }

            customerSpawner.SpawnCustomer();
            if (orderSpawner != null)
            {
                bool isInKitchen = FindObjectOfType<ViewSwitcher>().inKitchen; //check if in kitchen (had to add this because it would keep showing the subsequent order in the kitchen)
                orderSpawner.GenerateAndDisplayOrder(startHidden: isInKitchen); //generate new order and keep hidden until swwitching to dining room
            }
        }
    }

    private int CalculateScore(List<string> completedOrder, List<string> customerOrder)
    {
        int score = 0;

        foreach (string ingredient in completedOrder)
        {
            if (!customerOrder.Contains(ingredient)) //for the love of god why is my scoring system not working like i want it to T.T
            {
                score -= 10;
            }
            else
            {
                score += 10;
            }
        }

        return score * -1; //it keeps giving me a negative number so i just multiplied it by -1 so it increments 10 points for each correct ingredient you put in
    }
}



//fix and delete note later: bruh send order is NOT WORKINGGGGG im cooked. it works one time and then everytime after that another order will pop up, but no customer prefab, and then send order doesnt do anything anymore T.T