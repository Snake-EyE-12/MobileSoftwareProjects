using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheatManager : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0, 40)] private Vector2 effectTimeRange;
    private float timeOfNextEffect;
    private void OnEnable()
    {
        RaceController.OnRaceStart += OnRaceStart;
        
    }

    private float GetRandomTime()
    {
        return Time.time + Random.Range(effectTimeRange.x, effectTimeRange.y);
    }


    private bool doingEffects;
    public void OnRaceStart()
    {
        timeOfNextEffect = GetRandomTime();
        doingEffects = true;
    }

    private void Update()
    {
        if (!doingEffects) return;

        if (Time.time > timeOfNextEffect)
        {
            timeOfNextEffect = GetRandomTime();
            GenerateRaceEffect();
        }
    }

    [SerializeReference] private List<CheatEffect> cheatEffects = new();
    private void GenerateRaceEffect()
    {
        if (cheatEffects.Count > 0) cheatEffects[Random.Range(0, cheatEffects.Count)].Apply();
    }
    
}
