using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "CheatEffect/LaneShift", fileName = "LaneShift", order = 0)]
[Serializable]
public class LaneChangeEffect : CheatEffect
{
    [SerializeField, MinMaxSlider(0, 4)] private Vector2Int horseEffectCount;
    public override void Apply()
    {
        foreach (var horse in GetRandomHorses(Random.Range(horseEffectCount.x, horseEffectCount.y + 1)))
        {
            horse.ShiftLane();
        }
    }

}