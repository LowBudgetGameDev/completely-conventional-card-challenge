using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainScene);
        });
    }
}
