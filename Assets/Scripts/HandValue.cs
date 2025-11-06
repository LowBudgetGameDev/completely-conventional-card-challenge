using System.Collections.Generic;
using UnityEngine;

public class HandValue : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardList;

    public void SetValue(Hand hand)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            if (i >= hand.cards.Count)
            {
                cardList[i].SetActive(false);
                continue;
            }

            cardList[i].GetComponent<CardValue>().SetValue(hand.cards[i]);
        }
    }
}
