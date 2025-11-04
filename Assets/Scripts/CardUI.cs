using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private List<RectTransform> cardPoints;
    [SerializeField] private RectTransform cardPrefab;

    private List<RectTransform> cardList;

    private int selectedCardAmount;
    private int maxSelectedCards = 5;

    private void Start()
    {
        cardList = new List<RectTransform>();

        FunctionTimer.Create(() =>
        {
            for (int i = 0; i < CardManager.Instance.GetAvailableCards().Count; i++)
            {
                RectTransform cardTransform = Instantiate(cardPrefab, cardPoints[i].position, Quaternion.Euler(0, 180, 0), cardPoints[i]);

                cardTransform.GetComponent<CardValue>().SetValue(CardManager.Instance.GetAvailableCards()[i]);
                cardList.Add(cardTransform);
            }

            foreach (RectTransform cardTransform in cardList)
            {
                CardSelect card = cardTransform.GetComponent<CardSelect>();

                card.SetOnClickInitialAction(() =>
                {
                    if (selectedCardAmount == maxSelectedCards && !card.IsSelected()) return false;

                    selectedCardAmount += card.IsSelected() ? -1 : 1; // Invert because selected -> unselected and vice versa

                    return true;
                });
            }
        }, 3f);
    }
}
