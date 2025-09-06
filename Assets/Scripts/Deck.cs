using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck
{
    private List<Card> cardList;

    public Deck()
    {
        cardList = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cardList.Add(new Card(suit, rank));
            }
        }

        Shuffle();
    }

    // This uses the Fisher Yates Shuffle Algorithm to suffle the deck of cards
    private void Shuffle()
    {
        int length = cardList.Count;

        for (int n = length - 1; n > 0; n--)
        {
            int k = Random.Range(0, n + 1);

            Card value = cardList[k];

            cardList[k] = cardList[n];
            cardList[n] = value;
        }
    }

    public List<Card> GetCardList()
    {
        return cardList;
    }
}
