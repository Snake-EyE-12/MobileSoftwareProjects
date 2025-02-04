using System.Collections.Generic;
using UnityEngine;

public class RaceConstructor : MonoBehaviour
{
    public int LaneCount { get; set; }
    [SerializeField] private Lane playerLanePrefab;
    [SerializeField] private Lane lanePrefab;
    private List<Lane> lanes = new List<Lane>();
    

    public void Construct()
    {
        if (LaneCount < 1) return;
        BuildPlayerLane();
        for (int i = 0; i < LaneCount - 1; i++)
        {
            BuildLane(i + 1);
        }
    }
    public List<Lane> GetLanes() => lanes;

    private Vector3 GetOrigin()
    {
        return transform.position;
    }
    private void BuildPlayerLane()
    {
        Lane lane = Instantiate(playerLanePrefab, GetOrigin(), Quaternion.identity);
        lane.SpawnHorse();
        lanes.Add(lane);
    }
    private void BuildLane(int laneNumber)
    {
        Lane lane = Instantiate(lanePrefab, transform.position + Vector3.down * laneNumber, Quaternion.identity);
        lane.SpawnHorse();
        lanes.Add(lane);
    }

}