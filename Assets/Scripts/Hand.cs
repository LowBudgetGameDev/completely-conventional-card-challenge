using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HandType
{
    None,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush
}

public class Hand
{
    public List<Card> cards { get; private set; }

    private HandType handType;
    private int score;

    public Hand(List<Card> cards)
    {
        this.cards = cards;
        this.cards.Sort();

        // Turns 2-3-4-5-A into A-2-3-4-5 for straight
        if (this.cards.Count == 5 &&
            this.cards[0].Is(Rank.Two) &&
            this.cards[1].Is(Rank.Three) &&
            this.cards[2].Is(Rank.Four) &&
            this.cards[3].Is(Rank.Five) &&
            this.cards[4].Is(Rank.Ace))
        {
            Card ace = this.cards[4];

            this.cards.RemoveAt(4);
            this.cards.Insert(0, ace);
        }

        FindHandType();
    }

    public int GetScore()
    {
        if (score != 0) return score;

        int handSize = 0;

        if (handType == HandType.StraightFlush || handType == HandType.FullHouse || handType == HandType.Straight || handType == HandType.Flush) handSize = 5;

        if (handType == HandType.FourOfAKind || handType == HandType.TwoPair) handSize = 4;

        if (handType == HandType.ThreeOfAKind) handSize = 3;

        if (handType == HandType.Pair) handSize = 2;

        int totalScore = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            if (i < handSize)
            {
                totalScore += cards[i].score * GetHandMult();
            }
            else
            {
                totalScore += cards[i].score;
            }
        }

        score = totalScore;

        return score;
    }

    private int GetHandMult()
    {
        return handType switch
        {
            HandType.None => 1,
            HandType.Pair => 3,
            HandType.TwoPair => 6,
            HandType.ThreeOfAKind => 12,
            HandType.Straight => 20,
            HandType.Flush => 30,
            HandType.FullHouse => 45,
            HandType.FourOfAKind => 70,
            HandType.StraightFlush => 100,
            _ => 0
        };
    }

    public override string ToString()
    {
        return handType.ToString();
    }

    // Idea from: https://gamedev.stackexchange.com/questions/49302/determining-poker-hands
    private void FindHandType()
    {
        Dictionary<Rank, int> rankCountDictionary = new Dictionary<Rank, int>();

        foreach (Card card in cards)
        {
            if (rankCountDictionary.ContainsKey(card.rank))
            {
                rankCountDictionary[card.rank]++;
                continue;
            }

            rankCountDictionary[card.rank] = 1;
        }

        Dictionary<Suit, int> suitCountDictionary = new Dictionary<Suit, int>();

        foreach (Card card in cards)
        {
            if (suitCountDictionary.ContainsKey(card.suit))
            {
                suitCountDictionary[card.suit]++;
                continue;
            }

            suitCountDictionary[card.suit] = 1;
        }

        bool containsPair = ContainsPair(rankCountDictionary);
        bool containsTwoPair = ContainsTwoPair(rankCountDictionary);
        bool containsThree = ContainsThree(rankCountDictionary);
        bool containsFour = ContainsFour(rankCountDictionary);
        bool containsFlush = ContainsFlush(suitCountDictionary);
        bool containsStraight = ContainsStraight(rankCountDictionary);

        if (containsFlush && containsStraight)
        {
            handType = HandType.StraightFlush;
        }
        else if (containsFour)
        {
            handType = HandType.FourOfAKind;

            MoveActiveHandToBack(FindFourRank(rankCountDictionary));
        }
        else if (containsThree && containsPair)
        {
            handType = HandType.FullHouse;
        }
        else if (containsFlush)
        {
            handType = HandType.Flush;
        }
        else if (containsStraight)
        {
            handType = HandType.Straight;
        }
        else if (containsThree)
        {
            handType = HandType.ThreeOfAKind;

            MoveActiveHandToBack(FindThreeRank(rankCountDictionary));
        }
        else if (containsTwoPair)
        {
            handType = HandType.TwoPair;

            List<Rank> handRanks = FindTwoPairRanks(rankCountDictionary);
            handRanks.Reverse(); // Reverse the order to that smaller stays to the left of large

            foreach (Rank rank in handRanks) MoveActiveHandToBack(rank);
        }
        else if (containsPair)
        {
            handType = HandType.Pair;

            MoveActiveHandToBack(FindPairRank(rankCountDictionary));
        }
        else
        {
            handType = HandType.None;
        }
    }

    private void MoveActiveHandToBack(Rank handRank)
    {
        List<Card> handCards = new List<Card>();

        foreach (Card card in cards)
        {
            if (card.rank == handRank)
            {
                handCards.Add(card);
            }
        }

        handCards.Reverse(); // Used to preserve order when moving cards back

        foreach (Card handCard in handCards)
        {
            cards.Remove(handCard);
            cards.Insert(0, handCard);
        }
    }

    private bool ContainsPair(Dictionary<Rank, int> rankCountDictionary)
    {
        int numPairs = 0;

        foreach (int count in rankCountDictionary.Values)
        {
            if (count == 2) numPairs++;
        }

        return numPairs == 1;
    }

    private Rank FindPairRank(Dictionary<Rank, int> rankCountDictionary)
    {
        return rankCountDictionary.First(x => x.Value == 2).Key;
    }

    private bool ContainsTwoPair(Dictionary<Rank, int> rankCountDictionary)
    {
        int numPairs = 0;

        foreach (int count in rankCountDictionary.Values)
        {
            if (count == 2) numPairs++;
        }

        return numPairs == 2;
    }

    private List<Rank> FindTwoPairRanks(Dictionary<Rank, int> rankCountDictionary)
    {
        List<Rank> pairs = new List<Rank>();

        foreach (Rank rank in rankCountDictionary.Keys)
        {
            if (rankCountDictionary[rank] == 2) pairs.Add(rank);
        }

        return pairs;
    }

    private bool ContainsThree(Dictionary<Rank, int> rankCountDictionary)
    {
        int numThrees = 0;

        foreach (int count in rankCountDictionary.Values)
        {
            if (count == 3) numThrees++;
        }

        return numThrees == 1;
    }

    private Rank FindThreeRank(Dictionary<Rank, int> rankCountDictionary)
    {
        return rankCountDictionary.First(x => x.Value == 3).Key;
    }

    private bool ContainsFour(Dictionary<Rank, int> rankCountDictionary)
    {
        int numFours = 0;

        foreach (int count in rankCountDictionary.Values)
        {
            if (count == 4) numFours++;
        }

        return numFours == 1;
    }

    private Rank FindFourRank(Dictionary<Rank, int> rankCountDictionary)
    {
        return rankCountDictionary.First(x => x.Value == 4).Key;
    }

    private bool ContainsFlush(Dictionary<Suit, int> suitCountDictionary)
    {
        foreach (int count in suitCountDictionary.Values)
        {
            if (count == 5) return true;
        }

        return false;
    }

    private bool ContainsStraight(Dictionary<Rank, int> rankCountDictionary)
    {
        if (rankCountDictionary.Count != 5) return false;

        // Handle A-2-3-4-5 case
        bool isLowStraight = rankCountDictionary.Keys.ElementAt(0) == Rank.Ace &&
                             rankCountDictionary.Keys.ElementAt(1) == Rank.Two &&
                             rankCountDictionary.Keys.ElementAt(2) == Rank.Three &&
                             rankCountDictionary.Keys.ElementAt(3) == Rank.Four &&
                             rankCountDictionary.Keys.ElementAt(4) == Rank.Five;

        // https://stackoverflow.com/questions/18225010/functional-way-to-check-if-array-of-numbers-is-sequential
        return rankCountDictionary.Keys.Zip(rankCountDictionary.Keys.Skip(1), (a, b) => (a + 1) == b).All(x => x) || isLowStraight;
    }
}
