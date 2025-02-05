using System.Collections.Generic;
using UnityEngine;

public class RaceConstructor : MonoBehaviour
{
    [SerializeField] private int laneCount;
    [SerializeField] private float separation;
    [SerializeField] private Lane lanePrefab;
    [SerializeField] private HorseData horsePrefab;
    

    public void Construct(float scale)
    {
        for (int i = 0; i < laneCount; i++)
        {
            Vector3 pos = Vector3.down * i * separation;
            BuildLane(pos);
            BuildHorse(pos, i == 0);
        }
    }
    private void BuildLane(Vector3 position)
    {
        Lane lane = Instantiate(lanePrefab, transform);
        lane.transform.position = position;
    }

    private void BuildHorse(Vector3 position, bool player)
    {
        HorseData horse = Instantiate(horsePrefab, transform);
        horse.transform.position = position;
        if(player) horse.gameObject.AddComponent<Jockey>();
    }

}