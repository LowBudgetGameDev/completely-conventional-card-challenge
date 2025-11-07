using System;
using UnityEngine;

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public struct Card : IComparable<Card>
{
    public readonly Suit suit;
    public readonly Rank rank;
    public readonly int score;

    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;

        score = rank switch
        {
            Rank.Two => 2,
            Rank.Three => 3,
            Rank.Four => 4,
            Rank.Five => 5,
            Rank.Six => 6,
            Rank.Seven => 7,
            Rank.Eight => 8,
            Rank.Nine => 9,
            Rank.Ten => 10,
            Rank.Jack => 10,
            Rank.Queen => 10,
            Rank.King => 10,
            Rank.Ace => 11,
            _ => 0
        };
    }

    public int CompareTo(Card other)
    {
        return rank.CompareTo(other.rank);
    }

    public override string ToString()
    {
        return $"{rank} of {suit}";
    }

    public static bool operator <(Card a, Card b)
    {
        return a.rank < b.rank;
    }

    public static bool operator >(Card a, Card b)
    {
        return a.rank > b.rank;
    }

    public bool Is(Rank rank)
    {
        return this.rank == rank;
    }
}
