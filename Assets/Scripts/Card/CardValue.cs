using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardValue : MonoBehaviour
{
    [SerializeField] private List<Sprite> cardSpriteList;

    [SerializeField] private Image frontImage;

    private Card card;

    public void SetValue(Card card)
    {
        this.card = card;

        int suitIndex = (int) card.suit;   // 0 = Hearts, 1 = Diamonds, etc.

        int rankValue = (int) card.rank;
        int rankIndex;

        if (rankValue == 14)
        {
            // Ace is first in sprites
            rankIndex = 0;
        }
        else
        {
            // 2 maps to index 1, 3 -> 2, ... King -> 13
            rankIndex = rankValue - 1;
        }

        int spriteIndex = suitIndex * 13 + rankIndex;

        frontImage.sprite = cardSpriteList[spriteIndex];
    }

    public Card GetCard()
    {
        return card;
    }
}
