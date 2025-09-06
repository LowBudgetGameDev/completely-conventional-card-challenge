using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    private Deck deck;

    private void Awake()
    {
        Instance = this;

        deck = new Deck();
    }

    public List<Card> GetCards()
    {
        return deck.GetCardList();
    }
}
