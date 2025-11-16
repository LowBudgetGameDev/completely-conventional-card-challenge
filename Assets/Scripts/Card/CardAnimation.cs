using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    private float timerMax;
    private float timer;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private bool isLocal;

    public void Animate(Vector3 initialPosition, Vector3 finalPosition, float duration, bool isLocal = false)
    {
        this.isLocal = isLocal;

        if (isLocal)
        {
            transform.localPosition = initialPosition;
        }
        else
        {
            transform.position = initialPosition;
        }

        timer = 0f;
        timerMax = duration;
        this.initialPosition = initialPosition;
        this.finalPosition = finalPosition;
        GetComponent<CardSelect>()?.SetCanClick(false);
    }

    private void Update()
    {
        if (timer >= timerMax) return;

        timer += Time.deltaTime;

        if (isLocal)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(timer / timerMax));
        }
        else
        {
            transform.position = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(timer / timerMax));
        }

        if (timer >= timerMax)
        {
            GetComponent<CardSelect>()?.SetCanClick(true);
        }
    }
}
