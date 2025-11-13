using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    private float timerMax;
    private float timer;
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    public void Animate(Vector3 initialPosition, Vector3 finalPosition, float duration)
    {
        transform.position = initialPosition;
        timer = 0f;
        timerMax = duration;
        this.initialPosition = initialPosition;
        this.finalPosition = finalPosition;
    }

    private void Update()
    {
        if (timer >= timerMax) return;

        timer += Time.deltaTime;

        transform.position = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(timer / timerMax));
    }
}
