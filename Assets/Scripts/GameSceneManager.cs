using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public enum Scene
    {
        MainMenuScene,
        MainScene
    }

    public static void LoadScene(Scene scene)
    {
        TransitionManager.Instance.StartTransition();

        FunctionTimer.Create(() =>
        {
            SceneManager.LoadScene(scene.ToString());
        }, 0.5f);
    }
}
