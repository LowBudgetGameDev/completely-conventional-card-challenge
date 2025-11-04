using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour, IPointerClickHandler
{
    private bool isSelected;
    private float initialY;
    private float height;

    private Func<bool> onClickInitialAction;

    private void Awake()
    {
        initialY = transform.GetComponent<RectTransform>().localPosition.y;
        height = transform.GetComponent<RectTransform>().rect.height;
    }

    public void SetOnClickInitialAction(Func<bool> initialAction)
    {
        onClickInitialAction = initialAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Runs code to check if code should run and exits method when return false
        if (onClickInitialAction() == false) return;

        isSelected = !isSelected;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float yPosition = isSelected ? initialY + height / 2 : initialY;

        transform.GetComponent<RectTransform>().localPosition = new Vector3(0, yPosition, 0);
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
