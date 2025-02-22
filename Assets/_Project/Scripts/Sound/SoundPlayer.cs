using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private Sound sound;
    public void PlaySound()
    {
        SoundManager.instance.Play(sound);
    }
}
