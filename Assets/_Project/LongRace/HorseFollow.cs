using System;
using UnityEngine;

public class HorseFollow : MonoBehaviour
{

    private HorseController target;
    private Vector3 startingPos;
    private void OnEnable()
    {
        RaceController.OnRaceStart += SetStartingHorse;
        startingPos = transform.position;
    }

    private void Update()
    {
        if (target == null) return;
        transform.position = startingPos + Vector3.right * (target.transform.position.x + 0.5f);
    }

    public void SetStartingHorse()
    {
        target = GetPlayerHorse();
    }

    private HorseController GetPlayerHorse()
    {
        foreach (var horse in HorseController.horses)
        {
            if (horse.IsPlayer) return horse;
        }

        return null;
    }
}
