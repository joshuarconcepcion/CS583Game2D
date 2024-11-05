using System.Collections.Generic;
using UnityEngine;

public class CompletedHotPot : MonoBehaviour
{
    public List<string> orderList = new List<string>();

    public void SetOrderList(List<string> ingredients)
    {
        orderList = new List<string>(ingredients); //copy over list from hotpot to completed hotpot prefab
    }
}
