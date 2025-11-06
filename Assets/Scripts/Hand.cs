using System.Collections.Generic;
using UnityEngine;

// TODO create logic for determining hand score
public struct Hand
{
    public readonly List<Card> cards;

    public Hand(List<Card> cards)
    {
        this.cards = cards;
    }
}
