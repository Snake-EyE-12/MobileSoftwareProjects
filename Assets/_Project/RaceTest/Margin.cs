using System;
using UnityEngine;

public class Margin : MonoBehaviour
{
    [SerializeField] private float width;
    public void SetMarginDistance(float distance) => this.width = distance;

    private void Update()
    {
        transform.position = new Vector3(GetPositionOfNonPlayerFirstPlaceHorse() + width, transform.position.y, transform.position.z);
    }
    private float GetPositionOfNonPlayerFirstPlaceHorse()
    {
        float furthest = -1;
        foreach (var horse in HorseController.horses)
        {
            if(horse.IsPlayer) continue;
            if (horse.transform.position.x > furthest) furthest = horse.transform.position.x;
        }
        return furthest;
    }

}