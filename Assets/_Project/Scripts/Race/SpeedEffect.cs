using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;



[CreateAssetMenu(menuName = "CheatEffect/SpeedEffect", fileName = "SpeedEffect", order = 0)]
[Serializable]
public class SpeedEffect : CheatEffect
{
    [SerializeField, MinMaxSlider(0, 4)] private Vector2 multiplier;
    [SerializeField, MinMaxSlider(0, 20)] private Vector2 duration;
    public override void Apply()
    {
        GetRandomHorse().AddSpeedModifier(Random.Range(multiplier.x, multiplier.y), Random.Range(duration.x, duration.y));
    }

}


[Serializable]
public abstract class CheatEffect : ScriptableObject
{
    public abstract void Apply();
    
    protected HorseController GetRandomHorse()
    {
        return HorseController.horses[Random.Range(0, HorseController.horses.Count)];
    }
    protected List<HorseController> GetRandomHorses(int count)
    {
        List<HorseController> decidedHorses = new();
        int[] numbers = new int[HorseController.horses.Count];
        for (int i = 0; i < HorseController.horses.Count; i++)
        {
            numbers[i] = i;
        }
        Array.Sort(numbers, (x, y) => Random.Range(-1, 2));
        for (int i = 0; i < count; i++)
        {
            decidedHorses.Add(HorseController.horses[numbers[i]]);
        }
        return decidedHorses;

    }
}