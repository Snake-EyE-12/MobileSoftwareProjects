using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaceSetup : MonoBehaviour
{
    [SerializeField] private int laneCount = 4;
    [SerializeField] private float separation = 1f;
    [SerializeField] private float length = 13.5f;

    [SerializeField] private Lane lanePrefab;
    [SerializeField] private FinishLine flagPrefab;
    [SerializeField] private Gate gatePrefab;
    [SerializeField] private HorseController horsePrefab;

    [SerializeField] private Transform SerializeTransform;

    [SerializeField] private TiledSpriteSizeSetter picketFence;
    [SerializeField] private OakTree treePrefab;
    [SerializeField, MinMaxSlider(0, 30)] private Vector2 treeSpacing;
    [SerializeField] private Vector2 startOffset;


    private void BuildTrees()
    {
        float totalDistance = 0;
        while (totalDistance < length + 10)
        {
            float newDistance = Random.Range(treeSpacing.x, treeSpacing.y);
            totalDistance += newDistance;
            OakTree newTree = Instantiate(treePrefab, new Vector3(totalDistance + startOffset.x, startOffset.y, 0), Quaternion.identity, SerializeTransform);
            newTree.transform.SetParent(null);
        }
    }
    
    public void BuildRace()
    {
        BuildTrees();
        picketFence.SetWidth(10 + length);
        picketFence.transform.position =
            new Vector3(length * 0.5f - 5, picketFence.transform.position.y, 0);
        float yOffset = laneCount * 0.5f - 0.5f;
        Vector3 offset = new Vector3(length * 0.5f, yOffset, 0f);
        for (int i = 0; i < laneCount; i++)
        {
            Vector3 pos = i * separation * Vector3.down;

            Lane newLane = Instantiate(lanePrefab, pos + offset, Quaternion.identity, SerializeTransform);
            newLane.transform.SetParent(null);
            newLane.transform.localScale = new Vector3(length, 1f, 1f);

            Vector3 startLocation = pos + new Vector3(-0.5f, yOffset, 0);
            Gate newGate = Instantiate(gatePrefab, startLocation, Quaternion.identity, SerializeTransform);
            newGate.transform.SetParent(null);

            HorseController newHorse = Instantiate(horsePrefab, startLocation, Quaternion.identity, SerializeTransform);
            newHorse.transform.SetParent(null);
            newHorse.SetLaneShiftData(i, laneCount, separation);
            newHorse.raceLength = length;
            if(i == 0) newHorse.SetAsPlayer();
        }

        Vector3 flagPos = new Vector3(length, 0f, 0f);
        FinishLine finishLine = Instantiate(flagPrefab, flagPos, Quaternion.identity, SerializeTransform);
        finishLine.transform.SetParent(null);
        finishLine.SetScale(laneCount);
    }

}