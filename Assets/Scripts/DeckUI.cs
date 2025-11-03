using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private RectTransform deckPoint;
    [SerializeField] private RectTransform cardPrefab;

    private void Start()
    {
        Instantiate(cardPrefab, deckPoint.position, Quaternion.identity, deckPoint);
    }
}
