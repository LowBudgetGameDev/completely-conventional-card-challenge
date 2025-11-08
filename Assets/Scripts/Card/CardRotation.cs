using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CardRotation : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;

    // Back image will be shown on top by default
    private void Update()
    {
        if ((transform.rotation.eulerAngles.y + 90f) % 360f > 180f)
        {
            backImage.gameObject.SetActive(false);
            frontImage.gameObject.SetActive(true);
        }
        else
        {
            backImage.gameObject.SetActive(true);
            frontImage.gameObject.SetActive(false);
        }
    }
}
