using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class HorseSoundController : MonoBehaviour
{
    [SerializeField] private List<Sound> neighs = new();
    [SerializeField] private List<Sound> snorts = new();
    [SerializeField] private List<Sound> trots = new();

    private void OnEnable()
    {
        PlayNeighGroup(4, 0.52f);
        PlaySnortGroup(2, 1.3f);
        
        
        RaceController.OnRaceStart += StartNoises;
        RaceController.OnRacePause += StopAllNoises;
        HorseController.OnCrossFinish += StopNoises;
    }

    private void OnDisable()
    {
        RaceController.OnRaceStart -= StartNoises;
        RaceController.OnRacePause -= StopAllNoises;
        HorseController.OnCrossFinish -= StopNoises;
    }

    private bool makingNoise = false;
    public void StartNoises()
    {
        makingNoise = true;
        PlayNeighGroup(2, 0.2f);
        PlaySnortGroup(3, 0.34f);
    }

    public void StopNoises(HorseController horse)
    {
        makingNoise = false;
        if(horse.IsPlayer) PlayNeigh();
    }

    public void StopAllNoises()
    {
        makingNoise = false;
    }


    [SerializeField, MinMaxSlider(0, 30)] private Vector2 randomSnortNeighDelay;
    private float timeOfNextHuffNoise;
    [SerializeField, MinMaxSlider(0, 30)] private Vector2 randomTrotDelay;
    private float timeOfNextTrotNoise;
    private void Update()
    {
        if (!makingNoise) return;
        if (timeOfNextHuffNoise < Time.time)
        {
            timeOfNextHuffNoise = Time.time + Random.Range(randomSnortNeighDelay.x, randomSnortNeighDelay.y);
            if(Random.Range(0, 2) == 0) PlaySnort();
            else PlayNeigh();
        }
        if (timeOfNextTrotNoise < Time.time)
        {
            timeOfNextTrotNoise = Time.time + Random.Range(randomTrotDelay.x, randomTrotDelay.y);
            PlayTrot();
        }
        
    }


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

    public void PlayNeighGroup(int count, float timeApart)
    {
        for (int i = 0; i < count; i++)
        {
            Invoke(nameof(PlayNeigh), timeApart * i);
        }
    }
    public void PlaySnortGroup(int count, float timeApart)
    {
        for (int i = 0; i < count; i++)
        {
            Invoke(nameof(PlaySnort), timeApart * i);
        }
    }
    public void PlayTrotGroup(int count, float timeApart)
    {
        for (int i = 0; i < count; i++)
        {
            Invoke(nameof(PlayTrot), timeApart * i);
        }
    }
}