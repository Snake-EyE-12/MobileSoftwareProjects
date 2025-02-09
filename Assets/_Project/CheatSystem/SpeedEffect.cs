using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;



[CreateAssetMenu(menuName = "CheatEffect", fileName = "SpeedEffect", order = 0)]
[Serializable]
public class SpeedEffect : CheatEffect
{
    [SerializeField, MinMaxSlider(0, 4)] private Vector2 multiplier;
    [SerializeField, MinMaxSlider(0, 20)] private Vector2 duration;
    public override void Apply()
    {
        GetRandomHorse().AddSpeedModifier(Random.Range(multiplier.x, multiplier.y), Random.Range(duration.x, duration.y));
    }

    private HorseController GetRandomHorse()
    {
        return HorseController.horses[Random.Range(0, HorseController.horses.Count)];
    }
}





[Serializable]
public abstract class CheatEffect : ScriptableObject
{
    public abstract void Apply();
}