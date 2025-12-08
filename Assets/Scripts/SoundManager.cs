using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        CardFlip,
        CardShuffle,
        ChipCollect,
        ChipCollect2,
        ChipCollect3,
        PickupCard
    }

    public enum SoundType
    {
        ChipCollect
    }

    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private Dictionary<SoundType, List<AudioClip>> soundTypeAudioClipDictionary;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        soundTypeAudioClipDictionary = new Dictionary<SoundType, List<AudioClip>>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
        {
            List<AudioClip> audioClipList = new List<AudioClip>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                if (sound.ToString().Contains(soundType.ToString())) audioClipList.Add(soundAudioClipDictionary[sound]);
            }

            soundTypeAudioClipDictionary[soundType] = audioClipList;
        }
    }

    public void PlaySound(Sound sound)
    {
        PlaySound(soundAudioClipDictionary[sound]);
    }

    public void PlaySoundType(SoundType soundType)
    {
        int randomIndex = Random.Range(0, soundTypeAudioClipDictionary[soundType].Count);

        PlaySound(soundTypeAudioClipDictionary[soundType][randomIndex]);
    }

    private void PlaySound(AudioClip soundClip)
    {
        float pitch = Random.Range(0.8f, 1.3f);

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(soundClip, volume);
    }
}
