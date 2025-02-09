using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] private GameState newState;
    public void ChangeState()
    {
        RoundController.instance.State = newState;
    }
}
