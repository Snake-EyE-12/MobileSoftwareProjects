using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] protected Transform startingPosition;
    public Vector3 GetStartingPosition() => startingPosition.position;
    [SerializeField] protected Horse horsePrefab;

    protected Horse activeHorse;

    public Vector3 GetHorsePosition()
    {
        return activeHorse.transform.position;
    }
    public void SpawnHorse()
    {
        Horse horse = Instantiate(horsePrefab, GetStartingPosition(), Quaternion.identity);
        InitializeHorse(horse);
        activeHorse = horse;
    }

    public virtual void InitializeHorse(Horse horse)
    {
        
    }

    public void AdvanceHorses()
    {
        activeHorse.Tick();
    }
}