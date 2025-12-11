using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    private float timerMax;
    private float timer;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private bool isLocal;

    private bool flipCard;
    private float initialRotation;
    private float finalRotation;

    private float delayTimer;

    // I know this isn't neat but its used like 4 times total and this just works
    public void Animate(Vector3 initialPosition, Vector3 finalPosition, float duration, bool isLocal = false, bool flipCard = false, float delay = 0f)
    {
        delayTimer = delay;

        this.isLocal = isLocal;

        if (isLocal)
        {
            transform.localPosition = initialPosition;
        }
        else
        {
            transform.position = initialPosition;
        }

        this.flipCard = flipCard;

        if (flipCard)
        {
            initialRotation = 0f;
            finalRotation = 180f;
        }

        timer = 0f;
        timerMax = duration;
        this.initialPosition = initialPosition;
        this.finalPosition = finalPosition;
        GetComponent<CardSelect>()?.SetCanClick(false);
    }

    private void Update()
    {
        delayTimer -= Time.deltaTime;

        if (delayTimer > 0f) return;

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

        if (flipCard)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(initialRotation, finalRotation, curve.Evaluate(timer / timerMax)), transform.eulerAngles.z);
        }

        if (timer >= timerMax)
        {
            GetComponent<CardSelect>()?.SetCanClick(true);
        }
    }
}
