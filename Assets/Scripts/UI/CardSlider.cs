using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlider : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public event EventHandler OnValueChanged;

    [SerializeField] private List<Image> cardImageList;

    private float value;

    public void OnDrag(PointerEventData eventData)
    {
        UpdateCards(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateCards(eventData);
    }

    private void UpdateCards(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
 
        value = Mathf.Clamp01((localPoint.x + rectTransform.rect.width) / rectTransform.rect.width);
        OnValueChanged?.Invoke(this, EventArgs.Empty);

        int cardsToShow = (int)(value * cardImageList.Count + 0.5);

        for (int i = 0; i < cardImageList.Count; i++)
        {
            Color original = cardImageList[i].color;

            cardImageList[i].color = new Color(original.r, original.g, original.b, i < cardsToShow ? 1f : 0.05f);
        }
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float newValue)
    {
        value = newValue;
        OnValueChanged?.Invoke(this, EventArgs.Empty);

        int cardsToShow = (int)(value * cardImageList.Count + 0.5);

        for (int i = 0; i < cardImageList.Count; i++)
        {
            Color original = cardImageList[i].color;

            cardImageList[i].color = new Color(original.r, original.g, original.b, i < cardsToShow ? 1f : 0.05f);
        }
    }
}
