using System;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private RectTransform handPrefab;

    private void Start()
    {
        HandManager.Instance.onHandCreated += (object sender, EventArgs e) =>
        {
            RectTransform hand = Instantiate(handPrefab, container);

            hand.GetComponent<HandValue>().SetValue(HandManager.Instance.GetLastHand());
        };
    }
}
