using System;
using UnityEngine;

public class Margin : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float defaultWidth = 18;
    [SerializeField] private bool playerIsAhead = false;

    public void SetMarginDistance(float distance) => this.width = distance;


    private void Start()
    {
        float susModifiedWidth = this.defaultWidth - RoundController.instance.globalSuspicion;
        SetMarginDistance(susModifiedWidth);
    }

    private void Update()
    {
        transform.position = new Vector3(GetPositionOfNonPlayerFirstPlaceHorse() + width, transform.position.y, transform.position.z);
    }

    private float GetPositionOfNonPlayerFirstPlaceHorse()
    {
        float furthest = -1;
        foreach (var horse in HorseController.horses)
        {
            if (horse.IsPlayer) { IsPlayerAhead(horse); continue; }
            if (horse.transform.position.x > furthest) furthest = horse.transform.position.x;
        }
        return furthest;
    }

    private void IsPlayerAhead(HorseController player)
    {
        if (player.transform.position.x > transform.position.x) playerIsAhead = true;
        else playerIsAhead = false;
    }
}