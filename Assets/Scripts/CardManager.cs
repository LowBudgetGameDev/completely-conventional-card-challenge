using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    private int maxAvailableCards = 7;

    private Deck deck;

    private List<Card> availableCards;

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
}
