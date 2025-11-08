using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public event EventHandler OnDeckEmpty;
    public event EventHandler OnAllCardsUsed;

    private int maxAvailableCards = 7;

    private Deck deck;

    private List<Card> availableCards;

    private bool isDeckEmpty;
    private bool isAvailableCardsEmpty;

    private void Awake()
    {
        Instance = this;

        deck = new Deck();

        availableCards = deck.TakeTopCards(maxAvailableCards);
    }

    public List<Card> GetDeckCards()
    {
        return deck.GetCardList();
    }

    public List<Card> GetAvailableCards()
    {
        return availableCards;
    }

    public void CreateHandFromCards(List<int> cardIndeces)
    {
        List<Card> handCards = new List<Card>();

        foreach (int i in cardIndeces.OrderByDescending(x => x))
        {
            handCards.Add(availableCards[i]);
            availableCards.RemoveAt(i);
        }

        HandManager.Instance.CreateHand(handCards);

        availableCards.AddRange(deck.TakeTopCards(maxAvailableCards - availableCards.Count));

        if (deck.GetCardList().Count == 0 && !isDeckEmpty)
        { 
            OnDeckEmpty?.Invoke(this, EventArgs.Empty);
            isDeckEmpty = true;
        }

        if (availableCards.Count == 0 && !isAvailableCardsEmpty)
        {
            OnAllCardsUsed?.Invoke(this, EventArgs.Empty);
            isAvailableCardsEmpty = true;
        }
    }
}
