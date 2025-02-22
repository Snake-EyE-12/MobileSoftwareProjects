
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField] private VisualHorseStateController visual;
    [SerializeField] private Speed speedControls;
    [SerializeField] private Bounds kickBounds;
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

    [Button]
    public void Kick()
    {
        if (flying) return;
        var collection = Physics2D.OverlapBoxAll(transform.position + kickBounds.center, kickBounds.size, 0f, LayerMask.GetMask("Horse"));
        visual.Kick();
        foreach (var horse in collection)
        {
            if (horse.transform.parent.TryGetComponent(out HorseController controller))
            {
                if(controller == this) continue;
                controller.Stun();
            }
        }
    }

    private bool flying;
    
    [Button]
    public void SpringForward(float duration = 1f)
    {
        flying = true;
        visual.Spring(duration);
        Invoke(nameof(Land), duration);
    }

    private void Land()
    {
        flying = false;
    }

    private float timeOfStunEnd;
    public void Stun(float duration = 1.8f)
    {
        if (flying) return;
        timeOfStunEnd = Time.time + duration;
        visual.Dazed();
    }

    private bool CanMove => runningRace && Time.time > timeOfStunEnd && !flying;
    private void Move()
    {
        if (!CanMove) return;
        visual.Moving();
        ChangePositionBy(speedControls.Value * Time.deltaTime);
    }

    private void ChangePositionBy(float value)
    {
        DistanceTraveled += value;
        transform.position =
            new Vector3(startPosition.x + DistanceTraveled, transform.position.y, transform.position.z);
        if (DistanceTraveled > raceLength) CrossFinish();
    }

    private int currentLaneIndex;
    private int laneCount;
    private float yOffset;
    private bool laneShifting;
    private int desiredLaneDirection;
    private float startingY;
    private float endingY;
    public void SetLaneShiftData(int currentLaneIndex, int laneCount, float ySeparation)
    {
        this.currentLaneIndex = currentLaneIndex;
        this.laneCount = laneCount;
        this.yOffset = ySeparation;
    }
    public void ShiftLane()
    {
        if(laneShifting || !CanMove) return;
        laneShifting = true;
        laneShiftStartTimer = 0;
        startingY = transform.position.y;
        PickNewAdjacentLane();
        endingY = startingY - (yOffset * desiredLaneDirection);
    }

    private void PickNewAdjacentLane()
    {
        if (currentLaneIndex == 0)
        {
            desiredLaneDirection = 1;
            return;
        }

        if (currentLaneIndex == laneCount - 1)
        {
            desiredLaneDirection = -1;
            return;
        }
        desiredLaneDirection = (UnityEngine.Random.Range(0, 2) == 0) ? 1 : -1;
    }

    [SerializeField] private float laneShiftDuration;
    private float laneShiftStartTimer;
    private void AttemptLaneShift()
    {
        if(!laneShifting || !CanMove) return;
        laneShiftStartTimer += Time.deltaTime;
        float t = laneShiftStartTimer / laneShiftDuration;
        float newY = Mathf.Lerp(startingY, endingY, t);
        if (t >= 1)
        {
            newY = endingY;
            laneShifting = false;
            currentLaneIndex += desiredLaneDirection;
        }
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
        if (flying)
        {
            Fly();
            return;
        }
        Move();
        AttemptLaneShift();
        visual.SetSpeed(speedControls.Value, speedControls.Default);
    }

    [SerializeField] private float flySpeed = 5f;
    private void Fly()
    {
        ChangePositionBy(flySpeed * Time.deltaTime);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + kickBounds.center, kickBounds.size);
    }
}

[Serializable]
public class Speed
{
    [SerializeField] private float baseSpeed;
    private List<SpeedModifier> speedModifiers = new();
    public float Default => baseSpeed;

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