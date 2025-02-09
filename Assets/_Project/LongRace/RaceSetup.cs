using System;
using NaughtyAttributes;
using UnityEngine;

public class RaceSetup : MonoBehaviour
{
    [SerializeField] private int laneCount = 4;
    [SerializeField] private float separation = 1f;
    [SerializeField] private float length = 13.5f;

    [SerializeField] private Lane lanePrefab;
    [SerializeField] private FinishLine flagPrefab;
    [SerializeField] private Gate gatePrefab;
    [SerializeField] private HorseController horsePrefab;
    
    
    public void BuildRace()
    {
        float yOffset = laneCount * 0.5f - 0.5f;
        Vector3 offset = new Vector3(length * 0.5f, yOffset, 0f);
        for (int i = 0; i < laneCount; i++)
        {
            Vector3 pos = Vector3.down * i * separation;
            Lane newLane = Instantiate(lanePrefab, pos + offset, Quaternion.identity);
            newLane.transform.localScale = new Vector3(length, 1f, 1f);
            Vector3 startLocation = pos + new Vector3(-0.5f, yOffset, 0);
            Gate newGate = Instantiate(gatePrefab, startLocation, Quaternion.identity);
            HorseController newHorse = Instantiate(horsePrefab, startLocation, Quaternion.identity);
            newHorse.raceLength = length;
            newHorse.IsPlayer = i == 0;
        }

        Vector3 flagPos = new Vector3(length, 0f, 0f);
        FinishLine finishLine = Instantiate(flagPrefab, flagPos, Quaternion.identity);
        finishLine.SetScale(laneCount);
    }

}