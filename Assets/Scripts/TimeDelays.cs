using UnityEngine;

public static class TimeDelays
{
    public const float GiveInitialCardsDelay = 1.0f; // How long after the scene is loaded will the cards be given
    public const float GiveCardTime = 1.0f; // How long it takes to move a card from deck to hand
    public const float DelayBetweenGivingCards = 0.2f; // How much time between each card that is given
    public const float MoveCardsToHandTime = 1.0f; // How long it takes to move the card to the hand
    public const float CardSelectTime = 0.25f; // How long it takes to move card up when selecting it
}
