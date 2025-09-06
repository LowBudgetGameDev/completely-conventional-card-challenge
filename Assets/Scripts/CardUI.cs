using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private RectTransform deckPoint;
    [SerializeField] private RectTransform cardPrefab;

    private void Start()
    {
        foreach (Card card in CardManager.Instance.GetCards())
        {
            RectTransform cardUI = Instantiate(cardPrefab, deckPoint.position, Quaternion.identity, deckPoint);

            cardUI.GetComponent<CardValue>().SetValue(card);
        }
    }
}
