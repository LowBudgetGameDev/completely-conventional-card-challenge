using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private List<RectTransform> cardPoints;
    [SerializeField] private RectTransform cardPrefab;

    private List<RectTransform> cardList;

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
        }, 3f);
    }
}
