using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private void Awake() => instance = this;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    

    public void Play(Sound sound)
    {
        switch (sound.type)
        {
            case SoundType.Music:
                musicSource.volume = Random.Range(sound.volume.x, sound.volume.y);
                musicSource.pitch = Random.Range(sound.pitch.x, sound.pitch.y);
                musicSource.PlayOneShot(sound.clip);
                break;
            case SoundType.SFX:
                sfxSource.volume = Random.Range(sound.volume.x, sound.volume.y);
                sfxSource.pitch = Random.Range(sound.pitch.x, sound.pitch.y);
                sfxSource.PlayOneShot(sound.clip);
                break;
        }
    }


}

[Serializable]
public class Sound
{
    [SerializeField] public SoundType type;
    [SerializeField] public AudioClip clip;
    [SerializeField, MinMaxSlider(0, 4)] public Vector2 volume;
    [SerializeField, MinMaxSlider(0, 4)] public Vector2 pitch;
    
}

public enum SoundType
{
    Music,
    SFX
}