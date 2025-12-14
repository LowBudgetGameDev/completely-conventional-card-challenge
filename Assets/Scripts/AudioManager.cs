using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;

    private static float volume = 1f; // Ranges from 0.0 - 1.0

    private void Awake()
    {
        Instance = this;
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;

        volume = Mathf.Clamp01(volume);

        audioMixer.SetFloat("Volume", VolumeToGain(volume));
    }

    public float GetVolume()
    {
        return volume;
    }

    private float VolumeToGain(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, 0.001f, 1f);

        return Mathf.Log10(clampedVolume) * 20f;
    }
}
