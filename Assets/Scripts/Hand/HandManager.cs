using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    public event EventHandler OnHandCreated;

    private List<Hand> handList;

    private Dictionary<Card, Vector3> cardPositionDictionary;

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

    public int GetTotalScore()
    {
        int totalScore = 0;

        foreach (Hand hand in handList)
        {
            totalScore += hand.GetScore();
        }

        return totalScore;
    }

    // This is strictly for getting the positions needed to animate the cards into the hand
    public void SetPositionsOfUsedCards(Dictionary<Card, Vector3> positions)
    {
        cardPositionDictionary = positions;
    }

    public Dictionary<Card, Vector3> GetPositionsOfUsedCards()
    {
        return cardPositionDictionary;
    }
}
