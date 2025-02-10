using System;
using UnityEditor;
using UnityEngine;

public class VisualHorseStateController : MonoBehaviour
{
    private HorseState currentState;
    [SerializeField] private ReadyState readyState;
    [SerializeField] private MovingState movingState;
    [SerializeField] private FinishedState finishedState;
    [SerializeField] private DazedState dazedState;
    private void ChangeState(HorseState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
    
    public void ReadyUp() => ChangeState(readyState);
    public void Moving() => ChangeState(movingState);
    public void Finished() => ChangeState(finishedState);
    public void Dazed() => ChangeState(dazedState);

    private void Awake()
    {
        ChangeState(readyState);
    }
    private void Update()
    {
        currentState?.OnUpdate(this);
    }

    [SerializeField] private GameObject dazedStars;

    public void SetStars(bool active)
    {
        dazedStars.SetActive(active);
    }

    [SerializeField] private float rotationSpeed;
    public void RotateTowardAngle(float angle)
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float angleDifference = Mathf.DeltaAngle(currentAngle, angle);
        float rotationStep = Mathf.Clamp(Mathf.Abs(angleDifference), 0, 1) * rotationSpeed * Time.deltaTime;
        float newAngle = currentAngle + Mathf.Sign(angleDifference) * rotationStep;
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }


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

    public override void OnUpdate(VisualHorseStateController controller)
    {
        controller.RotateTowardAngle(targetAngle);
        CheckDirection(controller.transform.rotation.eulerAngles.z);
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
        UnityEditor.EditorApplication.ExitPlaymode();
    }
    public override void OnUpdate(VisualHorseStateController controller) { }
}
