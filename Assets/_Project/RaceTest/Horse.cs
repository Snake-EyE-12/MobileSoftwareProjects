using System;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public bool IsPlayerHorse { get; set; }
    [SerializeField] private float baseMoveSpeed;
    [SerializeField, Range(0, 1)] private float travelChance;
    [SerializeField] private Destinator destinator;
    private List<SpeedModifier> speedModifiers = new();
    public void AddBonusMoveSpeedMultiplier(SpeedModifier multiplier)
    {
        speedModifiers.Add(multiplier);
    }

    private float GetMoveSpeed()
    {
        for (int i = speedModifiers.Count - 1; i >= 0; i--)
        {
            if(speedModifiers[i].isOver()) speedModifiers.RemoveAt(i);
        }
        
        float moveSpeed = baseMoveSpeed;
        foreach (var modifier in speedModifiers)
        {
            modifier.ModifyValue(ref moveSpeed);
        }

        return moveSpeed;
    }

    public void Tick()
    {
        if(PassChance()) destinator.Move((Vector2)transform.position + Vector2.right * GetMoveSpeed());
    }
    private bool PassChance()
    {
        return UnityEngine.Random.Range(0f, 1f) < travelChance;
    }
}

public class SpeedModifier
{
    private float multiplier;
    private float timeOfEndDuration;

    public SpeedModifier(float mult, float duration)
    {
        multiplier = mult;
        timeOfEndDuration = Time.time + duration;
    }

    public bool isOver()
    {
        return Time.time > timeOfEndDuration;
    }

    public void ModifyValue(ref float value)
    {
        value *= multiplier;
    }
}