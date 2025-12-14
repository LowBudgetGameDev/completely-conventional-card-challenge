using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandValue : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardList;

    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetValue(Hand hand, Dictionary<Card, Vector3> cardPositions)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            if (i >= hand.cards.Count)
            {
                cardList[i].SetActive(false);
                continue;
            }

            cardList[i].GetComponent<CardValue>().SetValue(hand.cards[i]);

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>()); // Fix positions in the scroll layout group so positions are accuarte

            Vector3 localStartPosition = transform.InverseTransformPoint(cardPositions[hand.cards[i]]);

            cardList[i].GetComponent<CardAnimation>().Animate(localStartPosition, cardList[i].transform.localPosition, TimeDelays.MoveCardsToHandTime, true);

            scoreText.gameObject.SetActive(false);
            FunctionTimer.Create(() =>
            {
                scoreText.gameObject.SetActive(true);
            }, TimeDelays.MoveCardsToHandTime);
        }

        scoreText.SetText("<sprite=0> " + hand.GetScore().ToString());
    }
}
