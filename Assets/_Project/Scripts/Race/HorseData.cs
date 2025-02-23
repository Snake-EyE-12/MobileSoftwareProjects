using System;
using System.Collections.Generic;
using UnityEngine;

public class HorseData : MonoBehaviour
{
    public static List<HorseData> horses = new();
    
    
    [SerializeField] private float baseMoveSpeed;
    private List<SpeedModifier> speedModifiers = new();
    [SerializeField, Range(0, 1)] private float travelChance;

    //public bool IsPlayer => jockey != null;
    
    // private Jockey jockey;
    // private void Start()
    // {
    //     horses.Add(this);
    //     if (TryGetComponent(out Jockey jockey))
    //     {
    //         this.jockey = jockey;
    //     }
    // }
    //
    // public void AddBonusMoveSpeedMultiplier(SpeedModifier multiplier)
    // {
    //     speedModifiers.Add(multiplier);
    // }
    //
    //
    // private float GetTickDistance()
    // {
    //     for (int i = speedModifiers.Count - 1; i >= 0; i--)
    //     {
    //         if(speedModifiers[i].IsOver()) speedModifiers.RemoveAt(i);
    //     }
    //     
    //     float moveSpeed = baseMoveSpeed;
    //     foreach (var modifier in speedModifiers)
    //     {
    //         modifier.ModifyValue(ref moveSpeed);
    //     }
    //
    //     return moveSpeed;
    // }
    //
    // private float totalDistanceTraveled;
    // public void Advance()
    // {
    //     if(PassChance()) totalDistanceTraveled += GetTickDistance();
    // }
    // private bool PassChance()
    // {
    //     return UnityEngine.Random.Range(0f, 1f) < travelChance;
    // }

    //public float GetTotalDistanceTraveled => totalDistanceTraveled;
}
