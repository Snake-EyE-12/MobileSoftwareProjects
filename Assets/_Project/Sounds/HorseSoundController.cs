using System.Collections.Generic;
using UnityEngine;

public class HorseSoundController : MonoBehaviour
{
    [SerializeField] private List<Sound> neighs = new();
    [SerializeField] private List<Sound> snorts = new();
    [SerializeField] private List<Sound> trots = new();

    public void PlayNeigh()
    {
        SoundManager.instance.Play(neighs[Random.Range(0, neighs.Count)]);
    }
    public void PlaySnort()
    {
        SoundManager.instance.Play(snorts[Random.Range(0, snorts.Count)]);
    }
    public void PlayTrot()
    {
        SoundManager.instance.Play(trots[Random.Range(0, trots.Count)]);
    }
}