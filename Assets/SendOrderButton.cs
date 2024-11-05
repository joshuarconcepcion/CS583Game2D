using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendOrderButton : MonoBehaviour
{
    private HotPotOrderManager hotPotOrderManager;
    private Button button;

    public void Initialize(HotPotOrderManager orderManager, CustomerSpawner spawner)
    {
        hotPotOrderManager = orderManager;

        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnSendOrderClick); 
        }
    }

    private void OnSendOrderClick()
    {
        if (hotPotOrderManager != null)
        {
            hotPotOrderManager.SendOrder(); //send order (deletes prefab, plays sound, calculates points, spawns new order + customer)
            Destroy(gameObject); //destroy send button (should only pop up once order is finished, shouldnt stay on screen)
        }
    }
}
