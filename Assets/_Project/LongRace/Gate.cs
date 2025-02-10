using System;
using NaughtyAttributes;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Door topDoor;
    [SerializeField] private Door bottomDoor;

    private void OnEnable()
    {
        RaceController.OnRaceStart += Open;
    }

    private void Open()
    {
        topDoor.Open();
        bottomDoor.Open();
    }
}