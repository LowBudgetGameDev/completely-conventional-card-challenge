using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour, IPointerClickHandler
{
    private bool isSelected;
    private float initialY;
    private float height;
    private bool canClick;

    private Func<bool> onClickInitialAction;

    private void Awake()
    {
        initialY = transform.GetComponent<RectTransform>().position.y;
        height = transform.GetComponent<RectTransform>().rect.height;
    }

    public void SetOnClickInitialAction(Func<bool> initialAction)
    {
        onClickInitialAction = initialAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Runs code to check if code should run and exits method when return false
        if (!canClick || onClickInitialAction() == false) return;

        isSelected = !isSelected;

        SoundManager.Instance.PlaySound(SoundManager.Sound.PickupCard);
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float yPosition = isSelected ? initialY + height / 2 : initialY;

        GetComponent<CardAnimation>().Animate(transform.position, new Vector3(transform.position.x, yPosition, transform.position.z), TimeDelays.CardSelectTime);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetCanClick(bool canClick)
    {
        this.canClick = canClick;
    }
}
