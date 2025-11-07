using System;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private RectTransform deckPoint;
    [SerializeField] private RectTransform cardPrefab;

    private bool isDeckGone;

    private void Start()
    {
        Transform deck = Instantiate(cardPrefab, deckPoint.position, Quaternion.identity, deckPoint);

        CardManager.Instance.OnDeckEmpty += (object sender, EventArgs e) =>
        {
            if (isDeckGone) return;

            Destroy(deck.gameObject);

            isDeckGone = true;
        };
    }
}
