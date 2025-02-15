using System;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisualHorseStateController : MonoBehaviour
{
    private HorseState currentState;
    [SerializeField] private ReadyState readyState;
    [SerializeField] private MovingState movingState;
    [SerializeField] private FinishedState finishedState;
    [SerializeField] private DazedState dazedState;

    [SerializeField] private GameObject footprintPrefab;
    private void ChangeState(HorseState newState)
    {
        if (currentState == newState) return;
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    public void SpawnFootprint()
    {
        Instantiate(footprintPrefab, transform.position, Quaternion.identity, null);
    }
    
    public void ReadyUp() => ChangeState(readyState);
    public void Moving() => ChangeState(movingState);
    public void Finished() => ChangeState(finishedState);
    public void Dazed() => ChangeState(dazedState);

    private void Awake()
    {
        ChangeState(readyState);
        originalSprite = horseSpriteRenderer.sprite;
    }
    private void Update()
    {
        currentState?.OnUpdate(this);
    }


    public void SetStars(bool active)
    {
        if(active) stars.Begin();
        else stars.Stop();
    }

    [SerializeField] private float speedTolerance = 0.2f;
    public void SetSpeed(float current, float normal)
    {
        if (current > normal + speedTolerance)
        {
            //Go Fast
            fastParticle.Begin();
            slowParticle.Stop();
            return;
        }
        else if (current < normal - speedTolerance)
        {
            //Go Slow
            slowParticle.Begin();
            fastParticle.Stop();
            return;
        }
        //Normal Speed
        fastParticle.Stop();
        slowParticle.Stop();
        
        
    }

    public void StopParticles()
    {
        fastParticle.Stop();
        slowParticle.Stop();
        stars.Stop();
    }

    [SerializeField] private ParticleController fastParticle;
    [SerializeField] private ParticleController slowParticle;
    [SerializeField] private ParticleController stars;


    [SerializeField] private float rotationSpeed;
    public void RotateTowardAngle(float angle)
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float angleDifference = Mathf.DeltaAngle(currentAngle, angle);
        float rotationStep = Mathf.Clamp(Mathf.Abs(angleDifference), 0, 1) * rotationSpeed * Time.deltaTime;
        float newAngle = currentAngle + Mathf.Sign(angleDifference) * rotationStep;
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    [SerializeField] private float kickDuration;
    [SerializeField] private SpriteRenderer horseSpriteRenderer;
    [SerializeField] private Sprite kickSprite;
    private Sprite originalSprite;
    public void Kick()
    {
        horseSpriteRenderer.sprite = kickSprite;
        Invoke(nameof(ResetSprite), kickDuration);
    }
    public void ResetSprite() => horseSpriteRenderer.sprite = originalSprite;


}

[System.Serializable]
public abstract class HorseState
{
    public abstract void OnEnter(VisualHorseStateController controller);
    public abstract void OnExit(VisualHorseStateController controller);
    public abstract void OnUpdate(VisualHorseStateController controller);
}
[System.Serializable]
public class ReadyState : HorseState
{
    [SerializeField] private float targetAngle = -20;
    public override void OnEnter(VisualHorseStateController controller)
    {
    }

    public override void OnExit(VisualHorseStateController controller)
    {
        
    }

    public override void OnUpdate(VisualHorseStateController controller)
    {
        controller.RotateTowardAngle(targetAngle);
    }
}
[System.Serializable]
public class MovingState : HorseState
{
    [SerializeField] private float min = -7;
    [SerializeField] private float max = 17;
    private float targetAngle;
    [SerializeField] private float tolerance = 0.5f;
    

    public override void OnEnter(VisualHorseStateController controller)
    {
        float currentAngle = controller.transform.rotation.eulerAngles.z;
        float distanceFromMin = Mathf.Abs(currentAngle - min);
        float distanceFromMax = Mathf.Abs(currentAngle - max);
        targetAngle = distanceFromMin < distanceFromMax ? min : max;
    }
    public override void OnExit(VisualHorseStateController controller) { }

    [SerializeField, MinMaxSlider(0, 3)] private Vector2 footPrintInterval;
    private float timeOfNextFootPrint;
    public override void OnUpdate(VisualHorseStateController controller)
    {
        controller.RotateTowardAngle(targetAngle);
        CheckDirection(controller.transform.rotation.eulerAngles.z);
        if (Time.time > timeOfNextFootPrint)
        {
            timeOfNextFootPrint = Time.time + Random.Range(footPrintInterval.x, footPrintInterval.y);
            controller.SpawnFootprint();
        }
    }

    private void CheckDirection(float current)
    {
        if(current > 180) current -= 360;
        if (current > max - tolerance) targetAngle = min;
        else if(current - tolerance < min) targetAngle = max;
    }
}
[System.Serializable]
public class FinishedState : HorseState
{
    [SerializeField] private float targetAngle = 40;
    public override void OnEnter(VisualHorseStateController controller)
    {
        controller.StopParticles();
    }

    public override void OnExit(VisualHorseStateController controller)
    {
        
    }

    public override void OnUpdate(VisualHorseStateController controller)
    {
        controller.RotateTowardAngle(targetAngle);
    }
}
[System.Serializable]
public class DazedState : HorseState
{
    public override void OnEnter(VisualHorseStateController controller)
    {
        controller.SetStars(true);
    }

    public override void OnExit(VisualHorseStateController controller)
    {
        controller.SetStars(false);
    }
    public override void OnUpdate(VisualHorseStateController controller) { }
}
