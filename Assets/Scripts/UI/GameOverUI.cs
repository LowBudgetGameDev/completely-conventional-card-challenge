using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        CardManager.Instance.OnAllCardsUsed += (object sender, EventArgs e) =>
        {
            Show();

            scoreText.SetText("Total: <sprite=0> " + HandManager.Instance.GetTotalScore());
        };

        retryButton.onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainScene);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
