using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private RectTransform deckPoint;
    [SerializeField] private List<RectTransform> cardPoints;
    [SerializeField] private RectTransform cardPrefab;
    [SerializeField] private Button confirmHandButton;

    private List<RectTransform> cardList;

    private int selectedCardAmount;
    private int maxSelectedCards = 5;

    private void Start()
    {
        cardList = new List<RectTransform>();

        FunctionTimer.Create(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CardFlip);
            AddAvailableCards();
            
            int cardIndex = 0;
            foreach (RectTransform card in cardList)
            {
                card.GetComponent<CardAnimation>().Animate(deckPoint.position, cardPoints[cardList.IndexOf(card)].position, TimeDelays.GiveCardTime, false, true, TimeDelays.DelayBetweenGivingCards * cardIndex);

                cardIndex++;
            }
        }, TimeDelays.GiveInitialCardsDelay);

        confirmHandButton.onClick.AddListener(() =>
        {
            if (selectedCardAmount == 0) return;

            selectedCardAmount = 0;

            List<int> selectedCardIndeces = new List<int>();

            for (int i = 0; i < cardList.Count; i++)
            {
                bool isSelected = cardList[i].GetComponent<CardSelect>().IsSelected();

                if (isSelected) selectedCardIndeces.Add(i);
            }

            Dictionary<Card, Vector3> cardPositionDictionary = new Dictionary<Card, Vector3>();

            foreach (int index in selectedCardIndeces)
            {
                cardPositionDictionary.Add(CardManager.Instance.GetAvailableCards()[index], cardList[index].position);
            }

            HandManager.Instance.SetPositionsOfUsedCards(cardPositionDictionary);

            CardManager.Instance.CreateHandFromCards(selectedCardIndeces);

            SoundManager.Instance.PlaySound(SoundManager.Sound.CardFlip);
            ClearAvailableCards();
            AddAvailableCards();

            FunctionTimer.Create(() =>
            {
                SoundManager.Instance.PlaySoundType(SoundManager.SoundType.ChipCollect);
            }, TimeDelays.MoveCardsToHandTime);

            int cardIndex = 0;
            foreach (RectTransform card in cardList)
            {
                Vector3 startPosition = deckPoint.position;

                bool staysInHand = false;
                if (staysInHand = CardManager.Instance.WasCardAtThisIndexPreviouslyAvailable(cardList.IndexOf(card), out int oldIndex)) startPosition = cardPoints[oldIndex].position;

                card.GetComponent<CardAnimation>().Animate(startPosition, cardPoints[cardList.IndexOf(card)].position, TimeDelays.GiveCardTime, false, !staysInHand, TimeDelays.DelayBetweenGivingCards * cardIndex);

                cardIndex++;
            }
        });

        CardManager.Instance.OnAllCardsUsed += (object sender, EventArgs e) =>
        {
            gameObject.SetActive(false);
        };
    }

    private void AddAvailableCards()
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
    }

    private void ClearAvailableCards()
    {
        foreach (RectTransform cardTransform in cardList)
        {
            Destroy(cardTransform.gameObject);
        }

        cardList.Clear();
    }
}
