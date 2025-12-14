using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private CardSlider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainScene);
            SoundManager.Instance.PlaySoundType(SoundManager.SoundType.ChipCollect);
        });

        volumeSlider.OnValueChanged += (object sender, EventArgs e) =>
        {
            AudioManager.Instance.SetVolume(volumeSlider.GetValue());
            int volumePercent = (int) (volumeSlider.GetValue() * 100);
            volumeText.SetText(volumePercent.ToString() + "%");
        };

        volumeSlider.SetValue(AudioManager.Instance.GetVolume());

        int volumePercent = (int)(volumeSlider.GetValue() * 100);
        volumeText.SetText(volumePercent.ToString() + "%");
    }
}
