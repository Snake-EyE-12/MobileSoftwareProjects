
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField] private VisualHorseStateController visual;
    [SerializeField] private Speed speedControls;
    public float DistanceTraveled { get; private set; }
    public static List<HorseController> horses = new();
    public float raceLength { get; set; }
    public bool IsPlayer { get; set; }
    public float PercentDistanceTraveled => DistanceTraveled / raceLength;
    public void AddSpeedModifier(float multiplier, float duration) => speedControls.AddModifier(new SpeedModifier(multiplier, duration));
    private void OnEnable()
    {
        RaceController.OnRaceStart += OnRaceStart;
        RaceController.OnRacePause += PauseRunning;
        horses.Add(this);
    }

    private void OnDisable()
    {
        RaceController.OnRaceStart -= OnRaceStart;
        RaceController.OnRacePause -= PauseRunning;
        horses.Remove(this);
    }

    private bool CanMove => runningRace;
    private void Move()
    {
        if (!CanMove) return;
        DistanceTraveled += speedControls.Value * Time.deltaTime;
        transform.position = startPosition + (Vector3.right * DistanceTraveled);
        if (DistanceTraveled > raceLength) CrossFinish();
    }

    public delegate void RaceEvent(HorseController horse);
    public static RaceEvent OnCrossFinish;
    private void CrossFinish()
    {
        runningRace = false;
        visual.Finished();
        OnCrossFinish?.Invoke(this);
    }
    private void Update()
    {
        Move();
    }
    

    private bool runningRace;

    private Vector3 startPosition;
    private void OnRaceStart()
    {
        startPosition = transform.position;
        StartRunning();
    }
    private void StartRunning()
    {
        runningRace = true;
        visual.Moving();
    }

    private void PauseRunning()
    {
        runningRace = false;
    }
}

[Serializable]
public class Speed
{
    [SerializeField] private float baseSpeed;
    private List<SpeedModifier> speedModifiers = new();

    public float Value => DetermineSpeed();
    private float cachedSpeed;
    private float timeOfCacheReset;

    private float DetermineSpeed()
    {
        if(speedModifiers.Count <= 0) return baseSpeed;
        if (Time.time > timeOfCacheReset)
        {
            CleanUp();
            cachedSpeed = GetSpeed();
        }
        return cachedSpeed;
    }

    private void CleanUp()
    {
        float minimumTime = float.MaxValue;
        for (int i = speedModifiers.Count - 1; i >= 0; i--)
        {
            if (speedModifiers[i].IsOver)
            {
                speedModifiers.RemoveAt(i);
            }
            else
            {
                if(speedModifiers[i].TimeLeft < minimumTime) minimumTime = speedModifiers[i].TimeLeft;
            }
        }
        timeOfCacheReset = Time.time + minimumTime;
    }
    private float GetSpeed()
    {
        float moveSpeed = baseSpeed;
        foreach (var modifier in speedModifiers)
        {
            modifier.ModifyValue(ref moveSpeed);
        }
        return moveSpeed;
    }
    public void AddModifier(SpeedModifier multiplier)
    {
        speedModifiers.Add(multiplier);
        timeOfCacheReset = 0;
        Debug.Log("Amount: " + speedModifiers.Count);
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
    public float TimeLeft => timeOfEndDuration - Time.time;

    public bool IsOver => Time.time > timeOfEndDuration;

    public void ModifyValue(ref float value)
    {
        value *= multiplier;
    }
}