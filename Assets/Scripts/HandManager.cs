using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    public event EventHandler OnHandCreated;

    private List<Hand> handList;

    private void Awake()
    {
        Instance = this;

        handList = new List<Hand>();
    }

    public void CreateHand(List<Card> cards)
    {
        handList.Add(new Hand(cards));

        OnHandCreated?.Invoke(this, EventArgs.Empty);
    }

    public Hand GetLastHand()
    {
        return handList[handList.Count - 1];
    }
}
