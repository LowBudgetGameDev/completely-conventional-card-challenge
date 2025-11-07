using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Hand hand = new Hand(new System.Collections.Generic.List<Card>()
        {
            new Card(Suit.Hearts, Rank.Five),
            new Card(Suit.Hearts, Rank.Three),
            new Card(Suit.Hearts, Rank.Ace),
            new Card(Suit.Hearts, Rank.Four),
            new Card(Suit.Hearts, Rank.Two),
        });

        Debug.Log(hand.ToString());
    }
}
